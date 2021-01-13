using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory_DataCreatorUI : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("Prefabs")]
		[SerializeField] private InventoryUI m_inventory = null;
		[SerializeField] private GameObject m_item = null;
                
    //Functions
	
		//MonoBehaviour Functions
		private void OnValidate() {

			if (m_inventory == null) TryGetComponent<InventoryUI>(out m_inventory);
			}
        
        
		//Public Functions
		public void SetData(InventoryData inventoryData) {
			
			CreateEntity(inventoryData);
			}

		//Private Functions
        protected GameObject GetItemPrefab() => m_item;
		protected InventoryUI GetInventory() => m_inventory;
		
		protected Transform GetItemHolder() => GetInventory().GetItemHolder();

		protected abstract void CreateEntity(InventoryData inventoryData);
        
        
	//Coroutines
	
	}
