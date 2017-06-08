using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TextBox = System.Windows.Controls.TextBox;

namespace TibiaBotMvvm.Models
{
    public class KeySender
    {
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        const uint WM_KEYDOWN = 0x0100;
        private const uint WM_KEYUP = 0x0101;
        const uint WM_SYSKEYDOWN = 0x0104;
        const uint WM_SYSKEYUP = 0x0105;
        const int VK_F5 = 0x74;
        private const int VK_F6 = 0x75;

        public bool Stop = false;
        private int _totalrnd;

        /// <summary>
        /// Sends specified key to a process
        /// </summary>
        /// <param name="proc">Tibia process</param>
        /// <param name="mana">Required mana for a spell / delay between clicks</param>
        /// <param name="prof">Class param, set 1 to ignore</param>
        /// <param name="item">Item equipped param, set 0 to ignore</param>
        /// <param name="key">VK key id</param>
        /// <param name="textBox">Textbox for debug</param>
        /// <param name="timesPressed">Amount of presses in single loop</param>
        /// <returns></returns>
        public async Task SendKey(Process proc, int key, int timesPressed)
        {
                await Task.Delay(250);

                //todo: remove this trash
                if (timesPressed == 2)
                {
                    //press esc
                    PostMessage(proc.MainWindowHandle, WM_KEYDOWN, 0x1B, 0);
                    PostMessage(proc.MainWindowHandle, WM_KEYUP, 0x1B, 0);
                    await Task.Delay(250 + new Random().Next(1, 50));
                }
                for (var i = 0; i < timesPressed; i++)
                {
                    PostMessage(proc.MainWindowHandle, WM_KEYDOWN, key, 0);
                    PostMessage(proc.MainWindowHandle, WM_KEYUP, key, 0);
                }
        }
    }
}
