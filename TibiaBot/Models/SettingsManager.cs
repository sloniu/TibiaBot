using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TibiaBot.Controls;

namespace TibiaBot.Models
{
    public class SettingsManager
    {
        private static volatile SettingsManager instance;
        private static object _syncRoot = new object();

        private SettingsManager()
        {
            HealerSettings = new List<HealerSetting>();
        }

        public static SettingsManager Instance
        {
            get
            {
                if (instance != null) return instance;
                lock (_syncRoot)
                {
                    if (instance == null)
                        instance = new SettingsManager();
                }
                return instance;
            }
        }

        public string ManaSittingSpellMana { get; set; }
        public int ManaSittingSpellKey { get; set; }
        public string ManaSittingFoodTime { get; set; }
        public int ManaSittingFoodKey { get; set; }
        public string ManaSittingSkillTime { get; set; }
        public string ManaSittingSkillKey { get; set; }
        public string ManaSittingLogoutTime { get; set; }
        public string AlarmAmount { get; set; }
        public bool AlarmIsPercent { get; set; }
        public bool AlarmIsFlat { get; set; }
        public bool AlarmIsHealth { get; set; }
        public bool AlarmIsMana { get; set; }
        public bool AlarmIsSouls { get; set; }
        public string ManaSittingLogoutKey { get; set; }
        public List<HealerSetting> HealerSettings { get; set; }




        public void ApplyCustomSettings()
        {
            Properties.Settings.Default.ManaSittingSpellMana = ManaSittingSpellMana;
            Properties.Settings.Default.ManaSittingSpellKey = ManaSittingSpellKey;
            Properties.Settings.Default.ManaSittingFoodTime = ManaSittingFoodTime;
            Properties.Settings.Default.ManaSittingFoodKey = ManaSittingFoodKey;
            Properties.Settings.Default.ManaSittingSkillTime = ManaSittingSkillTime;
            Properties.Settings.Default.ManaSittingSkillKey = ManaSittingSkillKey;
            Properties.Settings.Default.ManaSittingLogoutTime = ManaSittingLogoutTime;
            Properties.Settings.Default.AlarmAmount = AlarmAmount;
            Properties.Settings.Default.AlarmIsPercent = AlarmIsPercent;
            Properties.Settings.Default.AlarmIsFlat = AlarmIsFlat;
            Properties.Settings.Default.AlarmIsHealth = AlarmIsHealth;
            Properties.Settings.Default.AlarmIsMana = AlarmIsMana;
            Properties.Settings.Default.AlarmIsSouls = AlarmIsSouls;
            Properties.Settings.Default.ManaSittingLogoutKey = ManaSittingLogoutKey;
        }

        public void LoadCurrentSettings()
        {
            ManaSittingSpellMana= Properties.Settings.Default.ManaSittingSpellMana;
            ManaSittingSpellKey = Properties.Settings.Default.ManaSittingSpellKey;
            ManaSittingFoodTime = Properties.Settings.Default.ManaSittingFoodTime;
            ManaSittingFoodKey = Properties.Settings.Default.ManaSittingFoodKey;
            ManaSittingSkillTime = Properties.Settings.Default.ManaSittingSkillTime;
            ManaSittingSkillKey = Properties.Settings.Default.ManaSittingSkillKey;
            ManaSittingLogoutTime = Properties.Settings.Default.ManaSittingLogoutTime;
            AlarmAmount = Properties.Settings.Default.AlarmAmount;
            AlarmIsPercent = Properties.Settings.Default.AlarmIsPercent;
            AlarmIsFlat = Properties.Settings.Default.AlarmIsFlat;
            AlarmIsHealth = Properties.Settings.Default.AlarmIsHealth;
            AlarmIsMana = Properties.Settings.Default.AlarmIsMana;
            AlarmIsSouls = Properties.Settings.Default.AlarmIsSouls;
            ManaSittingLogoutKey = Properties.Settings.Default.ManaSittingLogoutKey;
        }

        public void Serialize(string path, SettingsManager settings)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(SettingsManager));
                var writer = new StreamWriter(path);
                serializer.Serialize(writer, settings);
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Deserialize(string path, ref SettingsManager settings)
        {
            try
            {
                var mySerializer = new XmlSerializer(typeof(SettingsManager));
                var myFileStream = new FileStream(path, FileMode.Open);
                settings = (SettingsManager)mySerializer.Deserialize(myFileStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }
    }

    public class HealerSetting
    {
        public string From { get; set; }
        public string To { get; set; }
        public string RequiredMana { get; set; }
        public Healer.ResourceType Type { get; set; }
        //        public bool IsHealth { get; set; }
        //        public bool IsMana { get; set; }
        //        public bool IsFlat { get; set; }
        public bool IsPercent { get; set; }
        public int Key { get; set; }
        public int Priority { get; set; }

        public HealerSetting()
        {
            
        }

        public HealerSetting(HealControl heal)
        {
            From = heal.Healresfrom.Text;
            To = heal.Healresto.Text;
            RequiredMana = heal.Healreqmana.Text;
            //Type = heal.ResourceType;
            if (heal.ProcRadio.IsChecked == true)
            {
                IsPercent = true;
            }
            else if (heal.FlatRadio.IsChecked == true)
            {
                IsPercent = false;
            }

            if (heal.HealthRadio.IsChecked == true)
            {
                Type = Healer.ResourceType.Health;
            }
            else if (heal.ManaRadio.IsChecked == true)
            {
                Type = Healer.ResourceType.Mana;
            }


            Key = int.Parse(heal.Keysres.SelectedValue.ToString());
            Priority = int.Parse(heal.slValue.Value.ToString());


            
        }


    }
}
