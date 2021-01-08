using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMapController : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
		
        
        
		//No Static
		[Header("References")]
		[SerializeField] private MapController m_smallMap = null;
		[SerializeField] private MapController m_bigMap = null;
        
        private bool m_isBigMap = false;
		private bool m_isInAnimation = false;

    //Functions
	
		//MonoBehaviour Functions
        private void Start() {

			m_smallMap.Open();
			}
        
		//Public Functions
		public void Interact() {

			if (m_isInAnimation) return;

			if (m_isBigMap) {

				m_smallMap.Open();
				m_bigMap.Close();
				InputController.SetLookObject(CameraController.GetSingleton());
				GameSystem.SetUI(null);
				}	
			
			else {

				m_smallMap.Close();
				m_bigMap.Open();
				InputController.SetLookObject(null);
				GameSystem.SetUI(m_bigMap);
				}

			m_isBigMap = !m_isBigMap;

			IEnumerator Wait() {

				m_isInAnimation = true;
				yield return new WaitForSeconds(1);
				m_isInAnimation = false;
				}	 
			StartCoroutine(Wait());
			}


		//Private Functions
        
        
	//Coroutines
	
	}
