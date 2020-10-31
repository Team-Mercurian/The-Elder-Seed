using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			private static PlayerBrain m_instance;

            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] private PlayerMovement m_playerMovement = null;
            [SerializeField] private PlayerHealth m_playerHealth = null;
            [SerializeField] private PlayerAttack m_playerAttack = null;
			
            //Privadas.
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {

            m_instance = this;
            }

        //Funciones privadas.
		
        //Funciones publicas.
        public static PlayerBrain GetSingleton() {
            
            return m_instance;
            }
        
        public PlayerMovement GetMovement() {

            return m_playerMovement;
            }
        public PlayerAttack GetAttack() {

            return m_playerAttack;
            }
        public PlayerHealth GetHealth() {

            return m_playerHealth;
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
