using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SeedSelectorUI : PanelUI, IHasLookInput {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] private Transform m_seedHolder = null;

            [Space]
            [SerializeField] private RectTransform m_selector = null;
            [SerializeField] private TMPro.TextMeshProUGUI m_selectedText = null;

            [Space]
            [SerializeField] private GameObject m_seed = null;
            [SerializeField] private GameObject m_lineSeparator = null;

            [Header("Values")]
            [SerializeField] private float m_distance = 64;

            //Privadas.
            private List<SeedSelector_SeedUI> m_seedsController;
            private Coroutine m_IORoutine = null;
			
            private int m_selectedSeedID;
            private List<ItemData> m_unlockedSeeds;

    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
        private void CreateSeeds() {

            m_seedsController = new List<SeedSelector_SeedUI>();

            m_unlockedSeeds = DataSystem.GetSingleton().GetGameData().GetInventoryData().GetSeedList().FindAll(c => c.GetUnlocked() == true);
            
            int m_count = m_unlockedSeeds.Count;

            for(int i = 0; i < m_count * 2; i ++) {

                bool m_isOdd = i % 2 >= 1;
                float m_angle = (360f / (m_count * 2f)) * i;

                GameObject m_object = null; 

                if (m_isOdd) {

                    m_object = Instantiate(m_lineSeparator, m_seedHolder);
                    m_object.transform.eulerAngles = new Vector3(0, 0, m_angle);
                    }

                else {

                    m_object = Instantiate(m_seed, m_seedHolder);

                    SeedSelector_SeedUI m_seedController = m_object.GetComponent<SeedSelector_SeedUI>();

                    m_seedController.SetData(DataSystem.GetSingleton().GetSeed(m_unlockedSeeds[i/2].GetID()).GetIcon(), m_unlockedSeeds[i/2].GetCount());
                    m_seedsController.Add(m_seedController);
                    }

                m_object.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Cos(m_angle * Mathf.Deg2Rad), Mathf.Sin(m_angle * Mathf.Deg2Rad)) * m_distance;
                }
            }
        private void SelectSeed(int selectedSeedIndex) {

            for(int i = 0; i < m_seedsController.Count; i ++) {
                
                if (selectedSeedIndex == i) {

                    m_seedsController[i].Select();
                    m_selectedSeedID = selectedSeedIndex == -1 ? -1 : m_unlockedSeeds[i].GetID();
                    m_selectedText.text = DataSystem.GetSingleton().GetSeed(m_unlockedSeeds[i].GetID()).GetName();
                    }
                
                else m_seedsController[i].UnSelect();
                }

            if (selectedSeedIndex == -1) m_selectedText.text = "";
            }
		
        //Funciones publicas.
		
        //Funciones heredadas. 
        [ContextMenu("Debug Open")]
        public override void Open() {
			
            CreateSeeds();
            m_selectedText.text = "";
            gameObject.SetActive(true);

            if (m_IORoutine != null) StopCoroutine(m_IORoutine);
            m_IORoutine = StartCoroutine(PanelIOCoroutine(true));
            
            InputController.SetLookObject(this);

            m_selector.anchoredPosition = Vector2.zero;
            m_selectedSeedID = -1;
            }
            
        [ContextMenu("Debug Close")]
        public override void Close() {
            
            for(int i = 0; i < m_seedHolder.childCount; i ++) Destroy(m_seedHolder.GetChild(i).gameObject);

            if (m_IORoutine != null) StopCoroutine(m_IORoutine);
            m_IORoutine = StartCoroutine(PanelIOCoroutine(false));
            
            InputController.SetLookObject(CameraController.GetSingleton());
            PlayerFarming.SetSeedID(m_selectedSeedID);
            } 
		
        public void Look(Vector2 velocity) {
            
            if (InputController.GetInputType() == InputType.Keyboard) m_selector.anchoredPosition += velocity;
            else m_selector.anchoredPosition = velocity * m_distance;

            float m_angle = Mathf.Atan2(m_selector.anchoredPosition.y, m_selector.anchoredPosition.x) * Mathf.Rad2Deg;
            if (m_angle < 0) m_angle += 360f;

            float m_selectorDistance = Vector2.Distance(Vector2.zero, m_selector.anchoredPosition);
            m_selectorDistance = Mathf.Clamp(m_selectorDistance, -m_distance, m_distance);

            m_selector.anchoredPosition = new Vector2(Mathf.Cos(m_angle * Mathf.Deg2Rad), Mathf.Sin(m_angle * Mathf.Deg2Rad)) * m_selectorDistance;

            float m_angleDistance = (360f / m_seedsController.Count);

            for(int i = 0; i < m_seedsController.Count; i ++) {

                if (m_selectorDistance > m_distance/2) {

                    if (i == 0 && (m_angle > (-(m_angleDistance/2)) || m_angle < (m_angleDistance/2))) SelectSeed(0);
                    else if (m_angle > (m_angleDistance / 2f) + (m_angleDistance * (i - 1)) && m_angle < (m_angleDistance / 2f) + (m_angleDistance * (i))) 
                        SelectSeed(i); 
                    }

                else {

                    SelectSeed(-1);
                    }
                }
            }

        //Funciones ha heredar.
		
        //Corotinas.
        private IEnumerator PanelIOCoroutine(bool active) {

            float m_time = 0.25f;
            float m_a = GetCanvasGroup().alpha;
            float m_b = active ? 1 : 0;

            for(float i = 0; i < m_time; i += Time.deltaTime) {

                GetCanvasGroup().alpha = Mathf.Lerp(m_a, m_b, GetAnimationCurveEvaluation(i / m_time));
                yield return null;
                }
            
            GetCanvasGroup().alpha = m_b;
            
            if (!active) gameObject.SetActive(false);
            }
		
        }
