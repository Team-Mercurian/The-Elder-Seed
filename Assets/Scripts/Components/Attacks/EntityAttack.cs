using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            [SerializeField] private BoxCollider m_attackCollider = null;
            [SerializeField] private Transform m_parent = null;
			
            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour.
        protected virtual void Start() {

            m_attackCollider.enabled = false;
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
        protected BoxCollider GetCollider() => m_attackCollider;
		protected Transform GetParentTransform() => m_parent;

        //Funciones heredadas.
		
        //Funciones ha heredar.
        public virtual void Attack() {

            StartCoroutine(AttackCoroutine());
            }
        public abstract void DoDamage(Collider collider);

        //Corotinas.
		protected virtual IEnumerator AttackCoroutine() {

            m_attackCollider.enabled = true;
            yield return new WaitForSeconds(0.05f);
            m_attackCollider.enabled = false;
            }
        }
