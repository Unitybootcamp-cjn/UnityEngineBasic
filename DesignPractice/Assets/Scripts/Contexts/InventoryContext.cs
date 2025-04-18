using DP.Models;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace DP.Contexts
{
    /// <summary>
    /// 인벤토리 데이터를 로드 / 세이브 
    /// </summary>
    public class InventoryContext
    {
        public InventoryContext()
        {
            PATH = Application.persistentDataPath + "/Inventory.json";
            Load();
        }


        public Inventory Inventory { get; private set; }


        const int DEFAULT_SLOT_SIZE = 32;
        readonly string PATH;


        public void Load()
        {

            if (File.Exists(PATH))
            {

                string json = File.ReadAllText(PATH);
                Inventory = JsonConvert.DeserializeObject<Inventory>(json);
            }
            else
            {
                Inventory = new Inventory(DEFAULT_SLOT_SIZE);
                Save();
            }
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(Inventory);
            File.WriteAllText(PATH, json);
        }
    }
}