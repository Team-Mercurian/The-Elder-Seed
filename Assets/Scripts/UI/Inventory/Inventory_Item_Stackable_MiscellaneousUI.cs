using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Item_Stackable_MiscellaneousUI : Inventory_Item_StackableUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
        public override void SetValues(Item item, int count, bool isOdd, InventoryUI inventory) {
			
			base.SetValues(item, count, isOdd, inventory);
			SetInteractable(false);
			}
		public override void Use() {

			if (GetCount() <= 0) return;
			Debug.Log("Fragmentos magicos aun no implementados");
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
