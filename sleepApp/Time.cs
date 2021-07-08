using System.Collections.Generic;

namespace sleepApp {
    class Time {

        public int hour;
        public int minutes;
        public string amOrPm;
        public List<string> timesToDisplay = new List<string>();

        public Time() {
            hour = 0;
            minutes = 0;
            amOrPm = "";
        }

        public Time(int hourInt, int minutesInt, string amOrPmString) {
            hour = hourInt;
            minutes = minutesInt;
            amOrPm = amOrPmString;
        }

    }
}