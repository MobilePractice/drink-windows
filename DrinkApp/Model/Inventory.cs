using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace DrinkApp.Model
{
    /// <summary>
    /// Inventory data model
    /// </summary>
    public class Inventory {
        public Inventory(dynamic inventory) {
            //this.Store = store;
            //this.Product = product;
            this.IsDead = (bool)inventory["isDead"];
            this.Quantity = (int)inventory["quantity"];
            //this.UpdatedOn = updatedOn;
            //this.UpdatedAt = updatedAt;
        }

        public Store Store { get; private set; }
        public Product Product { get; private set; }
        public bool IsDead { get; private set; }
        public int Quantity { get; private set; }
        public DateTime UpdatedOn { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
