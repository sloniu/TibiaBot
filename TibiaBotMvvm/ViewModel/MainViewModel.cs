using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TibiaBotMvvm.Models;
using System.Diagnostics;
using GalaSoft.MvvmLight.Messaging;
using TibiaBotMvvm.Models.Memory;

namespace TibiaBotMvvm.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Views.Add(new ManaSitterViewModel());
            Views.Add(new HealingViewModel());
            Views.Add(new AlarmViewModel());

            LoadProcessesCommand = new RelayCommand(LoadProcesses);
            LoadProcesses();
            MemoryReader.Initialize();
        }

        

        #region Views

        private ObservableCollection<TabViewModel> _views = new ObservableCollection<TabViewModel>();

        public ObservableCollection<TabViewModel> Views
        {
            get { return _views; }
            set { _views = value; }
        }

        #endregion

        #region TibiaProcesses

        private List<TibiaProc> _tibiaProcs;

        public List<TibiaProc> TibiaProcs
        {
            get { return _tibiaProcs; }
            set
            {
                _tibiaProcs = value;
                RaisePropertyChanged(() => TibiaProcs);
            }
        }

        private TibiaProc _selectedTibiaProc;

        public TibiaProc SelectedTibiaProc
        {
            get { return _selectedTibiaProc; }
            set
            {
                _selectedTibiaProc = value;
                Messenger.Default.Send(SelectedTibiaProc);
                if (SelectedTibiaProc != null)
                {
                    MemoryReader.OpenProcess(SelectedTibiaProc.Process);

                }
                RaisePropertyChanged(() => SelectedTibiaProc);
            }
        }

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                if (value != _selectedTabIndex)
                {
                    _selectedTabIndex = value;
                    RaisePropertyChanged("SelectedTabIndex");
                }
            }
        }

        public RelayCommand LoadProcessesCommand { get; set; }
        private void LoadProcesses()
        {
            var list = new List<TibiaProc>();
            var proc = Process.GetProcessesByName("client");
            foreach (var process in proc)
            {
                var p = new TibiaProc();
                p.Process = process;
                p.WindowTitle = process.MainWindowTitle;
                list.Add(p);
            }
            TibiaProcs = list;
        }

        #endregion

    }
}