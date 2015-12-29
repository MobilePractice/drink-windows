using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DrinkApp.Utils {
    public static class Geolocation {
        // Captures the value entered by user.
        private static uint _desireAccuracyInMetersValue = 500;
        private static CancellationTokenSource _cts = null;
        private static Geoposition _position = null;

        public static async Task<Geoposition> GetGeoPosition() {
            try {
                // Request permission to access location
                var accessStatus = await Geolocator.RequestAccessAsync();

                switch (accessStatus) {
                    case GeolocationAccessStatus.Allowed:
                        // Get cancellation token
                        _cts = new CancellationTokenSource();
                        CancellationToken token = _cts.Token;

                        // If DesiredAccuracy or DesiredAccuracyInMeters are not set (or value is 0), DesiredAccuracy.Default is used.
                        Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = _desireAccuracyInMetersValue };

                        // Carry out the operation
                        _position = await geolocator.GetGeopositionAsync().AsTask(token);

                        break;

                    case GeolocationAccessStatus.Denied:
                        break;

                    case GeolocationAccessStatus.Unspecified:
                        break;
                }
            } catch (TaskCanceledException) {
                
            } catch (Exception) {
                
            } finally {
                _cts = null;
            }

            return _position;
        }
    }
}
