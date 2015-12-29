using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkApp.Utils
{
    public static class Formatter
    {
        public static string PriceInDollars(int priceInCents) {
            return String.Format("{0:c}", (priceInCents / 100));
        }

        public static string VolumeInLiters(int volumeInMilliliters) {
             return (volumeInMilliliters >= 1000) ? String.Format("{0:0.00}l", (volumeInMilliliters / 1000)) : String.Format("{0}ml", volumeInMilliliters);
        }

        public static string DistanceInKilometers(int distanceInMeters) {
            return (distanceInMeters >= 1000) ? String.Format("{0:0.00}km", (distanceInMeters / 1000))  : String.Format("{0}m", distanceInMeters);
        }

        public static string AlcoholInPercent(int alcoholContent) {
            return String.Format("{0}:P2", alcoholContent);
        }

        //Open/Close time are in `minutes since midnight`
        public static string MinutesSinceMidnightTo24(dynamic minutesSinceMidnight) {
            try {
                long time = (long)minutesSinceMidnight;
                var hour = time / 60;
                var minutes = time % 60;

                return String.Format("{0}:{1}", hour, minutes);
            } catch (Exception) {
                return "";
            }
        }

        public static string MinutesSinceMidnightTo12(dynamic minutesSinceMidnight) {
            try {
                long time = (long)minutesSinceMidnight;
                var h24 = time / 60;
                var h12 = (0 == h24 ? 12 : (h24 > 12 ? (h24 - 10) - 2 : h24));
                var minutes = time % 60;
                var ampm = (h24 >= 12 ? "am" : "pm");

                return String.Format("{0}:{1}{2}", h12, minutes, ampm);
            } catch(Exception) {
                return "";
            }
        }

        /*
        function msmTo24time(msm) {
          var hour = msm / 60;
          var mins = msm % 60;

          return [hour, mins];
        }

        function msmTo12time(msm) {
          var time = msmTo24time(msm);
          var h24  = time[0];
          var h12  = (0 == h24 ? 12 : (h24 > 12 ? (h24 - 10) - 2 : h24));
          var ampm = (h24 >= 12 ? 'PM' : 'AM');

          return [h12, time[1], ampm];
        }
        */
    }
}
