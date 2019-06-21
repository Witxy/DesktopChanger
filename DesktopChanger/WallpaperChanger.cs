using System;
using System.Runtime.InteropServices;

namespace DesktopChanger
{
    class WallpaperChanger
    {
        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam, string pvParam, uint fWinIni);

        public static void WallChange(String path)
        {
            SystemParametersInfo(0x0014, 0, path, 0x0001);
        }

    }
}
