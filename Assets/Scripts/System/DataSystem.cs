using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        [SerializeField] private GameObject[] m_enemies = null;
        [SerializeField] private Weapon[] m_weapons = null;
        
        [SerializeField] private Seed[] m_seedTypes = null;

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

        m_masterData = Load();
        m_gameData = m_masterData.GetGameData(0);
        }

    //Funciones estaticas.
    public static void Save() {

        string m_rawData = JsonUtility.ToJson(m_masterData, true);
        File.WriteAllText(Application.persistentDataPath + "/save.json", m_rawData);

        Debug.Log("Saved");
        }

    //Funciones privadas.
    private MasterData Load() {

        MasterData m_data = new MasterData(m_seedTypes);

        if (File.Exists(Application.persistentDataPath + "/save.json")) {

            string m_rawData = File.ReadAllText(Application.persistentDataPath + "/save.json");
            JsonUtility.FromJsonOverwrite(m_rawData, m_data);
            }

        return m_data;
        }

    //Funciones publicas.

        //Getters and Setters

            //Data System
            public static DataSystem GetSingleton() => m_instance;

            //Temporal Data
            public void SetDungeonData(DungeonData dungeonData) => m_dungeonData = dungeonData;
            public DungeonData GetDungeonData() => m_dungeonData;

            //Game Data
            public GameData GetGameData() => m_gameData;    

            //Prefab Data

                //Dungeon Room Prefabs
                public GameObject GetRoomPrefab(int index) => m_rooms[index];
                public int GetRandomRoomPrefabIndex() => Random.Range(0, m_rooms.Length);

                //Chest Rooms Prefabs
                public GameObject GetChestRoomPrefab(int index) => m_chestRooms[index];
                public int GetRandomChestRoomPrefabIndex() => Random.Range(0, m_rooms.Length);

                //Enemies Prefabs
                public GameObject GetEnemyPrefab(int index) => m_enemies[index];
                public int GetRandomEnemyPrefabIndex() => Random.Range(0, m_enemies.Length);

                //Weapons
                public Weapon GetWeapon(int index) => m_weapons[index];   
                public Weapon[] GetAllWeapons() => m_weapons;

                public Weapon GetActualWeapon() => m_weapons[m_gameData.GetInventoryData().GetActualWeapon().GetIndex()];   

                //Seeds
                public Seed GetSeed(int index) => m_seedTypes[index];   
                public Seed[] GetAllSeeds() => m_seedTypes;    

        //Functions
        private struct ItemData {
            
            private int m_index;
            private Item m_item;

            public ItemData(int index, Item item) {

                m_index = index;
                m_item = item;
                }
            
            public int GetIndex() => m_index; 
            public Item GetItem() => m_item; 

            public Rarity GetRarity() => m_item.GetRarity();
            } 

        public int GetRandomWeaponIndex(int probabilityIncrement) {
            
            List<ItemData> m_itemDatas = new List<ItemData>();

            for(int i = 0; i < m_weapons.Length; i ++) {

                m_itemDatas.Add(new ItemData(i, m_weapons[i]));
                }

            return GetRandomItemsIndex(probabilityIncrement, m_itemDatas);
            }
        public int GetRandomSeedIndex(int probabilityIncrement, Seed.SeedType seedType) {
            
            List<ItemData> m_itemDatas = new List<ItemData>();

            for(int i = 0; i < m_seedTypes.Length; i ++) {

                if (m_seedTypes[i].GetSeedType() == seedType) m_itemDatas.Add(new ItemData(i, m_seedTypes[i]));
                }

            return GetRandomItemsIndex(probabilityIncrement, m_itemDatas);
            }
        private int GetRandomItemsIndex(int probabilityIncrement, List<ItemData> items) {

            probabilityIncrement = Mathf.Clamp(probabilityIncrement, 0, 16);

            List<ItemData> m_rarityList = new List<ItemData>();
            int m_probability = Mathf.Clamp(Random.Range(0, 100) + probabilityIncrement, 0, 100);

            if (m_probability < 50) {

                for(int i = 0; i < items.Count; i ++) {
                    
                    if (items[i].GetRarity() == Rarity.Common) m_rarityList.Add(items[i]); 
                    }
                }

            else if (m_probability < 80) {

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
            
            if (m_rarityList.Count > 0) return m_rarityList[Random.Range(0, m_rarityList.Count)].GetIndex();
            else {
                
                Debug.Log("No existe un item dentro de esta categoria.");
                return -1;
                }
            }
        }

[System.Serializable]
public class MasterData {

    [SerializeField] private List<GameData> m_gameDatas;

    public MasterData(Seed[] seeds) {

        m_gameDatas = new List<GameData>();
        m_gameDatas.Add(new GameData(seeds));
        }

    public GameData GetGameData(int file) {

        return m_gameDatas[file];
        }
    }

[System.Serializable]
public class GameData {

    [SerializeField] private FarmData m_farmData;
    [SerializeField] private InventoryData m_inventoryData;

    public GameData(Seed[] seeds) { 

        m_farmData = new FarmData(seeds);
        m_inventoryData = new InventoryData();
        }

    public FarmData GetFarmData() => m_farmData;
    public InventoryData GetInventoryData() => m_inventoryData;
    }

[System.Serializable] 
public class FarmData {

    [SerializeField] private List<GridData> m_gridDatas;
    [SerializeField] private List<SeedData> m_seedDatas;

    [SerializeField] private List<int> m_harvestedSeeds;

    public FarmData(Seed[] seeds) {

        //Establecer listas.
        m_gridDatas = new List<GridData>();
        m_seedDatas = new List<SeedData>();
        m_harvestedSeeds = new List<int>();
        
        //Añadir semillas por defecto.
        for(int i = 0; i < seeds.Length; i ++) {

            m_seedDatas.Add(new SeedData(0, false, i));
            }

        AddSeed(Seed.SeedType.Durability, Rarity.Common, 5);
        AddSeed(Seed.SeedType.Potion, Rarity.Common, 5);
        }

    //Getters and Setters

        //Seed Data.
        private int FindSeedIndex(Seed.SeedType seedType, Rarity rarity) {

            Seed[] m_seeds = DataSystem.GetSingleton().GetAllSeeds();
            int m_index = -1;

            for(int i = 0; i < m_seeds.Length; i ++) {

                if (m_seeds[i].GetSeedType() == seedType && m_seeds[i].GetRarity() == rarity) {

                    m_index = i;
                    break;
                    }    
                }
            
            return m_index;
            }
        private SeedData FindSeedData(Seed.SeedType seedType, Rarity rarity) {

            int m_index = FindSeedIndex(seedType, rarity);

            foreach(SeedData m_sd in m_seedDatas) {

                if (m_index == m_sd.GetIndex()) return m_sd;
                }    

            return null;
            }    
        
        public int GetSeedCount(Seed.SeedType seedType, Rarity rarity) => FindSeedData(seedType, rarity).GetCount();

        public SeedData GetSeedData(Seed.SeedType seedType, Rarity rarity) => FindSeedData(seedType, rarity); 
        public List<SeedData> GetSeedDatas() => m_seedDatas;

        public void AddSeed(Seed.SeedType seedType, Rarity rarity) => AddSeedData(seedType, rarity, 1);
        public void AddSeed(Seed.SeedType seedType, Rarity rarity, int count) => AddSeedData(seedType, rarity, count);
        public void RemoveSeed(Seed.SeedType seedType, Rarity rarity) => FindSeedData(seedType, rarity).AddCount(-1);

        private void AddSeedData(Seed.SeedType seedType, Rarity rarity, int count) {

            SeedData m_seedData = FindSeedData(seedType, rarity);

            m_seedData.AddCount(count);
            m_seedData.SetUnlocked(true);
            }    
            
        //Harvested Seeds.
        public void AddHarvestedSeed(int index) => m_harvestedSeeds.Add(index);

        //Grid Data.
        public void AddGridData(GridData data) => m_gridDatas.Add(data);
        public void RemoveGridData(GridData data) => m_gridDatas.Remove(data);
        public void RemoveGridData(Vector3Int position) {
            
            GridData m_d = null;

            foreach(GridData m_gd in m_gridDatas) {

                if (position == m_gd.GetSeedPosition()) {

                    m_d = m_gd;
                    break;
                    }
                }

            m_gridDatas.Remove(m_d);
            }   

        public List<GridData> GetGridDatas() => m_gridDatas;

        public bool GetIfGridIsUsed(Vector3Int position) {

            foreach(GridData m_gd in m_gridDatas) {

                if (m_gd.GetSeedPosition() == position) return true;
                }

            return false;
            }    
        }

[System.Serializable]
public class InventoryData {

    [SerializeField] private int m_actualWeaponInventoryIndex;
    [SerializeField] private List<WeaponData> m_inventoryWeapons;

    public InventoryData() {

        m_inventoryWeapons = new List<WeaponData>();
        m_inventoryWeapons.Add(new WeaponData(0, DataSystem.GetSingleton().GetWeapon(0).GetUses()));

        m_actualWeaponInventoryIndex = 0;
        }
        
    //Getters and Setters

        //Weapons
        public void SetActualWeapon(int index) => m_actualWeaponInventoryIndex = index;
        public WeaponData GetActualWeapon() => SearchInWeaponInventory(m_actualWeaponInventoryIndex);

        public void UseWeapon(int index) => m_inventoryWeapons[index].UseWeapon();
        public void AddWeapon(int index, int uses) => m_inventoryWeapons.Add(new WeaponData(index, uses));

        public void UseActualWeapon() => UseWeapon(m_actualWeaponInventoryIndex);

        public WeaponData SearchInWeaponInventory(int index) {

            foreach(WeaponData m_w in m_inventoryWeapons) {

                if (m_w.GetIndex() == index) return m_w;
                }

            return new WeaponData(-1, 0);
            }
        }

[System.Serializable]
public class WeaponData {
    
    [SerializeField] private int m_weaponIndex;
    [SerializeField] private int m_uses;

    public WeaponData() {

        m_weaponIndex = -1;
        m_uses = 0;
        }
    public WeaponData(int weaponIndex, int uses) {

        m_weaponIndex = weaponIndex;
        m_uses = uses;
        }

    public int GetIndex() => m_weaponIndex;
    public void UseWeapon() => m_uses --;
    public void SetUses(int count) => m_uses = count;
    public int GetUses() => m_uses;
    }

[System.Serializable] 
public class GridData {

    [SerializeField] private int m_seedIndex;
    [SerializeField] private Vector3Int m_position;
    [SerializeField] private bool m_canHarvest;

    public GridData(int seedIndex, Vector3Int position) {

        m_seedIndex = seedIndex;
        m_position = position;

        m_canHarvest = false;
        }   

    public int GetSeedIndex() => m_seedIndex;
    public Vector3Int GetSeedPosition() => m_position;
    public bool GetHarvest() => m_canHarvest;
    public void SetHarvest(bool harvest) => m_canHarvest = harvest;
    }

[System.Serializable]
public class SeedData {

    [SerializeField] private int m_count;
    [SerializeField] private bool m_unlocked;
    [SerializeField] private int m_index;

    public SeedData() {

        m_count = 0;
        m_index = -1;
        m_unlocked = false;
        }
    public SeedData(int count, bool unlocked, int index) {

        m_count = count;
        m_index = index;
        m_unlocked = unlocked;
        }

    public int GetCount() => m_count;
    public void SetCount(int count) => SetCountAndUnlock(count);
    public void AddCount(int count) => SetCountAndUnlock(m_count + count);

    private void SetCountAndUnlock(int count) {
        
        m_count = count;
        m_unlocked = true;
        }

    public bool GetUnlocked() => m_unlocked;
    public void SetUnlocked(bool unlocked) => m_unlocked = unlocked; 

    public int GetIndex() => m_index;
    public Seed GetSeed() => DataSystem.GetSingleton().GetSeed(m_index);
    }

public class DungeonData {

    [SerializeField] private PlayerData m_playerData; 
    [SerializeField] private List<RoomData> m_rooms;
    [SerializeField] private Vector2Int m_actualRoom;

    [SerializeField] private List<int> m_recolectedSeedsIndex;

    public DungeonData(PlayerData playerData) {

        m_playerData = playerData;
        m_rooms = null;
        m_actualRoom = Vector2Int.zero;

        m_recolectedSeedsIndex = new List<int>();
        }   
        
    public PlayerData GetPlayer() => m_playerData;
    public RoomData GetRoomData(int index) => m_rooms[index];
    public RoomData GetRoomData(Vector2Int position) {

        foreach(RoomData m_d in m_rooms) {

            if (m_d.GetRoomPosition() == position) return m_d;
            }

        return null;
        }    

    public List<RoomData> GetRoomsDatas() => m_rooms;

    public void SetRoomDatas(List<RoomData> rooms) => m_rooms = rooms;

    public void SetActualRoom(Vector2Int position) => m_actualRoom = position;
    public Vector2Int GetActualRoom() => m_actualRoom;

    public void AddSeed(int index) => m_recolectedSeedsIndex.Add(index);

    public void LoseInventoryPart(int percent) {

        Debug.Log("Inventario antes de morir: " + m_recolectedSeedsIndex.Count);

        List<int> m_newSeeds = m_recolectedSeedsIndex;
        int m_actualPercentIndex = 0;
        int m_maxPercentIndex = Mathf.FloorToInt(m_recolectedSeedsIndex.Count * (percent/100f));

        while(m_actualPercentIndex < m_maxPercentIndex) {

            m_newSeeds.RemoveAt(Random.Range(0, m_newSeeds.Count));
            m_actualPercentIndex ++;
            }

        Debug.Log("Inventario despues de morir: " + m_newSeeds.Count);
        }
    }
public class RoomData {
    
    public enum RoomType {

        Room,
        Chest,
        }

    [SerializeField] private Vector2Int m_roomPosition;
    [SerializeField] private int m_roomPrefabIndex;
    [SerializeField] private RoomType m_roomType;
    
    public RoomData(Vector2Int roomPosition, int prefabIndex, RoomType roomType) {
        
        m_roomPosition = roomPosition;
        m_roomPrefabIndex = prefabIndex;
        m_roomType = roomType;
        }

    public Vector2Int GetRoomPosition() => m_roomPosition;
    public int GetRoomPrefabIndex() => m_roomPrefabIndex;
    public RoomType GetRoomType() => m_roomType;
    }
public class PlayerData {

    [SerializeField] private int m_playerHealth;

    public PlayerData(int playerHealth) {

        m_playerHealth = playerHealth;
        }   

    public void SetHealth(int health) => m_playerHealth = health; 
    public int GetHealth() => m_playerHealth;
    }
