using Foundation;

using PokeDex.Models;
using PokeDex.iOS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IosForceKeyboardDismissalService))]
namespace PokeDex.iOS
{
    class IosForceKeyboardDismissalService : IForceKeyboardDismissalService
    {
        public void DismissKeyboard()
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var window = UIApplication.SharedApplication.KeyWindow;
                var viewController = window.RootViewController;
                while (viewController.PresentedViewController != null)
                {
                    viewController = viewController.PresentedViewController;
                }
                viewController.View.EndEditing(true);
            });
        }
    }
}