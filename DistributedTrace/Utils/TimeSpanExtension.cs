using System;

namespace DistributedTrace.Utils
{
    /// <summary>
    /// Расширение работы с интервалом времени
    /// </summary>
    public static class TimeSpanExtension
    {
        public static string GetDisplayText(this TimeSpan? ts)
        {
            if (ts == null) return null;
            return ts.Value.GetDisplayText();
        }

        /// <summary>
        /// Получить отображаемый текст интервала
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static string GetDisplayText(this TimeSpan ts)
        {
            if (ts.TotalDays > 1)
                return string.Format("{0}d, {1}h", ts.Days, ts.Hours);
            else if (ts.TotalHours > 1)
                return string.Format("{0}h, {1}m", ts.Hours, ts.Minutes);
            else if (ts.TotalMinutes > 1)
                return string.Format("{0}m, {1}s", ts.Minutes, ts.Seconds);
            else if (ts.TotalSeconds > 1)
                return string.Format("{0}s, {1}ms", ts.Seconds, ts.Milliseconds);
            else
                return string.Format("{0}ms", ts.Milliseconds);
        }
    }
}
