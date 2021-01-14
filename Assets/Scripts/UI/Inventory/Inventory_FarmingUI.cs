using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Inventory_FarmingUI : InventoryUI {
	
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
		[SerializeField] private Inventory_DataCreatorUI m_miscCreator = null;
        
    //Functions
	
		//MonoBehaviour Functions
        
		//Public Functions
		public override void Open() {

			base.Open();

            if (DataSystem.GetSingleton().GetGameData().GetTutorialIndex() < 2) 
                TutorialController.GetSingleton().SetTutorialText(2);
            
            else if (DataSystem.GetSingleton().GetGameData().GetTutorialIndex() == 4) 
                TutorialController.GetSingleton().SetTutorialText(5);
			}

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
				case Sections.Miscellaneous :
				
					m_iDC = m_miscCreator; 
					SetMiscTexts();
					break;
				}
			
			if (m_iDC != null) m_iDC.SetData(DataSystem.GetSingleton().GetGameData().GetInventoryData());
			}

		protected void SetPlantsTexts() => SetTexts("Plantas", "", "Tienes", "Usar");
		protected void SetPotionsTexts() => SetTexts("Pociones", "", "", "Tienes");
		protected void SetSeedsTexts() => SetTexts("Semillas", "Equipado", "Tienes", "Inspeccionar");
		protected void SetWeaponsTexts() => SetTexts("Armas", "", "Durabilidad", "Inspeccionar");
		protected void SetMiscTexts() => SetTexts("Miscelaneo", "", "Tienes", "Inspeccionar");

		protected override Sections SetStartSection() => GetLastSection();

	//Coroutines
	
	}
