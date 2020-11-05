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
			
            if (collider.CompareTag("Interactable")) {

                collider.GetComponent<InteractableBehaviour>().Interact();
                }
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
