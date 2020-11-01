using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBrain : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] protected EntityAttack m_attack = null;
            [SerializeField] protected EntityHealth m_health = null;
            [SerializeField] protected JumpingCharacter m_movement = null;
			
            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
		
        //Funciones publicas.
        public JumpingCharacter GetMovement() => m_movement;
        public EntityAttack GetAttack() => m_attack;
        public EntityHealth GetHealth() => m_health;
            

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        }
