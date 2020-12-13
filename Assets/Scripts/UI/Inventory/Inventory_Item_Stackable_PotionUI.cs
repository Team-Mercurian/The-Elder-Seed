using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Item_Stackable_PotionUI : Inventory_Item_StackableUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void Use() {

			if (GetCount() <= 0) return;
			Debug.Log("Uso de pociones aun no implementado.");
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
