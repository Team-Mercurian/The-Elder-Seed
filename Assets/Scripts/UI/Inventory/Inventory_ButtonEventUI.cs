using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_ButtonEventUI : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        [Header("Script")]
		[SerializeField] private Inventory_ItemUI m_itemController = null;

        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public void Use() {

			if (m_itemController == null) return;
			m_itemController.Use();
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
