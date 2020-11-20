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
            [SerializeField] private GameObject m_plant = null;
			
            //Privadas.
            private Vector3Int? m_actualGrid;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {
			
            StartCoroutine(CheckGrid(0.025f));
            }
		
        //Funciones privadas.
		private void Harvest() {

            PlantController m_plant = FarmingEnviromentController.GetSingleton().GetPlantController(new Vector3Int(m_actualGrid.Value.x, m_actualGrid.Value.y, m_actualGrid.Value.z));
            
            if (m_plant != null && m_plant.GetIfCanHarvest()) {
                
                DataSystem.GetSingleton().RemoveGridData(m_plant.GetPosition());

                FarmingEnviromentController.GetSingleton().RemovePlant(m_plant);

                Destroy(m_plant.gameObject);
                }
            }

        //Funciones publicas.
        public void Interact(InputAction.CallbackContext context) {
            
            if (m_seedIndex < 0 || m_actualGrid == null || context.phase != InputActionPhase.Canceled) return;

            Vector3Int m_pos = new Vector3Int(m_actualGrid.Value.x, m_actualGrid.Value.y, m_actualGrid.Value.z);

            if (DataSystem.GetSingleton().GetIfGridIsUsed(m_pos)) {

                Harvest();
                }
            
            else {

                Seed m_seed = DataSystem.GetSingleton().GetSeed(m_seedIndex);

                GameObject m_entity = Instantiate(m_plant, m_pos, Quaternion.identity); 
                m_entity.GetComponent<PlantController>().SetData(m_seed, m_pos);

                FarmingEnviromentController.GetSingleton().AddPlant(m_entity.GetComponent<PlantController>());

                DataSystem.GetSingleton().AddGridData(new GridData(m_seedIndex, m_pos, new System.TimeSpan(System.DateTime.Now.Ticks)));
                DataSystem.Save();
                }
            }

        public static void SetSeed(int seedIndex) {

            m_seedIndex = seedIndex;
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        private IEnumerator CheckGrid(float presition) {

            while(true) {
                
                Vector2Int m_pos = new Vector2Int(Mathf.CeilToInt(transform.position.x - 0.5f), Mathf.CeilToInt(transform.position.z - 0.5f));
                
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
