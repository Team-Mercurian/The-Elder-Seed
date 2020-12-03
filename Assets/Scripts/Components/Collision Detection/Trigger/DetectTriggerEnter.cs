using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectTriggerEnter : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[SerializeField] private UnityEvent<Collider> m_event = null; 
        
        
    //Functions
	
		//MonoBehaviour Functions
		private void OnTriggerEnter(Collider collider) {
			
			m_event.Invoke(collider);
			}
        
        
		//Public Functions
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
