using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBrain : EnemyBrain {
	
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
            [SerializeField] private float m_normalSpeed = 1;
            [SerializeField] private float m_attackSpeed = 2;

            [Header("Attack")]
			[SerializeField] private float m_minAttackWait = 1;
			[SerializeField] private float m_maxAttackWait = 3;

            [Header("Player Detection")]
            [SerializeField] private float m_detectionDistance = 4;
            [SerializeField] private float m_detectionBreak = 7;

            //Privadas.
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {

            StartCoroutine(Brain());
            ConstantMovement(m_minMovementTime, m_maxMovementTime);
            }
		
        //Funciones publicas.
		
        //Funciones privadas.

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

                        GetMovement().SetSpeed(m_attackSpeed);
                        if (m_attackRoutine != null) StopCoroutine(m_attackRoutine);
                        m_attackRoutine = StartCoroutine(AttackRoutine());
                        m_reConfig = false;
                        }

                    if (GetPlayerDistance() > m_detectionBreak) {
                        
                        if (m_attackRoutine != null) StopCoroutine(m_attackRoutine);
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

                FollowPlayer();
                yield return new WaitForSeconds(m_time);
                
                MovementRoutine_Stop();
                yield return new WaitForSeconds(0.5f);
                
                FollowPlayer();
                JumpAttack();
                yield return new WaitForSeconds(0.5f);
                }
            }
        }
