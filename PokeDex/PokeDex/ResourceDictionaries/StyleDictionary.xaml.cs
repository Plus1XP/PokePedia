using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeDex.ResourceDictionaries
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StyleDictionary : ResourceDictionary
    {
        public StyleDictionary()
        {
            InitializeComponent();
        }
    }
}