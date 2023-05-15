using UnityEngine;

namespace Util
{
    public static class TimeFormatter
    {
        public static string FormatTime(double seconds, string divider)
        {
            int millis = (int) (seconds % 1 * 100);
            int secs = (int) seconds % 60;
            int mins = (int) seconds / 60;
            int hours = (int) seconds / 3600;
            return (hours <= 9 ? "0" : "") + hours + divider + (mins <= 9 ? "0" : "") + mins + divider
                        + (secs <= 9 ? "0" : "") + secs + divider + (millis <= 9 ? "0" : "") + millis;
        }

        public static string FormatTime(double seconds)
        {
            return FormatTime(seconds, ":");
        }
    }
}