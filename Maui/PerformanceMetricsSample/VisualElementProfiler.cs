using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace PerformanceMetricsSample;

internal class VisualElementProfiler
{
    private readonly ConditionalWeakTable<CustomLayout, LayoutPassTimer> _elementLayoutPassTimers = new();
    private readonly Dictionary<Type, LayoutPassStats> _typeStats = new();

    public IEnumerable<Type> TrackedTypes => _typeStats.Keys;
    public IEnumerable<KeyValuePair<Type, LayoutPassStats>> TypeStats => _typeStats.Select(kv => new KeyValuePair<Type, LayoutPassStats>(kv.Key, kv.Value));

    public bool TryGetStats(CustomLayout element, [NotNullWhen(true)] out LayoutPassStats? stats)
    {
        var found = _elementLayoutPassTimers.TryGetValue(element, out var timer);
        stats = found ? timer?.ToStats() : null;
        return stats != null;
    }

    public void Attach(CustomLayout visualElement)
    {
        Observe(visualElement);
    }

    public void Detach(CustomLayout visualElement)
    {
        Forget(visualElement);
    }

    public override string ToString()
    {
        var statsTable = new List<string[]>();
        statsTable.Add(["Type", "M (count)", "M (ns)", "A (count)", "A (ns)"]);
        foreach (var ts in _typeStats)
        {
            var type = ts.Key;
            var stats = ts.Value;

            var m = stats[LayoutPassType.Measure];
            var a = stats[LayoutPassType.Arrange];
            statsTable.Add([
                type.Name,
                $"{m.StandaloneCount} / {m.Count}",
                m.GetAverageTimeNs().ToString("N0", CultureInfo.InvariantCulture),
                $"{a.StandaloneCount} / {a.Count}",
                a.GetAverageTimeNs().ToString("N0", CultureInfo.InvariantCulture),
            ]);
        }

        var colLengths = new int[5];
        foreach (var row in statsTable)
        {
            for (int i = 0; i < colLengths.Length; i++)
            {
                colLengths[i] = Math.Max(colLengths[i], row[i].Length);
            }
        }

        var sb = new StringBuilder();
        var rowIndex = 0;
        foreach (var row in statsTable)
        {
            sb.Append("| ");
            for (int i = 0; i < colLengths.Length; i++)
            {
                sb.Append(i == 0 ? row[i].PadRight(colLengths[i]) : row[i].PadLeft(colLengths[i]));
                sb.Append(" | ");
            }
            sb.Append('\n');

            if (rowIndex++ == 0)
            {
                sb.Append("| ");
                foreach (var l in colLengths)
                {
                    sb.Append(string.Empty.PadRight(l, '-'));
                    sb.Append(" | ");
                }
                sb.Append('\n');
            }
        }

        return sb.ToString();
    }

    private void Observe(CustomLayout descendant)
    {
        descendant.LayoutPassEvent += OnLayoutPass;
        var elementType = descendant.GetType();
        if (!_typeStats.ContainsKey(elementType))
        {
            _typeStats[elementType] = new LayoutPassStats();
        }
    }

    private void Forget(CustomLayout descendant)
    {
        descendant.LayoutPassEvent -= OnLayoutPass;
        _elementLayoutPassTimers.Remove(descendant);
    }

    private void OnLayoutPass(object? sender, LayoutPassEventTypes e)
    {
        if (sender is not CustomLayout visualElement)
        {
            return;
        }

        var timer = _elementLayoutPassTimers.GetValue(visualElement, _ => new LayoutPassTimer());
        var tracked = timer.Track(e);

        if (tracked is { } value)
        {
            var passStats = _typeStats[visualElement.GetType()][value.Type];
            ++passStats.Count;
            if (value.Standalone)
            {
                ++passStats.StandaloneCount;
            }

            passStats.TotalTime += value.Duration;
        }
    }


}

internal class LayoutPassStats
{
    private readonly LayoutPassTypeStats[] _layoutPassTypeStats =
    [
        new(LayoutPassType.Measure),
            new(LayoutPassType.Arrange)
    ];

    public LayoutPassTypeStats this[LayoutPassType type]
    {
        get
        {
            return _layoutPassTypeStats[(int)type];
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var typeStats in _layoutPassTypeStats)
        {
            sb.AppendLine($"{typeStats.LayoutPassType}: {typeStats.StandaloneCount} / {typeStats.Count} ({typeStats.GetAverageTimeNs()} ns)");
        }
        return sb.ToString();
    }
}

internal class LayoutPassTimer
{
    // [ Measure, CrossPlatformMeasure, Arrange, CrossPlatformArrange ]
    private readonly long[] _startTimes = new long[4];
    private readonly long[] _totalTimes = new long[4];
    private readonly long[] _counts = new long[4];
    private readonly long[] _standaloneCounts = new long[4];

    public LayoutPassStats ToStats()
    {
        var stats = new LayoutPassStats();
        for (int i = 0; i < 4; i++)
        {
            var typeStats = stats[(LayoutPassType)i];
            typeStats.Count = _counts[i];
            typeStats.TotalTime = _totalTimes[i];
        }

        return stats;
    }

    public (LayoutPassType Type, bool Standalone, long Duration)? Track(LayoutPassEventTypes evt)
    {
        var index = (int)evt >> 1;
        bool isStartEvent = ((int)evt & 1) == 0;
        if (isStartEvent)
        {
            _startTimes[index] = Stopwatch.GetTimestamp();
            return null;
        }

        var start = _startTimes[index];
        if (start == 0)
        {
            // If we don't have a start time, we ignore this event
            return null;
        }

        var stop = Stopwatch.GetTimestamp();

        // If the cross-platform pass is part of a non-platform pass, we need to adjust the start time of the non-platform pass
        // so that it doesn't count the time of the cross-platform pass.
        long nonPlatformStartTime;
        bool isStandalone = true;
        if (index % 2 == 1 && (nonPlatformStartTime = _startTimes[index - 1]) != 0)
        {
            var nonPlatformInitialDuration = start - nonPlatformStartTime;
            _startTimes[index - 1] = stop - nonPlatformInitialDuration;
            isStandalone = false;
        }

        var totalTime = stop - start;
        var count = 1L;
        var standaloneCount = isStandalone ? 1L : 0L;

        _startTimes[index] = 0;
        _totalTimes[index] += totalTime;
        _counts[index] += count;
        _standaloneCounts[index] += standaloneCount;

        return ((LayoutPassType)index, isStandalone, totalTime);
    }
}

internal enum LayoutPassType
{
    Measure,
    Arrange,
}

internal class LayoutPassTypeStats
{
    public LayoutPassTypeStats(LayoutPassType layoutPassType, long count = 0, long standaloneCount = 0, long totalTime = 0)
    {
        LayoutPassType = layoutPassType;
        Count = count;
        StandaloneCount = standaloneCount;
        TotalTime = totalTime;
    }

    public LayoutPassType LayoutPassType { get; }
    public long StandaloneCount { get; set; }
    public long Count { get; set; }
    public long TotalTime { get; set; }
}

internal static class LayoutPassTypeStatsExtensions
{
    public static double GetAverageTimeNs(this LayoutPassTypeStats stats)
    {
        if (stats.Count == 0)
        {
            return 0;
        }

        return (double)stats.TotalTime / stats.Count / Stopwatch.Frequency * 1000000.0;
    }
}