using System.Collections.Generic;

namespace DrinkApp.Utils {
    public static class Endpoint {
        private const string _baseURL = "http://mobilepractice.herokuapp.com/api/drink";

        public static string Products = _baseURL + "/products";
        public static string ProductsById = _baseURL + "/products/{0}";
        public static string ProductsInStore = _baseURL + "/products?store_id={0}";
        public static string Beers = _baseURL + "/products?q=beers";
        public static string Wines = _baseURL + "/products?q=wines";
        public static string Spirits = _baseURL + "/products?q=spirits";

        public static string Stores = _baseURL + "/stores";
        public static string StoresById = _baseURL + "/stores/{0}";
        public static string StoresWithProduct = _baseURL + "/stores?product_id={0}";
        public static string StoresNearPoint = _baseURL + "/stores?lat={0}&lon=-{1}";
        public static string StoresNearPointWithProduct = _baseURL + "/stores?lat={0}&lon={1}&product_id={2}";

        public static string Inventory = _baseURL + "/stores/{0}/products{1}/inventory";
        public static string InventoriesByProduct = _baseURL + "/inventories?product_id={0}";
        public static string InventoriesByStore = _baseURL + "/inventories?store_id={0}";
    }

    public static class QueryString {

        private static List<string> _where;
        public static List<string> Where(ServiceType serviceType) {
            _where.Add("is_dead");

            switch (serviceType) {
                case ServiceType.Store:
                    _where.Add("has_wheelchair_accessability");
                    _where.Add("has_bilingual_services");
                    _where.Add("has_product_consultant");
                    _where.Add("has_tasting_bar");
                    _where.Add("has_beer_cold_room");
                    _where.Add("has_special_occasion_permits");
                    _where.Add("has_vintages_corner");
                    _where.Add("has_parking");
                    _where.Add("has_transit_access");

                    break;
                case ServiceType.Product:
                    _where.Add("is_discontinued");
                    _where.Add("has_value_added_promotion");
                    _where.Add("has_limited_time_offer");
                    _where.Add("has_bonus_reward_miles");
                    _where.Add("is_seasonal");
                    _where.Add("is_vqa");
                    _where.Add("is_ocb");
                    _where.Add("is_kosher");

                    break;
            }            

            return _where;
        }

        private static List<string> _whereNot;
        public static List<string> WhereNot(ServiceType serviceType) {
            _whereNot.Add("is_dead");

            switch(serviceType) {
                case ServiceType.Store:
                    _whereNot.Add("has_wheelchair_accessability");
                    _whereNot.Add("has_bilingual_services");
                    _whereNot.Add("has_product_consultant");
                    _whereNot.Add("has_tasting_bar");
                    _whereNot.Add("has_beer_cold_room");
                    _whereNot.Add("has_special_occasion_permits");
                    _whereNot.Add("has_vintages_corner");
                    _whereNot.Add("has_parking");
                    _whereNot.Add("has_transit_access");

                    break;
                case ServiceType.Product:
                    _where.Add("is_discontinued");
                    _where.Add("has_value_added_promotion");
                    _where.Add("has_limited_time_offer");
                    _where.Add("has_bonus_reward_miles");
                    _where.Add("is_seasonal");
                    _where.Add("is_vqa");
                    _where.Add("is_ocb");
                    _where.Add("is_kosher");

                    break;
            }

            return _whereNot;
        }

        private static List<string> _orderBy;
        public static List<string> OrderBy(ServiceType serviceType) {
            
            switch (serviceType) {
                case ServiceType.Store:
                    _orderBy.Add("id");
                    _orderBy.Add("distance_in_meters");
                    _orderBy.Add("inventory_volume_in_milliliters");
                    _orderBy.Add("products_count");
                    _orderBy.Add("inventory_count");
                    _orderBy.Add("inventory_price_in_cents");

                    break;
                case ServiceType.Product:
                    _orderBy.Add("id");
                    _orderBy.Add("price_in_cents");
                    _orderBy.Add("regular_price_in_cents");
                    _orderBy.Add("limited_time_offer_savings_in_cents");
                    _orderBy.Add("limited_time_offer_ends_on");
                    _orderBy.Add("bonus_reward_miles");
                    _orderBy.Add("bonus_reward_miles_ends_on");
                    _orderBy.Add("package_unit_volume_in_milliliters");
                    _orderBy.Add("total_package_units");
                    _orderBy.Add("total_package_volume_in_milliliters");
                    _orderBy.Add("volume_in_milliliters");
                    _orderBy.Add("alcohol_content");
                    _orderBy.Add("price_per_liter_of_alcohol_in_cents");
                    _orderBy.Add("price_per_liter_in_cents");
                    _orderBy.Add("inventory_count");
                    _orderBy.Add("inventory_volume_in_milliliters");
                    _orderBy.Add("inventory_price_in_cents");
                    _orderBy.Add("released_on");

                    break;
                case ServiceType.Inventory:
                    _orderBy.Add("quantity");
                    _orderBy.Add("updated_on");

                    break;
            }

            return _orderBy;
        }

        public static string PageNumber = "page={0}";
        public static string PerPage = "per_page={0}";
        public static string SearchBy = "q={0}";
        public static string Geo = "geo={0}";
        public static string StoreId = "store_id={0}";
        public static string ProductId = "product_id={0}";
    }
}



//var nvc = new NameValueCollection
//    {
//        { "key1", "value1" },
//        { "key2", "value2" },
//        { "key3", "" }
//    };


//var builder = new UriBuilder("http://www.example.com");

//// Create query string with all values
//builder.Query = string.Join("&", nvc.AllKeys.Select(key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(nvc[key]))));

//// Omit empty values
//builder.Query = string.Join("&", nvc.AllKeys.Where(key => !string.IsNullOrWhiteSpace(nvc[key])).Select(key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(nvc[key]))));


//uri.Query = string.Join("&",
//                    nvc.AllKeys.Where(key => !string.IsNullOrWhiteSpace(nvc[key]))
//                        .Select(
//                            key => string.Join("&", nvc.GetValues(key).Select(val => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(val))))));