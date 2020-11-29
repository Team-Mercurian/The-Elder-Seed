using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : JumpingCharacter {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas.
			
        //Establecer variables.
		
            //Publicas.
            [Header("Player Values")]
            [SerializeField] private float m_staminaMaxSecs = 4;
            [SerializeField] private float m_staminaFillVelocity = 1.5f;
            [SerializeField] private float m_staminaFillTiredVelocity = 1f;
            [SerializeField] private float m_tiredSpeed = 3.5f; 
            [SerializeField] private float m_runSpeed = 6f; 

            [Header("Player Slope Limits")]
            [SerializeField] private float m_defaultSlopeLimit = 45;
            [SerializeField] private float m_runSlopeLimit = 60;

            [Header("Player References")]
            [SerializeField] private Animator m_animator = null;
            [SerializeField] private AimHead m_aimHead = null;

            [Header("Player Attack Movement")]
            [SerializeField] private PlayerAttack m_playerAttack = null;
            [SerializeField] private float m_attackForce = 1f;
            [SerializeField] private float m_attackTime = 0.25f;

            
            //Privadas.
            private float m_stamina;
			private bool m_run = false;
            private bool m_tired = false;

            private Vector3 m_moveDirection = Vector3.zero;
            private bool m_isAttacking;

    //Funciones
		
        //Funciones de MonoBehaviour
        protected override void Start() {

            base.Start();

            //Establecer referencia de la camara.
            m_stamina = m_staminaMaxSecs;
            }
        
        protected override void Update() {

            DetectGround();
            SetGravityVelocity();
            SetVerticalVelocity(-m_fallVelocity);

            if (m_velocity.magnitude > 0.05f && !m_isAttacking) m_moveDirection = m_velocity;

            RotateSmooth(m_moveDirection);

            bool m_isRunning;
            float m_finalSpeed = 0;

            if (m_run && m_stamina > 0 && !m_tired) {

                m_stamina -= Time.deltaTime;
                m_finalSpeed = m_runSpeed;
                m_isRunning = true;

                if (m_stamina <= 0) {
                    
                    m_stamina = 0;
                    m_tired = true;
                    }
                }

            else {

                m_stamina = Mathf.Clamp(m_stamina + (m_tired ? m_staminaFillTiredVelocity : m_staminaFillVelocity) * Time.deltaTime, 0, m_staminaMaxSecs); 
                m_isRunning = false;

                if (m_tired && m_stamina == m_staminaMaxSecs) m_tired = false;    
                m_finalSpeed = m_tired ? m_tiredSpeed : m_movementSpeed;
                }

            SetIfCanJump(!m_tired);
            m_characterController.slopeLimit = m_isRunning ? m_runSlopeLimit : m_defaultSlopeLimit;

            SetSpeed(m_finalSpeed);
            }
		
        //Funciones privadas.

        //Funciones publicas.
        public void SetRun(bool active) {

            m_run = active;
            }
        public void OnAttack() {

            Transform m_target = m_aimHead.GetEnemyTarget();
            float m_dir = (CameraController.GetDirection().eulerAngles.y + 90) * Mathf.Deg2Rad;

            m_moveDirection = m_target == null ? new Vector3(-Mathf.Cos(m_dir), 0, Mathf.Sin(m_dir)) : m_target.position - transform.position;

            if (m_target != null) m_playerAttack.MoveColliderTo(m_target);

            StartCoroutine(AttackDelay());
            }

        //Funciones heredadas.
        public override void SetHorizontalVelocity(Vector2 velocity) {
            
            if (m_isAttacking) {

                Vector3 m_vel = m_moveDirection.normalized * m_attackForce;
                velocity = new Vector2(m_vel.x, m_vel.z);
                }

            else {

                Vector3 m_calculatedVelocity = CameraController.GetDirection() * new Vector3(velocity.x, 0, velocity.y);
                velocity = new Vector2(m_calculatedVelocity.x, m_calculatedVelocity.z);
                }

            m_animator.SetFloat("velocity", velocity.magnitude);
            base.SetHorizontalVelocity(velocity);
            }

        //Funciones ha heredar.

    //Corotinas.  
    private IEnumerator AttackDelay() {

        m_isAttacking = true;

        yield return new WaitForSeconds(m_attackTime);

        m_isAttacking = false;
        }
    }
