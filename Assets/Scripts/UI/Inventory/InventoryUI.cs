using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class InventoryUI : PanelUI {
	
	//Enumerators
	public enum Sections {

		Seeds,
		Weapons,
		Plants, 
		Potions,
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
		[SerializeField] private TextMeshProUGUI m_changeableText = null;
		[Space]
		[SerializeField] private WeaponStatsUI m_weaponStatsUI = null;

		[Header("Prefabs")]
		[SerializeField] private GameObject m_plantItem = null;
		[Space]
		[SerializeField] private GameObject m_seedItem = null;
		[SerializeField] private GameObject m_weaponItem = null;
		[SerializeField] private GameObject m_potionItem = null;
        
    //Functions
	
		//MonoBehaviour Functions
		protected override void Start() {
			
			base.Start();
			SetItemDatas(m_lastSection, true);
			}
        
        
		//Public Functions
		public void SetItemDatas(Sections section, bool overwrite) {

			if (!overwrite && section == m_lastSection) return; 

			//Destroy item holder childs.
			for(int i = 0; i < m_itemHolder.childCount; i ++) Destroy(m_itemHolder.GetChild(i).gameObject);

			switch(section) {

				case Sections.Seeds : CreateSeeds(); break;
				case Sections.Weapons : CreateWeapons(); break;
				case Sections.Plants : CreatePlants(); break;
				case Sections.Potions : CreatePotions(); break;
				}
			
			m_lastSection = section;
			}

		public void Reset(Sections section) {

			SetItemDatas(section, true);
			}

		public override void Open() {

			base.Open();
            InputController.SetLookObject(null);
            CursorController.GetSingleton().SetVisibility(true);
			}
		public override void Close() {

			base.Close();
            InputController.SetLookObject(CameraController.GetSingleton());
            CursorController.GetSingleton().SetVisibility(false);
			}

		public void ChangeSection_Seeds() => SetItemDatas(Sections.Seeds, false);
		public void ChangeSection_Weapons() => SetItemDatas(Sections.Weapons, false);
		public void ChangeSection_Plants() => SetItemDatas(Sections.Plants, false);
		public void ChangeSection_Potions() => SetItemDatas(Sections.Potions, false);

		public WeaponStatsUI GetWeaponStats() => m_weaponStatsUI;

		//Private Functions
		private void CreateWeapons() {	

			m_changeableText.text = "Durabilidad";
			m_sectionsTitle.text = "Armas";
			
			List<WeaponEntityData> m_iD = DataSystem.GetSingleton().GetGameData().GetInventoryData().GetWeaponList();
			List<Weapon> m_weapons = DataSystem.GetSingleton().GetWeapons();

			m_iD = m_iD.OrderBy(c => c.GetUses()).ThenBy(c => m_weapons[c.GetID()].GetRarity()).ToList();

			for(int i = 0; i < m_iD.Count; i ++) {
				
				Weapon m_item = DataSystem.GetSingleton().GetWeapon(m_iD[i].GetID());
				Instantiate(m_weaponItem, m_itemHolder).GetComponent<Inventory_Item_WeaponUI>().SetValues(m_item, m_iD[i], (i % 2) < 1, this);
				}
			}

        private void CreateSeeds() {

			m_sectionsTitle.text = "Semillas";

			DataSystem m_ds = DataSystem.GetSingleton();

			List<ItemData> m_iD = m_ds.GetGameData().GetInventoryData().GetSeedList().FindAll(c => c.GetUnlocked());
			List<Item> m_items = m_ds.GetSeeds().Cast<Item>().ToList();

			m_iD = m_iD.OrderBy(c => m_ds.GetSeed(c.GetID()).GetSeedType()).ThenBy(c => m_ds.GetSeed(c.GetID()).GetRarity()).ToList();


			CreateStackableItems(m_seedItem, m_iD, m_items);
			}
		private void CreatePlants() {

			m_sectionsTitle.text = "Plantas";

			DataSystem m_ds = DataSystem.GetSingleton();

			List<ItemData> m_iD = m_ds.GetGameData().GetInventoryData().GetPlantList().FindAll(c => c.GetUnlocked());
			List<Item> m_items = m_ds.GetPlants().Cast<Item>().ToList();

			m_iD = m_iD.OrderBy(c => m_ds.GetPlant(c.GetID()).GetSeedType()).ThenBy(c => m_ds.GetPlant(c.GetID()).GetRarity()).ToList();

			CreateStackableItems(m_plantItem, m_iD, m_items);
			}
		private void CreatePotions() {
			
			m_sectionsTitle.text = "Pociones";

			List<ItemData> m_iD = DataSystem.GetSingleton().GetGameData().GetInventoryData().GetPotionList().FindAll(c => c.GetUnlocked());
			List<Item> m_items = DataSystem.GetSingleton().GetPotions().Cast<Item>().ToList();

			m_iD = m_iD.OrderBy(c => m_items[c.GetID()].GetRarity()).ToList();

			CreateStackableItems(m_potionItem, m_iD, m_items);
			}
        
		private void CreateStackableItems(GameObject itemUI, List<ItemData> itemDatas, List<Item> items) {

			m_changeableText.text = "Tienes";

			for(int i = 0; i < itemDatas.Count; i ++) {
				
				ItemData m_item = itemDatas[i];
				Instantiate(itemUI, m_itemHolder).GetComponent<Inventory_Item_StackableUI>().SetValues(items.Find(c => c.GetID() == m_item.GetID()), m_item.GetCount(), (i % 2) < 1);
				}
			}

	//Coroutines
	
	}
