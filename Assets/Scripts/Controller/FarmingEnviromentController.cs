using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			private static int m_gridSize = 2;
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Farming Controller")]
            [SerializeField] private Transform m_playerGrid = null;
            [SerializeField] private GameObject m_plant = null;

            //Privadas.
            private List<FarmSections> m_grid;
            private List<PlantController> m_plants;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {

            m_instance = this;
            m_grid = new List<FarmSections>();
            m_plants = new List<PlantController>();
            }
        private void Start() {

            foreach(GridData m_d in DataSystem.GetSingleton().GetGameData().GetFarmData().GetGridDatas()) {
                
                CreatePlant(m_d.GetSeedPosition() * m_gridSize, m_d.GetSeedIndex(), m_d.GetHarvest());
                }   

            m_playerGrid.localScale = Vector3.one * m_gridSize;
            }

        //Funciones privadas.
		
        //Funciones publicas.
        public List<FarmSections> GetGrid() => m_grid;
        public void AddSection(FarmSections section) => m_grid.Add(section);

        public List<PlantController> GetPlantControllers() => m_plants;
        public PlantController GetPlantController(int index) => m_plants[index]; 
        public PlantController GetPlantController(Vector3Int position) {

            foreach(PlantController m_p in m_plants) {
                
                if (m_p.GetPosition() == position) return m_p;
                }
            
            return null;
            }

        public void AddPlant(PlantController plant) => m_plants.Add(plant);
        public void RemovePlant(PlantController plant) => m_plants.Remove(plant);
        public void CreatePlant(Vector3Int position, int seedIndex, bool canHarvest) {
        
            GameObject m_entity = Instantiate(m_plant, position, Quaternion.identity); 
            m_entity.GetComponent<PlantController>().SetData(seedIndex, position, canHarvest);

            FarmingEnviromentController.GetSingleton().AddPlant(m_entity.GetComponent<PlantController>());
            }

        public static int GetCellSize() => m_gridSize;

        public static FarmingEnviromentController GetSingleton() => m_instance;

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
