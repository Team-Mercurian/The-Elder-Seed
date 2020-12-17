using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class DataSystem : MonoBehaviour {

    //Establecer variables.

        //Privadas 
        private static DataSystem m_instance;

        private static MasterData m_masterData;
        private static GameData m_gameData;
            
        private static DungeonData m_dungeonData;

        //Publicas.
        [Header("Persistent Room Holder")]
        [SerializeField] private GameObject[] m_rooms = null;
        [SerializeField] private GameObject[] m_chestRooms = null;
        [SerializeField] private GameObject[] m_stairsRooms = null;

        [Header("Persistent Props Holder")]
        [SerializeField] private GameObject[] m_enemies = null;

        [Header("Persistent Items Holder")]
        [SerializeField] private Weapon m_defaultWeapon = null;
        [Space]
        [SerializeField] private List<Weapon> m_weapons = null;
        [SerializeField] private List<Seed> m_seeds = null;
        [SerializeField] private List<Plant> m_plants = null;
        [SerializeField] private List<Potion> m_potions = null;
        [SerializeField] private List<Item> m_miscellaneous = null;

        [Header("Values")]
        [SerializeField] private int m_playerHealth = 1200;

    //Funciones de MonoBehaviour.
    private void Awake() {

        if (m_instance == null) {

            m_instance = this;
            DontDestroyOnLoad(gameObject);

            #if UNITY_ANDROID && !UNITY_EDITOR
            Application.targetFrameRate = 60;
            #endif
            }

        else {

            Destroy(gameObject);
            return;
            }

        m_masterData = SaveSystem.Load();
        m_gameData = m_masterData.GetGameData(0);

        SetItemsDatas();

        SaveSystem.Save();
        }

    //Funciones estaticas.

    //Funciones publicas.

        //Getters and Setters

            //Data System
            public static DataSystem GetSingleton() => m_instance;

            //Temporal Data
            public void SetDungeonData(DungeonData dungeonData) => m_dungeonData = dungeonData;
            public DungeonData GetDungeonData() => m_dungeonData;

            //Game Data
            public GameData GetGameData() => m_gameData;    

            //Master Data
            public static MasterData GetMasterData() => m_masterData;

            //Prefab Data

                //Dungeon Room Prefabs
                public GameObject GetRoomPrefab(int index) => m_rooms[index];
                public int GetRandomRoomPrefabIndex() => Random.Range(0, m_rooms.Length);

                //Chest Rooms Prefabs
                public GameObject GetChestRoomPrefab(int index) => m_chestRooms[index];
                public int GetRandomChestRoomPrefabIndex() => Random.Range(0, m_chestRooms.Length);

                //Stairs Rooms Prefabs
                public GameObject GetStairsRoomPrefab(int index) => m_stairsRooms[index];
                public int GetRandomStairsRoomPrefabIndex() => Random.Range(0, m_stairsRooms.Length);

                //Enemies Prefabs
                public GameObject GetEnemyPrefab(int index) => m_enemies[index];
                public int GetRandomEnemyPrefabIndex() => Random.Range(0, m_enemies.Length);

                //Weapons
                public Weapon GetWeapon(int id) => m_weapons.Find(c => c.GetID() == id);   
                public List<Weapon> GetWeapons() => m_weapons;

                public Weapon GetActualWeapon() => m_weapons.Find(c => c.GetID() == m_dungeonData.GetActualWeapon().GetID());   

                //Seeds
                public Seed GetSeed(int id) => m_seeds.Find(c => c.GetID() == id);   
                public List<Seed> GetSeeds() => m_seeds;    

                //Potions 
                public Plant GetPlant(int id) => m_plants.Find(c => c.GetID() == id);
                public List<Plant> GetPlants() => m_plants;  

                //Potions 
                public Potion GetPotion(int id) => m_potions.Find(c => c.GetID() == id);   
                public List<Potion> GetPotions() => m_potions;   

                //Miscelaneous 
                public Item GetMiscellaneous(int id) => m_miscellaneous.Find(c => c.GetID() == id);   
                public List<Item> GetMiscellaneous() => m_miscellaneous;  

            //Player
            public int GetPlayerHealth() => m_playerHealth;

        //Functions
        public int GetRandomWeaponID(int probabilityIncrement) => GetRandomItemsIndex(probabilityIncrement, m_weapons.Cast<Item>().ToList());
        public int GetRandomSeedID(int probabilityIncrement, Seed.SeedType seedType) => GetRandomItemsIndex(probabilityIncrement, m_seeds.FindAll(s => s.GetSeedType() == seedType).Cast<Item>().ToList());
        
        private int GetRandomItemsIndex(int probabilityIncrement, List<Item> items) {

            probabilityIncrement = Mathf.Clamp(probabilityIncrement, 0, 16);

            List<Item> m_rarityList = new List<Item>();
            int m_probability = Mathf.Clamp(Random.Range(0, 100) + probabilityIncrement, 0, 100);

            if (m_probability < 70) {

                for(int i = 0; i < items.Count; i ++) {
                    
                    if (items[i].GetRarity() == Rarity.Common) m_rarityList.Add(items[i]); 
                    }
                }

            else if (m_probability < 85) {

                for(int i = 0; i < items.Count; i ++) {
                    
                    if (items[i].GetRarity() == Rarity.Rare) m_rarityList.Add(items[i]); 
                    }
                }
                
            else if (m_probability < 95) {
                
                for(int i = 0; i < items.Count; i ++) {
                    
                    if (items[i].GetRarity() == Rarity.Epic) m_rarityList.Add(items[i]); 
                    }
                }

            else {
                
                for(int i = 0; i < items.Count; i ++) {

                    if (items[i].GetRarity() == Rarity.Legendary) m_rarityList.Add(items[i]); 
                    }
                }
            
            if (m_rarityList.Count > 0) return m_rarityList[Random.Range(0, m_rarityList.Count)].GetID();
            else {
                
                Debug.Log("No existe un item dentro de esta categoria.");
                return -1;
                }
            }

        public void GoToRuins() {

            SaveSystem.Save();
            SceneController.GetSingleton().LoadScene(Scenes.Ruins, false);
            }

        private void SetItemsDatas() {

            #region Weapons
            
            List<WeaponBaseData> m_weaponBaseData = m_gameData.GetWeaponBaseData();

            if (m_weaponBaseData.Count == 0) m_gameData.GetWeaponBaseData().Add(new WeaponBaseData(m_defaultWeapon.GetID(), m_defaultWeapon.GetDefaultUnlocked())); 
            if (m_weaponBaseData.Count < m_weapons.Count + 1) {

                foreach(Weapon m_w in m_weapons) {
                    
                    if (!m_gameData.GetIfIDExists(m_w.GetID())) {

                        WeaponBaseData m_bd = new WeaponBaseData(m_w.GetID(), m_w.GetDefaultUnlocked());
                        m_gameData.AddWeaponBaseData(m_bd);
                        }
                    }
                }

            if (m_gameData.GiftData()) {
                
                for(int i = 0; i < m_weapons.Count; i ++) {
                    
                    for(int c = 0; c < m_weapons[i].GetDefaultCount(); c ++) { 
                    
                        m_gameData.GetInventoryData().AddWeapon(m_weapons[i].GetID(), m_weapons[i].GetUses(), ref m_gameData.GetInventoryData().m_lastWeaponID);
                        }   
                    }
                }

            #endregion
            #region Items

                List<ItemData> SetItemData(List<ItemData> savedData, List<Item> items) {

                    if (savedData.Count == 0) {

                        List<ItemData> m_newList = new List<ItemData>();

                        for(int i = 0; i < items.Count; i ++) {

                            int m_count = items[i].GetDefaultCount();
                            ItemData m_iD = new ItemData(i, m_count, m_count > 0 || items[i].GetDefaultUnlocked());
                            m_newList.Add(m_iD);
                            }

                        return m_newList;
                        }
                    
                    return savedData;
                    }

                //Variables
                InventoryData m_iData = m_gameData.GetInventoryData();
                List<ItemData> m_dataList = null;

                //Seeds
                m_dataList = SetItemData(m_iData.GetSeedList(), m_seeds.Cast<Item>().ToList());
                m_iData.SetSeedList(m_dataList);

                //Potions
                m_dataList = SetItemData(m_iData.GetPotionList(), m_potions.Cast<Item>().ToList());
                m_iData.SetPotionList(m_dataList);

                //Plants
                m_dataList = SetItemData(m_iData.GetPlantList(), m_plants.Cast<Item>().ToList());
                m_iData.SetPlantList(m_dataList);

                //Miscelaneous
                m_dataList = SetItemData(m_iData.GetMiscellaneousList(), m_miscellaneous);
                m_iData.SetMiscellaneousList(m_dataList);
            
            #endregion
            
            m_gameData.TurnGiftOff();
            }
        public InventoryData GetNewDungeonInventoryData(bool giftData) {

            InventoryData m_inventory = new InventoryData();

            List<ItemData> SetItemData(List<ItemData> gameData, List<ItemData> savedData, List<Item> items) {

                if (savedData.Count == 0) {

                    List<ItemData> m_newList = new List<ItemData>();

                    for(int i = 0; i < items.Count; i ++) {

                        int m_count = 5;
                        ItemData m_iD = new ItemData(i, giftData ? m_count : 0, (giftData ? m_count > 0 : false) || gameData.Find(c => c.GetID() == i).GetUnlocked());
                        m_newList.Add(m_iD);
                        }

                    return m_newList;
                    }
                
                return savedData;
                }
            List<ItemData> m_dataList = null;
            InventoryData m_gameInventory = DataSystem.GetSingleton().GetGameData().GetInventoryData();

            //Weapons
            if (giftData) {
                
                for(int i = 0; i < m_weapons.Count; i ++) {
                    
                    for(int c = 0; c < 5; c ++) { 
                    
                        m_inventory.AddWeapon(m_weapons[i].GetID(), m_weapons[i].GetUses(), ref m_gameData.GetInventoryData().m_lastWeaponID);
                        }   
                    }
                }

            //Seeds
            m_dataList = SetItemData(m_gameInventory.GetSeedList(), m_inventory.GetSeedList(), m_seeds.Cast<Item>().ToList());
            m_inventory.SetSeedList(m_dataList);

            //Potions
            m_dataList = SetItemData(m_gameInventory.GetPotionList(), m_inventory.GetPotionList(), m_potions.Cast<Item>().ToList());
            m_inventory.SetPotionList(m_dataList);

            //Plants
            m_dataList = SetItemData(m_gameInventory.GetPlantList(), m_inventory.GetPlantList(), m_plants.Cast<Item>().ToList());
            m_inventory.SetPlantList(m_dataList);

            return m_inventory;
            }
        }
