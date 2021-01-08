using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObtainedObjects_ItemUI : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        [Header("References")]
		[SerializeField] private RectTransform m_rectTransform = null;
		[SerializeField] private CanvasGroup m_canvasGroup = null;
		[SerializeField] private Image m_iconImage = null;
		[SerializeField] private TextMeshProUGUI m_titleText = null;

		[Header("Values")]
		[SerializeField] private float m_movementTime = 0.5f;
		[SerializeField] private float m_lifeTime = 4;
		[SerializeField] private float m_inTime = 0.2f;
		[SerializeField] private float m_outTime = 0.5f;
		[SerializeField] private AnimationCurve m_animationCurve = null;

		private Coroutine m_positionRoutine = null;
        private ObtainedObjectsUI m_parent = null;

    //Functions
	
		//MonoBehaviour Functions
        private void OnValidate() {

            if (m_animationCurve == null) {
                
                Keyframe m_kf1 = new Keyframe(0, 0);
                Keyframe m_kf2 = new Keyframe(1, 1);

                Keyframe[] m_kfs = new Keyframe[2] {m_kf1, m_kf2};

                m_animationCurve = new AnimationCurve(m_kfs);
                }
			}
        
		//Public Functions
		public void SetData(Sprite icon, string text, ObtainedObjectsUI parent) {
			
			m_iconImage.sprite = icon;
			m_titleText.text = text;
			m_parent = parent;

			StartCoroutine(LifeRoutine());
			IEnumerator LifeRoutine() {
				
				for(float i = 0; i < m_inTime; i += Time.deltaTime) {
					
					m_canvasGroup.alpha = Mathf.Lerp(0, 1, i / m_inTime);
					yield return null;
					}

				yield return new WaitForSeconds(m_lifeTime);

				for(float i = 0; i < m_outTime; i += Time.deltaTime) {

					m_canvasGroup.alpha = Mathf.Lerp(1, 0, i / m_outTime);
					yield return null;
					}

				m_parent.Remove(this);
				Destroy(gameObject);
				}
			}
        
		public void SetPosition(float position) {

			if (m_positionRoutine != null) StopCoroutine(m_positionRoutine);
			m_positionRoutine = StartCoroutine(PositionRoutine(position));
			}
        
		//Private Functions
        
        
	//Coroutines
	private IEnumerator PositionRoutine(float positionToMove) {

		float m_defX = m_rectTransform.anchoredPosition.x;
		float m_defY = m_rectTransform.anchoredPosition.y;

		for(float i = 0; i < m_movementTime; i += Time.deltaTime) {

			m_rectTransform.anchoredPosition = new Vector2(m_defX, Mathf.Lerp(m_defY, positionToMove, m_animationCurve.Evaluate(i / m_movementTime)));
			yield return null;
			}

		m_rectTransform.anchoredPosition = new Vector2(m_defX, positionToMove);
		}	
	}
