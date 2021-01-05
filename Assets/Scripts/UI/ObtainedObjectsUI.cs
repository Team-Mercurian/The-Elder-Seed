using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainedObjectsUI : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        private static ObtainedObjectsUI m_instance;
        
		//Non Static
		[Header("References")]
		[SerializeField] private CanvasGroup m_canvasGroup = null;  
		[SerializeField] private Transform m_itemHolder = null;
		[SerializeField] private GameObject m_obtainedItemPrefab = null; 

		[Header("Values")]
		[SerializeField] private float m_separationBetweenItems = 4;
		[SerializeField] private float m_inOutTime = 0.25f;

		private bool m_isFullOpened = false;
		private List<ObtainedObjects_ItemUI> m_obtainedItems;  
        
		private float m_itemSize;
		private Coroutine m_ioRoutine;

    //Functions
	
		//MonoBehaviour Functions
		private void Awake() {

			m_instance = this;
			m_obtainedItems = new List<ObtainedObjects_ItemUI>();
			m_itemSize = m_obtainedItemPrefab.GetComponent<RectTransform>().sizeDelta.y;

			for(int i = 0; i < m_itemHolder.childCount; i ++) Destroy(m_itemHolder.GetChild(i).gameObject);

			m_canvasGroup.alpha = 0;
			}
        
		//Public Functions
        public static ObtainedObjectsUI GetSingleton() => m_instance;
		public void AddItem(Sprite icon, string title, Rarity rarity) {

			if (!m_isFullOpened) {

				Open();
				}

			ObtainedObjects_ItemUI m_item = Instantiate(m_obtainedItemPrefab, m_itemHolder).GetComponent<ObtainedObjects_ItemUI>();

			string m_itemString = "";

			switch(rarity) {
				
				case Rarity.Common : m_itemString = "<color=#ffffff>" + title + "</color>"; break;
				case Rarity.Rare : m_itemString = "<color=#0099db>" + title + "</color>"; break;
				case Rarity.Epic : m_itemString = "<color=#b55088>" + title + "</color>"; break;
				case Rarity.Legendary : m_itemString = "<color=#fee761>" + title + "</color>"; break;
				}

			m_item.SetData(icon, m_itemString, this);
			m_obtainedItems.Insert(0, m_item);
			ReSetItemPositions();
			}

		public void Remove(ObtainedObjects_ItemUI item) {
			
			m_obtainedItems.Remove(item);
			if (m_obtainedItems.Count == 0) Close();
			}

		//Private Functions
        private void Open() => IO(true);
		private void Close() => IO(false);

		private void IO(bool active) {

			if (m_ioRoutine != null) StopCoroutine(m_ioRoutine);
			m_ioRoutine = StartCoroutine(IORoutine(active));
			}

		private void ReSetItemPositions() {

			float m_size = (m_itemSize + m_separationBetweenItems);
			for(int i = 0; i < m_obtainedItems.Count; i ++) m_obtainedItems[i].SetPosition(-m_size * i); 
			}

	//Coroutines
	private IEnumerator IORoutine(bool active) {
		
		float m_alphaA = m_canvasGroup.alpha;
		float m_alphaB = active ? 1 : 0;

		for(float i = 0; i < m_inOutTime; i += Time.deltaTime) {
			
			m_canvasGroup.alpha = Mathf.Lerp(m_alphaA, m_alphaB, i / m_inOutTime);
			yield return null;
			}
		
		m_canvasGroup.alpha = m_alphaB;
		}
	}
