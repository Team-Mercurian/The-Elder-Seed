using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory_Item_Stackable_Dungeon_SeedUI : Inventory_Item_Stackable_DungeonUI {
	
	//Singleton
	//private static Inventory_Item_Stackable_Dungeon_SeedUI m_instance = null;
	//private void Awake() => m_instance = this;
	//public static Inventory_Item_Stackable_Dungeon_SeedUI GetSingleton() => m_instance;
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void SetValues(Item item, int count, bool isOdd, InventoryUI inventory) {
			
			base.SetValues(item, count, isOdd, inventory);
			SetName(item.GetName());
			}	 
		public override void Use() {

			ItemDescriptionUI.GetSingleton().SetData(GetItem(), GetCount(), null);
			ItemDescriptionUI.GetSingleton().Open();
			}

		//Private Functions
        
        
	//Coroutines
	
	}
