using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EntityAttack {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Damage")]
            [SerializeField] protected int m_attackDamage = 2;
			
            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
        protected override string SetOtherTag() {

            return "Player";
            }
        protected override void SetDamage() {

            m_damage = m_attackDamage;
            }
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
