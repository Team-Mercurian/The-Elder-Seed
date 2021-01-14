using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedWeaponUI : MonoBehaviour {
	
	//Singleton
	private static SelectedWeaponUI m_instance = null;
	private void Awake() => m_instance = this;
	public static SelectedWeaponUI GetSingleton() => m_instance;
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
        [Header("References")]
		[SerializeField] private Image m_swordIcon = null;
        [SerializeField] private TextMeshProUGUI m_progressText = null;
        [SerializeField] private Transform m_progressFill = null;
        [SerializeField] private RectTransform m_progressBar = null;

    //Functions
	
		//MonoBehaviour Functions
		
		//Public Functions
        public void SetData(Weapon weapon, int uses) {

			m_swordIcon.sprite = weapon.GetIcon();
			if (m_progressBar != null) m_progressBar.sizeDelta = new Vector2(16, 256 * (weapon.GetUses() / 125f));
			if (m_progressText != null) m_progressText.text = uses.ToString() + "/" + weapon.GetUses().ToString();
			m_progressFill.localScale = new Vector3(1, (float) uses / weapon.GetUses(), 1);
			}
        
		//Private Functions
        
        
	//Coroutines
	
	}
