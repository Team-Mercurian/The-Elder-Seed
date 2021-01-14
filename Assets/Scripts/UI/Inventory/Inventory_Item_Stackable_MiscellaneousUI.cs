using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory_Item_Stackable_MiscellaneousUI : Inventory_Item_StackableUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void Use() {

			UnityEvent m_event = new UnityEvent();
			m_event.AddListener(() => SetMisc());

			ItemDescriptionUI.GetSingleton().SetData(GetItem(), GetCount(), m_event);
			ItemDescriptionUI.GetSingleton().Open();
			}

		public void SetMisc() {

			if (GetCount() <= 0) return;
			PlayerFarming.GetSingleton().ActiveMagicalFragments();
			
            if (DataSystem.GetSingleton().GetGameData().GetTutorialIndex() < 7) 
                TutorialController.GetSingleton().SetTutorialText(7);

			GetInventory().Close();
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
