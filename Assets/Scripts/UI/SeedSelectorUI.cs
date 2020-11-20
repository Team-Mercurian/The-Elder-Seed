using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            [SerializeField] private CanvasGroup m_canvasGroup = null;

            [Space]
            [SerializeField] private RectTransform m_selector = null;

            [Space]
            [SerializeField] private GameObject m_seed = null;
            [SerializeField] private GameObject m_lineSeparator = null;

            [Header("Values")]
            [SerializeField] private AnimationCurve m_animationCurve = null;
            [SerializeField] private float m_distance = 64;

            //Privadas.
            private List<Transform> m_seedsTransform;
            private Coroutine m_IORoutine = null;
			
            private int m_selectedSeed;

    //Funciones
		
        //Funciones de MonoBehaviour
        protected override void Start() {

            CreateSeeds();
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;

            base.Start();
            }  
		
        //Funciones privadas.
        private void CreateSeeds() {

            m_seedsTransform = new List<Transform>();
            int m_count = DataSystem.GetSingleton().GetAllSeeds().Length;

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
                    m_seedsTransform.Add(m_object.transform);
                    }

                m_object.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Cos(m_angle * Mathf.Deg2Rad), Mathf.Sin(m_angle * Mathf.Deg2Rad)) * m_distance;
                }
            }
        private void SelectSeed(int selectedSeedIndex) {

            for(int i = 0; i < m_seedsTransform.Count; i ++) {
                
                if (selectedSeedIndex == i) {

                    m_seedsTransform[i].localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    m_selectedSeed = i;
                    }
                else m_seedsTransform[i].localScale = Vector3.one;
                }
            }
		
        //Funciones publicas.
		
        //Funciones heredadas. 
        [ContextMenu("Debug Open")]
        public override void Open() {
			
            gameObject.SetActive(true);

            if (m_IORoutine != null) StopCoroutine(m_IORoutine);
            m_IORoutine = StartCoroutine(PanelIOCoroutine(true));
            
            InputController.SetLookObject(this);

            m_selector.anchoredPosition = Vector2.zero;
            m_selectedSeed = -1;
            }
            
        [ContextMenu("Debug Close")]
        public override void Close() {
            
            if (m_IORoutine != null) StopCoroutine(m_IORoutine);
            m_IORoutine = StartCoroutine(PanelIOCoroutine(false));
            
            InputController.SetLookObject(CameraController.GetSingleton());
            PlayerFarming.SetSeed(m_selectedSeed);
            } 
		
        public void Look(Vector2 velocity) {
            
            if (InputController.GetInputType() == InputType.Keyboard) m_selector.anchoredPosition += velocity;
            else m_selector.anchoredPosition = velocity * m_distance;

            float m_angle = Mathf.Atan2(m_selector.anchoredPosition.y, m_selector.anchoredPosition.x) * Mathf.Rad2Deg;
            if (m_angle < 0) m_angle += 360f;

            float m_selectorDistance = Vector2.Distance(Vector2.zero, m_selector.anchoredPosition);
            m_selectorDistance = Mathf.Clamp(m_selectorDistance, -m_distance, m_distance);

            m_selector.anchoredPosition = new Vector2(Mathf.Cos(m_angle * Mathf.Deg2Rad), Mathf.Sin(m_angle * Mathf.Deg2Rad)) * m_selectorDistance;

            float m_angleDistance = (360f / m_seedsTransform.Count);

            for(int i = 0; i < m_seedsTransform.Count; i ++) {

                if (m_selectorDistance > m_distance/2) {

                    if (i == 0 && (m_angle > (-(m_angleDistance/2)) || m_angle < (m_angleDistance/2))) SelectSeed(0);
                    else if (m_angle > (m_angleDistance / 2f) + (m_angleDistance * (i - 1)) && m_angle < (m_angleDistance / 2f) + (m_angleDistance * (i))) SelectSeed(i); 
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
            float m_a = m_canvasGroup.alpha;
            float m_b = active ? 1 : 0;

            for(float i = 0; i < m_time; i += Time.deltaTime) {

                m_canvasGroup.alpha = Mathf.Lerp(m_a, m_b, m_animationCurve.Evaluate(i / m_time));
                yield return null;
                }
            
            m_canvasGroup.alpha = m_b;
            
            if (!active) gameObject.SetActive(false);
            }
		
        }
