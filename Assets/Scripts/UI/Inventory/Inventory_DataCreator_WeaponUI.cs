using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory_DataCreator_WeaponUI : Inventory_DataCreatorUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
        
		//Private Functions
		protected override void CreateEntity(InventoryData inventoryData) {	
			
			List<WeaponEntityData> m_iD = inventoryData.GetWeaponList();
			List<Weapon> m_weapons = DataSystem.GetSingleton().GetWeapons();

			if (DataSystem.GetSingleton().GetDungeonData() != null) m_iD = m_iD.OrderBy(c => c.GetIndex() != DataSystem.GetSingleton().GetDungeonData().GetActualWeaponIndex()).ThenByDescending(c => c.GetUses()).ThenByDescending(c => m_weapons[c.GetID()].GetRarity()).ToList();
			else m_iD = m_iD.OrderByDescending(c => c.GetUses()).ThenByDescending(c => m_weapons[c.GetID()].GetRarity()).ToList();

			for(int i = 0; i < m_iD.Count; i ++) {
				
				Weapon m_item = DataSystem.GetSingleton().GetWeapon(m_iD[i].GetID());
				
				Inventory_Item_WeaponUI m_wUI = Instantiate(GetItemPrefab(), GetItemHolder()).GetComponent<Inventory_Item_WeaponUI>();
				m_wUI.SetValues(m_item, m_iD[i], (i % 2) < 1, GetInventory());
				}
			}
        
        
	//Coroutines
	
	}
