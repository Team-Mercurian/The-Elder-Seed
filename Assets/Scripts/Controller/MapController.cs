using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("References")]
		[SerializeField] private RectTransform m_itemHolder = null;
		[SerializeField] private GameObject m_mapRoom = null;
        
		[SerializeField] private Transform m_playerPivot = null;
		[SerializeField] private TMPro.TextMeshProUGUI m_floorText = null;
        
    //Functions
	
		//MonoBehaviour Functions
        private void Update() {

			SetPlayerPivotRotation();
			}
        
		//Public Functions
		public void SetMap(DungeonData dungeonData) {
			
			m_itemHolder.anchoredPosition = -dungeonData.GetActualRoom() * 24;

			foreach(RoomData m_rD in dungeonData.GetRoomDatas().FindAll(c => c.GetUnlocked())) {
				
				RectTransform m_rt = Instantiate(m_mapRoom, m_itemHolder).GetComponent<RectTransform>();
				m_rt.anchoredPosition = m_rD.GetRoomPosition() * 24;

				Image m_im = m_rt.GetComponent<Image>();
				float m_c = m_rD.HasVisited() ? 1 : 0.5f;				

				m_im.color = new Color(m_c, m_c, m_c, 1); 
				}

			m_floorText.text = "Piso : " + (dungeonData.GetFloor() + 1);
			}
        
        
		//Private Functions
		private void SetPlayerPivotRotation() {
			
			m_playerPivot.eulerAngles = new Vector3(0, 0, -CameraController.GetDirection().eulerAngles.y + 90);
			}
        
        
	//Coroutines
	
	}
