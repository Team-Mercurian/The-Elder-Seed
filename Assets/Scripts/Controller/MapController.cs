using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : PanelUI {
	
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

		[Space]
		[SerializeField] private Sprite m_chestIcon = null;
		[SerializeField] private Sprite m_stairsIcon = null;

		[Header("Values")]
		[SerializeField] private int m_roomSize = 32;
		[SerializeField] private int m_roomSeparation = 4;
        
    //Functions
	
		//MonoBehaviour Functions
        private void Update() {

			SetPlayerPivotRotation();
			}
        
		//Public Functions
		public void SetMap(DungeonData dungeonData) {
			
			foreach(Transform m_t in m_itemHolder.Cast<Transform>()) Destroy(m_t.gameObject);

			m_itemHolder.anchoredPosition = -dungeonData.GetActualRoom() * (m_roomSize + m_roomSeparation);

			foreach(RoomData m_rD in dungeonData.GetRoomDatas().FindAll(c => c.GetUnlocked())) {
				
				RectTransform m_rt = Instantiate(m_mapRoom, m_itemHolder).GetComponent<RectTransform>();
				m_rt.anchoredPosition = m_rD.GetRoomPosition() * (m_roomSize + m_roomSeparation);

				Image m_im = m_rt.GetComponent<Image>();
				float m_c = m_rD.HasVisited() ? 0.8f : 0.5f;
				m_c = dungeonData.GetActualRoom() == m_rD.GetRoomPosition() ? 1 : m_c;

				Image m_iconHolder = m_rt.Find("Icon Holder").GetComponent<Image>();

				if (dungeonData.GetActualRoom() == m_rD.GetRoomPosition() || m_rD.GetIfIsCompleted()) 
					m_iconHolder.gameObject.SetActive(false);
					
				else {

					switch(m_rD.GetRoomType()) {

						case RoomData.RoomType.Room : m_iconHolder.gameObject.SetActive(false); break;
						case RoomData.RoomType.Chest : m_iconHolder.sprite = m_chestIcon; break;
						case RoomData.RoomType.Stairs : m_iconHolder.sprite = m_stairsIcon; break;
						}
					}				

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
