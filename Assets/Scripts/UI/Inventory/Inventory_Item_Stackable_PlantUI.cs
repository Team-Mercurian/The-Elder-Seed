using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Item_Stackable_PlantUI : Inventory_Item_StackableUI {
	
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

			if (UsePlant()) {

				SubtractCount(1);
				DataSystem.GetSingleton().GetGameData().GetInventoryData().AddPlant(GetItem().GetID(), -1);
				SaveSystem.Save();
				}
			}

		//Private Functions
		private bool UsePlant() {

			Seed.SeedType m_seedType = DataSystem.GetSingleton().GetPlant(GetItem().GetID()).GetSeedType();

			switch(m_seedType) {

				case Seed.SeedType.Durability : return UseDurabilityPlant(); 
				case Seed.SeedType.Potion : return UsePotionPlant(); 
				case Seed.SeedType.Unlock : return UseUnlockPlant(); 
				}
			
			return false;
			}

		private bool UseDurabilityPlant() {

			Rarity m_rarity = GetItem().GetRarity();
			GetInventory().GetRepairWeapon().Open(m_rarity);

			return false;
			}
		private bool UsePotionPlant() {

			int m_potionID = DataSystem.GetSingleton().GetPotions().Find(c => c.GetRarity() == GetItem().GetRarity()).GetID();
			DataSystem.GetSingleton().GetGameData().GetInventoryData().AddPotion(m_potionID, 1);
			return true;
			}
		private bool UseUnlockPlant() {

			Debug.Log("Aun no existen suficientes armas para desbloquear.");
			return false;
			}
        
        
	//Coroutines
	
	}
