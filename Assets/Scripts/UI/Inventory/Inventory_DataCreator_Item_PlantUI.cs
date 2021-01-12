using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory_DataCreator_Item_PlantUI : Inventory_DataCreator_ItemUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
        
        
		//Private Functions
		protected override void CreateEntity(InventoryData inventoryData) {

			DataSystem m_ds = DataSystem.GetSingleton();

			List<ItemData> m_iD = inventoryData.GetPlantList().FindAll(c => c.GetCount() > 0 || c.GetUnlocked());
			List<Item> m_items = m_ds.GetPlants().Cast<Item>().ToList();

			m_iD = m_iD.OrderBy(c => m_ds.GetPlant(c.GetID()).GetSeedType()).ThenBy(c => m_ds.GetPlant(c.GetID()).GetRarity()).ToList();

			CreateStackableItems(m_iD, m_items);
			}        
        
	//Coroutines
	
	}
