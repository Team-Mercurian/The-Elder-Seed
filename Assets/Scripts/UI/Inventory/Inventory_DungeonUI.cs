using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_DungeonUI : InventoryUI {
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
		//No Static
		[Header("References")]
		[SerializeField] private Inventory_DataCreatorUI m_seedCreator = null;
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

				case Sections.Seeds : 
				
					m_iDC = m_seedCreator; 
					SetSeedsTexts();
					break;
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
			
			if (m_iDC != null) m_iDC.SetData(DataSystem.GetSingleton().GetDungeonData().GetInventoryData());
			}

		protected void SetPlantsTexts() => SetTexts("Plantas", "", "Tienes", "Usar");
		protected void SetPotionsTexts() => SetTexts("Pociones", "Curacion", "Tienes", "Usar");
		protected void SetSeedsTexts() => SetTexts("Semillas", "", "", "Tienes");
		protected void SetWeaponsTexts() => SetTexts("Armas", "Equipado", "Durabilidad", "Inspeccionar");

		protected override Sections SetStartSection() => Sections.Weapons;

	//Coroutines
	
	}
