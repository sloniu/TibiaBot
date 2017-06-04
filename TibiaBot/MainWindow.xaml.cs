using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using TibiaBot.Controls;
using TibiaBot.Models;
using TibiaBot.Memory;


namespace TibiaBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            keys1.ItemsSource = _keys;
            keys2.ItemsSource = _keys;
            keys3.ItemsSource = _keys;
            classCombo.ItemsSource = Classes;
            itemCombo.ItemsSource = _items;
            loadProcessButtonBase_OnClick(new object(), new AccessKeyPressedEventArgs());
            HealControls = new List<HealControl>();
        }


        //public List<TibiaProc> ProcessList { get; set; }

        private KeySender _ksF5;
        private bool _ksF5isrunning = true;
        private KeySender _ksF6;
        private KeySender _ksSpace;
        private KeySender _ksLogout;
        private Alarm _alarm;
        //private List<HealControl> heals = new List<HealControl>();

        public List<HealControl> HealControls { get; set; }

        #region class

        private static readonly Dictionary<string, double> Classes = new Dictionary<string, double>()
        {
            {"Knight", 0.33},
            {"Paladin", 0.5},
            {"Sorcerer", 0.66},
            {"Druid", 0.66},
            {"Elite Knight", 0.33},
            {"Royal Paladin", 0.66},
            {"Master Sorcerer", 1},
            {"Elder Druid", 1},
        };

        #endregion

        #region items

        private readonly Dictionary<string, double> _items = new Dictionary<string, double>()
        {
            {"Brak", 0},
            {"Life Ring", 1.33},
            {"Ring of Healing", 4},
            {"Soft Boots", 2},
        };

        #endregion

        #region keys

        private readonly Dictionary<string, uint> _keys = new Dictionary<string, uint>()
        {
            {"F1 key", 0x70},
            {"F2 key", 0x71},
            {"F3 key", 0x72},
            {"F4 key", 0x73},
            {"F5 key", 0x74},
            {"F6 key", 0x75},
            {"F7 key", 0x76},
            {"F8 key", 0x77},
            {"F9 key", 0x78},
            {"F10 key", 0x79},
            {"F11 key", 0x7A},
            {"F12 key", 0x7B},
            {"SPACEBAR", 0x20},
            {"ESC key", 0x1B },
            {"ALT key", 0x12 },
            {"0 key", 0x30},
            {"1 key", 0x31},
            {"2 key", 0x32},
            {"3 key", 0x33},
            {"4 key", 0x34},
            {"5 key", 0x35},
            {"6 key", 0x36},
            {"7 key", 0x37},
            {"8 key", 0x38},
            {"9 key", 0x39},
            {"A key", 0x41},
            {"B key", 0x42},
            {"C key", 0x43},
            {"D key", 0x44},
            {"E key", 0x45},
            {"F key", 0x46},
            {"G key", 0x47},
            {"H key", 0x48},
            {"I key", 0x49},
            {"J key", 0x4A},
            {"K key", 0x4B},
            {"L key", 0x4C},
            {"M key", 0x4D},
            {"N key", 0x4E},
            {"O key", 0x4F},
            {"P key", 0x50},
            {"Q key", 0x51},
            {"R key", 0x52},
            {"S key", 0x53},
            {"T key", 0x54},
            {"U key", 0x55},
            {"V key", 0x56},
            {"W key", 0x57},
            {"X key", 0x58},
            {"Y key", 0x59},
            {"Z key", 0x5A},
            {"Numeric keypad 0 key", 0x60},
            {"Numeric keypad 1 key", 0x61},
            {"Numeric keypad 2 key", 0x62},
            {"Numeric keypad 3 key", 0x63},
            {"Numeric keypad 4 key", 0x64},
            {"Numeric keypad 5 key", 0x65},
            {"Numeric keypad 6 key", 0x66},
            {"Numeric keypad 7 key", 0x67},
            {"Numeric keypad 8 key", 0x68},
            {"Numeric keypad 9 key", 0x69},
            {"Multiply key", 0x6A},
            {"Add key", 0x6B},
            {"Separator key", 0x6C},
            {"Subtract key", 0x6D},
            {"Decimal key", 0x6E},
            {"Divide key",0x6F},
        };

        #endregion


        #region Button1

        private async void F5_OnClick(object sender, RoutedEventArgs e)
        {
            if (mana1.Text.Length == 0)
            {
                return;
            }

            keys1.IsEnabled = false;

            logBox.AppendText("\nF5 started.");
            logBox.ScrollToEnd();
            _ksF5 = new KeySender();
            var proc = (TibiaProc)processCombo.SelectedValue;
            while (_ksF5isrunning)
            {
                if (MemoryReader.mana >= int.Parse(mana1.Text))
                {
                    await _ksF5.SendKey(proc.Process, int.Parse(keys1.SelectedValue.ToString()));
                    

                }
                await Task.Delay(250);
            }
            //await _ksF5.SendKey(proc.Process, int.Parse(mana1.Text), double.Parse(classCombo.SelectedValue.ToString()), double.Parse(itemCombo.SelectedValue.ToString()), int.Parse(keys1.SelectedValue.ToString()), logBox, 1);
        }

        private void F5stopbutton_OnClick(object sender, RoutedEventArgs e)
        {
            _ksF5isrunning = false;
            //_ksF5.Stop = true;

            keys1.IsEnabled = true;
            logBox.AppendText("\nF5 stopped.");
            logBox.ScrollToEnd();
        }

        #endregion

        #region Button2

        private async void F6_OnClick(object sender, RoutedEventArgs e)
        {
            if (mana2.Text.Length == 0)
            {
                return;
            }


            keys2.IsEnabled = false;

            logBox.AppendText("\nF6 started.");
            logBox.ScrollToEnd();
            _ksF6 = new KeySender();
            var proc = (TibiaProc)processCombo.SelectedValue;
            await _ksF6.SendKey(proc.Process, int.Parse(mana2.Text), 1, 0, int.Parse(keys2.SelectedValue.ToString()), logBox, 1);

        }

        public void F6stopButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _ksF6.Stop = true;

            keys2.IsEnabled = true;
            logBox.AppendText("\nF6 stopped.");
            logBox.ScrollToEnd();
        }

        #endregion

        #region Button3

        private async void SpaceButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (spacebox.Text.Length == 0)
            {
                return;
            }


            keys3.IsEnabled = false;

            logBox.AppendText("\nSpace started.");
            logBox.ScrollToEnd();
            _ksSpace = new KeySender();
            var proc = (TibiaProc)processCombo.SelectedValue;
            await _ksSpace.SendKey(proc.Process, int.Parse(spacebox.Text), 1, 0, int.Parse(keys3.SelectedValue.ToString()), logBox, 2);
        }

        private void StopSpaceButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _ksSpace.Stop = true;

            keys3.IsEnabled = true;
            logBox.AppendText("\nSpace stopped.");
            logBox.ScrollToEnd();
        }

        #endregion


        //todo: logout after X time, shutdown pc
        private async void LogoutButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (logout.Text.Length == 0)
            {
                return;
            }

            logBox.AppendText("\nLogout started.");
            logBox.ScrollToEnd();
            _ksLogout = new KeySender();
            var keys = new []{ 0x7B };
            var proc = (TibiaProc)processCombo.SelectedValue;
            await _ksLogout.SendKeyMultiple(proc.Process, int.Parse(logout.Text), 1, 0, keys, logBox);
        }

        private void Logoutstopbutton_OnClick(object sender, RoutedEventArgs e)
        {
            _ksLogout.Stop = true;

            logBox.AppendText("\nLogout stopped.");
            logBox.ScrollToEnd();
        }

        private void loadProcessButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var processList = new List<TibiaProc>();
            var proc = Process.GetProcessesByName("client");
            foreach (var process in proc)
            {
                var p = new TibiaProc();
                p.Process = process;
                p.WindowTitle = process.MainWindowTitle;
                processList.Add(p);
            }
            processCombo.ItemsSource = processList;
            processCombo.SelectedIndex = 0;
        }

        private void ProcessCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
//                foreach (var heal in heals)
//                {
//                    heal.Process = ((TibiaProc)processCombo.SelectedItem).Process;
//
//
//                }
                //Heal1.Process = ((TibiaProc)processCombo.SelectedItem).Process;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //throw;
            }

            try
            {
                Window.Title = ((TibiaProc)processCombo.SelectedItem).WindowTitle.Replace("Tibia", "Bot");
                MemoryReader.Initialize();
                MemoryReader.OpenProcess(((TibiaProc)processCombo.SelectedItem).Process);
//                foreach (var heal in heals)
//                {
//                    //heal = new HealControl(((TibiaProc)processCombo.SelectedItem).Process);
//                }
                //Heal1 = new HealControl(((TibiaProc)processCombo.SelectedItem).Process);
            }
            catch (Exception ex)
            {
                var alert = new Alarm();
                alert.IsEnabled = true;
                alert.Play();

                MessageBoxResult result = MessageBox.Show($"{ex}", "Something happened", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.OK)
                {
                    alert.Stop();
                    alert.IsEnabled = false;
                }
                Console.WriteLine(ex);
                //throw;
            }
        }

        private async void RefreshHealthButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                hpTextblock.Text = $"{MemoryReader.health}/{MemoryReader.maxHealth} Health";
                mpTextblock.Text = $"{MemoryReader.mana}/{MemoryReader.maxMana} Mana";
                soulsTextblock.Text = $"{MemoryReader.souls} Souls";
                await Task.Delay(100);
            }
        }


        private void AlertToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            _alarm = new Alarm();
            _alarm.IsEnabled = true;
            bool isPercent = true;
            if (AlarmProcRadio.IsChecked == true)
            {
                isPercent = true;
            }
            else if (AlarmFlatRadio.IsChecked == true)
            {
                isPercent = false;
            }
            if (AlertHealth.IsChecked == true)
            {
                _alarm.Panic(int.Parse(AlertRes.Text), Alarm.ResourceType.Health, isPercent);
            }
            else if (AlertMana.IsChecked == true)
            {
                _alarm.Panic(int.Parse(AlertRes.Text), Alarm.ResourceType.Mana, isPercent);
            }
            else if (AlertSouls.IsChecked == true)
            {
                _alarm.Panic(int.Parse(AlertRes.Text), Alarm.ResourceType.Souls, isPercent);
            }
            
        }

        private void AlertToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _alarm.IsEnabled = false;
            _alarm.Stop();
        }

        private void AddHealButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var heal = new HealControl();
            HealControls.Add(heal);

            //heal.Process = ((TibiaProc)processCombo.SelectedItem).Process;
            icHeal.Children.Add(heal);
        }

        private void HealersToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            //var healers = HealControls.Select(healControl => healControl.Healer).ToList();
            foreach (var healControl in HealControls)
            {
                //healControl.Healers = healers;
                healControl.EnableHeals();

            }

            var healer = new Healer();
            healer.Process = ((TibiaProc) processCombo.SelectedItem).Process;
            healer.HealControls = HealControls;
            healer.HealPrio();
        }

        private void HealersToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            foreach (var healControl in HealControls)
            {
                healControl.DisableHeals();
            }
        }
    }
}
