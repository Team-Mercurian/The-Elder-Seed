using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class EntityMovement : GravityValues {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Movement")]
			[SerializeField] protected float m_movementSpeed = 4;             //Multiplicador de velocidad.
            [SerializeField] protected float m_movementSmoothness = 0.1f;     //Suavidad con la que se interpolara la velocidad actual con la nueva (0 es ninguna, > a 0 es suave).

            [Header("Rotation")]
            [SerializeField] protected float m_rotationSmoothness = 0.2f;

            [Header("Character References")]
            [SerializeField] protected CharacterController m_characterController = null;          //Referencia al controlador del personaje.

            [Header("Gravity")]
            [SerializeField] protected float m_upGravityMultiplier = 1;
            [SerializeField] protected float m_downGravityMultiplier = 1;

            [Header("Ground Detection")]
            [SerializeField] protected float m_groundCheckDistance = 0.3f;
            [SerializeField] protected Vector3 m_groundCheckOffset = new Vector3();

            [Header("Knockback")]
            [SerializeField] protected AnimationCurve m_knockbackCurve = null;

//            [Header("Slide")]
//            [SerializeField] protected Vector2 m_slideForce = Vector2.zero;

            //Privadas.

                //Movimiento.
                protected Vector3 m_velocity = Vector3.zero;                          //Velocidad del jugador.
                protected Vector2 m_smoothVelocity = Vector2.zero;
                protected Vector2 m_reachVelocity = Vector3.zero;                     //Variable para guardar el movimiento a utilizar por el rigidbody.

                protected Coroutine m_movementCoroutine = null;                     //Corotina a utilizar para mover el personaje sin usar update.
                protected float m_rotationVelocity = 0;
                protected float m_speed = 0;
                
                protected float m_fallVelocity = 0;
                protected bool m_isGrounded;

                protected Vector2 m_knockbackValue = new Vector2();
                private Coroutine m_knockbackCoroutine;
                private bool m_canMove = true;

//                protected bool m_isInSlope = false;
//                protected Vector3 m_slopeHitPlace;
//                protected Vector3 m_slopeFallForce;

//                protected RaycastHit m_groundHit;

    //Funciones
		
        //Funciones de MonoBehaviour.
        protected virtual void Start() {

            m_speed = m_movementSpeed;
            }
        protected virtual void Update() {

            DetectGround();
            SetGravityVelocity();
//            SlideDown();
            SetVerticalVelocity(-m_fallVelocity);
            RotateSmooth(m_velocity);
            }
        protected void FixedUpdate() {

            //Mover al jugador de manera suave en el rigidbody.
            m_velocity = SetFinalVelocity(new Vector3(m_smoothVelocity.x + m_knockbackValue.x, m_velocity.y, m_smoothVelocity.y + m_knockbackValue.y));
            m_characterController.Move(m_velocity * Time.deltaTime); 
            }
        protected void OnDrawGizmosSelected() {

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + m_groundCheckOffset, transform.position + m_groundCheckOffset + new Vector3(0, -m_groundCheckDistance, 0));
            }
//        protected void OnControllerColliderHit(ControllerColliderHit hit) {

//            m_slopeHitPlace = hit.normal;
//            }
		
        //Funciones privadas.
        protected void DetectGround() {

            m_isGrounded = Physics.Linecast(transform.position + m_groundCheckOffset, transform.position + m_groundCheckOffset + new Vector3(0, -m_groundCheckDistance, 0), 1 << 8);
            }
//        protected void SlideDown() {

            //m_slopeHitPlace = m_groundHit.normal;

//            float m_angle = Vector3.Angle(Vector3.up, m_slopeHitPlace);
//            m_isInSlope = m_angle >= 45;

//            if (m_angle > m_characterController.slopeLimit) m_slopeFallForce = new Vector3(m_slopeHitPlace.x * m_slideForce.x, -m_slideForce.y, m_slopeHitPlace.z * m_slideForce.x);
//            else if (m_angle > 1) m_slopeFallForce = new Vector3(0, -m_slideForce.y, 0);
//            else m_slopeFallForce = Vector3.zero;

//            Debug.Log(m_angle);
//            }
        protected void RotateSmooth(Vector2 lookAtPoint) {
            
            if (lookAtPoint.x != 0 || lookAtPoint.y != 0) {

                float m_rotationValue = (Mathf.Atan2(lookAtPoint.y, -lookAtPoint.x) * Mathf.Rad2Deg) - 90;
                transform.eulerAngles = new Vector3(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, m_rotationValue, ref m_rotationVelocity, m_rotationSmoothness), 0);
                }
            }
        protected bool CompareHorizontalVelocity(Vector2 velocity, float threshold) {

            bool m_a = Mathf.Abs(velocity.x) > threshold;
            bool m_b = Mathf.Abs(velocity.y) > threshold;

            if (m_a || m_b) return true;
            else return false;
            }
        protected virtual void SetGravityVelocity() {

            if (m_isGrounded) 
                m_fallVelocity = 0;

            else if (m_characterController.velocity.y < 0.05f)
                m_fallVelocity += GetGravityDownIntensity(m_downGravityMultiplier) * Time.deltaTime;
            }

        //Funciones publicas.
        public Vector3 GetVelocity() {

            return m_velocity;
            }
        public void SetKnockback(Knockback knockback) {

            if (m_knockbackCoroutine != null) StopCoroutine(m_knockbackCoroutine); 
            m_knockbackCoroutine = StartCoroutine(KnockbackAnimation(knockback));
            }
        public void SetPositionAndDirection(Vector3 position, float angle) {

            m_characterController.enabled = false;
            transform.position = position;
            m_characterController.enabled = true;

            transform.eulerAngles = new Vector3(0, angle, 0);
            }
        public virtual void SetHorizontalVelocity(Vector2 velocity) {

            if (!m_canMove) return;
            SetReachVelocity(velocity);
            }
        public Vector2 GetMovementDirection() {
            
            Vector3 m_rawDir = transform.forward;
            Vector2 m_direction = new Vector2(m_rawDir.x, m_rawDir.z);

            return m_direction;
            }
        public void SetIfCanMove(bool canMove) => m_canMove = canMove; 
        public bool GetIfIsGrounded() => m_isGrounded;

        //Funciones ha heredar.

            //Protegidas.
            protected void SetReachVelocity(Vector2 velocity) {

                m_reachVelocity = velocity;
                m_smoothVelocity = (m_reachVelocity * m_speed); //if (m_movementCoroutine == null) m_movementCoroutine = StartCoroutine(HorizontalSmoothMove());
                }
            public virtual void SetSpeed(float speed) {

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
        private IEnumerator KnockbackAnimation(Knockback knockback) {
            
            m_knockbackValue += knockback.GetDirection() * knockback.GetForce();
            Vector2 m_defPos = m_knockbackValue;

            for(float i = 0; i < knockback.GetTime(); i += Time.deltaTime) {

                m_knockbackValue = Vector2.Lerp(m_defPos, Vector2.zero, m_knockbackCurve.Evaluate(i / knockback.GetTime()));
                yield return null;
                }

            m_knockbackValue = Vector2.zero;
            }
        }

public abstract class JumpingCharacter : EntityMovement {

    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Privadas

            //Publicas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Jump Values")]
            [SerializeField] protected float m_jumpForce = 6;
            [SerializeField] protected float m_jumpFrontForce = 1;

            //Privadas.
            protected bool m_canJump = true;
            protected bool m_isJumping;
            protected Vector2 m_jumpDirection;

    //Funciones
		
        //Funciones heredadas.
        protected override void SetGravityVelocity() {

            if (m_isGrounded && !m_isJumping) 
                m_fallVelocity = 0;

            else if (m_characterController.velocity.y < 0.05f)
                m_fallVelocity += GetGravityDownIntensity(m_downGravityMultiplier) * Time.deltaTime;

            else if (m_characterController.velocity.y > 0.05f) 
                m_fallVelocity += GetGravityUpIntensity(m_upGravityMultiplier) * Time.deltaTime;
            }
        protected override Vector3 SetFinalVelocity(Vector3 velocity) {
            
            float m_x = velocity.x + m_jumpDirection.x;
            float m_y = velocity.y;
            float m_z = velocity.z + m_jumpDirection.y;
            
            return new Vector3(m_x, m_y, m_z);
            }

        //Funciones publicas.
        public virtual void Jump() {

            if (!m_isGrounded || !m_canJump) return;
            StartCoroutine(JumpAnimation());
            }

        public void SetIfCanJump(bool canJump) {

            m_canJump = canJump;
            }
    
        //Corotinas.
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

public struct Knockback {

    private Vector2 m_direction;
    private float m_force;
    private float m_time;

    public Knockback(Vector2 direction, float force, float time) {

        m_direction = direction;
        m_force = force;
        m_time = time;
        }

    public Vector2 GetDirection() => m_direction;
    public float GetForce() => m_force;
    public float GetTime() => m_time;
    }