using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelUI : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Base Values")]
            [SerializeField] private bool m_closeAtStart = true;
			
            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour.
        protected virtual void Start() {

            gameObject.SetActive(!m_closeAtStart);
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
        public abstract void Open();
        public abstract void Close();

        //Corotinas.
		
        }
