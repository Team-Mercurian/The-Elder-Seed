using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityHealth : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Health")]
			[SerializeField] protected int m_health = 8;
            [SerializeField] protected GameObject m_damageText = null;
            [SerializeField] protected float m_damageTextOffset = 1;

            [Header("Damage Cooldown")]
            [SerializeField] protected GameObject m_meshToDeactive = null;
            [SerializeField] protected float m_damageCooldown = 1;

            [Header("References")]
            [SerializeField] protected EntityMovement m_entityMovement = null;
            [SerializeField] protected HealthBar m_healthBar = null;

            //Privadas.
            private Coroutine m_damageCoroutine;

            private bool m_canReceiveDamage = true;
            private int m_actualHealth;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        protected virtual void Start() {
			
            m_actualHealth = SetActualHealth();
            if (m_healthBar != null) m_healthBar.SetValue(m_actualHealth, m_health, true);
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
        public void GetDamage(int damage, Knockback knockback) {
            
            if (!m_canReceiveDamage) return;

            //Reducir la vida.
            m_actualHealth = Mathf.Clamp(m_actualHealth - damage, 0, m_health);
            DamageTextController m_dt = Instantiate(m_damageText, transform.position + (Vector3.up * m_damageTextOffset), Quaternion.identity).GetComponent<DamageTextController>();
            m_dt.SetData(damage.ToString());

            m_entityMovement.SetKnockback(knockback);

            //Detectar si este perdio toda su vida.
            if (m_actualHealth == 0) {

                Dead();
                if (m_healthBar != null) HealthBarDeadAction();
                }

            else {
                
                SaveHealth(m_actualHealth);
                if (m_healthBar != null) m_healthBar.SetValue(m_actualHealth, m_health, false);
                if (m_damageCooldown > 0) m_damageCoroutine = StartCoroutine(CooldownAnimation());
                }
            }
        public void AddHealth(int health) {
            
            //Añadir vida.
            m_actualHealth = Mathf.Clamp(m_actualHealth + health, 0, m_health);
            }

        protected abstract void SaveHealth(int health);
        public int GetMaxHealth() => m_health;

        public void OverrideHealth(int health) {

            m_actualHealth = Mathf.Clamp(health, 0, m_health);
            SaveHealth(m_actualHealth);
            if (m_healthBar != null) m_healthBar.SetValue(m_actualHealth, m_health, false);
            }


        [ContextMenu("Get Debug Damage")]
        private void DebugReceiveDamage() {

            GetDamage(150, new Knockback());
            }
            
        [ContextMenu("Debug Dead")]
        private void DebugDead() {

            Dead();
            }
        //Funciones heredadas.
		
        //Funciones ha heredar.
        protected abstract int SetActualHealth();
        protected abstract void Dead();
        protected abstract void HealthBarDeadAction();
		
        //Corotinas.
        private IEnumerator CooldownAnimation() {

            m_canReceiveDamage = false;

            for(float i = 0; i < m_damageCooldown; i += Time.deltaTime) {
                
                if (m_meshToDeactive != null) m_meshToDeactive.SetActive(((i * 10f) % 2f > 1));
                yield return null;
                }

            if (m_meshToDeactive != null) m_meshToDeactive.SetActive(true);
            m_canReceiveDamage = true;
            }
        }
