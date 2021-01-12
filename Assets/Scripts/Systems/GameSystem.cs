using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        private static PanelUI m_openedUI = null;
        
		//No Static
		
    
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public static void SetUI(PanelUI ui) {
			
			m_openedUI = ui;
			
			if (ui == null) {
					
				CursorController.GetSingleton().SetVisibility(false);
				InputController.GetSingleton().SetUIMode(false);
				}
			
			else {

				CursorController.GetSingleton().SetVisibility(true);
				InputController.GetSingleton().SetUIMode(true);
				}
			}
		public static PanelUI GetUI() => m_openedUI; 
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
