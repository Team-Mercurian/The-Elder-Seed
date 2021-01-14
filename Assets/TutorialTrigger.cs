using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {
	
	//Singleton
	//private static TutorialTrigger m_instance = null;
	//private void Awake() => m_instance = this;
	//public static TutorialTrigger GetSingleton() => m_instance;
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
		[Header("Values")]
        [SerializeField] private int m_tutorialIndex = 0;
        
    //Functions
	
		//MonoBehaviour Functions
        private void OnTriggerEnter(Collider collider) {

			if (collider.CompareTag("Player")) {

				if (DataSystem.GetSingleton().GetGameData().GetTutorialIndex() < m_tutorialIndex) 
					TutorialController.GetSingleton().SetTutorialText(m_tutorialIndex);
				}
			}
        
		//Public Functions
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
