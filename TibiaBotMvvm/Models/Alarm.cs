using System;
using System.Media;
using System.Threading.Tasks;
using TibiaBotMvvm.Models.Memory;

namespace TibiaBotMvvm.Models
{
    public class Alarm
    {
        public int ResoruceAmount { get; set; }

        public enum ResourceType
        {
            Health,
            Mana,
            Souls
        }

        public bool IsEnabled { get; set; }


        public SoundPlayer Player { get; set; }
        public string Path { get; set; } = "alarm.wav";

        public async void Panic(int resourceAmount, ResourceType resourceType, bool isPercent)
        {
            try
            {
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
                            if (health < resourceAmount)
                            {
                                Play();
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
                            if (mana < resourceAmount)
                            {
                                Play();
                            }
                            break;
                        case ResourceType.Souls:
                            if (MemoryReader.souls <= resourceAmount)
                            {
                                Play();
                            }
                            break;
                    }
                    await Task.Delay(3000);
                }
                
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

        public void Play()
        {
            Player = new SoundPlayer(Path);
            Player.PlayLooping();
            if (IsEnabled == false)
            {
                Player.Stop();
            }
        }

        public void Stop()
        {
            try
            {
                Player.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
