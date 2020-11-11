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
			private int m_gridSize = 2;

            //Privadas.
            private List<Vector3Int> m_grid;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {

            m_instance = this;
            m_grid = new List<Vector3Int>();
            }
        private void Update() {

            if (m_instance == null) m_instance = this;
            }
		private void OnValidate() {

            m_gridSize = Mathf.Clamp(m_gridSize, 1, 5);
            }

        //Funciones privadas.
		
        //Funciones publicas.
        public List<Vector3Int> GetGrid() => m_grid;
        public void AddCells(List<Vector3Int> cells) => m_grid.AddRange(cells);
        public int GetCellSize() => m_gridSize;

        public static FarmingEnviromentController GetSingleton() => m_instance;
            
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
