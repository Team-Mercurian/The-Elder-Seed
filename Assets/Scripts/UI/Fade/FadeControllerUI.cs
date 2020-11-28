using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FadeControllerUI : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		#if UNITY_EDITOR

        [Header("Values")]
		[SerializeField][Range(0, 1)] private float m_debugValue = 0;

		#endif

    //Functions
	
		//MonoBehaviour Functions.
		private void Awake() {

			DontDestroyOnLoad(gameObject);
			}

		#if UNITY_EDITOR

		private void OnValidate() {

			SetValue(m_debugValue);
			}

		#endif
        
        
		//Public Functions.
        public abstract void SetValue(float value);
        
		//Private Functions
        
        
	//Coroutines
	
	}
