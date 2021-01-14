using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_Item_Weapon_DISUI : Inventory_Item_WeaponUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("DIS Weapon References")]
		[SerializeField] private GameObject m_selectionImage = null;
        
		private bool m_selected = false;

    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions

    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void SetValues(Weapon weapon, WeaponEntityData entityData, bool isOdd, InventoryUI inventory) {
			
			base.SetValues(weapon, entityData,isOdd, inventory);

			m_selected = GetDefaultSavedCount();
			m_selectionImage.SetActive(m_selected);
			}	 
        
		//Private Functions        
		public override void Use() {

			m_selected = !m_selected;
			Select(m_selected);
			} 

		private void Select(bool select) {

			InventoryData m_iD = DataSystem.GetSingleton().GetDungeonData().GetInventoryData();

			if (select) {
				
				if (m_iD.GetWeaponList().Count == 0) DataSystem.GetSingleton().GetDungeonData().SetActualWeapon(GetWeaponEntity().GetIndex());
				m_iD.AddWeapon(GetWeaponEntity());
				}
				
			else m_iD.RemoveWeapon(GetWeaponEntity());
			m_selectionImage.SetActive(m_selected);
			}
        
		private bool GetDefaultSavedCount() => DataSystem.GetSingleton().GetDungeonData().GetInventoryData().GetWeaponData(GetWeaponEntity().GetIndex()) != null;

	//Coroutines
	
	}
