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

            // Gets the calculate button element and declares an onclick
            Button calculateBtn = FindViewById<Button>(Resource.Id.toWakeUpCalBtn);
            calculateBtn.Click += calculateBtnClick;

            // Gets the calculate button for going to bed now and sets an onclick
            Button goToBedNowBtn = FindViewById<Button>(Resource.Id.goToBedNowBtn);
            goToBedNowBtn.Click += goToBedNowBtnClick;

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

            // Makes sure the user has selected an hour, minute (AM or PM is already selected)
            if((hour == "Hour") || (minute == "Minute")) {
                Toast.MakeText(Application.Context, "Please enter a valid hour and minute", ToastLength.Short).Show();
                return;
            }

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

        private void goToBedNowBtnClick(object sender, EventArgs e) {
            // Gets the text view element
            TextView time1 = FindViewById<TextView>(Resource.Id.time1GoToBedNow);
            TextView time2 = FindViewById<TextView>(Resource.Id.time2GoToBedNow);
            TextView time3 = FindViewById<TextView>(Resource.Id.time3GoToBedNow);
            TextView time4 = FindViewById<TextView>(Resource.Id.time4GoToBedNow);
            TextView time5 = FindViewById<TextView>(Resource.Id.time5GoToBedNow);
            TextView time6 = FindViewById<TextView>(Resource.Id.time6GoToBedNow);

            // Makes the timeGoToBed object and assigns the variables to the corrosponding current time
            Time timeGoToBed = new Time();
            timeGoToBed = getCurrentTime(timeGoToBed);

            timeGoToBed = getTimeIfGoingToBed(timeGoToBed);

            time1.Text = timeGoToBed.timesToDisplay[0];
            time2.Text = timeGoToBed.timesToDisplay[1];
            time3.Text = timeGoToBed.timesToDisplay[2];
            time4.Text = timeGoToBed.timesToDisplay[3];
            time5.Text = timeGoToBed.timesToDisplay[4];
            time6.Text = timeGoToBed.timesToDisplay[5];
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

        // Gets time if going to bed now
        private Time getTimeIfGoingToBed(Time time) {

            for (int i = 0; i < 8; i++) {

                // Adds 1 hour and 30 minutes onto the time
                time.hour = add1ToHours(time).hour;
                time.minutes = add30ToMinutes(time).minutes;

                if ((i >= 2) && (i <= 8)) {

                    // Converts the hourInt and minuteInt to a String
                    string hourString = time.hour.ToString();
                    string minuteString = time.minutes.ToString();

                    // Adds an extra 0 to the hourString and minuteString if needed
                    if (hourString.Length == 1) {
                        hourString = string.Concat("0", hourString);
                    }

                    if (minuteString.Length == 1) {
                        minuteString = string.Concat("0", minuteString);
                    }

                    // Saves the time if they're the 3, 4, 5, 6 or 7th iteration (to give the user multiple times)
                    time.timesToDisplay.Add(string.Concat(hourString, ":", minuteString, " ", time.amOrPm));

                }

            }

            return time;

        }

        // Gets the current time
        private Time getCurrentTime(Time time) {

            // Gets the current hour
            Int16 hour = Int16.Parse(DateTime.Now.ToString("hh"));
            time.hour = hour;

            // Gets the current minute (and adds 14 minutes as it takes that long (on average) to fall asleep)
            Int16 minute = Int16.Parse(DateTime.Now.ToString("mm"));
            time.minutes = minute + 14;

            time.minutes = checkIfMinutesAreAbove60(time).minutes;

            // Gets the AM or PM
            string amOrPm = DateTime.Now.ToString("tt");
            time.amOrPm = amOrPm;

            return time;
        }

        // Functions for the time to wake up part

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

        // Functions for the going to bed now bit

        #region Functions that adds 1 to the time.hour and 30 to the time.minutes

        private Time add30ToMinutes(Time time) {

            // Adds 30 from the time.minutes and makes it positive
            time.minutes = time.minutes + 30;
            time.minutes = checkIfMinutesAreAbove60(time).minutes;

            return time;
        }

        private Time add1ToHours(Time time) {

            // Adds 1 from the time.hour and makes it positive
            time.hour = time.hour + 1;
            time.hour = checkIfHoursAreAbove12(time).hour;

            return time;
        }

        #endregion

        #region Functions that checks if the hours and minutes are 60 or above

        private Time checkIfHoursAreAbove12(Time time) {

            // Checks if the hour is above 12 (as it's 12 hour time, not 24)
            if(time.hour > 12) {
                time.hour = makeHoursWithin12Hours(time).hour;
            }

            return time;
        }

        private Time checkIfMinutesAreAbove60(Time time) {

            // Checks if the minutes is above 60 (as that's a new hour)
            if (time.minutes >= 60) {
                time.minutes = makeMinutesWithin60Minutes(time).minutes;
                time = add1ToHours(time);
            }

            return time;
        }

        #endregion

        #region Functions that makes the minutes and hours positive

        private Time makeMinutesWithin60Minutes(Time time) {

            // Subtracts 12 from time.minutes
            time.minutes = time.minutes - 60;

            return time;
        }

        private Time makeHoursWithin12Hours(Time time) {

            // Subtracts 12 from time.hour
            time.hour = time.hour-12;

            // Swaps the AM and PM sign since it went past the AM or PM when it went past 12
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
