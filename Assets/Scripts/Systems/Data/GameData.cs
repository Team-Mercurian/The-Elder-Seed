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

        public bool GetIfGridIsUsed(Vector3Int position) {

            foreach(GridData m_gd in m_gridDatas) {

                if (m_gd.GetSeedPosition() == position) return true;
                }

            return false;
            }    
        }

[System.Serializable]
public class InventoryData {

	[SerializeField] private int m_actualWeaponIndex;
    [SerializeField] private int m_lastWeaponID = 0;

    [SerializeField] private List<WeaponEntityData> m_weapons;

    [SerializeField] private List<ItemData> m_plants;
    [SerializeField] private List<ItemData> m_potions;
    [SerializeField] private List<ItemData> m_seeds;
    [SerializeField] private List<ItemData> m_miscellaneous;

    public InventoryData() {

		m_actualWeaponIndex = 0;

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

		public List<WeaponEntityData> GetWeaponList() => m_weapons;  

		public void SetSeedList(List<ItemData> seedList) => m_seeds = seedList;
		public void SetPlantList(List<ItemData> plantList) => m_plants = plantList;
		public void SetPotionList(List<ItemData> potionList) => m_potions = potionList;
        public void SetMiscellaneousList(List<ItemData> miscellaneousList) => m_miscellaneous = miscellaneousList;

		public WeaponEntityData GetActualWeapon() => SearchInWeaponInventory(m_actualWeaponIndex);
        public WeaponEntityData GetWeaponData(int index) => SearchInWeaponInventory(index);
        public WeaponEntityData SearchInWeaponInventory(int index) => m_weapons.Find(c => c.GetIndex() == index);

        public int GetActualWeaponIndex() => m_actualWeaponIndex;

        public void SetActualWeapon(int index) => m_actualWeaponIndex = index; 

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

        public void AddWeapon(int id, int uses) => m_weapons.Add(new WeaponEntityData(id, m_lastWeaponID += 1, uses));
		public void AddSeed(int id, int count) => FindItemDataViaID(m_seeds, id).AddCount(count);
		public void AddPlant(int id, int count) => FindItemDataViaID(m_plants, id).AddCount(count);
		public void AddPotion(int id, int count) => FindItemDataViaID(m_potions, id).AddCount(count);

        public void RemoveWeapon(int index, int magicalFragments) {

            m_weapons.Remove(SearchInWeaponInventory(index));
            AddMagicalFragments(magicalFragments);
            }    

        public void UseWeapon() => SearchInWeaponInventory(m_actualWeaponIndex).UseWeapon();
        public void UseWeapon(int index) => SearchInWeaponInventory(index).UseWeapon();

        public void AddMagicalFragments(int count) => SearchMagicalFragments().AddCount(count);
        public int GetMagicalFragments() => SearchMagicalFragments().GetCount();

        private ItemData SearchMagicalFragments() => m_miscellaneous.Find(c => c.GetID() == 0);
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
    public void UseWeapon() => m_uses --;
    public void SetUses(int count) => m_uses = count;
    public int GetUses() => m_uses;
    }