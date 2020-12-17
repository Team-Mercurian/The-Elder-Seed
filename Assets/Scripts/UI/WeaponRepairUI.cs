using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponRepairUI : PanelUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
		[Header("References")]
		[SerializeField] private Transform m_itemHolder = null;
		[SerializeField] private GameObject m_specialWeaponItem = null;
		[SerializeField] private InventoryUI m_inventory = null;
		[SerializeField] private TextMeshProUGUI m_countText = null;
        
		[Header("Values")]
		[SerializeField] private bool m_dungeonRepair = false;

		private Rarity m_rarity;
		private InventoryData m_inventoryData;

    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public void SetItemDatas(Rarity rarity) {

			m_rarity = rarity;
			m_inventoryData = m_dungeonRepair ? DataSystem.GetSingleton().GetDungeonData().GetInventoryData() : DataSystem.GetSingleton().GetGameData().GetInventoryData();

			//Destroy item holder childs.
			for(int i = 0; i < m_itemHolder.childCount; i ++) Destroy(m_itemHolder.GetChild(i).gameObject);
			CreateWeapons(rarity);

			Plant m_plant = DataSystem.GetSingleton().GetPlants().Find(c => c.GetRarity() == rarity);
			int m_count = m_inventoryData.GetPlantData(m_plant.GetID()).GetCount();

			string m_text = "";

			if (m_count == 0) m_text = "No tienes plantas de reparacion.";
			else if (m_count == 1) m_text = "Tienes una planta de reparacion.";
			else m_text = "Tienes " + m_count + " plantas de reparacion.";

			m_countText.text = m_text;
			}

		public void Reset() => SetItemDatas(m_rarity); 
		public void Reset(Rarity rarity) => SetItemDatas(rarity);
		
		public void Open(Rarity rarity) {

			base.Open();
			Reset(rarity);
			}

		public override void Close() {

			base.Close();
			m_inventory.Reset();
			}
			
		public void UsePlant() {
			
			int m_plantID = DataSystem.GetSingleton().GetPlants().Find(c => c.GetRarity() == m_rarity).GetID();
			m_inventoryData.GetPlantData(m_plantID).AddCount(-1);
			}
        
		//Private Functions
		private void CreateWeapons(Rarity rarity) {	
			
			List<Weapon> m_weapons = DataSystem.GetSingleton().GetWeapons();
			List<WeaponEntityData> m_iD = m_inventoryData.GetWeaponList();
			m_iD = m_iD.FindAll(c => DataSystem.GetSingleton().GetWeapon(c.GetID()).GetRarity() == rarity);

			m_iD = m_iD.OrderBy(c => c.GetUses()).ThenBy(c => m_weapons[c.GetID()].GetName()).ToList();

			for(int i = 0; i < m_iD.Count; i ++) {
				
				Weapon m_item = DataSystem.GetSingleton().GetWeapon(m_iD[i].GetID());
				Instantiate(m_specialWeaponItem, m_itemHolder).GetComponent<WeaponRepair_WeaponUI>().SetValues(m_item, m_iD[i], (i % 2) < 1, m_inventory);
				}
			}
        
        
	//Coroutines
	
	}
