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
using DrinkApp.Views;
using DrinkApp.Utils;
using DrinkApp.Model;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Animation;

namespace DrinkApp.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchResult : Page {
        private ObservableCollection<Product> _products;
        private Product selectedProduct;

        public SearchResult() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            string serviceType = e.Parameter as string;
            LoadProductList(serviceType);

            base.OnNavigatedTo(e);
        }

        private async void LoadProductList(string serviceType) {
            SearchResultListView.Items.Clear();

            switch (serviceType) {
                case "Beers":
                    _products = await Product.GetBeers();
                    break;

                case "Wines":
                    _products = await Product.GetWines();
                    break;

                case "Spirits":
                    _products = await Product.GetSpirits();
                    break;

                default:
                    _products = await Product.GetProductByStoreId();
                    break;
            }

            SearchResultCVS.Source = Product.GetProductListGrouped(_products);

            SearchResultListView.ItemsSource = SearchResultCVS.View;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e) {
            // The clicked item it is the new selected product
            selectedProduct = e.ClickedItem as Product;

            this.Frame.Navigate(
                typeof(ProductDetail),
                selectedProduct,
                new DrillInNavigationTransitionInfo());
        }
    }
}
