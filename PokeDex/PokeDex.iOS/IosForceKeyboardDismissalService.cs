
using PokeDex.iOS;
using PokeDex.Models;

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
                UIWindow window = UIApplication.SharedApplication.KeyWindow;
                UIViewController viewController = window.RootViewController;
                while (viewController.PresentedViewController != null)
                {
                    viewController = viewController.PresentedViewController;
                }
                viewController.View.EndEditing(true);
            });
        }
    }
}