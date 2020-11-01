using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : EnemyMovement {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Constant Jump")]
            [SerializeField] private float m_minSecsBetweenJumps = 1;
            [SerializeField] private float m_maxSecsBetweenJumps = 3;
			
            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour.
        protected override void Start() {

            base.Start();
            ConstantJump(m_minSecsBetweenJumps, m_maxSecsBetweenJumps);
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
