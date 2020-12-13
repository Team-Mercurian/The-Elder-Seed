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

		[Header("Buttons")]
		[SerializeField] private Button m_buttonDestroy = null;
        
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
			m_actualDamage.text = "Daño actual: " + m_weapon.GetCalculatedDamage(m_weaponEntity.GetUses()).ToString();
			m_criticalProbability.text = "Probabilidad de Critico: " + m_weapon.GetCriticalProbability().ToString();
			m_knockbackForce.text = "Empuje: " + (m_weapon.GetKnockbackForce() * m_weapon.GetKnockbackTime()).ToString();

			m_buttonDestroy.interactable = DataSystem.GetSingleton().GetGameData().GetInventoryData().GetWeaponList().Count > 1;	
			}

		public void LeftEvent() {
			
			//Destroy
			int m_mFC = (int)DataSystem.GetSingleton().GetWeapon(m_weaponEntity.GetID()).GetRarity() + 1;
			string m_t = "¿Deseas convertir esta arma en " + m_mFC + " " + (m_mFC == 1 ? "fragmento magico" : "fragmentos magicos") + "?";
			
			UnityEvent m_rightEvent = new UnityEvent();
			m_rightEvent.AddListener(() => DestroyWeapon(m_mFC));

			ButtonEvent m_leftButtonEvent = new ButtonEvent("No", null);
			ButtonEvent m_rightButtonEvent = new ButtonEvent("Si", m_rightEvent);

			m_inventory.GetConfirmationUI().SetData(m_t, m_leftButtonEvent, m_rightButtonEvent);
			m_inventory.GetConfirmationUI().Open();
			}
		public void RightEvent() {
			
			//Equip
			DataSystem.GetSingleton().GetGameData().GetInventoryData().SetActualWeapon(m_weaponEntity.GetIndex());
			m_inventory.Reset(InventoryUI.Sections.Weapons);
			Close();
			}
        
		//Private Functions
        private void DestroyWeapon(int magicalFragments) {
			
			DataSystem.GetSingleton().GetGameData().GetInventoryData().RemoveWeapon(m_weaponEntity.GetIndex(), magicalFragments);
			m_inventory.Reset(InventoryUI.Sections.Weapons);
			Close();
			}
        
	//Coroutines
	
	}
