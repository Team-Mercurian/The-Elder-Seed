using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
public abstract class PanelUI : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Base Values")]
            [SerializeField] private bool m_closeAtStart = true;

            [Header("Base Animation")]
            [SerializeField] private AnimationCurve m_animationCurve = null;

            [Header("Base References")]
			[SerializeField] private RectTransform m_rectTransform = null;
			[SerializeField] private CanvasGroup m_canvasGroup = null;

            //Privadas.
            private bool m_inOutAnimation = false;
			private Vector2 m_defValue;
		    private bool m_isOpen = false;
			
    //Funciones
		
        //Funciones de MonoBehaviour.
        protected virtual void Start() {

            m_defValue = m_rectTransform.anchoredPosition;
            
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;

            m_isOpen = !m_closeAtStart;
            gameObject.SetActive(!m_closeAtStart);
            }
        private void OnValidate() {

            if (m_canvasGroup == null) TryGetComponent<CanvasGroup>(out m_canvasGroup);
            if (m_rectTransform == null) TryGetComponent<RectTransform>(out m_rectTransform);

            if (m_animationCurve == null) {
                
                Keyframe m_kf1 = new Keyframe(0, 0);
                Keyframe m_kf2 = new Keyframe(1, 1);

                Keyframe[] m_kfs = new Keyframe[2] {m_kf1, m_kf2};

                m_animationCurve = new AnimationCurve(m_kfs);
                }
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
		public void OpenClose() {

			if (m_isOpen) Close();
            else Open();
			}
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
        public virtual void Open() {
            
            if (m_inOutAnimation) return;
            gameObject.SetActive(true);
            StartCoroutine(PanelIOAnimation(true));
            InputController.SetLookObject(null);
            CursorController.GetSingleton().SetVisibility(true);
            }
        public virtual void Close() {
            
            if (m_inOutAnimation) return;
            StartCoroutine(PanelIOAnimation(false));
            InputController.SetLookObject(CameraController.GetSingleton());
            CursorController.GetSingleton().SetVisibility(false);
            }

        protected CanvasGroup GetCanvasGroup() => m_canvasGroup;
        protected float GetAnimationCurveEvaluation(float t) => m_animationCurve.Evaluate(t);

        //Corotinas.
        private IEnumerator PanelIOAnimation(bool entry) {
            
            m_inOutAnimation = true;
            float m_time = 0.5f;        

            Vector2 m_value = m_defValue + new Vector2(0, -32);
            
            Vector2 m_rTValue1 = entry ? m_value : m_defValue;
            Vector2 m_rTValue2 = entry ? m_defValue : m_value;

            float m_cGValue1 = entry ? 0 : 1;
            float m_cGValue2 = entry ? 1 : 0;

            for(float i = 0; i < m_time; i += Time.deltaTime) {
                
                float m_evaluation = m_animationCurve.Evaluate(i / m_time);

                m_rectTransform.anchoredPosition = Vector2.Lerp(m_rTValue1, m_rTValue2, m_evaluation);
                m_canvasGroup.alpha = Mathf.Lerp(m_cGValue1, m_cGValue2, m_evaluation);
                yield return null;
                }
            
            m_rectTransform.anchoredPosition = m_rTValue2;
            m_canvasGroup.alpha = m_cGValue2;

            m_isOpen = entry;
            m_inOutAnimation = false;

            if (!entry) gameObject.SetActive(true);
            m_canvasGroup.interactable = entry;
            }
        }
