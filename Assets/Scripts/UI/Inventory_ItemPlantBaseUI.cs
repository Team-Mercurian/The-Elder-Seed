using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory_ItemPlantBaseUI : Inventory_ItemBaseUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		private Seed.SeedType m_plantType;
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void SetValues(Item item, int count, bool isOdd) {
			
			m_plantType = GetPlantType();
			base.SetValues(item, count, isOdd);
			}
    
		public override void Use() {
			
			if (UsePlant()) {

				SubtractCount(1);
				DataSystem.GetSingleton().GetGameData().GetInventoryData().SubtractPlant(m_plantType, GetRarity());
				DataSystem.Save();
				}
			}

		//Private Functions
		protected abstract bool UsePlant();
		protected abstract Seed.SeedType GetPlantType();
        
        
	//Coroutines
	
	}
