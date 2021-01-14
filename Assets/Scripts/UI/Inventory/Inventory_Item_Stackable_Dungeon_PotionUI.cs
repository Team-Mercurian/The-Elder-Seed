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

			DataSystem.GetSingleton().GetDungeonData().SetActualPotion(GetItem().GetID());

            ItemData m_potion = DataSystem.GetSingleton().GetDungeonData().GetActualPotion();
            SelectedPotionUI.GetSingleton().SetData(DataSystem.GetSingleton().GetPotion(m_potion.GetID()), m_potion == null ? 0 : m_potion.GetCount());

			GetInventory().Close();
			}

		//Private Functions
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
