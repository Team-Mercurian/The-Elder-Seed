using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : CharacterBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas.
            private static PlayerBrain m_instance;
            private static Vector3 m_velocity = Vector3.zero;                              //Velocidad del jugador.
			
        //Establecer variables.
		
            //Publicas.
            [Header("Player Values")]
            [SerializeField] private float m_runSpeed = 8; 
            [SerializeField] private float m_rotationSmoothness = 0.2f;

            //Privadas.
            private CameraBrain m_cameraBrain = null;                               //Referencia a la camara.
			private bool m_run = false;
            private float m_rotationVelocity;

    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {

            m_instance = this;
            }
        protected override void Start() {

            //Establecer referencia a la base de la herarquia.
            base.Start();

            //Establecer referencia de la camara.
            m_cameraBrain = CameraBrain.GetSingleton();
            }
		
        private void Update() {

            //Crear fluidez entre la velocidad actual y la velocidad proxima.
            m_velocity = Vector3.SmoothDamp(m_velocity, (m_cameraBrain.GetDirection() * m_movementVelocity), ref m_moveDampVelocity, m_movementSmoothness, Mathf.Infinity, Time.deltaTime);
            
            //Girar el modelo del jugador dependiendo de la velocidad de este.
            if (m_velocity.x != 0 || m_velocity.y != 0) {

                float m_rotationValue = (Mathf.Atan2(m_velocity.z, -m_velocity.x) * Mathf.Rad2Deg) - 90;
                transform.eulerAngles = new Vector3(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, m_rotationValue, ref m_rotationVelocity, m_rotationSmoothness), 0);
                }
            }
        private void FixedUpdate() {

            //Mover al jugador de manera suave en el rigidbody.
            m_rigidbody.velocity = m_velocity * (m_run ? m_runSpeed : m_movementSpeed); 
            }
		
        //Funciones privadas.

        //Funciones publicas.
        public static PlayerBrain GetSingleton() {

            return m_instance;
            }
        public void SetVelocity(Vector3 velocity) {

            m_movementVelocity = velocity;
            }
        public void SetRun(bool active) {

            m_run = active;
            }
		
        //Funciones heredadas.
        protected override void Dead() {

            
            }
        protected override int GetActualHealth() {
            
            return GetMaxHealth();
            }
		
        //Funciones ha heredar.
		
        //Corotinas.  
        }
