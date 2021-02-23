using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace PokePedia.ViewModels.Behaviours
{
    public class ItemTappedBehaviour :Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(propertyName: "Command", returnType: typeof(ICommand), declaringType: typeof(ItemTappedBehaviour));

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
            bindable.ItemTapped += BindableOnItemTapped;
            bindable.BindingContextChanged += BindableOnBindingContextChanged;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemTapped -= BindableOnItemTapped;
            bindable.ItemTapped -= BindableOnBindingContextChanged;
        }

        private void BindableOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            ListView listView = sender as ListView;
            Command.Execute(CommandParameter);
            listView.SelectedItem = null;
        }

        private void BindableOnBindingContextChanged(object sender, EventArgs eventArgs)
        {
            ListView listView = sender as ListView;
            BindingContext = listView?.BindingContext;
        }
    }
}