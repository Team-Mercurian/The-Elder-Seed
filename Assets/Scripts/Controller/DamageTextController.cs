using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextController : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("References")]
		[SerializeField] private RectTransform m_rect = null;
		[SerializeField] private TextMeshPro m_text = null;

		[Header("Values")]
        [SerializeField] private AnimationCurve m_animationCurve = null;
        [SerializeField] private Vector2 m_jumpMultiplier = Vector2.one;
		[SerializeField] private float m_inOutTime = 0.5f;
		[SerializeField] private float m_inAirTime = 0.25f;

		private Vector3 m_defPosition;
		private bool m_negatePosition;

    //Functions
	
		//MonoBehaviour Functions

		//Public Functions
        public void SetData(string text) {
			
			m_text.text = text;
			m_defPosition = m_rect.position;
			m_negatePosition = Random.Range(0f, 1f) < 0.5f;
			StartCoroutine(TextAnimation());
			}
        
		//Private Functions
		private void SetPosition(float angle) {
			
			m_rect.eulerAngles = new Vector3(0, CameraController.GetDirection().eulerAngles.y, 0);
			m_rect.position = m_defPosition + (new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * m_jumpMultiplier.x * (m_negatePosition ? -1 : 1), Mathf.Sin(angle * Mathf.Deg2Rad) * m_jumpMultiplier.y));
			}
        
        
	//Coroutines
	private IEnumerator TextAnimation() {
		
		float m_angle = 0;
		bool m_negate = Random.Range(0f, 1f) < 0.5f;

		for(float i = 0; i < m_inOutTime; i += Time.deltaTime) {
			
			float m_evaluation = m_animationCurve.Evaluate(i / m_inOutTime);

			m_rect.localScale = Vector3.one * Mathf.Lerp(0, 1, m_evaluation);
			m_angle = Mathf.Lerp(0, 90, m_evaluation);
			SetPosition(m_angle);
			yield return null;
			}

		m_rect.localScale = Vector3.one;
		yield return new WaitForSeconds(m_inAirTime);

		for(float i = 0; i < m_inOutTime; i += Time.deltaTime) {
			
			float m_evaluation = m_animationCurve.Evaluate(i / m_inOutTime);

			m_rect.localScale = Vector3.one * Mathf.Lerp(1, 0, m_evaluation);
			m_angle = Mathf.Lerp(90, 180, m_evaluation);
			SetPosition(m_angle);
			yield return null;
			}

		Destroy(gameObject);
		}
	}
