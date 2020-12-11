using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_ItemPlant_UnlockUI : Inventory_ItemPlantBaseUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		protected override bool UsePlant() {

			Debug.Log("Aun no existen suficientes armas para desbloquear.");
			return false;
			}
        
		//Private Functions
        protected override Seed.SeedType GetPlantType() => Seed.SeedType.Unlock;

        
	//Coroutines
	
	}
