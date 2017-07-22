using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace ClipTweet.Utilities
{
    public class ClipboardWatcher : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SetClipboardViewer(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        private static extern bool ChangeClipboardChain(IntPtr hwnd, IntPtr hWndNext);

        const int WM_DRAWCLIPBOARD = 0x0308;
        const int WM_CHANGECBCHAIN = 0x030D;

        IntPtr nextHandle;
        IntPtr handle;

        HwndSource hwndSource = null;
        
        public event EventHandler DrawClipboard;
        
        public ClipboardWatcher(IntPtr handle)
        {
            this.hwndSource = HwndSource.FromHwnd(handle);
            this.hwndSource.AddHook(WndProc);
            this.handle = handle;
            this.nextHandle = SetClipboardViewer(this.handle);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_DRAWCLIPBOARD)
            {
                SendMessage(this.nextHandle, msg, wParam, lParam);
                RaiseDrawClipboard();
                handled = true;
            }
            else if (msg == WM_CHANGECBCHAIN)
            {
                if (wParam == this.nextHandle)
                {
                    this.nextHandle = lParam;
                }
                else
                {
                    SendMessage(this.nextHandle, msg, wParam, lParam);
                }
                handled = true;
            }

            return IntPtr.Zero;
        }

        private void RaiseDrawClipboard()
        {
            DrawClipboard?.Invoke(this, EventArgs.Empty);
        }
        
        public void Dispose()
        {
            ChangeClipboardChain(this.handle, this.nextHandle);
            this.hwndSource.Dispose();
        }
    }
}