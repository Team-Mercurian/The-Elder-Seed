using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas.
            private static CursorController m_instance;
			
        //Establecer variables.
		
            //Publicas.
            [Header("Values")]
            [SerializeField] private bool m_defaultVisibility = true;
			
            //Privadas.
            private bool m_cursorVisibility = false;
            private bool m_temporalVisibility = false;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {

            m_instance = this;
            }
        private void Start() {
			
            SetCursorVisibility(m_defaultVisibility);
            }
		
        //Funciones privadas.
        private void SetCursorVisibility(bool active) {

            bool m_isActive = active;
            if (m_temporalVisibility) m_isActive = true;

            m_cursorVisibility = active;
            Cursor.lockState = m_isActive ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = m_isActive;
            }
		
        //Funciones publicas.
        public static CursorController GetSingleton() {

            return m_instance;
            }
        public void SetTemporalVisible(bool active) {
            
            m_temporalVisibility = active;
            SetCursorVisibility(m_cursorVisibility);
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
