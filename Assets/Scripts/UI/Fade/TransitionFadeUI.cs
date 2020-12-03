using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFadeUI : FadeControllerUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        [Header("Animation")]
		[SerializeField] private RectTransform m_rectTransform = null;
		[SerializeField] private AnimationCurve m_animationCurve = null;
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void SetValue(float value) {
			 
			float m_evaluation;
			float m_value;

			if (value < 0.5f) { 

				m_evaluation = m_animationCurve.Evaluate(value * 2);
				m_value = Mathf.Lerp(1792, 0, m_evaluation);

				m_rectTransform.offsetMin = new Vector2(m_value, m_rectTransform.offsetMin.y);
				m_rectTransform.offsetMax = new Vector2(m_value, m_rectTransform.offsetMax.y);
				}	
			
			else {
			
                m_evaluation = m_animationCurve.Evaluate((value * 2) - 1);
                m_value = Mathf.Lerp(0, -1792, m_evaluation);

                m_rectTransform.offsetMin = new Vector2(m_value, m_rectTransform.offsetMin.y);
                m_rectTransform.offsetMax = new Vector2(m_value, m_rectTransform.offsetMax.y);
				}
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
