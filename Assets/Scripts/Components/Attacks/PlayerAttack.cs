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
            [SerializeField] private SwordSlash m_slash = null;

            [Header("Attack Force")]
            [SerializeField] private float m_attackForce = 0.5f;
            [SerializeField] private float m_attackForceTime = 0.25f;

            [Header("Auto Attack")]
            [SerializeField] private float m_autoTargetAttackDistance = 1f;

            //Privadas.
            private float m_delay;
            private Vector3 m_savedPosition;
            private bool m_inverse = false;
            private Coroutine m_attackCoroutine;

    //Funciones
		
        //Funciones de MonoBehaviour.
		protected void Start() {

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
                }

            else {
                
                m_dir = m_playerMovement.GetMovementDirection();
                }
            
            m_playerMovement.LookAt(m_dir);
            m_knockback = new Knockback(m_dir, m_attackForce, m_attackForceTime);
            m_playerMovement.SetKnockback(m_knockback);

            PlayerBrain.GetSingleton().GetAnimator().SetTrigger(m_inverse ? "attackL" : "attackR");

            m_inverse = !m_inverse;
            m_slash.Slash(m_inverse);

            base.Attack();
            return;
            }
            
        //Funciones heredadas.
/*        public override void DoDamage(Collider collider) {

            if (!collider.CompareTag("Enemy")) return;

            //Variables
            DungeonData m_data = DataSystem.GetSingleton().GetDungeonData();
            Weapon m_weapon = DataSystem.GetSingleton().GetActualWeapon();

            //Knockback
            Vector3 m_selfPos = GetParentTransform().position;
            Vector3 m_otherPos = collider.transform.position;
            Vector2 m_direction = (new Vector2(m_otherPos.x, m_otherPos.z) - new Vector2(m_selfPos.x, m_selfPos.z)).normalized;
            Knockback m_knockback = new Knockback(m_direction, m_weapon.GetKnockbackForce(), m_weapon.GetKnockbackTime());

            //Damage
            m_data.UseWeapon();

            int m_uses = m_data.GetActualWeapon().GetUses();
            int m_damage = m_weapon.GetCalculatedDamage(m_uses);

            //Do Damage
            collider.GetComponent<EntityHealth>().GetDamage(m_damage, m_knockback);   
            } */

        //Funciones ha heredar.

    //Corotinas.
    private IEnumerator AttackCoroutine() {

        m_playerMovement.IsAttacking(true);

        m_delay = m_attackDelay;

        while(m_delay > 0) {

            m_delay -= Time.deltaTime;
            yield return null;
            }

        m_playerMovement.IsAttacking(false);
        m_delay = 0;
        yield return new WaitForSeconds(0.10f);
        GetCollider().transform.localPosition = m_savedPosition;
        }
    }
