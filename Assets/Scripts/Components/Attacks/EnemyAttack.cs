using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EntityAttack {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Damage")]
            [SerializeField] protected int m_attackDamage = 2;

            [Header("Knockback")]
            [SerializeField] private float m_knockbackForce = 1;
            [SerializeField] private float m_knockbackTime = 0.5f;
            [SerializeField] private Animator m_animator = null;

    //Privadas.


    //Funciones

    //Funciones de MonoBehaviour

    //Funciones privadas.

    //Funciones publicas.

    //Funciones heredadas.
    /*        public override void DoDamage(Collider collider) {

                if (!collider.CompareTag("Player")) return;

                Vector3 m_selfPos = GetParentTransform().position;
                Vector3 m_otherPos = collider.transform.position;

                Vector2 m_direction = (new Vector2(m_otherPos.x, m_otherPos.z) - new Vector2(m_selfPos.x, m_selfPos.z)).normalized;

                Knockback m_knockback = new Knockback(m_direction, m_knockbackForce, m_knockbackTime);

                int m_damage = Mathf.RoundToInt(m_attackDamage * (1 + (0.25f * DataSystem.GetSingleton().GetDungeonData().GetFloor())));

                collider.GetComponent<EntityHealth>().GetDamage(m_damage, m_knockback);   
                }
            */
    public override void Attack()
    {
        base.Attack();
        if(m_animator != null) m_animator.SetTrigger("attack");
    }
    //Funciones ha heredar.

    //Corotinas.


        //Funciones ha heredar.
		
        //Corotinas.
		
        }
