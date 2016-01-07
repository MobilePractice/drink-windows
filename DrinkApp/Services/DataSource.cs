using DrinkApp.Model;
using DrinkApp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace DrinkApp.Services {
    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// DataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    internal sealed class DataSource {

        #region " Private Methods "

        private static DataSource _dataSource = new DataSource();
        private ObservableCollection<Store> _stores = new ObservableCollection<Store>();
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private ObservableCollection<Inventory> _inventories = new ObservableCollection<Inventory>();

        private async Task GetResponseFromService(ServiceType serviceType, string endpoint) {
            try {
                Uri _uri = new Uri(endpoint);
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

                            case ServiceType.Product:
                                Product product = new Product(result);
                                this._products.Add(product);

                                break;

                            case ServiceType.Inventory:
                                Inventory inventory = new Inventory(result);
                                this._inventories.Add(inventory);

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

        #region " Internal Methods "

        internal static async Task<ObservableCollection<Store>> GetStoresAsync(string endpoint) {
            await _dataSource.GetResponseFromService(ServiceType.Store, endpoint);

            return _dataSource._stores;
        }

        internal static async Task<ObservableCollection<Product>> GetProductsAsync(string endpoint) {
            await _dataSource.GetResponseFromService(ServiceType.Product, endpoint);

            return _dataSource._products;
        }

        internal static async Task<ObservableCollection<Inventory>> GetInventoriesAsync(string endpoint) {
            await _dataSource.GetResponseFromService(ServiceType.Inventory, endpoint);

            return _dataSource._inventories;
        }

        #endregion
    }
}
