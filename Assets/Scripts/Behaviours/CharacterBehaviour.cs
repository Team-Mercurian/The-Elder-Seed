using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class CharacterBehaviour : GameBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
            protected static Vector3 m_movementVelocity = Vector3.zero;              //Variable para guardar el movimiento a utilizar por el rigidbody.
			
        //Establecer variables.
		
            //Publicas.
            [Header("Brain Movement")]
			[SerializeField] protected float m_movementSpeed = 4;             //Multiplicador de velocidad.
            [SerializeField] protected float m_movementSmoothness = 0.1f;     //Suavidad con la que se interpolara la velocidad actual con la nueva (0 es ninguna, > a 0 es suave).
            [Space]
            [SerializeField] protected Axis m_horizontalAxis = Axis.X;        //Axis a utilizar al moverse hacia los lados.
            [SerializeField] protected Axis m_verticalAxis = Axis.Z;          //Axis a utilizar al moverse hacia atras y al frente.

            [Header("Brain Health System")]
            [SerializeField] protected int m_health = 10;               //Puntos de vida maximos.

            [Header("Brain References")]
            [SerializeField] protected Rigidbody m_rigidbody = null;          //Referencia al rigidbody del personaje.

            //Privadas.

                //Movimiento.
                protected Coroutine m_movementCoroutine = null;                   //Corotina a utilizar para mover el personaje sin usar update.
                protected Vector3 m_moveDampVelocity = Vector2.zero;              //Variable utilizada por la funcion SmoothDamp dentro de la corotina de movimiento fluido.

                //Vida.
                protected int m_actualHealth;

    //Funciones
		
        //Funciones de MonoBehaviour.
        protected virtual void Start() {

            m_actualHealth = GetActualHealth();
            }
		
        //Funciones privadas.
        protected bool CompareVelocity(float threshold) {

            bool m_a = Mathf.Abs(GetAxisVelocity(m_horizontalAxis)) > threshold;
            bool m_b = Mathf.Abs(GetAxisVelocity(m_verticalAxis)) > threshold;

            if (m_a || m_b) return true;
            else return false;
            }
        protected float GetAxisVelocity(Axis axis) {

            float m_value = 0;

            switch(axis) {

                case Axis.X : m_value = m_rigidbody.velocity.x; break;
                case Axis.Y : m_value = m_rigidbody.velocity.y; break;
                case Axis.Z : m_value = m_rigidbody.velocity.z; break;
                }

            return m_value;
            }
        protected void SetVelocity(ref Vector3 actualVelocity, Vector2 newVelocity, Axis horizontalAxis, Axis verticalAxis) {
            
            float m_x = 0;
            float m_y = 0;
            float m_z = 0;

            switch(horizontalAxis) {

                case Axis.X : m_x = newVelocity.x; break;
                case Axis.Y : m_y = newVelocity.x; break;
                case Axis.Z : m_z = newVelocity.x; break;
                }

            switch(verticalAxis) {

                case Axis.X : m_x = newVelocity.y; break;
                case Axis.Y : m_y = newVelocity.y; break;
                case Axis.Z : m_z = newVelocity.y; break;
                }

            actualVelocity = new Vector3(m_x, m_y, m_z);
            }
		
        //Funciones publicas.
        public void SmoothMove(Vector2 velocity) {

            SetVelocity(ref m_movementVelocity, velocity, m_horizontalAxis, m_verticalAxis);
            if (m_movementCoroutine == null) m_movementCoroutine = StartCoroutine(MoveSmoothly());
            }

        //Funciones ha heredar.

            //Protegidas.
            protected int GetMaxHealth() {

                return m_health;
                }

            //Abstractas.
            protected abstract int GetActualHealth();
            protected abstract void Dead();
		
        //Corotinas.
		protected virtual IEnumerator MoveSmoothly() {

            if (m_movementVelocity == Vector3.zero) yield break;

            while(true) {

                m_rigidbody.velocity = Vector3.SmoothDamp(m_rigidbody.velocity, m_movementVelocity * m_movementSpeed, ref m_moveDampVelocity, m_movementSmoothness, Mathf.Infinity, Time.deltaTime);
                
                if (m_movementVelocity == Vector3.zero && !CompareVelocity(0.025f)) break;
                yield return null;
                }

            m_rigidbody.velocity = Vector3.zero;
            m_movementCoroutine = null;
            }       
        }
