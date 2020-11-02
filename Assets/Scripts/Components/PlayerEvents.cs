using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : GameBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
			
            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void OnTriggerEnter(Collider collider) {
			
            if (collider.CompareTag("Forest Path")) {

                SceneController.GetSingleton().LoadScene(SceneController.Scenes.Ruins);
                }

            if (collider.CompareTag("Forest Passage")) {

                RuinsPassageController m_forestPassage = collider.GetComponent<RuinsPassageController>();

                Vector2Int m_pos = m_forestPassage.GetPositionToMove();

                if (m_pos == new Vector2Int(0, -1)) {
                    
                    Debug.Log("Going to House Scene");
                    SceneController.GetSingleton().LoadScene(SceneController.Scenes.House);
                    }
                    
                else {
                    
                    Debug.Log("Going to room in " + m_pos.x + ", " + m_pos.y);

                    Direction m_direction = GetDirection(m_forestPassage.GetDirectionToMove());
                    RoomController.SetAppearDirection(m_direction);

                    DataSystem.GetSingleton().SetActualRoom(m_pos);
                    SceneController.GetSingleton().LoadScene(SceneController.Scenes.Ruins);
                    }
                }
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
