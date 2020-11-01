using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthBehaviour : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Health")]
			[SerializeField] protected int m_health = 8;

            //Privadas.
            private int m_actualHealth;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {
			
            m_actualHealth = SetActualHealth();
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
        public void GetDamage(int damage) {
            
            //Reducir la vida.
            m_actualHealth = Mathf.Clamp(m_actualHealth - damage, 0, m_health);
            
            //Detectar si este perdio toda su vida.
            if (m_actualHealth == 0) {

                Dead();
                }
            }
        public void AddHealth(int health) {
            
            //Añadir vida.
            m_actualHealth = Mathf.Clamp(m_actualHealth + health, 0, m_health);
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
        protected abstract int SetActualHealth();
        protected abstract void Dead();
		
        //Corotinas.
		
        }
