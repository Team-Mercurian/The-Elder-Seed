using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory_DataCreator_ItemUI : Inventory_DataCreatorUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
        
        
		//Private Functions
		protected void CreateStackableItems(List<ItemData> itemDatas, List<Item> items) {

			for(int i = 0; i < itemDatas.Count; i ++) {
				
				ItemData m_item = itemDatas[i];
				Instantiate(GetItemPrefab(), GetItemHolder()).GetComponent<Inventory_Item_StackableUI>().SetValues(items.Find(c => c.GetID() == m_item.GetID()), m_item.GetCount(), (i % 2) < 1, GetInventory());
				}
			}

        
        
	//Coroutines
	
	}
