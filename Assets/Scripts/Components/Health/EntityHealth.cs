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

            //Privadas.
            private Coroutine m_damageCoroutine;

            private bool m_canReceiveDamage = true;
            private int m_actualHealth;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {
			
            m_actualHealth = SetActualHealth();
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
        public void GetDamage(int damage, Vector2 damageVelocity) {
            
            if (!m_canReceiveDamage) return;

            //Reducir la vida.
            m_actualHealth = Mathf.Clamp(m_actualHealth - damage, 0, m_health);
            DamageTextController m_dt = Instantiate(m_damageText, transform.position + (Vector3.up * m_damageTextOffset) , Quaternion.identity).GetComponent<DamageTextController>();
            m_dt.SetData(damage.ToString());

            m_entityMovement.GetKnockback(damageVelocity);

            //Detectar si este perdio toda su vida.
            if (m_actualHealth == 0) {

                Dead();
                }

            else {

                if (m_damageCooldown > 0) m_damageCoroutine = StartCoroutine(CooldownAnimation());
                }
            }
        public void AddHealth(int health) {
            
            //Añadir vida.
            m_actualHealth = Mathf.Clamp(m_actualHealth + health, 0, m_health);
            }
		
        public int GetMaxHealth() => m_health;

        [ContextMenu("Get Debug Damage")]
        private void DebugReceiveDamage() {

            GetDamage(1, Vector2.zero);
            }
            
        [ContextMenu("Debug Dead")]
        private void DebugDead() {

            Dead();
            }
        //Funciones heredadas.
		
        //Funciones ha heredar.
        protected abstract int SetActualHealth();
        protected abstract void Dead();
		
        //Corotinas.
        private IEnumerator CooldownAnimation() {

            m_canReceiveDamage = false;

            for(float i = 0; i < m_damageCooldown; i += Time.deltaTime) {
                
                m_meshToDeactive.SetActive(((i * 10f) % 2f > 1));
                yield return null;
                }

            m_meshToDeactive.SetActive(true);
            m_canReceiveDamage = true;
            }
        }
