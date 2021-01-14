using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryoSlimeBrain : EnemyBrain {

	//Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Movement")]
            [SerializeField] private float m_minMovementTime = 2;
            [SerializeField] private float m_maxMovementTime = 3;
            [Space]
            [SerializeField] private float m_normalSpeed = 1.25f;
            [SerializeField] private float m_runSpeed = 2.5f;

            [Header("Attack")]
            [SerializeField] private GameObject m_proyectile = null;
            [Space] 
			[SerializeField] private float m_minAttackWait = 2;
			[SerializeField] private float m_maxAttackWait = 3;
			[Space]
			[SerializeField] private int m_attackCounts = 2;
			[SerializeField] private float m_timeBetweenShoots = 0.35f;

            [Header("Player Detection")]
			[SerializeField] private float m_minShotDistance = 5;
			[SerializeField] private float m_maxShotDistance = 9;
			[Space]
            [SerializeField] private float m_detectionDistance = 7;
            [SerializeField] private float m_detectionBreak = 12;

            //Privadas.
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {

            StartCoroutine(Brain());
            ConstantMovement(m_minMovementTime, m_maxMovementTime);
            }
		
        //Funciones publicas.
		
        //Funciones privadas.
		private void Shoot() {

            Instantiate(m_proyectile, transform.position + (Vector3.up * 0.5f), Quaternion.identity).GetComponent<CryoSlimeProyectile>().SetData(GetAttack().GetWeapon(), transform);
			}

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        private IEnumerator Brain() {

            bool m_attackMode = false;
            bool m_reConfig = true;

            Coroutine m_attackRoutine = null;

            while(true) {

                while(!m_attackMode) {
                    
                    if (m_reConfig) {
                        
                        GetMovement().SetSpeed(m_normalSpeed);
                        MovementRoutine_Stop();
                        ConstantMovement(m_minMovementTime, m_maxMovementTime);
                        m_reConfig = false;
                        }

                    if (GetPlayerDistance() < m_detectionDistance) {
                        
                        m_attackMode = true;
                        m_reConfig = true;
                        }
                    
                    yield return null;
                    }
                
                while(m_attackMode) {
                    
                    if (m_reConfig) {

                        GetMovement().SetSpeed(m_runSpeed);
                        m_reConfig = false;
                        }

					if (GetPlayerDistance() < m_minShotDistance) {
						
                        if (m_attackRoutine != null) StopCoroutine(m_attackRoutine);
                        m_attackRoutine = null;
						FollowPlayerInverse();
						}

					else if (GetPlayerDistance() > m_maxShotDistance) {

                        if (m_attackRoutine != null) StopCoroutine(m_attackRoutine);
                        m_attackRoutine = null;
						FollowPlayer();
						}

                    else if (GetPlayerDistance() > m_minShotDistance && GetPlayerDistance() < m_maxShotDistance) {

                        MovementRoutine_Stop();
						if (m_attackRoutine == null) m_attackRoutine = StartCoroutine(AttackRoutine());
						}

                    if (GetPlayerDistance() > m_detectionBreak) {
                        
                        if (m_attackRoutine != null) StopCoroutine(m_attackRoutine);
                        m_attackRoutine = null;
                        m_attackMode = false;
                        m_reConfig = true;
                        }
                    
                    yield return null;
                    }

                yield return null;
                }
            }
        private IEnumerator AttackRoutine() {

            while(true) { 

                float m_time = Random.Range(m_minAttackWait, m_maxAttackWait);
                yield return new WaitForSeconds(m_time);

				for(int i = 0; i < m_attackCounts; i ++) {
                    
                    Shoot();
                    yield return new WaitForSeconds(m_timeBetweenShoots);
                    }    
                }
            }
	    }
