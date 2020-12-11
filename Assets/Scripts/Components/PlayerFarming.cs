using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFarming : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas.
            private static int m_seedIndex = -1;
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] private Transform m_playerCell = null;
			
            //Privadas.
            private Vector3Int? m_actualGrid;
            private FarmData m_data;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {
			
            StartCoroutine(CheckGrid(0.025f));
            m_data = DataSystem.GetSingleton().GetGameData().GetFarmData();
            }
		
        //Funciones privadas.
		private void Harvest(Vector3Int pos, int cellSize) {

            PlantController m_plant = FarmingEnviromentController.GetSingleton().GetPlantController(pos);
            
            if (m_plant != null && m_plant.GetIfCanHarvest()) {
                
                DataSystem.GetSingleton().GetGameData().GetInventoryData().AddPlant(m_plant.GetSeedIndex());
                Seed m_seed = DataSystem.GetSingleton().GetSeed(m_plant.GetSeedIndex());

                if (Random.Range(0f, 100f) < (20 - (4 * (int) m_seed.GetRarity()))) {
                    
                    m_data.AddSeed(m_seed.GetSeedType(), m_seed.GetRarity());
                    }

                m_data.RemoveGridData(pos / cellSize);

                FarmingEnviromentController.GetSingleton().RemovePlant(m_plant);
                DataSystem.Save();
                Destroy(m_plant.gameObject);
                }
            }

        //Funciones publicas.
        public void Interact(InputAction.CallbackContext context) {
            
            if (m_actualGrid == null || context.phase != InputActionPhase.Canceled) return;

            Vector3Int m_pos = new Vector3Int(m_actualGrid.Value.x, m_actualGrid.Value.y, m_actualGrid.Value.z);
            int m_cellSize = FarmingEnviromentController.GetCellSize();

            if (m_data.GetIfGridIsUsed(m_pos / m_cellSize)) {

                Harvest(m_pos, m_cellSize);
                }
            
            else {

                if (m_seedIndex < 0) return;
                Seed m_seed = DataSystem.GetSingleton().GetSeed(m_seedIndex);
                
                if (m_data.GetSeedCount(m_seed.GetSeedType(), m_seed.GetRarity()) > 0) {
                        
                    FarmingEnviromentController.GetSingleton().CreatePlant(m_pos, m_seedIndex, false);

                    m_data.AddGridData(new GridData(m_seedIndex, m_pos / m_cellSize));
                    m_data.RemoveSeed(m_seed.GetSeedType(), m_seed.GetRarity());
                    DataSystem.Save();
                    }
                
                else {

                    Debug.Log("Insuficientes semillas");
                    }
                }
            }

        public static void SetSeed(int seedIndex) {

            m_seedIndex = seedIndex;
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        private IEnumerator CheckGrid(float presition) {

            int m_gridSize = FarmingEnviromentController.GetCellSize();

            while(true) {
                
                Vector2Int m_pos = new Vector2Int(Mathf.CeilToInt((transform.position.x / m_gridSize) - 0.5f) * m_gridSize, Mathf.CeilToInt((transform.position.z / m_gridSize) - 0.5f) * m_gridSize);
                
                m_actualGrid = null;

                foreach(FarmingEnviromentController.FarmSections m_section in FarmingEnviromentController.GetSingleton().GetGrid()) {

                    if (m_pos.x > m_section.GetMinPosition().x && m_pos.x < m_section.GetMaxPosition().x && m_pos.y > m_section.GetMinPosition().y && m_pos.y < m_section.GetMaxPosition().y) {// new Vector2Int(m_cell.x, m_cell.z) == m_pos) {

                        m_actualGrid = new Vector3Int(m_pos.x, m_section.GetPosition().y, m_pos.y);

                        m_playerCell.gameObject.SetActive(true);
                        m_playerCell.position = new Vector3(m_actualGrid.Value.x, m_actualGrid.Value.y + 0.01f, m_actualGrid.Value.z);
                        }
                    }

                if (m_actualGrid == null) m_playerCell.gameObject.SetActive(false);
                yield return new WaitForSeconds(presition);
                }
            }
        }
