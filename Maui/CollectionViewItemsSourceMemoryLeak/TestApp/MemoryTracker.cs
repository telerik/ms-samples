namespace TestApp;

public static class MemoryTracker
{
    private static readonly List<WeakReference> weakReferences;

    public static readonly BindableProperty IsTrackedProperty =
        BindableProperty.CreateAttached("IsTracked", typeof(bool), typeof(MemoryTracker),
            false, propertyChanged: OnIsTrackedChanged);

    static MemoryTracker()
    {
        weakReferences = new List<WeakReference>();
    }

    public static int TotalObjectCount => weakReferences.Count;

    public static int AliveObjectCount => weakReferences.Count(weakReference => weakReference.IsAlive);

    public static bool GetIsTracked(BindableObject bindable)
    {
        return (bool)bindable.GetValue(IsTrackedProperty);
    }

    public static void SetIsTracked(BindableObject bindable, bool isTracked)
    {
        bindable.SetValue(IsTrackedProperty, isTracked);
    }

    private static void OnIsTrackedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if ((bool)newValue)
        {
            var weakReference = new WeakReference(bindable);

            weakReferences.Add(weakReference);
        }
    }
}
