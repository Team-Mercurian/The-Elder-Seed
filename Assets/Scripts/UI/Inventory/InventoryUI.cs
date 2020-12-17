using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public abstract class InventoryUI : PanelUI {
	
	//Enumerators
	public enum Sections {

		Seeds,
		Weapons,
		Plants, 
		Potions,
		Miscellaneous
		}
	
	//Structs
	
	//Set Variables
	
		//Static
		private static Sections m_lastSection = Sections.Seeds;
        
		//No Static
		[Header("References")]
		[SerializeField] private Transform m_itemHolder = null;
		[Space]
		[SerializeField] private TextMeshProUGUI m_sectionsTitle = null;
		[SerializeField] private TextMeshProUGUI m_changeableTextA = null;
		[SerializeField] private TextMeshProUGUI m_changeableTextB = null;
		[SerializeField] private TextMeshProUGUI m_changeableTextC = null;
		[Space]
		[SerializeField] private WeaponStatsUI m_weaponStatsUI = null;
		[SerializeField] private ConfirmationUI m_confirmationUI = null;
		[SerializeField] private WeaponRepairUI m_weaponRepairUI = null;
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public void SetItemDatas(Sections section, bool overwrite) {

			if (!overwrite && section == m_lastSection) return; 

			//Destroy item holder childs.
			for(int i = 0; i < m_itemHolder.childCount; i ++) Destroy(m_itemHolder.GetChild(i).gameObject);

			SetSectionsChanges(section);
			m_lastSection = section;
			}
        
		public override void Open() {

			if (GameSystem.GetUI()) return;

			m_lastSection = SetStartSection();
			base.Open();
			Reset(m_lastSection);
            InputController.SetLookObject(null);
			GameSystem.SetUI(this);
			}
		public override void Close() {

			base.Close();
            InputController.SetLookObject(CameraController.GetSingleton());
			GameSystem.SetUI(null);
			}
			
		public void ChangeSection_Seeds() => SetItemDatas(Sections.Seeds, false);
		public void ChangeSection_Weapons() => SetItemDatas(Sections.Weapons, false);
		public void ChangeSection_Plants() => SetItemDatas(Sections.Plants, false);
		public void ChangeSection_Potions() => SetItemDatas(Sections.Potions, false);
		public void ChangeSection_Miscellaneous() => SetItemDatas(Sections.Miscellaneous, false);

		public void Reset() => SetItemDatas(m_lastSection, true);
		public void Reset(Sections section) => SetItemDatas(section, true);

		public Transform GetItemHolder() => m_itemHolder;

		public WeaponStatsUI GetWeaponStats() => m_weaponStatsUI;
		public ConfirmationUI GetConfirmationUI() => m_confirmationUI;
		public WeaponRepairUI GetRepairWeapon() => m_weaponRepairUI;

		//Private Functions        	
		protected abstract void SetSectionsChanges(Sections section);
		protected abstract Sections SetStartSection();

		private void SetTMText(TextMeshProUGUI tm, string text) => tm.text = text;

		protected void SetSectionText(string text) => SetTMText(m_sectionsTitle, text);
		protected void SetTextA(string text) => SetTMText(m_changeableTextA, text);
		protected void SetTextB(string text) => SetTMText(m_changeableTextB, text);
		protected void SetTextC(string text) => SetTMText(m_changeableTextC, text);

		protected void SetTexts(string sections, string a, string b, string c) {
			
			SetSectionText(sections);
			SetTextA(a);
			SetTextB(b);
			SetTextC(c);
			}

	//Coroutines
	
	}
