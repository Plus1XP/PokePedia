using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace PokePedia.ViewModels.Behaviours
{
    public class ItemSelectedBehaviour : Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(propertyName: "Command", returnType: typeof(ICommand), declaringType: typeof(ItemSelectedBehaviour));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(propertyName: "CommandParameter", returnType: typeof(object), declaringType: typeof(ItemSelectedBehaviour));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ItemSelected += BindableOnItemSelected;
            bindable.BindingContextChanged += BindableOnBindingContextChanged;

        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemSelected -= BindableOnItemSelected;
            bindable.ItemSelected -= BindableOnBindingContextChanged;
        }

        private void BindableOnItemSelected(object sender, SelectedItemChangedEventArgs itemTappedEventArgs)
        {
            Command.Execute(CommandParameter);
        }

        private void BindableOnBindingContextChanged(object sender, EventArgs eventArgs)
        {
            ListView listView = sender as ListView;
            BindingContext = listView?.BindingContext;
        }
    }
}
