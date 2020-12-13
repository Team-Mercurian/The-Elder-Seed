using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Inventory_ItemUI : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
        [Header("References")]
		[SerializeField] private Image m_itemBackground = null;
		[SerializeField] private Image m_icon = null;
		[SerializeField] private TextMeshProUGUI m_nameHolder = null;
        
		private Item m_item;

    //Functions
	
		//MonoBehaviour Functions
        
		//Public Functions
		public abstract void Use();

		//Private Functions
		protected void SetBaseValues(Item item, bool isOdd) {

			SetItem(item);

			SetIcon(item.GetIcon());
			SetName(item.GetName());
						
			float m_alpha = isOdd ? 0.1f : 0.25f;
			SetBackgroundAlpha(m_alpha);
			}

		protected void SetBackgroundAlpha(float value) => m_itemBackground.color = new Color(m_itemBackground.color.r, m_itemBackground.color.g, m_itemBackground.color.b, value);
		protected void SetIcon(Sprite sprite) => m_icon.sprite = sprite;
		protected void SetName(string name) => m_nameHolder.text = name;

		protected void SetItem(Item item) => m_item = item;
		protected Item GetItem() => m_item;
        
        
	//Coroutines
	
	}
