using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class WeaponRepair_WeaponUI : Inventory_ItemUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("Weapon References")]
		[SerializeField] private TextMeshProUGUI m_countText = null;
		[SerializeField] private Button m_button = null;
        
		WeaponEntityData m_weaponEntity;
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public void SetValues(Weapon weapon, WeaponEntityData entityData, bool isOdd, InventoryUI inventory) {

			m_weaponEntity = entityData;
			m_countText.text = entityData.GetUses() + "/" + weapon.GetUses();
			m_button.interactable = entityData.GetUses() < weapon.GetUses() ;
			SetBaseValues(weapon, isOdd, inventory);
			}
		public override void Use() {

			string m_t = "¿Deseas reparar esta arma?";
			
			UnityEvent m_rightEvent = new UnityEvent();
			m_rightEvent.AddListener(RepairWeapon);

			ButtonEvent m_leftButtonEvent = new ButtonEvent("No", null);
			ButtonEvent m_rightButtonEvent = new ButtonEvent("Si", m_rightEvent);

			GetInventory().GetConfirmationUI().SetData(m_t, m_leftButtonEvent, m_rightButtonEvent, false);
			GetInventory().GetConfirmationUI().Open();
			}
        
		//Private Functions
		private void RepairWeapon() {

			m_weaponEntity.SetUses(DataSystem.GetSingleton().GetWeapon(m_weaponEntity.GetID()).GetUses());

			GetInventory().GetRepairWeapon().UsePlant();
			GetInventory().GetRepairWeapon().Reset();
			SaveSystem.Save();
			}
        
        
	//Coroutines
	
	}
