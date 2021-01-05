using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPanelUI : PanelUI {
	
	//Enumerators
	
	//Structs
	public struct LostItem {

		private Item m_item;
		private bool m_lost;

		public LostItem(Item item, bool lost) {

			m_item = item;
			m_lost = lost;
			}

		public Item GetItem() => m_item;
		public bool GetLost() => m_lost;
		}
	
	//Set Variables
	
		//Static
        private static DeadPanelUI m_instance;
        
		//No Static
		[Header("References")]
		[SerializeField] private Transform m_itemHolder = null;
		[SerializeField] private GameObject m_itemPrefab = null;
        [SerializeField] private TMPro.TextMeshProUGUI m_itemTitle = null;
        
		private DeadPanel_ItemUI m_selectedItem;

    //Functions
	
		//MonoBehaviour Functions
        private void Awake() {

			m_instance = this;
			}
        
		//Public Functions
		public override void Open() {

			base.Open();
            InputController.SetLookObject(null);
			GameSystem.SetUI(this);	
			}
		public void SetData(List<LostItem> items) {
			
			foreach(Transform m_t in m_itemHolder.Cast<Transform>()) Destroy(m_t.gameObject);
			foreach(LostItem m_item in items) {
				
				DeadPanel_ItemUI m_i = Instantiate(m_itemPrefab, m_itemHolder).GetComponent<DeadPanel_ItemUI>();
				m_i.SetData(m_item);
				}

			m_itemTitle.text = "";
			}

        public void SelectItem(DeadPanel_ItemUI item, bool select) {

			if (select) {

				m_selectedItem = item;
				m_itemTitle.text = item.GetTitle();
				}
			
			else {

				if (m_selectedItem != item) return;
				m_selectedItem = null;
				m_itemTitle.text = ""; 
				}
			}	 

		public void GoToFarm() {
			
            InputController.SetLookObject(CameraController.GetSingleton());
			GameSystem.SetUI(null);
            DataSystem.GetSingleton().SetDungeonData(null);
			SceneController.GetSingleton().LoadScene(Scenes.House, false);
			}
		public static DeadPanelUI GetSingleton() => m_instance;
        
		//Private Functions
        
        
	//Coroutines
	
	}
