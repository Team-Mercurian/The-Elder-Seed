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

        private WeaponEntityData m_entityData; 

    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public virtual void SetValues(Weapon weapon, WeaponEntityData entityData, bool isOdd, InventoryUI inventory) {

			m_entityData = entityData;
			
			if (m_usesCount != null) m_usesCount.text = entityData.GetUses() + "/" + weapon.GetUses();
			SetBaseValues(weapon, isOdd, inventory);
			}
		public override void Use() {

			GetInventory().GetWeaponStats().SetValues(m_entityData, GetInventory());
			GetInventory().GetWeaponStats().Open();
			}
        
		//Private Functions
        protected WeaponEntityData GetWeaponEntity() => m_entityData;
        
	//Coroutines
	
	}