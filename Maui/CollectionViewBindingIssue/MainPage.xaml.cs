using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CollectionViewBindingIssue;

public partial class MainPage : ContentPage
{
    public static readonly BindableProperty ClearItemCommandProperty =
        BindableProperty.Create(nameof(ClearItemCommand), typeof(ICommand), typeof(MainPage));

    private ObservableCollection<ItemModel> items = new();

    public MainPage()
    {
        InitializeComponent();

        this.ClearItemCommand = new Command<ItemModel>(ClearItem);
        this.Items = new ObservableCollection<ItemModel>
        {
            new ItemModel { Text = "Item 1" }
        };

        this.BindingContext = this;
    }

    public ObservableCollection<ItemModel> Items
    {
        get => this.items;
        set
        {
            if (this.items != value)
            {
                this.items = value;
                this.OnPropertyChanged(nameof(this.Items));
            }
        }
    }

    public ICommand ClearItemCommand
    {
        get => (ICommand)GetValue(ClearItemCommandProperty);
        set => SetValue(ClearItemCommandProperty, value);
    }

    private void ClearItem(ItemModel item)
    {
        if (item != null && this.Items.Contains(item))
        {
            this.Items.Remove(item);
        }
    }

    private void OnAddItemClicked(object sender, EventArgs e)
    {
        var itemCount = this.Items.Count + 1;
        var newItem = new ItemModel { Text = $"Item {itemCount}" };
        this.Items.Add(newItem);
    }
}
