using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Passages")]
            [SerializeField] private RuinsPassageController m_passageUp = null;
            [SerializeField] private RuinsPassageController m_passageLeft = null;
            [SerializeField] private RuinsPassageController m_passageDown = null;
            [SerializeField] private RuinsPassageController m_passageRight = null;

			
            //Privadas.
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

            SetPassageData(m_passageUp, m_generateUpPassage, m_roomPosition + Vector2Int.up);
            SetPassageData(m_passageLeft, m_generateLeftPassage, m_roomPosition + Vector2Int.left);
            SetPassageData(m_passageDown, m_generateDownPassage, m_roomPosition + Vector2Int.down);
            SetPassageData(m_passageRight, m_generateRightPassage, m_roomPosition + Vector2Int.right);
            }
        private void SetPassageData(RuinsPassageController passage, bool opened, Vector2Int teleportPosition) {
            
            passage.SetData(opened, teleportPosition);
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

            m_roomPosition = roomPosition;
            m_dataSystem = DataSystem.GetSingleton();
            GeneratePassages();
            }

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
