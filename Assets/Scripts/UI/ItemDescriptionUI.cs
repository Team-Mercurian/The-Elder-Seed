using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ItemDescriptionUI : PanelUI {
	
	//Singleton
	private static ItemDescriptionUI m_instance = null;
	private void Awake() => m_instance = this;
	public static ItemDescriptionUI GetSingleton() => m_instance;
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
        [Header("References")]
		[SerializeField] private Image m_iconHolder = null; 
		[SerializeField] private TextMeshProUGUI m_titleHolder = null; 
		[SerializeField] private TextMeshProUGUI m_countHolder = null; 
		[SerializeField] private TextMeshProUGUI m_descHolder = null; 
        [Space]
		[SerializeField] private Button m_equipButton = null;

		private UnityEvent m_event;

    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
        public void SetData(Item item, int count, UnityEvent equipEvent) {

			m_iconHolder.sprite = item.GetIcon();

			m_countHolder.text = "Disponibles en el inventario: " + count.ToString();
			m_titleHolder.text = item.GetName();
			m_descHolder.text = item.GetDescription();

			m_equipButton.interactable = count > 0;
			m_event = equipEvent;
			}
        public void Equip() {

			if (m_event == null) return;
			m_event.Invoke();
			Close();
			}

		//Private Functions
        
        
	//Coroutines
	
	}
