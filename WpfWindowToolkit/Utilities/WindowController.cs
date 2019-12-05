using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Utilities
{
    /// <summary>
    /// WindowController
    /// </summary>
    /// <remarks>Code from here:https://coderelief.net/2010/04/19/wpf-window-disable-minimize-and-maximize-buttons-through-attached-properties-from-xaml/ </remarks>
    internal static class WindowController
    {
        private const Int32 GWL_STYLE = -16;
        private const Int32 WS_MAXIMIZEBOX = 0x00010000;
        private const Int32 WS_MINIMIZEBOX = 0x00020000;
        private const Int32 WS_SYSMENU = 0x80000;

        public static void DisableClose(Window window)
        {
            lock (window)
            {
                IntPtr hWnd = new WindowInteropHelper(window).Handle;
                Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_SYSMENU);
            }
        }

        /// <summary>
        /// Disables the maximize functionality of a WPF window.
        /// </summary>
        ///The WPF window to be modified.
        public static void DisableMaximize(Window window)
        {
            lock (window)
            {
                IntPtr hWnd = new WindowInteropHelper(window).Handle;
                Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_MAXIMIZEBOX);
            }
        }

        /// <summary>
        /// Disables the minimize functionality of a WPF window.
        /// </summary>
        ///The WPF window to be modified.
        public static void DisableMinimize(Window window)
        {
            lock (window)
            {
                IntPtr hWnd = new WindowInteropHelper(window).Handle;
                Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_MINIMIZEBOX);
            }
        }

        public static void EnableClose(Window window)
        {
            lock (window)
            {
                IntPtr hWnd = new WindowInteropHelper(window).Handle;
                Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_SYSMENU);
            }
        }

        /// <summary>
        /// Enables the maximize functionality of a WPF window.
        /// </summary>
        ///The WPF window to be modified.
        public static void EnableMaximize(Window window)
        {
            lock (window)
            {
                IntPtr hWnd = new WindowInteropHelper(window).Handle;
                Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_MAXIMIZEBOX);
            }
        }

        /// <summary>
        /// Enables the minimize functionality of a WPF window.
        /// </summary>
        ///The WPF window to be modified.
        public static void EnableMinimize(Window window)
        {
            lock (window)
            {
                IntPtr hWnd = new WindowInteropHelper(window).Handle;
                Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_MINIMIZEBOX);
            }
        }

        /// <summary>
        /// Toggles the enabled state of a WPF window's maximize functionality.
        /// </summary>
        ///The WPF window to be modified.
        public static void ToggleMaximize(Window window)
        {
            lock (window)
            {
                IntPtr hWnd = new WindowInteropHelper(window).Handle;
                Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);

                if ((windowStyle | WS_MAXIMIZEBOX) == windowStyle)
                {
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_MAXIMIZEBOX);
                }
                else
                {
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_MAXIMIZEBOX);
                }
            }
        }

        /// <summary>
        /// Toggles the enabled state of a WPF window's minimize functionality.
        /// </summary>
        ///The WPF window to be modified.
        public static void ToggleMinimize(Window window)
        {
            lock (window)
            {
                IntPtr hWnd = new WindowInteropHelper(window).Handle;
                Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);

                if ((windowStyle | WS_MINIMIZEBOX) == windowStyle)
                {
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_MINIMIZEBOX);
                }
                else
                {
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_MINIMIZEBOX);
                }
            }
        }

        [DllImport("User32.dll", EntryPoint = "GetWindowLong")]
        private extern static Int32 GetWindowLongPtr(IntPtr hWnd, Int32 nIndex);

        [DllImport("User32.dll", EntryPoint = "SetWindowLong")]
        private extern static Int32 SetWindowLongPtr(IntPtr hWnd, Int32 nIndex, Int32 dwNewLong);
    }
}