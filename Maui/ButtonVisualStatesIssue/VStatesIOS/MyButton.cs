namespace VStatesIOS
{
    public class MyButton : Button
    {
        private static readonly string CustomStateName = "Custom";

        public static readonly BindableProperty IsCustomProperty =
            BindableProperty.Create(nameof(IsCustom), typeof(bool), typeof(MyButton), false, BindingMode.TwoWay, propertyChanged: OnIsCustomChanged);

        public bool IsCustom
        {
            get => (bool)this.GetValue(IsCustomProperty);
            set => this.SetValue(IsCustomProperty, value);
        }

        public MyButton()
        {
            this.Clicked += MyButton_Clicked;
        }

        private void MyButton_Clicked(object? sender, EventArgs e)
        {
            this.IsCustom = !this.IsCustom;
        }

        protected override void ChangeVisualState()
        {
            if(this.IsCustom == true && this.IsEnabled)
            {
                VisualStateManager.GoToState(this, CustomStateName);
            }
            else
            {
                base.ChangeVisualState();
            }
        }

        private static void OnIsCustomChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (MyButton)bindable;
            button.OnIsCustomChanged();
        }

        private void OnIsCustomChanged()
        {
            this.ChangeVisualState();
        }
    }
}
