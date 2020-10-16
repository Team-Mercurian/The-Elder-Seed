using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerBrain : CharacterBehaviour {
	
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
        private void OnMove(InputValue value) {

            SmoothMove(value.Get<Vector2>());
            }
		
        //Funciones publicas.
		
        //Funciones heredadas.
        protected override void Dead() {

            
            }
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
