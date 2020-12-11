using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Inventory_ItemBaseUI : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
        [Header("References")]
		[SerializeField] private Image m_itemBackground = null;
		[SerializeField] private Image m_icon = null;
		[SerializeField] private TextMeshProUGUI m_nameHolder = null;
		[SerializeField] private TextMeshProUGUI m_countHolder = null;
        
		private Item m_item;
		private int m_count;

    //Functions
	
		//MonoBehaviour Functions
        
		//Public Functions
        public virtual void SetValues(Item item, int count, bool isOdd) {
			
			m_item = item;

			m_icon.sprite = item.GetIcon();
			m_nameHolder.text = item.GetName();
			
			SetCount(count);
			
			float m_alpha = isOdd ? 0.1f : 0.25f;
			m_itemBackground.color = new Color(0, 0, 0, m_alpha);
			}

		public abstract void Use();

		public void SubtractCount(int count) {
			
			m_count -= count;
			SetCount(m_count);
			}
        public void SetCount(int count) {
			
			m_count = Mathf.Clamp(count, 0, 10000);
			m_countHolder.text = m_count.ToString();
			}

		//Private Functions
		protected Rarity GetRarity() => m_item.GetRarity();
        
        
	//Coroutines
	
	}
