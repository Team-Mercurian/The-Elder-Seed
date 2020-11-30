using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectCollisionEnter : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[SerializeField] private UnityEvent<Collision> m_event = null; 
        
        
    //Functions
	
		//MonoBehaviour Functions
		private void OnCollisionEnter(Collision collision) {
			
			m_event.Invoke(collision);
			}
        
        
		//Public Functions
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
