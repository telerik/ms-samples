namespace WinUIMultipleWindowsLeak
{
    public partial class MainPage : ContentPage
    {
        List<WeakReference> weakReferences = new List<WeakReference>();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCreateOneClicked(object sender, EventArgs e)
        {
#if WINDOWS
            var winuiWindow = new MauiWinUIWindow();
            weakReferences.Add(new WeakReference(winuiWindow));

            winuiWindow.Activate();
            await Task.Delay(100);
            winuiWindow.Close();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.WaitForFullGCComplete();

            int alive = weakReferences.Count(r => r.IsAlive);

            long totalMemory = System.GC.GetTotalMemory(false);
            ulong appMemory = global::Windows.System.MemoryManager.AppMemoryUsage;

            this.MauiUIWindowsCount.Text = $"Alive {alive}/{weakReferences.Count}";
            this.Memory.Text = $"Memory: gc {totalMemory}, app {appMemory}";
#endif
        }
    }

}
