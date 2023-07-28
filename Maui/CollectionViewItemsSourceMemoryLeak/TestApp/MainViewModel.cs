using System.Collections;
using System.Text;
using System.Windows.Input;

namespace TestApp;

public class MainViewModel : ViewModelBase
{
    private int startIndex;
    private int itemsCount;
    private IList itemsSource;
    private string memoryInfo;

    public MainViewModel(int startIndex, int itemsCount)
    {
        this.startIndex = startIndex;
        this.itemsCount = itemsCount;

        this.RefreshItemsSourceCommand = new Command(this.RefreshItemsSource);
        this.RefreshMemoryInfoCommand = new Command(this.RefreshMemoryInfo);

        this.RefreshItemsSource();
        this.RefreshMemoryInfo();
    }

    public IList ItemsSource
    {
        get => itemsSource;
        private set => this.SetValue(ref this.itemsSource, value);
    }

    public string MemoryInfo
    {
        get => memoryInfo;
        private set => this.SetValue(ref this.memoryInfo, value);
    }

    public ICommand RefreshItemsSourceCommand { get; }

    public ICommand RefreshMemoryInfoCommand { get; }

    private void RefreshItemsSource()
    {
        this.ItemsSource = this.CreateItemsSource();
        this.startIndex += this.itemsCount;
    }

    private IList CreateItemsSource()
    {
        return Enumerable.Range(this.startIndex, this.itemsCount)
                         .Select(itemIndex => $"Item {itemIndex}")
                         .ToList();
    }

    private void RefreshMemoryInfo()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        this.MemoryInfo = this.CreateMemoryInfo();
    }

    private string CreateMemoryInfo()
    {
        var memoryInfo = new StringBuilder();

        memoryInfo.AppendLine($"Total objects: {MemoryTracker.TotalObjectCount}");
        memoryInfo.AppendLine($"Alive objects: {MemoryTracker.AliveObjectCount}");

        return memoryInfo.ToString();
    }
}
