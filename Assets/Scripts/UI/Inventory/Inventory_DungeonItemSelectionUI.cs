using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_DungeonItemSelectionUI : InventoryUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("References")]
		[SerializeField] private Inventory_DataCreatorUI m_plantCreator = null;
		[SerializeField] private Inventory_DataCreatorUI m_weaponCreator = null;
		[SerializeField] private Inventory_DataCreatorUI m_potionCreator = null;
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
        
        
		//Private Functions
		protected override void SetSectionsChanges(Sections section) {
			
			Inventory_DataCreatorUI m_iDC = null;

			switch(section) {

				case Sections.Weapons :

					m_iDC = m_weaponCreator; 
					SetWeaponsTexts();
					break;
				case Sections.Plants : 
				
					m_iDC = m_plantCreator; 
					SetPlantsTexts();
					break;
				case Sections.Potions : 
				
					m_iDC = m_potionCreator; 
					SetPotionsTexts();
					break;
				}
			
			if (m_iDC != null) m_iDC.SetData(DataSystem.GetSingleton().GetGameData().GetInventoryData());
			}
			
		protected void SetPlantsTexts() => SetTexts("Plantas", "Disponibles", "Llevar", "Maximo 5");
		protected void SetPotionsTexts() => SetTexts("Pociones", "Disponibles", "Llevar", "Maximo 5");
		protected void SetWeaponsTexts() => SetTexts("Armas", "", "Durabilidad", "Llevar");

        public void EnterToRuins() {
			
			if (DataSystem.GetSingleton().GetDungeonData().GetInventoryData().GetWeaponList().Count == 0) return;

		 	DataSystem.GetSingleton().GetGameData().GetInventoryData().RemoveDungeonItems(DataSystem.GetSingleton().GetDungeonData().GetInventoryData());
			DataSystem.GetSingleton().GoToRuins();
			}

		protected override Sections SetStartSection() => Sections.Weapons;

	//Coroutines
	
	}
