using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;

namespace sleepApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
        }

        // Gets the time to wake up
        private string getTimeToWakeUp(string hourString, string minuteString, string amOrPmString) {

            int hourInt = Int16.Parse(hourString);
            int minuteInt = Int16.Parse(minuteString);

            

        }

        #region Functions that takes 1 from the hoursInt and 30 from the minutesInt

        private int remove30FromMinutes(int minutesInt) {
            
            // Removes 30 from the minutesInt and makes it positive
            minutesInt = minutesInt - 30;
            minutesInt = checkIfMinutesIsNegative(minutesInt);

            return minutesInt;
        }

        private int remove1FromHours(int hoursInt) {

            // Removes 1 from the hoursInt and makes it positive
            hoursInt = hoursInt - 1;
            hoursInt = checkIfHoursIsNegative(hoursInt);

            return hoursInt;
        }

        #endregion

        #region Functions that check if the hours and minutes are negative

        private int checkIfMinutesIsNegative(int minutesInt) {

            // Checks if the minutes are negative
            if(minutesInt < 0) {
                minutesInt = makeMinutesPositive(minutesInt);
            }

            return minutesInt;
        }

        private int checkIfHoursIsNegative(int hoursInt, string amOrPmString) {

            // Checks if the hours are negative
            if (hoursInt < 0) {

                // Makes the hours positive and makes the amOrPm the other one (am becomes pm and vise versa)
                hoursInt = makeHoursPositive(hoursInt);
                amOrPmString = swapAMandPM(amOrPmString);

                // Tuple so it returns the hours and the am or pm

            }

            return hoursInt;
        }

        #endregion

        #region Functions that makes the minutes and hours positive

        private int makeMinutesPositive(int minutesInt) {

            // Subtracts the positive version of minutesInt from 60
            minutesInt = 60 - (minutesInt * -1);

            return minutesInt;
        }

        private int makeHoursPositive(int hoursInt) {

            // Subtracts the positive version of hoursInt from 12
            hoursInt = 60 - (hoursInt * -1);

            // Swaps the AM and PM sign since it went past the AM or PM when it became negative

            return hoursInt;
        }

        #endregion

        private string swapAMandPM(string amOrPmString) {
            // Swaps the am or the pm
            if(amOrPmString.ToLower() == "am") {
                amOrPmString = "PM"
            } else {
                amOrPmString = "AM";
            }

            return amOrPmString;
        }

    }
}
