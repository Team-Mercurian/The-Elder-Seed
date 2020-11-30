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
            [Header("Child References")]
            [SerializeField] protected Transform m_parentTransform = null;

            [Header("Delay")]
            [SerializeField] protected float m_attackDelay = 1;

            //Privadas.
            private float m_delay;
            private Vector3 m_savedPosition;
			
    //Funciones
		
        //Funciones de MonoBehaviour.
		protected override void Start() {

            base.Start();
            m_savedPosition = transform.localPosition;
            m_delay = 0;
            }

        //Funciones privadas.
		
        //Funciones publicas.
        public override void Attack() {

            if (m_delay > 0) return;

            base.Attack();
            }
        public void MoveColliderTo(Transform target) {

            StartCoroutine(SetCollisionTarget(target));
            }

        //Funciones heredadas.
        protected override string SetOtherTag() {

            return "Enemy";
            }
        protected override void SetDamage() {

            m_damage = DataSystem.GetSingleton().GetActualWeapon().GetCalculatedDamage(DataSystem.GetSingleton().GetActualWeapon().GetUses());
            }

        protected override void OnTriggerEnter(Collider collider) {

            if (collider.CompareTag(m_otherTag)) {

                InventoryData m_data = DataSystem.GetSingleton().GetGameData().GetInventoryData();

                m_data.UseActualWeapon();

                Weapon m_weapon = DataSystem.GetSingleton().GetActualWeapon();
                int m_uses = m_data.GetActualWeapon().GetUses();

                m_damage = m_weapon.GetCalculatedDamage(m_uses);

                collider.GetComponent<EntityHealth>().GetDamage(m_damage, (new Vector2(collider.transform.position.x, collider.transform.position.z) - new Vector2(m_parentTransform.position.x, m_parentTransform.position.z)).normalized);
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
    private IEnumerator SetCollisionTarget(Transform target) {

        transform.position = target.position;
        yield return new WaitForSeconds(m_attackDelay);

        transform.localPosition = m_savedPosition;
        }
    }
