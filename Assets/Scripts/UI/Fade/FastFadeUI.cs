using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFadeUI : FadeControllerUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        [Header("References")]
		[SerializeField] private CanvasGroup m_canvasGroup = null;
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void SetValue(float value) {
			 
			if (m_canvasGroup == null) return;

			value = Mathf.Clamp(value, 0, 1);

			if (value <= 0.5f) m_canvasGroup.alpha = Mathf.Lerp(0, 1, value * 2f);
			else m_canvasGroup.alpha = Mathf.Lerp(1, 0, (value * 2) - 1);
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
