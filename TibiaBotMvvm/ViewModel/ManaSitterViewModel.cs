using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using TibiaBotMvvm.Models;
using TibiaBotMvvm.Models.Memory;

namespace TibiaBotMvvm.ViewModel
{
    public class ManaSitterViewModel : TabViewModel
    {
        public ManaSitterViewModel()
        {
            Header = "Mana Sitter";
            Messenger.Default.Register<TibiaProc>(this, (message) =>
            {
                TibiaProcess = message;
            });

            ManaburnCommand = new RelayCommand(Manaburn);
            EatingCommand = new RelayCommand(Eating);
            SkillingCommand = new RelayCommand(Skilling);
            LogoutCommand = new RelayCommand(Logout);
        }

        private TibiaProc _tibiaProcess;

        public TibiaProc TibiaProcess
        {
            get { return _tibiaProcess; }
            set
            {
                _tibiaProcess = value;
                RaisePropertyChanged(() => TibiaProcess);
            }
        }

        #region Manaburn

        private int _requiredMana;

        public int RequiredMana
        {
            get { return _requiredMana; }
            set
            {
                _requiredMana = value;
                RaisePropertyChanged(() => RequiredMana);
            }
        }

        private int _manaburnKey;

        public int ManaburnKey
        {
            get { return _manaburnKey; }
            set
            {
                _manaburnKey = value;
                RaisePropertyChanged(() => ManaburnKey);
            }
        }


        private int _manaburnKeyIndex;

        public int ManaburnKeyIndex
        {
            get { return _manaburnKeyIndex; }
            set
            {
                _manaburnKeyIndex = value;
                RaisePropertyChanged(() => ManaburnKeyIndex);
            }
        }

        private bool _isManaburnEnabled;
        public bool IsManaburnEnabled
        {
            get { return _isManaburnEnabled; }
            set
            {
                _isManaburnEnabled = value;
                RaisePropertyChanged(() => IsManaburnEnabled);
            }
        }

        private KeySender _manaburnKeysender;
        private bool _manaburnIsRunning = false;
        public RelayCommand ManaburnCommand { get; set; }
        private async void Manaburn()
        {
            if (_manaburnIsRunning)
            {
                _manaburnIsRunning = false;
                return;
            }
            _manaburnKeysender = new KeySender();
            _manaburnIsRunning = true;
            while (_manaburnIsRunning)
            {
                if (MemoryReader.mana >= RequiredMana)
                {
                    await _manaburnKeysender.SendKey(TibiaProcess.Process, ManaburnKey, 1);
                }
                await Task.Delay(250);
            }
        }

        #endregion

        #region Eating

        private int _eatingTime;
        public int EatingTime
        {
            get { return _eatingTime; }
            set
            {
                _eatingTime = value;
                RaisePropertyChanged(() => EatingTime);
            }
        }

        private int _eatingKey;
        public int EatingKey
        {
            get { return _eatingKey; }
            set
            {
                _eatingKey = value; 
                RaisePropertyChanged(() => EatingKey);
            }
        }


        private int _eatingKeyIndex;
        public int EatingKeyIndex
        {
            get { return _eatingKeyIndex; }
            set
            {
                _eatingKeyIndex = value;
                RaisePropertyChanged(() => EatingKeyIndex);
            }
        }

        private bool _isEatingEnabled;
        public bool IsEatingEnabled
        {
            get { return _isEatingEnabled; }
            set
            {
                _isEatingEnabled = value;
                RaisePropertyChanged(() => IsEatingEnabled);
            }
        }

        private KeySender _eatingKeysender;
        private bool _eatingIsRunning = false;
        public RelayCommand EatingCommand { get; set; }
        private async void Eating()
        {
            if (_eatingIsRunning)
            {
                _eatingIsRunning = false;
                _eatingKeysender.Stop = true;
                return;
            }
            _eatingKeysender = new KeySender();
            _eatingIsRunning = true;
            while (_eatingIsRunning)
            {
                await _eatingKeysender.SendKey(TibiaProcess.Process, EatingKey, 1);
                await Task.Delay(EatingTime * 1000 + new Random().Next(10, 200));
            }
        }

        #endregion

        #region Skilling

        private int _skillingTime;
        public int SkillingTime
        {
            get { return _skillingTime; }
            set
            {
                _skillingTime = value;
                RaisePropertyChanged(() => SkillingTime);
            }
        }

        private int _skillingKey;
        public int SkillingKey
        {
            get { return _skillingKey; }
            set
            {
                _skillingKey = value;
                RaisePropertyChanged(() => SkillingKey);
            }
        }

        private int _skillingKeyIndex;
        public int SkillingKeyIndex
        {
            get { return _skillingKeyIndex; }
            set
            {
                _skillingKeyIndex = value;
                RaisePropertyChanged(() => SkillingKeyIndex);
            }
        }

        private bool _isSkillingEnabled;
        public bool IsSkillingEnabled
        {
            get { return _isSkillingEnabled; }
            set
            {
                _isSkillingEnabled = value;
                RaisePropertyChanged(() => IsSkillingEnabled);
            }
        }

        private KeySender _skillingKeysender;
        private bool _skillingIsRunning = false;
        public RelayCommand SkillingCommand { get; set; }
        private async void Skilling()
        {
            if (_skillingIsRunning)
            {
                _skillingIsRunning = false;
                _skillingKeysender.Stop = true;
                return;
            }
            _skillingKeysender = new KeySender();
            _skillingIsRunning = true;
            while (_skillingIsRunning)
            {
                await _skillingKeysender.SendKey(TibiaProcess.Process, SkillingKey, 2);
                await Task.Delay(SkillingTime * 1000 + new Random().Next(10, 200));
            }
        }

        #endregion

        #region Logout

        private int _logoutTime;
        public int LogoutTime
        {
            get { return _logoutTime; }
            set
            {
                _logoutTime = value;
                RaisePropertyChanged(() => LogoutTime);
            }
        }

        private int _logoutKey;
        public int LogoutKey
        {
            get { return _logoutKey; }
            set
            {
                _logoutKey = value;
                RaisePropertyChanged(() => LogoutKey);
            }
        }

        private int _logoutKeyIndex;
        public int LogoutKeyIndex
        {
            get { return _logoutKeyIndex; }
            set
            {
                _logoutKeyIndex = value;
                RaisePropertyChanged(() => LogoutKeyIndex);
            }
        }

        private bool _isLogoutTimeChecked;
        public bool IsLogoutTimeChecked
        {
            get { return _isLogoutTimeChecked; }
            set
            {
                _isLogoutTimeChecked = value;
                RaisePropertyChanged(() => IsLogoutEnabled);
            }
        }

        private bool _isLogoutSoulsChecked;
        public bool IsLogoutSoulsChecked
        {
            get { return _isLogoutSoulsChecked; }
            set
            {
                _isLogoutSoulsChecked = value;
                RaisePropertyChanged(() => IsLogoutSoulsChecked);
            }
        }

        private bool _isLogoutNothingChecked;
        public bool IsLogoutNothingChecked
        {
            get { return _isLogoutNothingChecked; }
            set
            {
                _isLogoutNothingChecked = value;
                RaisePropertyChanged(() => IsLogoutNothingChecked);
            }
        }

        private bool _isLogoutShutdownChecked;
        public bool IsLogoutShutdownChecked
        {
            get { return _isLogoutShutdownChecked; }
            set
            {
                _isLogoutShutdownChecked = value;
                RaisePropertyChanged(() => IsLogoutShutdownChecked);
            }
        }

        private bool _isLogoutHibernateChecked;
        public bool IsLogoutHibernateChecked
        {
            get { return _isLogoutHibernateChecked; }
            set
            {
                _isLogoutHibernateChecked = value;
                RaisePropertyChanged(() => IsLogoutHibernateChecked);
            }
        }

        private bool _isLogoutEnabled;
        public bool IsLogoutEnabled
        {
            get { return _isLogoutEnabled; }
            set
            {
                _isLogoutEnabled = value;
                RaisePropertyChanged(() => IsLogoutEnabled);
            }
        }

        private KeySender _logoutKeysender;
        private bool _logoutIsRunning = false;
        public RelayCommand LogoutCommand { get; set; }
        private async void Logout()
        {
            if (_logoutIsRunning)
            {
                _logoutIsRunning = false;
                _logoutKeysender.Stop = true;
                return;
            }
            _logoutKeysender = new KeySender();
            _logoutIsRunning = true;
            while (_logoutIsRunning)
            {
                if (IsLogoutTimeChecked)
                {
                    await Task.Delay(LogoutTime * 1000 + new Random().Next(10, 200));
                    await _logoutKeysender.SendKey(TibiaProcess.Process, LogoutKey, 1);

                    if (_isLogoutHibernateChecked)
                    {
                        var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                        psi.CreateNoWindow = true;
                        psi.UseShellExecute = false;
                        Process.Start(psi);
                    }
                    if (IsLogoutHibernateChecked)
                    {
                        var psi = new ProcessStartInfo("shutdown", "/h /t 0");
                        psi.CreateNoWindow = true;
                        psi.UseShellExecute = false;
                        Process.Start(psi);
                    }

                }
                if (IsLogoutSoulsChecked)
                {
                    if (MemoryReader.souls >= LogoutTime)
                    {
                        await Task.Delay(new Random().Next(10, 200));
                        await _logoutKeysender.SendKey(TibiaProcess.Process, LogoutKey, 1);

                        if (_isLogoutHibernateChecked)
                        {
                            var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                            psi.CreateNoWindow = true;
                            psi.UseShellExecute = false;
                            Process.Start(psi);
                        }
                        if (IsLogoutHibernateChecked)
                        {
                            var psi = new ProcessStartInfo("shutdown", "/h /t 0");
                            psi.CreateNoWindow = true;
                            psi.UseShellExecute = false;
                            Process.Start(psi);
                        }
                    }
                }

                

            }
        }

        #endregion

    }
}
