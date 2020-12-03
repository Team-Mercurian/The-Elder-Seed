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

            [Header("Attack References")]
            [SerializeField] private AimHead m_aimHead = null;
            [SerializeField] private PlayerMovement m_playerMovement = null;

            [Header("Attack Force")]
            [SerializeField] private float m_attackForce = 0.5f;
            [SerializeField] private float m_attackForceTime = 0.25f;

            [Header("Auto Attack")]
            [SerializeField] private float m_autoTargetAttackDistance = 1f;

            //Privadas.
            private float m_delay;
            private Vector3 m_savedPosition;
			
    //Funciones
		
        //Funciones de MonoBehaviour.
		protected override void Start() {

            base.Start();
            m_savedPosition = GetCollider().transform.localPosition;
            m_delay = 0;
            }

        //Funciones privadas.
		
        //Funciones publicas.
        public override void Attack() {

            if (m_delay > 0) return;

            Transform m_target = m_aimHead.GetEnemyTarget();
            Vector2 m_dir;
            Knockback m_knockback; 

            if (m_target != null && Vector2.Distance(m_target.position, transform.position) > m_autoTargetAttackDistance) m_target = null; 
            if (m_target != null) {
                
                Vector3 m_rawDir = (m_target.position - transform.position).normalized;

                m_dir = new Vector2(m_rawDir.x, m_rawDir.z);
                StartCoroutine(SetCollisionTarget(m_target));
                }

            else {
                
                m_dir = m_playerMovement.GetMovementDirection();
                }
            
            m_playerMovement.LookAt(m_dir);
            m_knockback = new Knockback(m_dir, m_attackForce, m_attackForceTime);
            m_playerMovement.SetKnockback(m_knockback);

            base.Attack();
            }
            
        //Funciones heredadas.
        public override void DoDamage(Collider collider) {

            if (!collider.CompareTag("Enemy")) return;

            //Variables
            InventoryData m_data = DataSystem.GetSingleton().GetGameData().GetInventoryData();
            Weapon m_weapon = DataSystem.GetSingleton().GetActualWeapon();

            //Knockback
            Vector3 m_selfPos = GetParentTransform().position;
            Vector3 m_otherPos = collider.transform.position;
            Vector2 m_direction = (new Vector2(m_otherPos.x, m_otherPos.z) - new Vector2(m_selfPos.x, m_selfPos.z)).normalized;
            Knockback m_knockback = new Knockback(m_direction, m_weapon.GetKnockbackForce(), m_weapon.GetKnockbackTime());

            //Damage
            m_data.UseActualWeapon();

            int m_uses = m_data.GetActualWeapon().GetUses();
            int m_damage = m_weapon.GetCalculatedDamage(m_uses);

            //Do Damage
            collider.GetComponent<EntityHealth>().GetDamage(m_damage, m_knockback);   
            }

        //Funciones ha heredar.

    //Corotinas.
    protected override IEnumerator AttackCoroutine() {

        m_playerMovement.IsAttacking(true);
        yield return StartCoroutine(base.AttackCoroutine());

        m_delay = m_attackDelay;

        while(m_delay > 0) {

            m_delay -= Time.deltaTime;
            yield return null;
            }

        m_playerMovement.IsAttacking(false);
        m_delay = 0;
        }
    private IEnumerator SetCollisionTarget(Transform target) {

        GetCollider().transform.position = target.position;

        yield return new WaitForSeconds(0.05f);
        GetCollider().transform.localPosition = m_savedPosition;
        }
    }
