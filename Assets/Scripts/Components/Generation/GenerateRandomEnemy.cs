using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomEnemy : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
			
            //Privadas.
			private DataSystem m_dataSystem;
			
    //Funciones
		
        //Funciones de MonoBehaviour.
        private void Start() {
			
            m_dataSystem = DataSystem.GetSingleton();
            
            GameObject m_enemy = m_dataSystem.GetEnemyPrefab(m_dataSystem.GetRandomEnemyPrefabIndex());
            Instantiate(m_enemy, transform.position, Quaternion.identity);

            Destroy(gameObject);
            }
        private void OnDrawGizmos() {

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.25f);
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
