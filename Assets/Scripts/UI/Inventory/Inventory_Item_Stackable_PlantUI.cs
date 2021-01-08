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
		public override void SetValues(Item item, int count, bool isOdd, InventoryUI inventory) {
			
			base.SetValues(item, count, isOdd, inventory);

			Seed.SeedType m_seedType = DataSystem.GetSingleton().GetPlant(GetItem().GetID()).GetSeedType();
			if (m_seedType == Seed.SeedType.Potion) GetButton().interactable = false;
			}
		
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
				case Seed.SeedType.Unlock : return UseUnlockPlant(); 
				}
			
			return false;
			}

		private bool UseDurabilityPlant() {

			Rarity m_rarity = GetItem().GetRarity();
			GetInventory().GetRepairWeapon().Open(m_rarity);

			return false;
			}
		private bool UseUnlockPlant() {

			InventoryData m_inventoryData = DataSystem.GetSingleton().GetGameData().GetInventoryData();
			Rarity m_rarity = GetItem().GetRarity();
			List<WeaponBaseData> m_unlockeableWeapons = DataSystem.GetSingleton().GetGameData().GetWeaponBaseData().FindAll(c => !c.GetUnlocked());
			m_unlockeableWeapons = m_unlockeableWeapons.FindAll(c => DataSystem.GetSingleton().GetWeapon(c.GetID()).GetRarity() == m_rarity);

			if (m_unlockeableWeapons.Count > 0) {

				Weapon m_finalWeapon = DataSystem.GetSingleton().GetWeapon(m_unlockeableWeapons[Random.Range(0, m_unlockeableWeapons.Count)].GetID());
				DataSystem.GetSingleton().GetGameData().GetWeaponBaseData().Find(c => c.GetID() == m_finalWeapon.GetID()).Unlock();
				SaveSystem.Save();
				Debug.Log("Desbloqueada la arma en el indice: " + m_finalWeapon.GetID());
				return true;
				}	
			else {

				Debug.Log("No quedan armas para desbloquear.");
				return false;
				}
			}
        
        
	//Coroutines
	
	}
