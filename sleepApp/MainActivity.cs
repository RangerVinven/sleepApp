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

            string result = getTimeToWakeUp("07", "00", "AM");

            System.Diagnostics.Debug.WriteLine(result);

        }

        // Gets the time to wake up
        private string getTimeToWakeUp(string hourString, string minuteString, string amOrPmString) {

            int hourInt = Int16.Parse(hourString);
            int minuteInt = Int16.Parse(minuteString);

            Time time = new Time(hourInt, minuteInt, amOrPmString.ToUpper());

            for(int i = 0; i < 5; i++) {
                time = remove30FromMinutes(time);
                time = remove1FromHours(time);
            }

            string timeString = string.Format("{0}:{1} {2}", time.hour, time.minutes, time.amOrPm);

            return timeString;

        }

        #region Functions that takes 1 from the time.hour and 30 from the time.minutes

        private Time remove30FromMinutes(Time time) {
            
            // Removes 30 from the time.minutes and makes it positive
            time.minutes = time.minutes - 30;
            time.minutes = checkIfMinutesIsNegative(time).minutes;

            return time;
        }

        private Time remove1FromHours(Time time) {

            // Removes 1 from the time.hour and makes it positive
            time.hour = time.hour - 1;
            time.hour = checkIfHoursIsNegative(time).hour;

            return time;
        }

        #endregion

        #region Functions that check if the hours and minutes are negative

        private Time checkIfMinutesIsNegative(Time time) {

            // Checks if the minutes are negative
            if(time.minutes < 0) {
                time.minutes = makeMinutesPositive(time).minutes;
            }

            return time;
        }

        private Time checkIfHoursIsNegative(Time time) {

            // Checks if the hours are negative
            if (time.hour < 0) {

                // Makes the hours positive and makes the amOrPm the other one (am becomes pm and vise versa)
                time.hour = makeHoursPositive(time).hour;
            }

            return time;
        }

        #endregion

        #region Functions that makes the minutes and hours positive

        private Time makeMinutesPositive(Time time) {

            // Subtracts the positive version of time.minutes from 60
            time.minutes = 60 - (time.minutes * -1);

            return time;
        }

        private Time makeHoursPositive(Time time) {

            // Subtracts the positive version of time.hour from 12
            time.hour = 60 - (time.hour * -1);

            // Swaps the AM and PM sign since it went past the AM or PM when it became negative
            time.amOrPm = swapAMandPM(time).amOrPm;

            return time;
        }

        #endregion

        private Time swapAMandPM(Time time) {
            // Swaps the am or the pm
            if(time.amOrPm.ToLower() == "am") {
                time.amOrPm = "PM";
            } else {
                time.amOrPm = "AM";
            }

            return time;
        }

    }
}
