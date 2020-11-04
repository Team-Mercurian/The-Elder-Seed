using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : GameBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
            private static Direction m_appearDirection = Direction.Up;
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Passages")]
            [SerializeField] private RuinsPassageController m_passageUp = null;
            [SerializeField] private RuinsPassageController m_passageLeft = null;
            [SerializeField] private RuinsPassageController m_passageDown = null;
            [SerializeField] private RuinsPassageController m_passageRight = null;

			
            //Privadas.
            private PlayerBrain m_player;
            private Vector2Int m_roomPosition;
            private DataSystem m_dataSystem;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
		private void GeneratePassages() {
            
            List<RoomData> m_roomsDatas = m_dataSystem.GetRoomsData();
            List<Vector2Int> m_roomPositions = GetAllRoomsPositions(m_roomsDatas);

            bool m_generateLeftPassage = GetValueInList(m_roomPosition + Vector2Int.left, m_roomPositions);
            bool m_generateRightPassage = GetValueInList(m_roomPosition + Vector2Int.right, m_roomPositions);
            bool m_generateUpPassage = GetValueInList(m_roomPosition + Vector2Int.up, m_roomPositions);
            bool m_generateDownPassage = GetValueInList(m_roomPosition + Vector2Int.down, m_roomPositions);

            SetPassageData(m_passageUp, m_generateUpPassage, m_roomPosition, Vector2Int.up);
            SetPassageData(m_passageLeft, m_generateLeftPassage, m_roomPosition, Vector2Int.left);
            SetPassageData(m_passageDown, m_generateDownPassage, m_roomPosition, Vector2Int.down);
            SetPassageData(m_passageRight, m_generateRightPassage, m_roomPosition, Vector2Int.right);
            }
        private void SetPlayerPosition() {

            //Establecer posicion del personaje.
            Vector3 m_playerPos = new Vector3();
            float m_playerRot = 0;

            switch(m_appearDirection) {

                case Direction.Left : 
                    m_playerPos = m_passageRight.GetPlayerAppearPosition(); 
                    m_playerRot = 270;
                    break;

                case Direction.Right : 
                    m_playerPos = m_passageLeft.GetPlayerAppearPosition();
                    m_playerRot = 90;
                    break;

                case Direction.Up : 
                    m_playerPos = m_passageDown.GetPlayerAppearPosition();
                    m_playerRot = 0;
                    break;

                case Direction.Down : 
                    m_playerPos = m_passageUp.GetPlayerAppearPosition();
                    m_playerRot = 180;
                    break;
                }

            m_player.GetMovement().SetPosition(m_playerPos);
            m_player.transform.rotation = Quaternion.Euler(0, m_playerRot, 0);
            }
        private void SetPassageData(RuinsPassageController passage, bool opened, Vector2Int teleportPosition, Vector2Int direction) {
            
            passage.SetData(opened, teleportPosition, direction);
            }
        private List<Vector2Int> GetAllRoomsPositions(List<RoomData> rooms) {

            List<Vector2Int> m_list = new List<Vector2Int>();

            foreach(RoomData m_rd in rooms) {
                
                m_list.Add(m_rd.GetRoomPosition());
                }
                
            return m_list;
            }
        private bool GetValueInList(Vector2Int value, List<Vector2Int> list) {

            foreach(Vector2Int m_pos in list) {

                if (value == m_pos) return true;
                }
            
            return false;
            }
        
        //Funciones publicas.
        public void SetData(Vector2Int roomPosition) {

            m_player = PlayerBrain.GetSingleton();
            m_roomPosition = roomPosition;
            m_dataSystem = DataSystem.GetSingleton();
            GeneratePassages();
            SetPlayerPosition();
            }

        public static Direction GetAppearDirection() {

            return m_appearDirection;
            }
        public static void SetAppearDirection(Direction direction) {

            m_appearDirection = direction;
            }
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
