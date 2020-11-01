using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour {
	
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

                ForestPassageController m_forestPassage = collider.GetComponent<ForestPassageController>();

                Vector2Int m_pos = m_forestPassage.GetPositionToMove();
                GameBehaviour.Direction m_direction = m_forestPassage.GetDirectionToMove();

                GenerateForestRooms.SetAppearDirection(m_direction);

                DataSystem.GetSingleton().SetActualRoom(m_pos);
                SceneController.GetSingleton().LoadScene(SceneController.Scenes.Ruins);
                }
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
