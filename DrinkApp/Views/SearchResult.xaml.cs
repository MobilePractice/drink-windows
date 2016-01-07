using DrinkApp.Model;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace DrinkApp.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchResult : Page {
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private Product _selectedProduct;

        public SearchResult() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            string serviceType = e.Parameter as string;
            LoadProductList(serviceType);

            base.OnNavigatedTo(e);
        }

        private async void LoadProductList(string serviceType) {
            _products.Clear();
            RefreshProductList();
            ProgressRing.IsActive = true;

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
                    _products = await Product.GetProductsInStore();
                    break;
            }
 
            RefreshProductList();
        }

        private void RefreshProductList() {
            ProgressRing.IsActive = false;
            SearchResultListView.ItemsSource = _products;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e) {
            // The clicked item it is the new selected product
            _selectedProduct = e.ClickedItem as Product;

            this.Frame.Navigate(
                typeof(ProductDetail),
                _selectedProduct,
                new DrillInNavigationTransitionInfo());
        }
    }
}
