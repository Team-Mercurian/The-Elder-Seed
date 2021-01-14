using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MasterData {

    [SerializeField] private List<GameData> m_gameDatas;

    public MasterData() {

        m_gameDatas = new List<GameData>();
        m_gameDatas.Add(new GameData());
        }

    public GameData GetGameData(int file) {

        return m_gameDatas[file];
        }
    }

[System.Serializable]
public class GameData {

    [SerializeField] private FarmData m_farmData;
    [SerializeField] private InventoryData m_inventoryData;
    [SerializeField] private List<WeaponBaseData> m_weaponData;
	[SerializeField] private bool m_giftData;

    public GameData() { 

        m_farmData = new FarmData();
        m_inventoryData = new InventoryData();
        m_weaponData = new List<WeaponBaseData>();
        m_giftData = true;
		}

    public FarmData GetFarmData() => m_farmData;
    public InventoryData GetInventoryData() => m_inventoryData;

    public List<WeaponBaseData> GetWeaponBaseData() => m_weaponData;
    public void AddWeaponBaseData(WeaponBaseData m_data) => m_weaponData.Add(m_data);          
    public bool GetIfIDExists(int id) {

		foreach(WeaponBaseData m_eD in m_weaponData) {

			if (id == m_eD.GetID()) return true;
			}

		return false;
		}
    
	public bool GiftData() => m_giftData;
	public void TurnGiftOff() => m_giftData = false;
	}

[System.Serializable] 
public class FarmData {

    [SerializeField] private List<GridData> m_gridDatas;

    public FarmData() {

        //Establecer listas.
        m_gridDatas = new List<GridData>();
        }

    //Getters and Setters

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
        public GridData GetGridData(Vector3Int position) => m_gridDatas.Find(c => c.GetSeedPosition() == position);

        public bool GetIfGridIsUsed(Vector3Int position) {

            foreach(GridData m_gd in m_gridDatas) {

                if (m_gd.GetSeedPosition() == position) return true;
                }

            return false;
            }    
        }

[System.Serializable]
public class InventoryData {

    [SerializeField] public int m_lastWeaponID;

    [SerializeField] private List<WeaponEntityData> m_weapons;

    [SerializeField] private List<ItemData> m_plants;
    [SerializeField] private List<ItemData> m_potions;
    [SerializeField] private List<ItemData> m_seeds;
    [SerializeField] private List<ItemData> m_miscellaneous;

    public InventoryData() {

        m_weapons = new List<WeaponEntityData>();
        m_seeds = new List<ItemData>();
        m_plants = new List<ItemData>();
		m_potions = new List<ItemData>();
        m_miscellaneous = new List<ItemData>();
        }
        
    //Getters and Setters
		public List<ItemData> GetSeedList() => m_seeds;  
		public List<ItemData> GetPlantList() => m_plants;  
		public List<ItemData> GetPotionList() => m_potions;  
        public List<ItemData> GetMiscellaneousList() => m_miscellaneous;

        public void SetWeaponList(List<WeaponEntityData> weapons) => m_weapons = weapons;
		public List<WeaponEntityData> GetWeaponList() => m_weapons;  

		public void SetSeedList(List<ItemData> seedList) => m_seeds = seedList;
		public void SetPlantList(List<ItemData> plantList) => m_plants = plantList;
		public void SetPotionList(List<ItemData> potionList) => m_potions = potionList;
        public void SetMiscellaneousList(List<ItemData> miscellaneousList) => m_miscellaneous = miscellaneousList;

        public WeaponEntityData GetWeaponData(int index) => SearchInWeaponInventory(index);
        public WeaponEntityData SearchInWeaponInventory(int index) => m_weapons.Find(c => c.GetIndex() == index);

		public ItemData GetSeedData(int id) => FindItemDataViaID(m_seeds, id);
		public ItemData GetPotionData(int id) => FindItemDataViaID(m_potions, id);
		public ItemData GetPlantData(int id) => FindItemDataViaID(m_plants, id);

		private ItemData FindItemDataViaID(List<ItemData> items, int id) {
			
			ItemData m_itemData = null;

			foreach(ItemData m_item in items) {
				
				if (m_item.GetID() == id) {
                    
                    m_itemData = m_item;
                    break;
                    }
                }
                
			return m_itemData;
			}

        public void AddWeapon(int id, int uses, ref int lastID) {

            m_weapons.Add(new WeaponEntityData(id, lastID, uses));
            lastID ++;
            }

        public void AddWeapon(WeaponEntityData entityData) => m_weapons.Add(entityData);

		public void AddSeed(int id, int count) => FindItemDataViaID(m_seeds, id).AddCount(count);
		public void AddPlant(int id, int count) => FindItemDataViaID(m_plants, id).AddCount(count);
		public void AddPotion(int id, int count) => FindItemDataViaID(m_potions, id).AddCount(count);

        public void RemoveWeapon(int index, int magicalFragments) {

            m_weapons.Remove(SearchInWeaponInventory(index));
            AddMagicalFragments(magicalFragments);
            }    
        public void RemoveWeapon(WeaponEntityData entityData) => m_weapons.Remove(entityData);

        public void UseWeapon(int index) => SearchInWeaponInventory(index).UseWeapon();

        public void AddMagicalFragments(int count) => SearchMagicalFragments().AddCount(count);
        public int GetMagicalFragments() => SearchMagicalFragments().GetCount();

        private ItemData SearchMagicalFragments() => m_miscellaneous.Find(c => c.GetID() == 0);

        public int GetLastWeaponID() => m_lastWeaponID;

        public void AddDungeonInventory(InventoryData dungeonInventory) {
            
            foreach(WeaponEntityData m_ed in dungeonInventory.GetWeaponList()) {

                WeaponEntityData m_gameEntity = m_weapons.Find(c => c.GetIndex() == m_ed.GetIndex());

                if (m_gameEntity != null) m_gameEntity.SetUses(m_ed.GetUses());
                else m_weapons.Add(m_ed);    
                }

            MixItemData(m_seeds, dungeonInventory.GetSeedList());
            MixItemData(m_plants, dungeonInventory.GetPlantList());
            MixItemData(m_potions, dungeonInventory.GetPotionList());

            void MixItemData(List<ItemData> gameData, List<ItemData> dungeonData) {
                
                foreach(ItemData m_i in dungeonData) {
                    
                    gameData.Find(c => c.GetID() == m_i.GetID()).AddCount(m_i.GetCount());
                    }
                }
            }
        public void RemoveDungeonItems(InventoryData dungeonInventory) {

            RemoveItemData(m_seeds, dungeonInventory.GetSeedList());
            RemoveItemData(m_plants, dungeonInventory.GetPlantList());
            RemoveItemData(m_potions, dungeonInventory.GetPotionList());

            void RemoveItemData(List<ItemData> gameData, List<ItemData> dungeonData) {
                
                foreach(ItemData m_i in dungeonData) {
                    
                    gameData.Find(c => c.GetID() == m_i.GetID()).AddCount(-m_i.GetCount());
                    }
                }
            }
        }

[System.Serializable]
public class WeaponBaseData {
    
    [SerializeField] private int m_weaponID = -1;
    [SerializeField] private bool m_isUnlocked = false;
    
    public WeaponBaseData(int id, bool unlocked) {
        
        m_weaponID = id;
        m_isUnlocked = unlocked;
        }

    public bool GetUnlocked() => m_isUnlocked;
    public int GetID() => m_weaponID;

    public void Unlock() => m_isUnlocked = true;
    }

[System.Serializable] 
public class GridData {

    [SerializeField] private int m_seedIndex;
    [SerializeField] private Vector3Int m_position;
    [SerializeField] private int m_requiredRoomsToHarvest;
    [SerializeField] private bool m_canHarvest;
    [SerializeField] private int m_roomCount;

    public GridData(int seedIndex, Vector3Int position, int requiredRoomsToHarvest) {

        m_seedIndex = seedIndex;
        m_position = position;
        m_requiredRoomsToHarvest = requiredRoomsToHarvest;

        m_roomCount = 0;
        m_canHarvest = false;
        }   

    public void AddRoom() {

        if (m_canHarvest) return;

        m_roomCount ++;
        if (m_roomCount >= m_requiredRoomsToHarvest) m_canHarvest = true; 
        }   
    public int GetSeedIndex() => m_seedIndex;
    public Vector3Int GetSeedPosition() => m_position;

    public bool GetHarvest() => m_canHarvest;
    public void Harvest() => m_canHarvest = true;
    }

[System.Serializable] 
public class ItemData {

    [SerializeField] private int m_id;
    [SerializeField] private int m_count;
    [SerializeField] private bool m_unlocked;

    public ItemData(int id, int count, bool unlocked) {

        m_id = id;
        m_count = count;
        m_unlocked = unlocked;
        }

    public int GetID() => m_id;

    public void AddCount(int value) => m_count += value;
    public void SubtractCount(int value) => m_count -= value; 
    
    public void SetCount(int count) => m_count = count;
    public int GetCount() => m_count;

    public void Unlock() => m_unlocked = true;
    public bool GetUnlocked() => m_unlocked;
    }

[System.Serializable]
public class WeaponEntityData {
    
    [SerializeField] private int m_ID;
    [SerializeField] private int m_index;
    [SerializeField] private int m_uses;

    public WeaponEntityData(int weaponID, int weaponIndex, int uses) {

        m_ID = weaponID;
        m_index = weaponIndex;
        m_uses = uses;
        }

    public int GetID() => m_ID;
    public int GetIndex() => m_index;
    public void UseWeapon() => m_uses = Mathf.Clamp(m_uses - 1, 0, 5000);
    public void SetUses(int count) => m_uses = count;
    public int GetUses() => m_uses;
    }