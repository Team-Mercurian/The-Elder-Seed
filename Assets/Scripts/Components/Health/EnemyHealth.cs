using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Drops")]
            [SerializeField] private GameObject m_drop = null;
            [SerializeField] private int m_dropCount = 0;
			[SerializeField] [Range(0, 100)] private float m_probability = 100;

            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
		private void DropObject() {
            
            for(int i = 0; i < m_dropCount; i ++) {
                
                if (Random.Range(0, 100) <= m_probability) {
                    
                    Instantiate(m_drop, transform.position, Quaternion.identity);
                    }
                }
            }

        //Funciones publicas.
		protected override int SetActualHealth() {

            return m_health;
            }
		protected override void Dead() {
            
            DropObject();
            Destroy(gameObject);
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
