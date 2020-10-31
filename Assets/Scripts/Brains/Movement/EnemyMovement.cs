using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement : JumpingCharacter {
	
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
		
        //Funciones de MonoBehaviour.

        //Funciones privadas.
		
        //Funciones publicas.
		protected void ConstantJump(float minSecs, float maxSecs) {

            StartCoroutine(ConstantJumpCoroutine(minSecs, maxSecs));
            }

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        private IEnumerator ConstantJumpCoroutine(float minSecs, float maxSecs) {
            
            while(true) {

                Jump();
                yield return new WaitForSeconds(Random.Range(minSecs, maxSecs));
                }
            }
        }
