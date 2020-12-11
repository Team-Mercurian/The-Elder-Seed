using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_ItemPlant_PotionUI : Inventory_ItemPlantBaseUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		protected override bool UsePlant() {

			Debug.Log("Añadida en el sistema de datos una pocion de rareza: " + GetRarity());
			DataSystem.GetSingleton().GetGameData().GetInventoryData().AddPotion(GetRarity());
			return true;
			}
        
        
		//Private Functions
        protected override Seed.SeedType GetPlantType() => Seed.SeedType.Potion;
        
        
	//Coroutines
	
	}
