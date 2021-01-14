using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
			SetName(item.GetName());

			m_equipText.text = PlayerFarming.GetSingleton().GetSeedID() == GetItem().GetID() ? "X" : "";
			}	 
		public override void Use() {

			UnityEvent m_event = new UnityEvent();
			m_event.AddListener(() => SetSeed());

			ItemDescriptionUI.GetSingleton().SetData(GetItem(), GetCount(), m_event);
			ItemDescriptionUI.GetSingleton().Open();
			}

		public void SetSeed() {

			if (GetCount() <= 0) return;
			PlayerFarming.GetSingleton().SetSeedID(GetItem().GetID());

			if (DataSystem.GetSingleton().GetSeed(GetItem().GetID()).GetSeedType() == Seed.SeedType.Potion) {

				if (DataSystem.GetSingleton().GetGameData().GetTutorialIndex() < 3) 
					TutorialController.GetSingleton().SetTutorialText(3);
				}
			GetInventory().Close();
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
