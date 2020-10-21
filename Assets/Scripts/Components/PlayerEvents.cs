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

                SceneController.GetSingleton().LoadScene(SceneController.Scenes.Forest);
                }

            if (collider.CompareTag("Forest Passage")) {

                Vector2Int m_pos = collider.GetComponent<ForestPassageController>().GetPositionToMove();
                DataSystem.GetSingleton().SetActualRoom(m_pos);
                
                SceneController.GetSingleton().LoadScene(SceneController.Scenes.Forest);
                }
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
