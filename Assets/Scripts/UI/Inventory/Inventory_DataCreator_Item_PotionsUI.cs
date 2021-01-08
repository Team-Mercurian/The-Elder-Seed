using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory_DataCreator_Item_PotionsUI : Inventory_DataCreator_ItemUI {
	
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
			
			List<ItemData> m_iD = inventoryData.GetPotionList().FindAll(c => c.GetCount() > 0 || c.GetUnlocked());
			List<Item> m_items = DataSystem.GetSingleton().GetPotions().Cast<Item>().ToList();

			m_iD = m_iD.OrderBy(c => m_items[c.GetID()].GetRarity()).ToList();

			CreateStackableItems(m_iD, m_items);
			}
        
        
	//Coroutines
	
	}
