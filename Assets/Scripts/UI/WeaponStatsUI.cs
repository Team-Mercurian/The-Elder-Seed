using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class WeaponStatsUI : PanelUI, IHasTwoOptionsUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
		[Header("Stats References")]
		[SerializeField] private Image m_icon = null;
		[SerializeField] private TextMeshProUGUI m_title = null;
		[SerializeField] private TextMeshProUGUI m_baseDamage = null;
		[SerializeField] private TextMeshProUGUI m_actualUses = null;
		[SerializeField] private TextMeshProUGUI m_actualDamage = null;
		[SerializeField] private TextMeshProUGUI m_criticalProbability = null;
		[SerializeField] private TextMeshProUGUI m_knockbackForce = null;
        
        private WeaponEntityData m_weaponEntity;
		private InventoryUI m_inventory;

    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public void SetValues(WeaponEntityData weaponData, InventoryUI inventory) {

			Weapon m_weapon = DataSystem.GetSingleton().GetWeapon(weaponData.GetID());
			m_weaponEntity = weaponData;

			m_inventory = inventory;

			m_icon.sprite = m_weapon.GetIcon();
			m_title.text = m_weapon.GetName();
			m_baseDamage.text = "Daño base: " + m_weapon.GetBaseDamage().ToString();
			m_actualUses.text = "Durabilidad: " + weaponData.GetUses() + "/" + m_weapon.GetUses();
			m_actualDamage.text = "Daño actual: " + m_weapon.GetCalculatedDamage(m_weaponEntity.GetUses(), false).ToString();
			m_criticalProbability.text = "Probabilidad de Critico: " + m_weapon.GetCriticalProbability().ToString();
			m_knockbackForce.text = "Empuje: " + (m_weapon.GetKnockbackForce() * m_weapon.GetKnockbackTime()).ToString();
			}

		public void LeftEvent() {
			
			bool m_a = DataSystem.GetSingleton().GetGameData().GetInventoryData().GetWeaponList().Count > 1;
			if (m_a) {

				//Destroy
				int m_mFC = (int)DataSystem.GetSingleton().GetWeapon(m_weaponEntity.GetID()).GetRarity() + 1;
				string m_t = "¿Deseas convertir esta arma en " + m_mFC + " " + (m_mFC == 1 ? "fragmento magico" : "fragmentos magicos") + "?";
				
				UnityEvent m_rightEvent = new UnityEvent();
				m_rightEvent.AddListener(() => DestroyWeapon(m_mFC));

				ButtonEvent m_leftButtonEvent = new ButtonEvent("No", null);
				ButtonEvent m_rightButtonEvent = new ButtonEvent("Si", m_rightEvent);

				m_inventory.GetConfirmationUI().SetData(m_t, m_leftButtonEvent, m_rightButtonEvent, false);
				m_inventory.GetConfirmationUI().Open();
				}	

			else {

				WarningPanelUI.GetSingleton().SetData("Debes tener minimo un arma en tu inventario.", "Cerrar");
				WarningPanelUI.GetSingleton().Open();
				}
			}
		
		public void RightEvent() {

			//Equip
			DataSystem m_ds = DataSystem.GetSingleton();
			m_ds.GetDungeonData().SetActualWeapon(m_weaponEntity.GetIndex());
            PlayerBrain.GetSingleton().GetAttack().SetWeapon(m_ds.GetWeapon(m_ds.GetDungeonData().GetActualWeapon().GetID()));
			SelectedWeaponUI.GetSingleton().SetData(m_ds.GetActualWeapon(), m_ds.GetDungeonData().GetActualWeapon().GetUses());
			m_inventory.Reset(Inventory_FarmingUI.Sections.Weapons);
			Close();
			}
        
		//Private Functions
        private void DestroyWeapon(int magicalFragments) {
		
			DataSystem.GetSingleton().GetGameData().GetInventoryData().RemoveWeapon(m_weaponEntity.GetIndex(), magicalFragments);
			m_inventory.Reset(Inventory_FarmingUI.Sections.Weapons);
			Close();
			}
        
	//Coroutines
	
	}
