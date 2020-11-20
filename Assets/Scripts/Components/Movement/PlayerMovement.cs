using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            //Privadas.
            private float m_stamina;
			private bool m_run = false;
            private bool m_tired = false;

    //Funciones
		
        //Funciones de MonoBehaviour
        protected override void Start() {

            base.Start();

            //Establecer referencia de la camara.
            m_stamina = m_staminaMaxSecs;
            }
        
        protected override void Update() {

            base.Update();

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

        //Funciones heredadas.
        public override void SetHorizontalVelocity(Vector2 velocity) {
            
            Vector3 m_calculatedVelocity = CameraController.GetDirection() * new Vector3(velocity.x, 0, velocity.y);
            velocity = new Vector2(m_calculatedVelocity.x, m_calculatedVelocity.z);
            base.SetHorizontalVelocity(velocity);
            }

    //Funciones ha heredar.

    //Corotinas.  


        //Funciones ha heredar.
		
        //Corotinas.  
        }
