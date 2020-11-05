using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRuinsRooms : GameBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
            
        //Establecer variables.
		
            //Publicas.
            [Header("Generate All Rooms")]
			[SerializeField] private int m_roomsCount = 10;       
			[SerializeField] private int m_chestsRooms = 2;       

            [Header("Debug")]
            [SerializeField] private GameObject m_testScene = null;

            //Privadas.
            private DataSystem m_dataSystem;

    //Funciones
		
        //Funciones de MonoBehaviour.
        private void Start() {

            m_dataSystem = DataSystem.GetSingleton();
            DataSystem.GetSingleton().SetTemporalData(new TemporalData(new PlayerData(PlayerBrain.GetSingleton().GetHealth().GetMaxHealth())));
            GenerateAllRooms();
            }
		
        //Funciones privadas.
        private void GenerateAllRooms() {

            List<RoomData> m_roomsDatas = m_dataSystem.GetRoomsData();

            if (m_roomsDatas == null) {

                m_roomsDatas = new List<RoomData>();           
                int m_generatedRooms = 0;
                Vector2Int m_pos = new Vector2Int();

                List<Vector2Int> m_roomsPositions = new List<Vector2Int>();

                while(m_generatedRooms < (m_roomsCount + m_chestsRooms)) {

                    if (!GetValueInList(m_pos, m_roomsPositions)) {

                        bool m_generateChest = GetIfIsGenerateChest(m_generatedRooms);

                        if (m_generateChest) {

                            int m_roomPrefabIndex = m_dataSystem.GetRandomChestRoomPrefabIndex();
                            
                            m_roomsPositions.Add(m_pos);
                            m_roomsDatas.Add(new RoomData(m_pos, m_roomPrefabIndex, RoomData.RoomType.Chest));
                            m_generatedRooms++;
                            }

                        else {

                            int m_roomPrefabIndex = m_dataSystem.GetRandomRoomPrefabIndex();
                            
                            m_roomsPositions.Add(m_pos);
                            m_roomsDatas.Add(new RoomData(m_pos, m_roomPrefabIndex, RoomData.RoomType.Room));
                            m_generatedRooms ++;
                            }
                        }

                    int m_directionRaw = Random.Range(0, 4);
                    Vector2Int m_direction = new Vector2Int();

                    switch(m_directionRaw) {

                        case 0 : m_direction = Vector2Int.left; break;
                        case 1 : m_direction = Vector2Int.right; break;
                        case 2 : m_direction = Vector2Int.up; break;
                        case 3 : m_direction = Vector2Int.down; break;
                        }
                        
                    m_pos = new Vector2Int(m_pos.x + m_direction.x, Mathf.Clamp(m_pos.y + m_direction.y, 0, 100));
                    }

                m_dataSystem.SetRoomsData(m_roomsDatas);
                m_dataSystem.SetActualRoom(new Vector2Int(0, 0));
                }
                
            GenerateRoom(m_dataSystem.GetRoomData(m_dataSystem.GetActualRoom()));
            }
        private void GenerateRoom(RoomData roomData) {

            GameObject m_roomPrefab = null;

            if (roomData.GetRoomType() == RoomData.RoomType.Room) {

                if (m_testScene == null) m_roomPrefab = m_dataSystem.GetRoomPrefab(roomData.GetRoomPrefabIndex());
                else m_roomPrefab = m_testScene;
                }
            
            else if (roomData.GetRoomType() == RoomData.RoomType.Chest) {

                m_roomPrefab = m_dataSystem.GetChestRoomPrefab(roomData.GetRoomPrefabIndex());
                }

            RoomController m_roomController = Instantiate(m_roomPrefab, transform).GetComponent<RoomController>();
            m_roomController.SetData(roomData.GetRoomPosition());
            }		
        private bool GetIfIsGenerateChest(int actual) {

            int m_calc = Mathf.FloorToInt(((float) (m_roomsCount + m_chestsRooms) / m_chestsRooms));
            bool m_return = false;

            for(int i = m_calc - 1; i <= m_roomsCount + m_chestsRooms; i += m_calc) {
                
                if (i == actual) {
                    
                    m_return = true;
                    }
                }
            
            return m_return;
            }
        private bool GetValueInList(Vector2Int value, List<Vector2Int> list) {

            foreach(Vector2Int m_pos in list) {

                if (value == m_pos) return true;
                }
            
            return false;
            }

        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
