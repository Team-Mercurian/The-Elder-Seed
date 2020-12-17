using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_Item_Stackable_Dungeon_PotionUI : Inventory_Item_Stackable_DungeonUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("Dungeon References")]
		[SerializeField] private TextMeshProUGUI m_healHolder = null;
        
		private int m_healPercent = 0;
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void SetValues(Item item, int count, bool isOdd, InventoryUI inventory) {
			
			base.SetValues(item, count, isOdd, inventory);
			
			m_healPercent = ((int) item.GetRarity() + 1) * 25;
			m_healHolder.text = m_healPercent.ToString() + "%"; 
			}
		public override void Use() {

			if (GetCount() <= 0) return;

			if (UsePotion()) {

				SubtractCount(1);
				DataSystem.GetSingleton().GetDungeonData().GetInventoryData().AddPotion(GetItem().GetID(), -1);
				SaveSystem.Save();
				}
			}

		//Private Functions
		private bool UsePotion() {

			int m_actualHealth = DataSystem.GetSingleton().GetDungeonData().GetPlayer().GetHealth();
			int m_maxHealth = DataSystem.GetSingleton().GetPlayerHealth();
			if (m_actualHealth >= m_maxHealth) return false;

			PlayerBrain.GetSingleton().GetHealth().OverrideHealth(m_actualHealth + ((m_maxHealth / 100) * m_healPercent));
			return true;
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
