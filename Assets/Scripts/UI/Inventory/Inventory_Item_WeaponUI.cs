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
		[SerializeField] private TextMeshProUGUI m_equipText = null;
		[SerializeField] private TextMeshProUGUI m_usesCount = null;
        private WeaponEntityData m_entityData; 

    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public void SetValues(Weapon weapon, WeaponEntityData entityData, bool isOdd, InventoryUI inventory) {

			m_entityData = entityData;
			m_usesCount.text = entityData.GetUses() + "/" + weapon.GetUses();

			m_equipText.text = DataSystem.GetSingleton().GetGameData().GetInventoryData().GetActualWeaponIndex() == entityData.GetIndex() ? "X" : "";
			SetBaseValues(weapon, isOdd, inventory);
			}
		public override void Use() {

			GetInventory().GetWeaponStats().SetValues(m_entityData, GetInventory());
			GetInventory().GetWeaponStats().Open();
			}
        
		//Private Functions
        
        
	//Coroutines
	
	}