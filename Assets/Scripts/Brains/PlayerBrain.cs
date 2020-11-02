using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : EntityBrain {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			private static PlayerBrain m_instance;

            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [SerializeField] private PlayerMovement m_playerMovement = null;
			
            //Privadas.
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {

            m_instance = this;
            }

        //Funciones privadas.
		
        //Funciones publicas.
        public PlayerMovement GetPlayerMovement() {

            return m_playerMovement;
            }

        public static PlayerBrain GetSingleton() {
            
            return m_instance;
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
