using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class MovementBehaviour : GameBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Character Movement")]
			[SerializeField] protected float m_movementSpeed = 4;             //Multiplicador de velocidad.
            [SerializeField] protected float m_movementSmoothness = 0.1f;     //Suavidad con la que se interpolara la velocidad actual con la nueva (0 es ninguna, > a 0 es suave).

            [Header("Character Rotation")]
            [SerializeField] private float m_rotationSmoothness = 0.2f;

            [Header("Character References")]
            [SerializeField] protected CharacterController m_characterController = null;          //Referencia al controlador del personaje.

            //Privadas.

                //Movimiento.
                private Vector3 m_velocity = Vector3.zero;                          //Velocidad del jugador.
                private Vector2 m_smoothVelocity = Vector2.zero;
                private Vector2 m_reachVelocity = Vector3.zero;                     //Variable para guardar el movimiento a utilizar por el rigidbody.

                protected Coroutine m_movementCoroutine = null;                     //Corotina a utilizar para mover el personaje sin usar update.
                private float m_rotationVelocity = 0;
                private float m_speed = 0;

    //Funciones
		
        //Funciones de MonoBehaviour.
        protected virtual void Start() {

            m_speed = m_movementSpeed;
            }
        protected virtual void Update() {

            if (m_velocity.x != 0 || m_velocity.z != 0) {

                float m_rotationValue = (Mathf.Atan2(m_velocity.z, -m_velocity.x) * Mathf.Rad2Deg) - 90;
                transform.eulerAngles = new Vector3(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, m_rotationValue, ref m_rotationVelocity, m_rotationSmoothness), 0);
                }
            }
        private void FixedUpdate() {

            //Mover al jugador de manera suave en el rigidbody.
            m_velocity = SetFinalVelocity(new Vector3(m_smoothVelocity.x, m_velocity.y, m_smoothVelocity.y));
            m_characterController.Move(m_velocity * Time.deltaTime); 
            }
		
        //Funciones privadas.
        protected bool CompareHorizontalVelocity(Vector2 velocity, float threshold) {

            bool m_a = Mathf.Abs(velocity.x) > threshold;
            bool m_b = Mathf.Abs(velocity.y) > threshold;

            if (m_a || m_b) return true;
            else return false;
            }

        //Funciones publicas.
        public Vector3 GetVelocity() {

            return m_velocity;
            }
        public virtual void SetHorizontalVelocity(Vector2 velocity) {

            SetReachVelocity(velocity);
            }
        //Funciones ha heredar.

            //Protegidas.
            protected void SetReachVelocity(Vector2 velocity) {

                m_reachVelocity = velocity;
                m_smoothVelocity = m_reachVelocity * m_speed;//if (m_movementCoroutine == null) m_movementCoroutine = StartCoroutine(HorizontalSmoothMove());
                }
            protected virtual void SetSpeed(float speed) {

                m_speed = speed;
                }
            protected virtual Vector3 SetFinalVelocity(Vector3 velocity) {

                return velocity;
                }
            protected virtual void SetVerticalVelocity(float velocity) {

                m_velocity = new Vector3(m_velocity.x, velocity, m_velocity.z);
                }

            //Abstractas.
		
        //Corotinas.
		private IEnumerator HorizontalSmoothMove() {

            //if (m_reachVelocity == Vector2.zero) yield break;

            Vector2 m_moveDampVelocity = Vector2.zero;

            while(true) {

                m_smoothVelocity = Vector2.SmoothDamp(m_smoothVelocity, m_reachVelocity * m_speed, ref m_moveDampVelocity, m_movementSmoothness, Mathf.Infinity, Time.deltaTime);

                if (m_reachVelocity == Vector2.zero && !CompareHorizontalVelocity(m_smoothVelocity, 0.025f)) break;
                yield return null;
                }

            m_smoothVelocity = Vector2.zero;
            m_movementCoroutine = null;
            }       
        }

public abstract class GravityBehaviour : MovementBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas.
			
        //Establecer variables.
		
            //Publicas.
            [Header("Gravity Values")]
            [SerializeField] private float m_upGravityMultiplier = 1;
            [SerializeField] private float m_downGravityMultiplier = 1;

            [SerializeField] private float m_groundCheckDistance = 0.1f;
            [SerializeField] private Vector3 m_groundCheckOffset = new Vector3();

            //Privadas.
            protected float m_fallVelocity = 0;
            protected bool m_isGrounded;

			private readonly float m_upGravity = 15;
			private readonly float m_downGravity = 35;
			
    //Funciones
		
        //Funciones de MonoBehaviour.
        protected override void Update() {

            base.Update();
            DetectGround();
            SetGravityVelocity();
            SetVerticalVelocity(-m_fallVelocity);
            }
        private void OnDrawGizmosSelected() {

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + m_groundCheckOffset, transform.position + m_groundCheckOffset + new Vector3(0, -m_groundCheckDistance, 0));
            }

        //Funciones privadas.
        private void DetectGround() {

            m_isGrounded = Physics.Linecast(transform.position + m_groundCheckOffset, transform.position + m_groundCheckOffset + new Vector3(0, -m_groundCheckDistance, 0), 1 << 8);
            }
		
        //Funciones publicas.
        protected float GetGravityUpIntensity() {

            return m_upGravity * m_upGravityMultiplier;
            }
        protected float GetGravityDownIntensity() {

            return m_downGravity * m_downGravityMultiplier;
            }

        protected virtual void SetGravityVelocity() {

            if (m_isGrounded) 
                m_fallVelocity = 0;

            else if (m_characterController.velocity.y < 0.05f)
                m_fallVelocity += GetGravityDownIntensity() * Time.deltaTime;
            }
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }

public abstract class JumpingCharacter : GravityBehaviour {

            [Header("Jump Values")]
            [SerializeField] private float m_jumpForce = 10;
            [SerializeField] private float m_jumpFrontForce = 1;

            private bool m_canJump = true;
            private bool m_isJumping;
            private Vector2 m_jumpDirection;

        protected override void SetGravityVelocity() {

            if (m_isGrounded && !m_isJumping) 
                m_fallVelocity = 0;

            else if (m_characterController.velocity.y < 0.05f)
                m_fallVelocity += GetGravityDownIntensity() * Time.deltaTime;

            else if (m_characterController.velocity.y > 0.05f) 
                m_fallVelocity += GetGravityUpIntensity() * Time.deltaTime;
            }
        protected override Vector3 SetFinalVelocity(Vector3 velocity) {
            
            float m_x = velocity.x + m_jumpDirection.x;
            float m_y = velocity.y;
            float m_z = velocity.z + m_jumpDirection.y;
            
            return new Vector3(m_x, m_y, m_z);
            }

        public void Jump() {

            if (!m_isGrounded || !m_canJump) return;
            StartCoroutine(JumpAnimation());
            }

        public void SetIfCanJump(bool canJump) {

            m_canJump = canJump;
            }
    
        private IEnumerator JumpAnimation() {

            m_isJumping = true;

            m_fallVelocity = -m_jumpForce;
            m_jumpDirection = new Vector2(m_characterController.velocity.x, m_characterController.velocity.z) * m_jumpFrontForce;

            yield return new WaitForSeconds(0.2f);

            while(!m_isGrounded) {

                yield return null;
                }

            m_jumpDirection = Vector2.zero;
            m_isJumping = false;
            }
    }