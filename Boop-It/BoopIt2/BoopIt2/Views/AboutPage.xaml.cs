using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoopIt2.Views
{
  public partial class AboutPage : ContentPage
  {
    public AboutPage()
    {
      InitializeComponent();
    }
    async void OnButtonClicked(object sender, EventArgs e)
    {
      // Launch the specified URL in the system browser.
      await Launcher.OpenAsync("https://www.youtube.com/watch?v=qTjoSPga2Ic");
    }
  }
}