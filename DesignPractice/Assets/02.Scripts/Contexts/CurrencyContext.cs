using System;
using System.IO;
using DP.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace DP.Contexts
{
    public class CurrencyContext
    {
        public CurrencyContext()
        {
            PATH_GOLD = Application.persistentDataPath + "/gold.json";
            PATH_GEM = Application.persistentDataPath + "/gem.json";
            LoadGold();
            LoadGem();
        }
        public Gold Gold { get; private set; }
        public Gem Gem { get; private set; }


        readonly string PATH_GOLD;
        readonly string PATH_GEM;

        public event Action<Gold> OnGoldChanged;
        public event Action<Gem> OnGemChanged;

        public void IncreaseGold(int amount)
        {
            if (amount == 0)
                return;
            else if (Gold.Value + amount < 0)
                throw new ArgumentOutOfRangeException();

            Gold = new Gold(Gold.Value + amount);
            SaveGold();
        }
        public void DecreaseGold(int amount)
        {
            if (amount == 0)
                return;
            else if (Gold.Value - amount < 0)
                throw new ArgumentOutOfRangeException();

            Gold = new Gold(Gold.Value - amount);
            SaveGold();
        }
        public void IncreaseGem(int amount)
        {
            if (amount == 0)
                return;
            else if (Gem.Value + amount < 0)
                throw new ArgumentOutOfRangeException();

            Gem = new Gem(Gem.Value + amount);
            SaveGem();
        }
        public void DecreaseGem(int amount)
        {
            if (amount == 0)
                return;
            else if (Gem.Value - amount < 0)
                throw new ArgumentOutOfRangeException();

            Gem = new Gem(Gem.Value - amount);
            SaveGem();
        }

        public void LoadGold()
        {
            if (File.Exists(PATH_GOLD))
            {
                string json = File.ReadAllText(PATH_GOLD);
                Gold = JsonConvert.DeserializeObject<Gold>(json);
            }
            else
            {
                Gold = new Gold();
                SaveGold();
            }
        }

        public void SaveGold()
        {
            string json = JsonConvert.SerializeObject(Gold);
            File.WriteAllText(PATH_GOLD, json);
            OnGoldChanged?.Invoke(Gold);
        }

        public void LoadGem()
        {
            if (File.Exists(PATH_GEM))
            {
                string json = File.ReadAllText(PATH_GEM);
                Gem = JsonConvert.DeserializeObject<Gem>(json);
            }
            else
            {
                Gem = new Gem();
                SaveGem();
            }
        }

        public void SaveGem()
        {
            string json = JsonConvert.SerializeObject(Gem);
            File.WriteAllText(PATH_GEM, json);
            OnGemChanged?.Invoke(Gem);
        }
    }
}