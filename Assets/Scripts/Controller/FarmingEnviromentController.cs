using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FarmingEnviromentController : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
        public struct FarmSections {

            private Vector3Int m_position;
            private Vector2Int m_size;

            public FarmSections(Vector3Int position, Vector2Int size) {

                m_position = position;
                m_size = size;
                }

            public Vector3Int GetPosition() => m_position;
            public Vector2Int GetSize() => m_size;

            public Vector2Int GetMinPosition() => new Vector2Int(m_position.x - m_size.x, m_position.z - m_size.y);
            public Vector2Int GetMaxPosition() => new Vector2Int(m_position.x + m_size.x, m_position.z + m_size.y);
            }
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
            private static FarmingEnviromentController m_instance;
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Farming Controller")]
			private int m_gridSize = 1;

            //Privadas.
            private List<FarmSections> m_grid;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {

            m_instance = this;
            m_grid = new List<FarmSections>();
            }
        private void Update() {

            if (m_instance == null) m_instance = this;
            }
		private void OnValidate() {

            m_gridSize = Mathf.Clamp(m_gridSize, 1, 5);
            }

        //Funciones privadas.
		
        //Funciones publicas.
        public List<FarmSections> GetGrid() => m_grid;
        public void AddSection(FarmSections section) => m_grid.Add(section);
        public int GetCellSize() => m_gridSize;

        public static FarmingEnviromentController GetSingleton() => m_instance;
            
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
