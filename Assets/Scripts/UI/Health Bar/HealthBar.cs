using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	
	//Enumerators
	private enum HealthBarMode {

		Horizontal,
		Vertical,
		}

	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
		[Header("References")]
		[SerializeField] private Transform m_fill = null;
		[SerializeField] private Transform m_damageEffectFill = null;
        
		[Header("Settings")]
		[SerializeField] private HealthBarMode m_direction = HealthBarMode.Horizontal;
		[SerializeField] private float m_invisibleFillTime = 0.25f;

		[Header("Damage Effect")]
		[SerializeField] private float m_damageEffectDelay = 0.2f;
		[SerializeField] private float m_damageEffectTime = 0.25f;
		[SerializeField] private AnimationCurve m_damageEffectCurve = null;

        private float m_actualValue = 0;
		private Coroutine m_damageEffectRoutine = null;

    //Functions
	
		//MonoBehaviour Functions.
		        
        
		//Public Functions.
        public virtual void SetValue(float health, float maxHealth) {

			float m_value = Mathf.Clamp(health / maxHealth, 0, 1);
			if (m_value == m_actualValue) return;

			if (m_damageEffectRoutine != null) StopCoroutine(m_damageEffectRoutine);
			m_damageEffectRoutine = StartCoroutine(DamageEffect(m_value));
			}
        
		//Private Functions.
        
        
	//Coroutines
	private IEnumerator DamageEffect(float value) {
		
		//Set variables.
		float m_x = m_direction == HealthBarMode.Horizontal ? value : 1;
		float m_y = m_direction == HealthBarMode.Vertical ? value : 1;

		//Deactive for a moment the fill.
		m_fill.gameObject.SetActive(false);
		yield return new WaitForSeconds(m_invisibleFillTime);
		m_fill.gameObject.SetActive(true);

		//Set fill size.
		m_fill.localScale = new Vector3(m_x, m_y, 1);

		//Wait delay.
		yield return new WaitForSeconds(m_damageEffectDelay);

		//Set the damage fill scale depending of the time with a smooth move.
		Vector3 m_defScale = m_damageEffectFill.localScale;
		Vector3 m_finalScale = new Vector3(m_x, m_y, 1);

		for(float i = 0; i < m_damageEffectTime; i += Time.deltaTime) {

			m_damageEffectFill.localScale = Vector3.Lerp(m_defScale, m_finalScale, m_damageEffectCurve.Evaluate(i / m_damageEffectTime));
			yield return null;
			}
		
		//Set final values.
		m_damageEffectFill.localScale = m_finalScale;
		}
	}
