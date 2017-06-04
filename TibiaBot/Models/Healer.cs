using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TibiaBot.Controls;
using TibiaBot.Memory;

namespace TibiaBot.Models
{
    public class Healer
    {
        // press hotkey when health / mana is at X %
        public int ResoruceAmount { get; set; }

        public enum ResourceType
        {
            Health,
            Mana
        }

        public bool IsEnabled { get; set; } = true;
        public Process Process { get; set; }
        public List<HealControl> HealControls { get; set; }


        public async void HealPrio()
        {
            var sortedHealControls = HealControls.OrderByDescending(h => h.slValue.Value);

            try
            {
                var ks = new KeySender();
                var exitloop = false;
                while (IsEnabled)
                {
                    foreach (var sortedHealControl in sortedHealControls)
                    {
                        switch (sortedHealControl.ResourceType)
                        {
                            case ResourceType.Health:
                                int health;
                                if (sortedHealControl.IsPercent)
                                {
                                    health = 100 * MemoryReader.health / MemoryReader.maxHealth;
                                }
                                else
                                {
                                    health = MemoryReader.health;
                                }
                                if (health <
                                    new Random().Next(sortedHealControl.HealResFrom, sortedHealControl.HealResTo) &&
                                    sortedHealControl.RequiredMana <= MemoryReader.mana)
                                {
                                    await ks.SendKey(Process, sortedHealControl.Key);
                                    await Task.Delay(200);
                                    Console.WriteLine(
                                        $@"Healing in range of {sortedHealControl.HealResFrom} - {sortedHealControl
                                            .HealResTo} for {sortedHealControl.RequiredMana} mana.");
                                    exitloop = true;
                                }
                                else
                                {
                                    exitloop = false;

                                }
                                break;
                            case ResourceType.Mana:
                                int mana;
                                if (sortedHealControl.IsPercent)
                                {
                                    mana = 100 * MemoryReader.mana / MemoryReader.maxMana;
                                }
                                else
                                {
                                    mana = MemoryReader.mana;
                                }
                                if (mana < new Random().Next(sortedHealControl.HealResFrom, sortedHealControl.HealResTo))
                                {
                                    await ks.SendKey(Process, sortedHealControl.Key);
                                    await Task.Delay(200);
                                    Console.WriteLine($@"Manadrinking in range of {sortedHealControl.HealResFrom} - {sortedHealControl.HealResTo}");

                                    exitloop = true;
                                }
                                else
                                {
                                    exitloop = false;

                                }
                                break;
                        }
                        await Task.Delay(200);
                        if (exitloop)
                        {
                            break;
                        }
                    }
                    
                }

                ks.Stop = true;
            }
            catch (Exception e)
            {
                var alert = new Alarm();
                alert.IsEnabled = true;
                alert.Play();

                MessageBoxResult result = MessageBox.Show($"{e}", "Something happened", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.OK)
                {
                    alert.Stop();
                    alert.IsEnabled = false;
                }
                Console.WriteLine(e);
                
                
            }
        }

//        public async void Heal(ResourceType resourceType, int resourceMin, int resourceMax, int key, bool isPercent, int priority)
//        {
//            var sortedHealControls = HealControls.OrderByDescending(h => h.slValue.Value);
//
//            try
//            {
//                var ks = new KeySender();
//                while (IsEnabled)
//                {
//                    switch (resourceType)
//                    {
//                        case ResourceType.Health:
//                            int health;
//                            if (isPercent)
//                            {
//                                health = 100 * MemoryReader.health / MemoryReader.maxHealth;
//                            }
//                            else
//                            {
//                                health = MemoryReader.health;
//                            }
//                            if (health < new Random().Next(resourceMin, resourceMax) )
//                            {
//                                await ks.SendKey(Process, key);
//                            }
//                            break;
//                        case ResourceType.Mana:
//                            int mana;
//                            if (isPercent)
//                            {
//                                mana = 100 * MemoryReader.mana / MemoryReader.maxMana;
//                            }
//                            else
//                            {
//                                mana = MemoryReader.mana;
//                            }
//                            if (mana < resourceMin)
//                            {
//                                await ks.SendKey(Process, key);
//                            }
//                            break;
//                    }
//                    await Task.Delay(200);
//                }
//
//                ks.Stop = true;
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                //throw;
//                var alert = new Alarm();
//                alert.IsEnabled = true;
//                alert.Play();
//            }
//            
//
//        }
//
//        public async void Heal(ResourceType resourceType, int resourceMin, int resourceMax, int requiredMana, int key, bool isPercent, int priority)
//        {
//            try
//            {
//                var ks = new KeySender();
//                while (IsEnabled)
//                {
//                    if (resourceType == ResourceType.Health)
//                    {
//                        int health;
//                        if (isPercent == true)
//                        {
//                            health = 100 * MemoryReader.health / MemoryReader.maxHealth;
//                        }
//                        else
//                        {
//                            health = MemoryReader.health;
//                        }
//                        if (health < new Random().Next(resourceMin, resourceMax) && requiredMana <= MemoryReader.mana)
//                        {
//                            await ks.SendKey(Process, key);
//                        }
//                    }
//                    await Task.Delay(100);
//                }
//
//                ks.Stop = true;
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                //throw;
//                var alert = new Alarm();
//                alert.IsEnabled = true;
//                alert.Play();
//            }
//            
//
//        }

    }
}
