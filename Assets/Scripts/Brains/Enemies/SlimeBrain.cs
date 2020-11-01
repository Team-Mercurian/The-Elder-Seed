using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBrain : EntityBrain {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Constant Jump")]
			[SerializeField] private float m_minRandomJump = 2;
			[SerializeField] private float m_maxRandomJump = 3;

            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {

            ConstantJump(m_minRandomJump, m_maxRandomJump);
            }
		
        //Funciones publicas.
		
        //Funciones privadas.
        private void ConstantJump(float minRandomJump, float maxRandomJump) {

            StartCoroutine(ConstantJumpCoroutine(minRandomJump, maxRandomJump));
            }   

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        private IEnumerator ConstantJumpCoroutine(float minSecs, float maxSecs) {
            
            while(true) {

                GetMovement().Jump();
                GetAttack().Attack();
                yield return new WaitForSeconds(Random.Range(minSecs, maxSecs));
                }
            }
        }
