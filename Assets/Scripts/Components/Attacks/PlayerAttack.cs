using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : EntityAttack {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Delay")]
            [SerializeField] protected float m_attackDelay = 1;

            //Privadas.
            private float m_delay;
			
    //Funciones
		
        //Funciones de MonoBehaviour.
		protected override void Start() {

            base.Start();
            m_delay = 0;
            }

        //Funciones privadas.
		
        //Funciones publicas.
        public override void Attack() {

            if (m_delay > 0) return;

            base.Attack();
            }

        //Funciones heredadas.
        protected override string SetOtherTag() {

            return "Enemy";
            }
        protected override void SetDamage() {

            m_damage = DataSystem.GetSingleton().GetActualWeapon().GetMaxDamage();
            }

        protected override void OnTriggerEnter(Collider collider) {

            if (collider.CompareTag(m_otherTag)) {

                collider.GetComponent<EntityHealth>().GetDamage(m_damage);

                DataSystem.GetSingleton().UseActualWeapon();

                Weapon m_weapon = DataSystem.GetSingleton().GetActualWeapon();
                int m_uses = DataSystem.GetSingleton().GetActualWeaponData().GetUses();

                m_damage = m_weapon.GetCalculatedDamage(m_uses);

                Debug.Log("Weapon used. Actual durability: " + m_uses + ". Actual Damage: " + m_damage + "/" + m_weapon.GetMaxDamage() + ".");
                }
            }

        //Funciones ha heredar.

    //Corotinas.
    protected override IEnumerator AttackCoroutine() {

        yield return StartCoroutine(base.AttackCoroutine());

        m_delay = m_attackDelay;

        while(m_delay > 0) {

            m_delay -= Time.deltaTime;
            yield return null;
            }

        m_delay = 0;
        }
        }
