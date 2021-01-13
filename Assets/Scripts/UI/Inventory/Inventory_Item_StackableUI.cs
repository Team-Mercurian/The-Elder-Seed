using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Inventory_Item_StackableUI : Inventory_ItemUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("Stackable References")]
		[SerializeField] private TextMeshProUGUI m_countHolder = null;
		[SerializeField] private Button m_button = null;
        
		private int m_count;
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
        public virtual void SetValues(Item item, int count, bool isOdd, InventoryUI inventory) {
			
			SetBaseValues(item, isOdd, inventory);
			SetCount(count);
			}

		public void SubtractCount(int count) {
			
			m_count -= count;
			SetCount(m_count);
			}
        public void SetCount(int count) {
			
			m_count = Mathf.Clamp(count, 0, 10000);
			m_countHolder.text = m_count.ToString();
			m_button.interactable = count > 0;
			}
		public int GetCount() => m_count;
        
		//Private Functions
		protected void SetInteractable(bool interactable) => m_button.interactable = interactable;
        
        
	//Coroutines
	
	}
