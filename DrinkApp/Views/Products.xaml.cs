using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DrinkApp.Utils;
using DrinkApp.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DrinkApp.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Products : Page {
        public IEnumerable<Product> ProductList { get; set; }

        public Products() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            LoadProductList(e.Parameter.ToString());

            base.OnNavigatedTo(e);
        }

        private async void LoadProductList(string serviceType) {
            switch (serviceType) {
                case "Beers":
                    ProductList = await Product.GetBeers();
                    break;

                case "Wines":
                    ProductList = await Product.GetWines();
                    break;

                case "Spirits":
                    ProductList = await Product.GetSpirits();
                    break;

                default:
                    ProductList = await Product.GetBeers();
                    break;
            }
        }
    }
}
