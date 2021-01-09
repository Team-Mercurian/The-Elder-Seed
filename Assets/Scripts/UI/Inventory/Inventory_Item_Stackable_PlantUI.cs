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

			string m_str = "";

			if (m_unlockeableWeapons.Count > 0) {

				Weapon m_finalWeapon = DataSystem.GetSingleton().GetWeapon(m_unlockeableWeapons[Random.Range(0, m_unlockeableWeapons.Count)].GetID());
				DataSystem.GetSingleton().GetGameData().GetWeaponBaseData().Find(c => c.GetID() == m_finalWeapon.GetID()).Unlock();
				SaveSystem.Save();

				string m_strRarity = "";

				switch(m_rarity) {

					case Rarity.Common : m_strRarity = "comun"; break;
					case Rarity.Rare : m_strRarity = "rara"; break;
					case Rarity.Epic : m_strRarity = "epica"; break;
					case Rarity.Legendary : m_strRarity = "legendaria"; break;
					}	 

				string m_a = "¡Nueva arma " + m_strRarity + " desbloqueada!";
				string m_b = m_finalWeapon.GetName();
				string m_c = "<size=16> Para conseguirla deberas buscar en las ruinas.</size>";
				m_str = m_a + "\n\n" + m_b + "\n" + m_c;
				}	
			else {

				int m_fm = (int) m_rarity + 1;

				string m_a = "No quedan armas para desbloquear de este tipo.";
				string m_b = "<size=16> En compensacion has recibido " + m_fm + " " + (m_fm == 1 ? "fragmento magico." : "fragmentos magicos.") + "</size>";
				m_str = m_a + "\n\n" + m_b;
				}

			WarningPanelUI.GetSingleton().SetData(m_str, "Cerrar");
			WarningPanelUI.GetSingleton().Open();
			return true;
			}
        
        
	//Coroutines
	
	}
