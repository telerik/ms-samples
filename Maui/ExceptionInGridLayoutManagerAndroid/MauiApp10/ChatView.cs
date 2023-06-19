namespace MauiApp10;

public class ChatView : ContentView
{
	public static readonly BindableProperty TypingIndicatorProperty =
		BindableProperty.Create(nameof(TypingIndicator), typeof(View), typeof(ChatView), propertyChanged: OnComprisingElementChanged);

	public ChatView()
	{
		ResourceDictionary chatViewResources = new ChatViewResources();
		this.ControlTemplate = (ControlTemplate)chatViewResources["ChatView_ControlTemplate"];
	}

	public View TypingIndicator
	{
		get
		{
			return (View)this.GetValue(TypingIndicatorProperty);
		}
		set
		{
			this.SetValue(TypingIndicatorProperty, value);
		}
	}

	private static void OnComprisingElementChanged(BindableObject bindable, object oldValue, object newValue)
	{
		ChatView chat = (ChatView)bindable;
		chat.OnComprisingElementChanged((View)oldValue, (View)newValue);
	}

	private void OnComprisingElementChanged(View oldElement, View newElement)
	{
		if (oldElement != null)
		{
			oldElement.SizeChanged -= this.HandleSizeChanged;
		}

		if (newElement != null)
		{
			newElement.SizeChanged += this.HandleSizeChanged;
		}
	}

	private void HandleSizeChanged(object sender, EventArgs e)
	{
		this.InvalidateLayout();
	}
}
