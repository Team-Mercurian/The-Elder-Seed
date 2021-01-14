using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerFarming : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas.
            private static PlayerFarming m_instance;
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] private Transform m_playerCell = null;
            [SerializeField] private Image m_selectedSeed = null;
            [SerializeField] private TextMeshProUGUI m_selectedSeedCount = null;

            //Privadas.
            private Vector3Int? m_actualGrid;
            private GameData m_data;

            private int m_seedID = -1;
            private bool m_magicalFragment = false;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {

            m_instance = this;
            }
        private void Start() {
			
            StartCoroutine(CheckGrid(0.025f));
            m_data = DataSystem.GetSingleton().GetGameData();

            SetSeedID(m_seedID);
            }
		
        //Funciones privadas.
		private void Harvest(Vector3Int pos, int cellSize) {

            PlantController m_plant = FarmingEnviromentController.GetSingleton().GetPlantController(pos);
            
            if (m_plant != null && m_plant.GetIfCanHarvest()) {
                
                m_data.GetInventoryData().AddPlant(m_plant.GetSeedID(), 1);
                Seed m_seed = DataSystem.GetSingleton().GetSeed(m_plant.GetSeedID());
			    ObtainedObjectsUI.GetSingleton().AddItem(m_seed.GetPlant().GetIcon(), m_seed.GetPlant().GetName(), m_seed.GetPlant().GetRarity());

                if (Random.Range(0f, 100f) < (20 - (4 * (int) m_seed.GetRarity()))) {
                    
                    m_data.GetInventoryData().AddSeed(m_seed.GetID(), 1);
                    m_selectedSeedCount.text = m_data.GetInventoryData().GetSeedData(m_seedID).GetCount().ToString();
			        ObtainedObjectsUI.GetSingleton().AddItem(m_seed.GetIcon(), m_seed.GetName(), m_seed.GetRarity());
                    }

                m_data.GetFarmData().RemoveGridData(pos / cellSize);

                if (DataSystem.GetSingleton().GetGameData().GetTutorialIndex() < 9) 
                    TutorialController.GetSingleton().SetTutorialText(9);

                FarmingEnviromentController.GetSingleton().RemovePlant(m_plant);
                SaveSystem.Save();
                Destroy(m_plant.gameObject);
                }
            }

        //Funciones publicas.
        public void Interact(InputAction.CallbackContext context) {
            
            if (m_actualGrid == null || context.phase != InputActionPhase.Canceled) return;

            Vector3Int m_pos = new Vector3Int(m_actualGrid.Value.x, m_actualGrid.Value.y, m_actualGrid.Value.z);
            int m_cellSize = FarmingEnviromentController.GetCellSize();

            PlantController m_plant = FarmingEnviromentController.GetSingleton().GetPlantController(m_pos);

            if (m_data.GetFarmData().GetIfGridIsUsed(m_pos / m_cellSize)) {

                GridData m_gridData = m_data.GetFarmData().GetGridData(m_pos/m_cellSize);

                if (m_magicalFragment && !m_gridData.GetHarvest()) {
                    
                    if (m_data.GetInventoryData().GetMagicalFragments() <= 0) return;
                    
                    m_data.GetInventoryData().AddMagicalFragments(-1);
                    m_selectedSeedCount.text = m_data.GetInventoryData().GetMagicalFragments().ToString();
                    m_gridData.AddRoom();
                    m_gridData.AddRoom();

                    FarmingEnviromentController.GetSingleton().GetPlantController(m_pos).SetCount(m_gridData.GetRoomCount(), m_gridData.GetMaxRoomCount());

                    if (m_gridData.GetHarvest()) {
                        
                        if (DataSystem.GetSingleton().GetGameData().GetTutorialIndex() < 8) 
                            TutorialController.GetSingleton().SetTutorialText(8);

                        }
                    SaveSystem.Save();
                    }   
                
                else {

                    Harvest(m_pos, m_cellSize);
                    }
                }
            
            else {

                if (m_seedID < 0) return;

                Seed m_seed = DataSystem.GetSingleton().GetSeed(m_seedID);
                
                if (m_data.GetInventoryData().GetSeedData(m_seed.GetID()).GetCount() > 0) {

                    if (DataSystem.GetSingleton().GetGameData().GetTutorialIndex() < 4) 
                        TutorialController.GetSingleton().SetTutorialText(4);
                    
                    int m_rooms = (int) (m_seed.GetRarity() + 1) * 4;

                    FarmingEnviromentController.GetSingleton().CreatePlant(m_pos, m_seedID, 0, m_rooms);
                    m_data.GetFarmData().AddGridData(new GridData(m_seedID, m_pos / m_cellSize, m_rooms));
                    m_data.GetInventoryData().AddSeed(m_seed.GetID(), -1);
                    m_selectedSeedCount.text = m_data.GetInventoryData().GetSeedData(m_seedID).GetCount().ToString();
                    SaveSystem.Save();
                    }
                
                else {

                    Debug.Log("Insuficientes semillas");
                    }
                }
            }

        public void ActiveMagicalFragments() {
            
            m_selectedSeedCount.text = m_data.GetInventoryData().GetMagicalFragments().ToString();
            m_selectedSeed.gameObject.SetActive(true);
            m_selectedSeed.sprite = DataSystem.GetSingleton().GetMiscellaneous(0).GetIcon();
            m_magicalFragment = true; 
            }

        public void SetSeedID(int seedID) {
            
            m_magicalFragment = false;
            if (seedID >= 0) {
                
                m_selectedSeedCount.text = "";

                m_selectedSeed.gameObject.SetActive(true);
                m_selectedSeed.sprite = DataSystem.GetSingleton().GetSeed(seedID).GetIcon();
                m_selectedSeedCount.text = m_data.GetInventoryData().GetSeedData(seedID).GetCount().ToString();
                }

            else {

                m_selectedSeedCount.text = "";
                m_selectedSeed.gameObject.SetActive(false);
                }

            m_seedID = seedID;
            }
        public int GetSeedID() => m_seedID;

        public static PlayerFarming GetSingleton() => m_instance;
            
		
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
