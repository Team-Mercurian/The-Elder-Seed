using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_Item_Stackable_DIS_PotionUI : Inventory_Item_Stackable_DISUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void ChangeCount(int add) {
			
			int m_savedCount = 0;

			List<ItemData> m_iD = DataSystem.GetSingleton().GetDungeonData().GetInventoryData().GetPotionList();

			for(int i = 0; i < m_iD.Count; i ++) m_savedCount += m_iD[i].GetCount();

			if (m_savedCount + add > GetMaxDungeonItem()) return;
			base.ChangeCount(add);
			}
        
		//Private Functions
        protected override void UpdateData(int count) {

			DataSystem.GetSingleton().GetDungeonData().GetInventoryData().GetPotionData(GetItem().GetID()).SetCount(count);
			}
        protected override int GetDefaultSavedCount() {
			
			return DataSystem.GetSingleton().GetDungeonData().GetInventoryData().GetPotionData(GetItem().GetID()).GetCount();			
			}	 
        
	//Coroutines
	
	}
