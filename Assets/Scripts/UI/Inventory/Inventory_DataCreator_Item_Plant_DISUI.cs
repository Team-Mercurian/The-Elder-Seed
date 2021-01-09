using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_DataCreator_Item_Plant_DISUI : Inventory_DataCreator_Item_PlantUI {
	
	//Singleton
	//private static Inventory_DataCreator_Item_Plant_DISUI m_instance = null;
	//private void Awake() => m_instance = this;
	//public static Inventory_DataCreator_Item_Plant_DISUI GetSingleton() => m_instance;
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		protected override void CreateEntity(InventoryData inventoryData) {

			DataSystem m_ds = DataSystem.GetSingleton();

			List<ItemData> m_iD = inventoryData.GetPlantList().FindAll(c => c.GetCount() > 0 || c.GetUnlocked());
			List<Item> m_items = m_ds.GetPlants().Cast<Item>().ToList();

			m_iD = m_iD.FindAll(c => m_ds.GetPlant(c.GetID()).GetSeedType() == Seed.SeedType.Durability);
			m_iD = m_iD.OrderBy(c => m_ds.GetPlant(c.GetID()).GetSeedType()).ThenBy(c => m_ds.GetPlant(c.GetID()).GetRarity()).ToList();

			CreateStackableItems(m_iD, m_items);
			}  
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
