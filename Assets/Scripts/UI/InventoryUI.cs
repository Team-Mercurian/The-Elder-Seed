using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : PanelUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("References")]
		[SerializeField] private Transform m_itemHolder = null;

		[Header("Prefabs")]
		[SerializeField] private GameObject m_durabilityPlant = null;
		[SerializeField] private GameObject m_unlockPlant = null;
		[SerializeField] private GameObject m_potionPlant = null;
        
    //Functions
	
		//MonoBehaviour Functions
		protected override void Start() {
			
			base.Start();

			SetItemDatas();
			}
        
        
		//Public Functions
		public void SetItemDatas() {

			//Destroy item holder childs.
			for(int i = 0; i < m_itemHolder.childCount; i ++) Destroy(m_itemHolder.GetChild(i).gameObject);

			//Create updated childs.
			List<ItemData> m_iD = DataSystem.GetSingleton().GetGameData().GetInventoryData().GetPlants();

			for(int i = 0; i < m_iD.Count; i ++) {
				
				GameObject m_plant = null;
				Seed m_item = DataSystem.GetSingleton().GetSeed(m_iD[i].GetIndex());

				switch(m_item.GetSeedType()) {

					case Seed.SeedType.Durability : 

						m_plant = m_durabilityPlant;
						break;

					case Seed.SeedType.Unlock : 

						m_plant = m_unlockPlant;
						break;

					case Seed.SeedType.Potion : 

						m_plant = m_potionPlant;
						break;
					}

				Instantiate(m_plant, m_itemHolder).GetComponent<Inventory_ItemBaseUI>().SetValues(m_item, m_iD[i].GetCount(), (i % 2) < 1);
				}
			}

		//Private Functions
        
        
	//Coroutines
	
	}
