using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace PokeDex.ViewModels.Behaviours
{
    class TextChangedBehaviour : Behavior<SearchBar>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(propertyName: "Command", returnType: typeof(ICommand), declaringType: typeof(TextChangedBehaviour));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(propertyName: "CommandParameter", returnType: typeof(object), declaringType: typeof(TextChangedBehaviour));

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

        protected override void OnAttachedTo(SearchBar bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += BindableOnTextChanged;
            bindable.BindingContextChanged += BindableOnBindingContextChanged;
        }

        protected override void OnDetachingFrom(SearchBar bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= BindableOnTextChanged;
            bindable.TextChanged -= BindableOnBindingContextChanged;
        }

        private void BindableOnTextChanged(object sender, TextChangedEventArgs itemTappedEventArgs)
        {
            Command.Execute(CommandParameter);
        }

        private void BindableOnBindingContextChanged(object sender, EventArgs eventArgs)
        {
            SearchBar searchText = sender as SearchBar;
            BindingContext = searchText?.BindingContext;
        }
    }
}
