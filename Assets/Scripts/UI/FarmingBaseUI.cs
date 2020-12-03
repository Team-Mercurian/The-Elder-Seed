using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FarmingBaseUI : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas.
            private static FarmingBaseUI m_instance;
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] private SeedSelectorUI m_seedSelector = null;
			
            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {
			
            m_instance = this;
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
        public void OpenSeedSelector(InputAction.CallbackContext context) {

            if (context.started) m_seedSelector.Open();
            else if (context.canceled) m_seedSelector.Close();
            }

        public static FarmingBaseUI GetSingleton() => m_instance;
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
