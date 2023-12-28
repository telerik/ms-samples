namespace UnloadedRaisedInScenarioWithCustomLayout;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.ObjectModel;

public class CustomLayout : Grid
{
	private Grid innerGrid;

	public CustomLayout()
	{
		this.Log = new();
		this.Loaded += this.CustomLayout_Loaded;
		this.Unloaded += this.CustomLayout_Unloaded;
        this.HandlerChanging += this.CustomLayout_HandlerChanging;
		//// works fine if i call this here
		//this.InitInnerGrid();
	}

    public ObservableCollection<string> Log { get; }

	protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
	{
		if (this.innerGrid == null)
		{
			this.InitInnerGrid();
		}

		return base.MeasureOverride(widthConstraint, heightConstraint);
	}

	private void InitInnerGrid()
	{
		this.innerGrid = new() { WidthRequest = 100, HeightRequest = 33, BackgroundColor = Colors.HotPink };
		this.innerGrid.Loaded += this.innerGrid_Loaded;
		this.innerGrid.Unloaded += this.innerGrid_Unloaded;
        this.innerGrid.HandlerChanging += this.InnerGrid_HandlerChanging;
		this.Children.Add(this.innerGrid);
	}

    private void CustomLayout_Loaded(object sender, EventArgs e)
	{
		this.Log.Add("CustomLayout_Loaded");
	}

	private void CustomLayout_Unloaded(object sender, EventArgs e)
	{
		this.Log.Add("CustomLayout_Unloaded");
	}

    private void CustomLayout_HandlerChanging(object sender, HandlerChangingEventArgs e)
    {
        this.Log.Add("CustomLayout_HandlerChanging");
    }

    private void innerGrid_Loaded(object sender, EventArgs e)
	{
		this.Log.Add("innerGrid_Loaded");
	}

	private void innerGrid_Unloaded(object sender, EventArgs e)
	{
		this.Log.Add("innerGrid_Unloaded");
	}
    
	private void InnerGrid_HandlerChanging(object sender, HandlerChangingEventArgs e)
    {
        this.Log.Add("InnerGrid_HandlerChanging");
    }

}