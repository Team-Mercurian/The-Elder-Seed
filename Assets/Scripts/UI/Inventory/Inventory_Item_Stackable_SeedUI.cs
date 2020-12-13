using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Item_Stackable_SeedUI : Inventory_Item_StackableUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void SetValues(Item item, int count, bool isOdd) {
			
			base.SetValues(item, count, isOdd);
			SetName("Semilla de " + item.GetName());
			}	 
		public override void Use() {

			if (GetCount() <= 0) return;
			Debug.Log("Seleccion de semillas via inventario aun no implementada.");
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
