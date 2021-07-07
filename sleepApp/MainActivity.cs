using System;
using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using Android.Widget;
using System.Collections.Generic;

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

            //string result = getTimeToWakeUp("07", "00", "AM");
            //Toast.MakeText(Application.Context, result, ToastLength.Short).Show();

            // Gets the calculate button element
            Button calculateBtn = FindViewById<Button>(Resource.Id.toWakeUpCalBtn);

            calculateBtn.Click += calculateBtnClick;

        }

        private void calculateBtnClick(object sender, EventArgs e) {
            // Gets the text view element
            TextView time1 = FindViewById<TextView>(Resource.Id.time1);
            TextView time2 = FindViewById<TextView>(Resource.Id.time2);
            TextView time3 = FindViewById<TextView>(Resource.Id.time3);
            TextView time4 = FindViewById<TextView>(Resource.Id.time4);
            TextView time5 = FindViewById<TextView>(Resource.Id.time5);
            TextView time6 = FindViewById<TextView>(Resource.Id.time6);

            // Gets the hour, minutes and AM or PM
            string hour = FindViewById<Spinner>(Resource.Id.hourSpinner).SelectedItem.ToString();
            string minute = FindViewById<Spinner>(Resource.Id.minutesSpinner).SelectedItem.ToString();
            string amOrPm = FindViewById<Spinner>(Resource.Id.amOrPmSpinner).SelectedItem.ToString();

            // Toast.MakeText(Application.Context, hour, ToastLength.Short).Show();

            // Replaces the timeTextView to the time calculated
            //timeTextView.Text = getTimeToWakeUp(hour, minute, amOrPm).timesToDisplay.Count.ToString();
            
            // Replaces all the time text views with the times
            List<string> times = new List<string>(getTimeToWakeUp(hour, minute, amOrPm).timesToDisplay);

            time1.Text = times[0];
            time2.Text = times[1];
            time3.Text = times[2];
            time4.Text = times[3];
            time5.Text = times[4];
            time6.Text = times[5];

        }

        // Gets the time to wake up
        private Time getTimeToWakeUp(string hourString, string minuteString, string amOrPmString) {

            // Initializes necessary variables
            int hourInt = Int16.Parse(hourString);
            int minuteInt = Int16.Parse(minuteString);

            Time time = new Time(hourInt, minuteInt, amOrPmString.ToUpper());

            // Loops for the specified amount of times
            for(int i = 0; i < 8; i++) {
                time.minutes = remove30FromMinutes(time).minutes;
                time.hour = remove1FromHours(time).hour;

                if ((i >= 2) && (i <= 8)) {

                    // Converts the hourInt and minuteInt to a String
                    hourString = time.hour.ToString();
                    minuteString = time.minutes.ToString();

                    // Adds an extra 0 to the hourString and minuteString if needed
                    if (hourString.Length == 1) {
                        hourString = string.Concat("0", hourString);
                    }

                    if (minuteString.Length == 1) {
                        minuteString = string.Concat("0", minuteString);
                    }

                    // Saves the time if they're the 3, 4, 5, 6 or 7th iteration (to give the user multiple times)
                    time.timesToDisplay.Add(string.Concat(hourString, ":", minuteString, " ", time.amOrPm));

                    //Toast.MakeText(Application.Context, time.timesToDisplay[i-3], ToastLength.Short).Show();

                }

            }

            return time;

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
                time.hour = remove1FromHours(time).hour;
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
            time.hour = 12 - (time.hour * -1);

            // Swaps the AM and PM sign since it went past the AM or PM when it became negative
            time.amOrPm = swapAMandPM(time).amOrPm;

            return time;
        }

        #endregion

        private Time swapAMandPM(Time time) {
            // Swaps the am or the pm
            if(time.amOrPm == "AM") {
                time.amOrPm = "PM";
            } else {
                time.amOrPm = "AM";
            }

            return time;
        }

    }
}
