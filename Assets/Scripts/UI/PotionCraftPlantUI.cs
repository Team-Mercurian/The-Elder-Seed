using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PotionCraftPlantUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
	//Singleton
	//private static PotionCraftPlantUI m_instance = null;
	//private void Awake() => m_instance = this;
	//public static PotionCraftPlantUI GetSingleton() => m_instance;
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
		[Header("References")]
		[SerializeField] private Plant m_plant = null;
		[SerializeField] private Image m_iconHolder = null;
		[SerializeField] private TextMeshProUGUI m_countHolder = null;
		[SerializeField] private Button m_button = null;
        [SerializeField] private PotionCraftUI m_potionCraft = null;
        
		private int m_count = 0;

    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public void SetData() {

			m_iconHolder.sprite = m_plant.GetIcon();
			SetCount();
			}
        public void SetCount() {

			m_count = DataSystem.GetSingleton().GetGameData().GetInventoryData().GetPlantData(m_plant.GetID()).GetCount();
			m_countHolder.text = m_count.ToString();

			if (m_count == 0) m_button.interactable = false;
			}	
		public void SelectPlant() {

			m_potionCraft.SelectPlant(this, m_count);
			}

		public void OnPointerEnter(PointerEventData data) {

			m_potionCraft.HoverPlant(m_plant.GetName());
			}
		public void OnPointerExit(PointerEventData data) {
			
			m_potionCraft.UnHoverPlant();
			}

		public void Select() => transform.localScale = Vector3.one * 1.2f;
		public void UnSelect() => transform.localScale = Vector3.one;

		public Plant GetPlant() => m_plant;
		public int GetCount() => m_count;

		//Private Functions
        
        
	//Coroutines
	
	}
