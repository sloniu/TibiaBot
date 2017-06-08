using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TibiaBotMvvm.Models;

namespace TibiaBotMvvm.ViewModel
{
    public class HealingViewModel : TabViewModel
    {
        public HealingViewModel()
        {
            Header = "Healing";
            AddHealCommand = new RelayCommand(AddHeal);
            var heal = new Heal();
            heal.ResourceFrom = 1;
            heal.ResourceTo = 10;
            //heal.Key = 7;
            heal.Priority = 10;
            heal.Resource = Heal.MyResource.Health;
            heal.Percent = Heal.MyPercent.Percent;
            Heals = new ObservableCollection<Heal>();
            Heals.Add(heal);

        }

        public RelayCommand AddHealCommand { get; set; }

        private void AddHeal()
        {
            
        }

        public ObservableCollection<Heal> Heals { get; set; }

        public List<string> List { get; set; }
    }
}
