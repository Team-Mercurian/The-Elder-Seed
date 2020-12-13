using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_Item_WeaponUI : Inventory_ItemUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
		[Header("Weapon References")]
		[SerializeField] private TextMeshProUGUI m_usesCount = null;
		
		private InventoryUI m_inventory = null;
        private WeaponEntityData m_entityData; 

    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public void SetValues(Weapon weapon, WeaponEntityData entityData, bool isOdd, InventoryUI inventory) {

			m_entityData = entityData;
			m_usesCount.text = entityData.GetUses() + "/" + weapon.GetUses();
			m_inventory = inventory;
			SetBaseValues(weapon, isOdd);
			}
		public override void Use() {

			m_inventory.GetWeaponStats().SetValues(m_entityData, m_inventory);
			m_inventory.GetWeaponStats().Open();
			}
        
		//Private Functions
        
        
	//Coroutines
	
	}