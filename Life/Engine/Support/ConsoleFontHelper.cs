using System;
using System.Runtime.InteropServices;

namespace Life.Engine.Support
{
    public static class ConsoleFontHelper
    {
        private static readonly IntPtr _consoleOutputHandle = GetStdHandle(-11);

        //public static SetFontSize


        public static FontInfo[] SetCurrentFont(string font, short fontSize = 0)
        {
            FontInfo before = new FontInfo
            {
                cbSize = Marshal.SizeOf<FontInfo>()
            };

            if (GetCurrentConsoleFontEx(_consoleOutputHandle, false, ref before))
            {

                FontInfo set = new FontInfo
                {
                    cbSize = Marshal.SizeOf<FontInfo>(),
                    FontIndex = 0,
                    FontFamily = 54,
                    FontName = font,
                    FontWeight = 400,
                    FontSize = fontSize > 0 ? fontSize : before.FontSize
                };

                // Get some settings from current font.
                if (!SetCurrentConsoleFontEx(_consoleOutputHandle, false, ref set))
                {
                    var ex = new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                    Logger.ErrorLog(ex);
                    throw ex;
                }

                FontInfo after = new FontInfo
                {
                    cbSize = Marshal.SizeOf<FontInfo>()
                };
                GetCurrentConsoleFontEx(_consoleOutputHandle, false, ref after);

                return new[] { before, set, after };
            }
            else
            {
                var ex = new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                Logger.ErrorLog(ex);
                throw ex;
            }
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct FontInfo
        {
            internal int cbSize;
            internal int FontIndex;
            internal short FontWidth;
            public short FontSize;
            public int FontFamily;
            public int FontWeight;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            //[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.wc, SizeConst = 32)]
            public string FontName;
        }
    }
}