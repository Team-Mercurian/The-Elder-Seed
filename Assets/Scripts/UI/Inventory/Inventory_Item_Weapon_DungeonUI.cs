using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_Item_Weapon_DungeonUI : Inventory_Item_WeaponUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("Dungeon Weapon References")]
		[SerializeField] private TextMeshProUGUI m_equipText = null;

    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions

    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void SetValues(Weapon weapon, WeaponEntityData entityData, bool isOdd, InventoryUI inventory) {
			
			base.SetValues(weapon, entityData,isOdd, inventory);
			m_equipText.text = DataSystem.GetSingleton().GetDungeonData().GetActualWeapon().GetIndex() == entityData.GetIndex() ? "X" : "";
			}	 
        
		//Private Functions   

	//Coroutines
	
	}
