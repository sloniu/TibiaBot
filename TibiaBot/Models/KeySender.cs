using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextBox = System.Windows.Controls.TextBox;

namespace TibiaBot.Models
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
        public async Task SendKey(Process proc, int mana, double prof, double item, int key, TextBox textBox, int timesPressed)
        {
//            var proc = Process.GetProcessesByName("client").FirstOrDefault();
//            if (proc == null)
//            {
//                return;
//            }

            while (true)
            {
                if (Stop)
                {
                    break;
                }
                
                var rnd = new Random().Next(10, 200);
                //todo: remove this trash
                if (timesPressed == 2)
                {
                    PostMessage(proc.MainWindowHandle, WM_KEYDOWN, 0x1B, 0);
                    PostMessage(proc.MainWindowHandle, WM_KEYUP, 0x1B, 0);
                    await Task.Delay(250 + new Random().Next(1, 50));
                }
                for (var i = 0; i < timesPressed; i++)
                {
                    PostMessage(proc.MainWindowHandle, WM_KEYDOWN, key, 0);
                    PostMessage(proc.MainWindowHandle, WM_KEYUP, key, 0);
                }

                    
                _totalrnd += rnd;
                var delay = mana / (prof + item) * 1000 + rnd;

//                if (_totalrnd > 10000)
//                {
//                    delay -= _totalrnd;
//                    textBox?.AppendText($"\n{DateTime.Now:hh:mm:ss} - Delay of {_totalrnd} removed.");
//                    _totalrnd = 0;
//                }
                textBox?.AppendText($"\n{DateTime.Now:hh:mm:ss} - Pressed {key} with: {delay} ms.");
                textBox?.ScrollToEnd();
                await Task.Delay((int)delay);
            }

        }


        public async Task SendKey(Process proc, int key)
        {
            var rnd = new Random().Next(10, 200);
            await Task.Delay(rnd);

            PostMessage(proc.MainWindowHandle, WM_KEYDOWN, key, 0);
            PostMessage(proc.MainWindowHandle, WM_KEYUP, key, 0);
        }

        public async Task SendKeyMultiple(Process proc, int mana, double prof, double item, int[] key, TextBox textBox)
        {
//            var proc = Process.GetProcessesByName("client").FirstOrDefault();
//            if (proc == null)
//            {
//                return;
//            }

            while (true)
            {
                if (Stop)
                {
                    break;
                }

                var rnd = new Random().Next(10, 200);
                _totalrnd += rnd;
                var delay = mana / (prof + item) * 1000 + rnd;

                textBox?.AppendText($"\n{DateTime.Now:hh:mm:ss} - Pressed {key} with: {delay} ms.");
                textBox?.ScrollToEnd();

                await Task.Delay((int)delay);

                foreach (var t in key)
                {
                    PostMessage(proc.MainWindowHandle, WM_KEYDOWN, t, 0);
                    await Task.Delay(250);
                    PostMessage(proc.MainWindowHandle, WM_KEYUP, t, 0);
                    await Task.Delay(250);
                }

                if (_totalrnd > 10000)
                {
                    delay -= _totalrnd;
                    textBox?.AppendText($"\n{DateTime.Now:hh:mm:ss} - Delay of {_totalrnd} removed.");
                    _totalrnd = 0;
                }

                //await Task.Delay((int)delay);
            }

        }

    }
}
