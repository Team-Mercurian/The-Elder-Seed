using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeadPanel_ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("References")]
		[SerializeField] private GameObject m_lostSign = null;
		[SerializeField] private Image m_iconHolder = null;

        private string m_savedText;
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public void SetData(DeadPanelUI.LostItem item) {
			
			m_iconHolder.sprite = item.GetItem().GetIcon();
			m_lostSign.SetActive(item.GetLost());

			string m_itemString = "";
			string m_itemName = item.GetItem().GetName();

			switch(item.GetItem().GetRarity()) {
				
				case Rarity.Common : m_itemString = "<color=#ffffff>" + m_itemName + "</color>"; break;
				case Rarity.Rare : m_itemString = "<color=#0099db>" + m_itemName + "</color>"; break;
				case Rarity.Epic : m_itemString = "<color=#b55088>" + m_itemName + "</color>"; break;
				case Rarity.Legendary : m_itemString = "<color=#fee761>" + m_itemName + "</color>"; break;
				}

			m_savedText = m_itemString;
			}

		public void OnPointerEnter(PointerEventData m_data) => DeadPanelUI.GetSingleton().SelectItem(this, true);
		public void OnPointerExit(PointerEventData m_data) => DeadPanelUI.GetSingleton().SelectItem(this, false);
			
        public string GetTitle() => m_savedText;
        
		//Private Functions
        
        
	//Coroutines
	
	}
