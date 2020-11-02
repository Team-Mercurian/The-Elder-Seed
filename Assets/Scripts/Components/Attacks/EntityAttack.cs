using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class EntityAttack : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] protected BoxCollider m_attackCollider = null;
			
            [Header("Damage")]
            [SerializeField] protected int m_attackDamage = 2;

            //Privadas.
            private string m_otherTag;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour.
        private void Awake() {

            m_otherTag = SetOtherTag();
            }
        protected virtual void Start() {

            m_attackCollider.enabled = false;
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
        private void OnTriggerEnter(Collider collider) {

            if (collider.CompareTag(m_otherTag)) {

                collider.GetComponent<EntityHealth>().GetDamage(m_attackDamage);
                }
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
        public virtual void Attack() {

            StartCoroutine(AttackCoroutine());
            }
        protected abstract string SetOtherTag();

        //Corotinas.
		protected virtual IEnumerator AttackCoroutine() {

            m_attackCollider.enabled = true;
            yield return new WaitForSeconds(0.05f);
            m_attackCollider.enabled = false;
            }
        }
