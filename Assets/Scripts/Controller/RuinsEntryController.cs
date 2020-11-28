using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinsEntryController : InteractableBehaviour {
	
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

        //Funciones privadas.
		
        //Funciones publicas.
        public override void Interact() {

            DataSystem.Save();
            SceneController.GetSingleton().LoadScene(Scenes.Ruins, false);
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
