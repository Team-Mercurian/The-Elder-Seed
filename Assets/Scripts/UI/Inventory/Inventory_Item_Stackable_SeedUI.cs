using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_Item_Stackable_SeedUI : Inventory_Item_StackableUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("References")]
		[SerializeField] private TextMeshProUGUI m_equipText = null;
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void SetValues(Item item, int count, bool isOdd, InventoryUI inventory) {
			
			base.SetValues(item, count, isOdd, inventory);
			SetName("Semilla de " + item.GetName());

			m_equipText.text = PlayerFarming.GetSeedID() == GetItem().GetID() ? "X" : "";
			}	 
		public override void Use() {

			if (GetCount() <= 0) return;
			PlayerFarming.SetSeedID(GetItem().GetID());
			GetInventory().Close();
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
