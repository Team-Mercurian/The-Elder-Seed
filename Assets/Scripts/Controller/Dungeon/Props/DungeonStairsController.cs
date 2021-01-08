using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonStairsController : InteractableBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void Interact() {
			/*
			UnityEvent m_leftEvent = new UnityEvent();
			m_leftEvent.AddListener(ExitRuins);

			UnityEvent m_rightEvent = new UnityEvent();
			m_rightEvent.AddListener(NextFloor);

			ButtonEvent m_leftButtonEvent = new ButtonEvent("Salir", m_leftEvent);
			ButtonEvent m_rightButtonEvent = new ButtonEvent("Bajar", m_rightEvent);

			string m_t = "¿Deseas salir o bajar al siguiente piso?";

			ConfirmationUI.GetSingleton().SetData(m_t, m_leftButtonEvent, m_rightButtonEvent, true);
			ConfirmationUI.GetSingleton().Open();
			*/
			NextFloor();
			}

		public void NextFloor() => GenerateRuinsRooms.NextFloor();
		public void ExitRuins() => GenerateRuinsRooms.ExitRuins(false);
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
