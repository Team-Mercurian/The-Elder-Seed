using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBrain : EnemyBrain {
	
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

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        }
