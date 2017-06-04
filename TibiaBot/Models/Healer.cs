using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async void Heal(ResourceType resourceType, int resourceMin, int resourceMax, int key, bool isPercent)
        {
            try
            {
                var ks = new KeySender();
                while (IsEnabled)
                {
                    switch (resourceType)
                    {
                        case ResourceType.Health:
                            int health;
                            if (isPercent)
                            {
                                health = 100 * MemoryReader.health / MemoryReader.maxHealth;
                            }
                            else
                            {
                                health = MemoryReader.health;
                            }
                            if (health < new Random().Next(resourceMax, resourceMax))
                            {
                                await ks.SendKey(Process, key);
                            }
                            break;
                        case ResourceType.Mana:
                            int mana;
                            if (isPercent)
                            {
                                mana = 100 * MemoryReader.mana / MemoryReader.maxMana;
                            }
                            else
                            {
                                mana = MemoryReader.mana;
                            }
                            if (mana < resourceMin)
                            {
                                await ks.SendKey(Process, key);
                            }
                            break;
                    }
                    await Task.Delay(200);
                }

                ks.Stop = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                var alert = new Alarm();
                alert.IsEnabled = true;
                alert.Play();
            }
            

        }

        public async void Heal(ResourceType resourceType, int resourceMin, int resourceMax, int requiredMana, int key, bool isPercent)
        {
            try
            {
                var ks = new KeySender();
                while (IsEnabled)
                {
                    if (resourceType == ResourceType.Health)
                    {
                        int health;
                        if (isPercent == true)
                        {
                            health = 100 * MemoryReader.health / MemoryReader.maxHealth;
                        }
                        else
                        {
                            health = MemoryReader.health;
                        }
                        if (health < new Random().Next(resourceMax, resourceMax) && requiredMana <= MemoryReader.mana)
                        {
                            await ks.SendKey(Process, key);
                        }
                    }
                    await Task.Delay(100);
                }

                ks.Stop = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                var alert = new Alarm();
                alert.IsEnabled = true;
                alert.Play();
            }
            

        }

    }
}
