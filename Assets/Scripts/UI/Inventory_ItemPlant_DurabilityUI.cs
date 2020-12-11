using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_ItemPlant_DurabilityUI : Inventory_ItemPlantBaseUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		protected override bool UsePlant() {

			Debug.Log("El uso de la planta de durabilidad actualmente repara el arma que menos durabilidad tenga por falta de UI.");
			
			Rarity m_rarity = GetRarity();

			InventoryData m_iD = DataSystem.GetSingleton().GetGameData().GetInventoryData();
			List<WeaponEntityData> m_weapons = m_iD.GetAllWeapons();

			WeaponEntityData m_mostUsedWeapon = null;
			int m_maxUses = 100000;

			foreach(WeaponEntityData m_w in m_weapons) {

				if (m_w.GetUses() < m_maxUses) {
					
					m_mostUsedWeapon = m_w;
					m_maxUses = m_w.GetUses();
					}
				}

			if (m_mostUsedWeapon == null) {
				
				Debug.Log("No tienes ninguna arma");
				return false;
				}
			else {
				
				int m_lastUses = m_mostUsedWeapon.GetUses(); 

				m_iD.GetWeaponData(m_mostUsedWeapon.GetIndex()).SetUses(DataSystem.GetSingleton().GetWeapon(m_mostUsedWeapon.GetIndex()).GetUses());
				Debug.Log("Reparada el arma en el indice " + m_mostUsedWeapon.GetIndex() + " de la base de datos " + "(" + m_lastUses + "/" + DataSystem.GetSingleton().GetWeapon(m_mostUsedWeapon.GetIndex()).GetUses() + ").");
				return true;
				}
			}
        
		//Private Functions
		protected override Seed.SeedType GetPlantType() => Seed.SeedType.Durability;
        
        
	//Coroutines
	
	}
