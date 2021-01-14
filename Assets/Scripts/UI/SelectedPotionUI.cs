using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedPotionUI : MonoBehaviour {
	
	//Singleton
	private static SelectedPotionUI m_instance = null;
	private void Awake() => m_instance = this;
	public static SelectedPotionUI GetSingleton() => m_instance;
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
        [Header("References")]
		[SerializeField] private Image m_potionIcon = null;
		[SerializeField] private TextMeshProUGUI m_countText = null;

    //Functions
	
		//MonoBehaviour Functions
		
		//Public Functions
        public void SetData(Potion potion, int count) {

			if (potion != null) {
				
				m_potionIcon.gameObject.SetActive(true);
				m_potionIcon.sprite = potion.GetIcon();
				m_countText.text = count.ToString();
				}

			else {
				
				m_potionIcon.gameObject.SetActive(false);
				m_countText.text = "";
				}
			}
        
		//Private Functions
        
	//Coroutines
	
	}
