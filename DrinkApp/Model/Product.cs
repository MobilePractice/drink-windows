using DrinkApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkApp.Model {
    /// <summary>
    /// Product data model (items)
    /// </summary>
    public class Product {
        public Product(dynamic product) {
            this.Id = (int)product["id"];
            this.IsDead = (bool)product["is_dead"];
            this.Name = (string)product["name"];
            this.Tags = (string)product["tags"];
            this.IsDiscontinued = (bool)product["is_discontinued"];
            this.PrimaryCategory = (string)product["primary_category"];
            this.SecondaryCategory = (string)product["secondary_category"];
            this.Thumbnail = product["image_thumb_url"] != null ? (string)product["image_thumb_url"] : "";
        }

        public int Id { get; private set;  }
        public bool IsDead { get; private set;  }
        public string Name { get; private set;  }
        public string Tags { get; private set;  }
        public bool IsDiscontinued { get; private set;  }
        public double PriceInCents { get; private set;  }
        public double RegularPriceInCents { get; private set;  }
        public double LimitedTimeOfferSavingsInCents { get; private set;  }
        public DateTime LimitedTimeOfferEndsOn { get; private set;  }
        public int BonusRewardMiles { get; private set;  }
        public DateTime BonusRewardMilesEndsOn { get; private set;  }
        public string StockType { get; private set;  }
        public string PrimaryCategory { get; private set;  }
        public string SecondaryCategory { get; private set;  }
        public string Origin { get; private set;  }
        public string Package { get; private set;  }
        public string PackageUnitType { get; private set;  }
        public int PackageUnitVolumeInMilliliters { get; private set;  }

        public string Thumbnail { get; private set;  }
        public Inventory Inventory { get; private set;  }

        public async static Task<ObservableCollection<Product>> GetProducts() {
            return await DataSource.GetProductsAsync(Utils.Endpoint.Products);
        }

        public async static Task<Product> GetProduct(int productId = 288506) {
            string endpoint = String.Format(Utils.Endpoint.ProductsById, productId);

            return (await DataSource.GetProductsAsync(endpoint)).Single();
        }

        public async static Task<ObservableCollection<Product>> GetProductsInStore(int storeId = 534) {
            string endpoint = String.Format(Utils.Endpoint.ProductsInStore, storeId);

            return await DataSource.GetProductsAsync(endpoint);
        }

        public async static Task<ObservableCollection<Product>> GetBeers() {
            return await DataSource.GetProductsAsync(Utils.Endpoint.Beers);
        }

        public async static Task<ObservableCollection<Product>> GetWines() {
            return await DataSource.GetProductsAsync(Utils.Endpoint.Wines);
        }

        public async static Task<ObservableCollection<Product>> GetSpirits() {
            return await DataSource.GetProductsAsync(Utils.Endpoint.Spirits);
        }

        public static ObservableCollection<GroupInfoList> GetProductListGrouped(ObservableCollection<Product> products) {
            ObservableCollection<GroupInfoList> groups = new ObservableCollection<GroupInfoList>();

            var query = from item in products
                        group item by item.Name[0] into g
                        orderby g.Key
                        select new { GroupName = g.Key, Items = g };

            foreach (var g in query) {
                GroupInfoList info = new GroupInfoList();
                info.Key = g.GroupName;
                foreach (var item in g.Items) {
                    info.Add(item);
                }
                groups.Add(info);
            }

            return groups;
        }
    }
}
