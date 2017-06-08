using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TibiaBotMvvm.Models;


namespace TibiaBotMvvm.ViewModel
{
    public class AlarmViewModel : TabViewModel
    {
        public AlarmViewModel()
        {
            Header = "Alarm";
            AlarmCommand = new RelayCommand(Alarm);
        }

        #region Alarm

        private int _alarmResource;

        public int AlarmResource
        {
            get { return _alarmResource; }
            set
            {
                _alarmResource = value;
                RaisePropertyChanged(() => AlarmResource);
            }
        }

        private bool _alarmFlatIsChecked;

        public bool AlarmFlatIsChecked
        {
            get { return _alarmFlatIsChecked; }
            set
            {
                _alarmFlatIsChecked = value;
                RaisePropertyChanged(() => AlarmFlatIsChecked);
            }
        }

        private bool _alarmProcIsChecked;

        public bool AlarmProcIsChecked
        {
            get { return _alarmProcIsChecked; }
            set
            {
                _alarmProcIsChecked = value;
                RaisePropertyChanged(() => AlarmProcIsChecked);
            }
        }

        private bool _alarmHealthIsChecked;

        public bool AlarmHealthIsChecked
        {
            get { return _alarmHealthIsChecked; }
            set
            {
                _alarmHealthIsChecked = value;
                RaisePropertyChanged(() => AlarmHealthIsChecked);
            }
        }

        private bool _alarmManaIsChecked;

        public bool AlarmManaIsChecked
        {
            get { return _alarmManaIsChecked; }
            set
            {
                _alarmManaIsChecked = value;
                RaisePropertyChanged(() => AlarmManaIsChecked);
            }
        }

        private bool _alarmSoulsIsChecked;

        public bool AlarmSoulsIsChecked
        {
            get { return _alarmSoulsIsChecked; }
            set
            {
                _alarmSoulsIsChecked = value;
                RaisePropertyChanged(() => AlarmSoulsIsChecked);
            }
        }

        private bool _alarmIsEnabled;

        public bool AlarmIsEnabled
        {
            get { return _alarmIsEnabled; }
            set
            {
                _alarmIsEnabled = value;
                RaisePropertyChanged(() => AlarmIsEnabled);
            }
        }

        private Alarm _alarm;
        public RelayCommand AlarmCommand { get; set; }
        private void Alarm()
        {
            if (_alarm != null && _alarm.IsEnabled)
            {
                _alarm.IsEnabled = false;
                _alarm.Stop();
                return;
            }

            _alarm = new Alarm();
            _alarm.IsEnabled = true;

            if (AlarmHealthIsChecked)
            {
                _alarm.Panic(AlarmResource, Models.Alarm.ResourceType.Health, AlarmProcIsChecked);
            }
            else if (AlarmManaIsChecked)
            {
                _alarm.Panic(AlarmResource, Models.Alarm.ResourceType.Mana, AlarmProcIsChecked);
            }
            else if (AlarmSoulsIsChecked)
            {
                _alarm.Panic(AlarmResource, Models.Alarm.ResourceType.Souls, AlarmProcIsChecked);
            }
        }

        #endregion





    }
}
