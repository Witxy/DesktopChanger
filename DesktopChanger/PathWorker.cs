using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DesktopChanger
{
    internal class PathWorker
    {
        public static string[] WallpaperPath = new string[7];
        public static void WallpaperPathWorker()
        {
            if (!File.Exists("Path.txt"))
            {
                File.CreateText("Path.txt").Dispose();
            }
            else
            {
                using (var sr = new StreamReader("Path.txt"))
                {
                    for (int i = 0; i < 7; i++)
                    {
                        WallpaperPath[i] = sr.ReadLine();
                    }
                }
            }
        }

        public static void WallpaperPathChanger(string path, int day)
        {
            WallpaperPath[day] = path;
        }

        public static void WallpaperPathWriter()
        {
            using (var sw = new StreamWriter("Path.txt"))
            {
                for (int i = 0; i < 7; i++)
                {
                    sw.WriteLine(WallpaperPath[i]);
                }
            }
        }

    }
}
