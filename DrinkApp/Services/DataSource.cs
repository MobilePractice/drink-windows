using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.Net.Http;

using Newtonsoft.Json;

using System.Linq;
using System.Xml.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using DrinkApp.Utils;
using DrinkApp.Model;
using System.Collections.ObjectModel;
using Windows.Devices.Geolocation;

namespace DrinkApp.Services {
    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// DataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    internal sealed class DataSource {

        #region " Private Methods "

        private string _baseURL = "http://mobilepractice.herokuapp.com/api/drink";
        private static DataSource _dataSource = new DataSource();

        private ObservableCollection<Store> _stores = new ObservableCollection<Store>();
        private ObservableCollection<Store> _nearestStores = new ObservableCollection<Store>();
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private ObservableCollection<Product> _beers = new ObservableCollection<Product>();
        private ObservableCollection<Product> _wines = new ObservableCollection<Product>();
        private ObservableCollection<Product> _spirits = new ObservableCollection<Product>();

        private async Task GetResponseFromService(ServiceType serviceType, string endpoint) {
            try {
                Uri _uri = new Uri(_baseURL + endpoint);
                HttpClient _httpClient = new HttpClient();
                HttpResponseMessage _responseMessage = await _httpClient.GetAsync(_uri);
                string content = await _responseMessage.Content.ReadAsStringAsync();
                Response _response = JsonConvert.DeserializeObject<Response>(content);

                if (_response.Status == Status.OK) {
                    foreach (dynamic result in _response.Result) {
                        switch (serviceType) {
                            case ServiceType.Store:
                                Store store = new Store(result);
                                this._stores.Add(store);

                                break;

                            case ServiceType.NearestStore:
                                Store neareastStore = new Store(result);
                                this._nearestStores.Add(neareastStore);

                                break;

                            case ServiceType.Product:
                                Product product = new Product(result);
                                this._products.Add(product);

                                break;

                            case ServiceType.Beers:
                                Product beer = new Product(result);
                                this._beers.Add(beer);

                                break;

                            case ServiceType.Wines:
                                Product wine = new Product(result);
                                this._wines.Add(wine);

                                break;

                            case ServiceType.Spirits:
                                Product spirit = new Product(result);
                                this._spirits.Add(spirit);

                                break;
                        }
                    }
                }
            } catch (JsonSerializationException ex) {
                throw new JsonSerializationException(ex.Message, ex.InnerException);
            } catch (JsonException ex) {
                throw new JsonException(ex.Message, ex.InnerException);
            } catch (TaskCanceledException ex) {
                throw new TaskCanceledException(ex.Message, ex.InnerException);
            } catch (Exception ex) {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        #endregion

        #region " Public Methods "

        internal static async Task<ObservableCollection<Store>> GetStoresAsync() {
            await _dataSource.GetResponseFromService(ServiceType.Store, "/stores");

            return _dataSource._stores;
        }

        internal static async Task<ObservableCollection<Store>> GetStoresByLatLngAsync(BasicGeoposition _position) {
            string endpoint = String.Format("/stores/?lat={0}&lon={1}", _position.Latitude, _position.Longitude);
            await _dataSource.GetResponseFromService(ServiceType.NearestStore, endpoint);

            return _dataSource._nearestStores;
        }

        internal static async Task<Store> GetStoreByIdAsync(int id) {
            string endpoint = String.Format("/stores/{0}", id);
            await _dataSource.GetResponseFromService(ServiceType.Store , endpoint);

            return _dataSource._stores.Single();
        }

        internal static async Task<ObservableCollection<Product>> GetProductsAsync() {
            await _dataSource.GetResponseFromService(ServiceType.Product, "/products");

            return _dataSource._products;
        }

        internal static async Task<ObservableCollection<Product>> GetBeersAsync() {
            await _dataSource.GetResponseFromService(ServiceType.Beers, "/products?q=beer");

            return _dataSource._beers;
        }

        internal static async Task<ObservableCollection<Product>> GetWinesAsync() {
            await _dataSource.GetResponseFromService(ServiceType.Wines, "/products?q=wine");

            return _dataSource._wines;
        }

        internal static async Task<ObservableCollection<Product>> GetSpiritsAsync() {
            await _dataSource.GetResponseFromService(ServiceType.Spirits, "/products?q=spirit");

            return _dataSource._spirits;
        }

        internal static async Task<Product> GetProductByIdAsync(int id) {
            string endpoint = String.Format("/products/{0}", id);
            await _dataSource.GetResponseFromService(ServiceType.Product, endpoint);

            return _dataSource._products.Single();
        }

        #endregion
    }
}
