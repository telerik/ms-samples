namespace BrushesMemoryLeak;

public partial class GrumpyPage : ContentPage
{
	internal static readonly List<WeakReference> _weakReferences = new List<WeakReference>();

	public GrumpyPage()
	{
		this.InitializeComponent();

		_weakReferences.Add(new WeakReference(this));

		this.UpdateLabelStats();
	}

	private void UpdateLabelStats()
	{
		GC.Collect();
		GC.Collect();
		GC.Collect();

		int count = _weakReferences.Count(wr => wr.Target != null);
		this.LabelStats.Text = count + " alive instances from a total of " + _weakReferences.Count;
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		this.Navigation.PopAsync();
	}
}

