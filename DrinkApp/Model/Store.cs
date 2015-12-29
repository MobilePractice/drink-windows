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
using DrinkApp.Utils;
using DrinkApp.Services;

namespace DrinkApp.Model {
    // Store Hours
    public struct StoreHour {
        public DayOfWeek Day;
        public string Open;
        public string Close;
    }

    ////http://stackoverflow.com/questions/5761973/when-are-bitwise-operations-appropriate (Store Features)
    //[Flags]
    //public enum StoreFeatures {
    //    None = 0,
    //    HasWheelchairAccessibility = 1 << 0,
    //    HasBilingualServices = 1 << 1,
    //    HasProductConsultant = 1 << 2,
    //    HasTastingBar = 1 << 3,
    //    HasBeerColdRoom = 1 << 4,
    //    HasSpecialOccasionPermits = 1 << 5,
    //    HasVintagesCorner = 1 << 6,
    //    HasParking = 1 << 7,
    //    HasTransitAccess = 1 << 8
    //}


    /// <summary>
    /// Store data model (group)
    /// </summary>
    public class Store {
        public Store(dynamic store) {
            this.Id = (int)store["id"];
            this.IsDead = (bool)store["is_dead"];
            this.Name = (string)store["name"];
            this.Tags = (string)store["tags"];
            this.AddressLine1 = (string)store["address_line_1"];
            this.AddressLine2 = (string)store["address_line2"];
            this.City = (string)store["city"];
            this.PostalCode = (string)store["postal_code"];
            this.Telephone = (string)store["telephone"];
            this.Fax = (string)store["fax"];
            //TODO: Position
            this.ProductsCount = (int)store["products_count"];
            this.InventoryCount = (int)store["inventory_count"];
            this.InventoryPrice = Formatter.PriceInDollars((int)store["inventory_price_in_cents"]);
            this.InventoryVolume = Formatter.VolumeInLiters((int)store["inventory_volume_in_milliliters"]);

            // store hours
            this.StoreHours = new List<StoreHour>();
            StoreHour _storeHour;

            _storeHour = new StoreHour();
            _storeHour.Day = DayOfWeek.Sunday;
            _storeHour.Open = Formatter.MinutesSinceMidnightTo12(store["sunday_open"]);
            _storeHour.Close = Formatter.MinutesSinceMidnightTo12(store["sunday_close"]);
            this.StoreHours.Add(_storeHour);

            _storeHour = new StoreHour();
            _storeHour.Day = DayOfWeek.Monday;
            _storeHour.Open = Formatter.MinutesSinceMidnightTo12(store["monday_open"]);
            _storeHour.Close = Formatter.MinutesSinceMidnightTo12(store["monday_close"]);
            this.StoreHours.Add(_storeHour);

            _storeHour = new StoreHour();
            _storeHour.Day = DayOfWeek.Tuesday;
            _storeHour.Open = Formatter.MinutesSinceMidnightTo12(store["tuesday_open"]);
            _storeHour.Close = Formatter.MinutesSinceMidnightTo12(store["tuesday_close"]);
            this.StoreHours.Add(_storeHour);

            _storeHour = new StoreHour();
            _storeHour.Day = DayOfWeek.Wednesday;
            _storeHour.Open = Formatter.MinutesSinceMidnightTo12(store["wednesday_open"]);
            _storeHour.Close = Formatter.MinutesSinceMidnightTo12(store["wednesday_close"]);
            this.StoreHours.Add(_storeHour);

            _storeHour = new StoreHour();
            _storeHour.Day = DayOfWeek.Thursday;
            _storeHour.Open = Formatter.MinutesSinceMidnightTo12(store["thursday_open"]);
            _storeHour.Close = Formatter.MinutesSinceMidnightTo12(store["thursday_close"]);
            this.StoreHours.Add(_storeHour);

            _storeHour = new StoreHour();
            _storeHour.Day = DayOfWeek.Friday;
            _storeHour.Open = Formatter.MinutesSinceMidnightTo12(store["friday_open"]);
            _storeHour.Close = Formatter.MinutesSinceMidnightTo12(store["friday_close"]);
            this.StoreHours.Add(_storeHour);

            _storeHour = new StoreHour();
            _storeHour.Day = DayOfWeek.Saturday;
            _storeHour.Open = Formatter.MinutesSinceMidnightTo12(store["saturday_open"]);
            _storeHour.Close = Formatter.MinutesSinceMidnightTo12(store["saturday_close"]);
            this.StoreHours.Add(_storeHour);

            this.UpdatedAt = DateTime.Parse((string)store["updated_at"]);
            this.StoreNo = (int)store["id"];

            if (store["distance_in_meters"] != null) {
                this.Distance = Formatter.DistanceInKilometers((int)store["distance_in_meters"]);
            }
        }

        public int Id { get; private set; }
        public bool IsDead { get; private set; }
        public string Name { get; private set; }
        public string Tags { get; private set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string Telephone { get; private set; }
        public string Fax { get; private set; }
        public Geoposition Position { get; private set; }
        public int ProductsCount { get; private set; }
        public int InventoryCount { get; private set; }
        public string InventoryPrice { get; private set; }
        public string InventoryVolume { get; private set; }
        public bool HasWheelchairAccessibility { get; private set; }
        public bool HasBilingualServices { get; private set; }
        public bool HasProductConsultant { get; private set; }
        public bool HasTastingBar { get; private set; }
        public bool HasBeerColdRoom { get; private set; }
        public bool HasSpecialOccasionPermits { get; private set; }
        public bool HasVintagesCorner { get; private set; }
        public bool HasParking { get; private set; }
        public bool HasTransitAccess { get; private set; }
        public List<StoreHour> StoreHours { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public int StoreNo { get; private set; }
        public string Distance { get; private set; }
        public ObservableCollection<Product> Products { get; private set; }

        public async static Task<Store> GetStoreById(int id) {
            return await DataSource.GetStoreByIdAsync(id);
        }

        public async static Task<ObservableCollection<Store>> GetStores() {
            return await DataSource.GetStoresAsync();
        }

        public async static Task<ObservableCollection<Store>> GetStoresByLatLngStores(BasicGeoposition _position) {
            return await DataSource.GetStoresByLatLngAsync(_position);
        }
    }
}
