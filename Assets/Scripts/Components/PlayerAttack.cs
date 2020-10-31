using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Values")]
            [SerializeField] private int m_attackDamage = 2;
            [SerializeField] private float m_attackDelay = 1;

            [Header("References")]
            [SerializeField] private BoxCollider m_attackCollider = null;
			
            //Privadas.
            private float m_delay;
			
    //Funciones
		
        //Funciones de MonoBehaviour.
		private void Start() {

            m_attackCollider.enabled = false;
            m_delay = 0;
            }
        private void OnTriggerEnter(Collider collider) {

            if (collider.CompareTag("Enemy")) {

                collider.GetComponent<EnemyHealth>().GetDamage(m_attackDamage);
                }
            }

        //Funciones privadas.
		
        //Funciones publicas.
        public void Attack() {

            if (m_delay > 0) return;

            StartCoroutine(AttackCoroutine());
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		private IEnumerator AttackCoroutine() {

            m_attackCollider.enabled = true;
            yield return new WaitForSeconds(0.05f);
            m_attackCollider.enabled = false;

            m_delay = m_attackDelay;

            while(m_delay > 0) {

                m_delay -= Time.deltaTime;
                yield return null;
                }

            m_delay = 0;
            }
        }
