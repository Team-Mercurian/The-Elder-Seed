using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Inventory_Item_Stackable_DISUI : Inventory_Item_StackableUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("DIS References")]
		[SerializeField] private TextMeshProUGUI m_dungeonCountText = null;
		
		[Header("DIS Values")]
		[SerializeField] private int m_maxDungeonItem = 5;

		private int m_dungeonCount = 0;
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void SetValues(Item item, int count, bool isOdd, InventoryUI inventory) {

			base.SetValues(item, count, isOdd, inventory);
			m_dungeonCount = GetDefaultSavedCount();
			SetDungeonCount(m_dungeonCount);
			}
        
		//Private Functions
		public virtual void ChangeCount(int add) {
			
			if (m_dungeonCount + add > m_maxDungeonItem || m_dungeonCount + add < 0 || m_dungeonCount + add > GetCount()) return;
			m_dungeonCount += add;
			SetDungeonCount(m_dungeonCount);
			UpdateData(m_dungeonCount);
			}

		public void Button_Add() => ChangeCount(1);
		public void Button_Remove() => ChangeCount(-1);

		protected abstract void UpdateData(int count);
		public override void Use() {} //Unnecessary Method
        
		private void SetDungeonCount(int count) {
						
			m_dungeonCountText.text = count.ToString();
			}
		
		protected int GetMaxDungeonItem() => m_maxDungeonItem;
		protected int GetDungeonCount() => m_dungeonCount;
		
		protected abstract int GetDefaultSavedCount();
        
	//Coroutines
	
	}
