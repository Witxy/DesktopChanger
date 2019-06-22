using System;
using System.Globalization;

namespace DesktopChanger
{
    public class DayChangeLogic
    {
        public static System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        private static bool isZero = false;
        private static String newDay = "";
        public static void timerTick(object sender, EventArgs e)
        {
            if (!isZero)
            {
                timer.Interval = new TimeSpan(0, 0, 10);
                isZero = true;
            }
            DateTime localDate = DateTime.Now;
            String day = localDate.ToString("dddd", new CultureInfo("en-US"));
            if (newDay != day)
            {
                dayChange(day);
            }
        }
        private static void dayChange(string day)
        {
            switch (day)
            {
                case "Monday":
                    WallpaperChanger.WallChange(PathWorker.WallpaperPath[0], MainWindow.Style);
                    newDay = day;
                    break;
                case "Tuesday":
                    WallpaperChanger.WallChange(PathWorker.WallpaperPath[1], MainWindow.Style);
                    newDay = day;
                    break;
                case "Wednesday": WallpaperChanger.WallChange(PathWorker.WallpaperPath[2], MainWindow.Style);
                    newDay = day;
                    break;
                case "Thursday": WallpaperChanger.WallChange(PathWorker.WallpaperPath[3], MainWindow.Style); break;
                case "Friday":   WallpaperChanger.WallChange(PathWorker.WallpaperPath[4], MainWindow.Style); break;
                case "Saturday": WallpaperChanger.WallChange(PathWorker.WallpaperPath[5], MainWindow.Style); break;
                case "Sunday":   WallpaperChanger.WallChange(PathWorker.WallpaperPath[6], MainWindow.Style); break;
                default:
                    break;
            }
        }
    }
}
