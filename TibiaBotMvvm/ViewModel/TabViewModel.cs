using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using TibiaBotMvvm.Models;

namespace TibiaBotMvvm.ViewModel
{
    public class TabViewModel : ViewModelBase
    {
        public TabViewModel()
        {
            KeyList = Keys.Select(p => new Key {Name = p.Key, Value = p.Value}).ToList();

            KeyList = new List<Key>();
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                RaisePropertyChanged(() => Header);
            }
        }

        #region keys

        public Dictionary<string, int> Keys { get; } = new Dictionary<string, int>()
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

        public List<Key> KeyList { get; set; }

        #endregion

    }
}
