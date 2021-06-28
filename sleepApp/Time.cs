namespace sleepApp {
    class Time {

        public int hour;
        public int minutes;
        public string amOrPm;
        
        public Time(int hourInt, int minutesInt, string amOrPmString) {
            hour = hourInt;
            minutes = minutesInt;
            amOrPm = amOrPmString;
        }

    }
}