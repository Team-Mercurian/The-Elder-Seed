using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FarmingEnviromentController : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
            private static FarmingEnviromentController m_instance;
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Farming Controller")]
			[SerializeField] private int m_gridSize = 2;

            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {

            m_instance = this;
            }
        private void Update() {

            if (m_instance == null) m_instance = this;
            }
		private void OnValidate() {

            m_gridSize = Mathf.Clamp(m_gridSize, 1, 5);
            }

        //Funciones privadas.
		
        //Funciones publicas.
        public int GetGridSize() {

            return m_gridSize;
            }
        public static FarmingEnviromentController GetSingleton() {

            return m_instance;
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
