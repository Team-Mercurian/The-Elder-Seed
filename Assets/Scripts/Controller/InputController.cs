using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputController : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Input References")]
            [SerializeField] private PlayerInput m_playerInput = null;              //Referencia al Input System del jugador.
			
            //Privadas.
            private InputAction m_moveAction = null;                                //Referencia a la accion de mover del jugador.
            private InputAction m_lookAction = null;                                //Referencia a la accion de mirar del jugador.
			
            //Privadas.
            private CameraBrain m_cameraBrain;
            private CursorController m_cursorController;

            private bool m_isShowingCursor = false;

            private PlayerMovement m_playerMovement;
            private PlayerAttack m_playerAttack;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {
			
            //Establecer referencias a componentes.
            m_cameraBrain = CameraBrain.GetSingleton();
            m_playerMovement = PlayerBrain.GetSingleton().GetMovement();
            m_playerAttack = PlayerBrain.GetSingleton().GetAttack();
            m_cursorController = CursorController.GetSingleton();

            //Establecer las referencias a las acciones.
            m_moveAction = m_playerInput.actions["Player/Move"];
            m_lookAction = m_playerInput.actions["Player/Look"];
            }
        private void Update() {

            //Establecer la velocidad de rotacion de la camara solo si no se esta mostrando el cursor.
            if (!m_isShowingCursor) {

                Vector2 m_lookValue = m_lookAction.ReadValue<Vector2>();
                SetCameraVelocity(m_lookValue);
                }

            //Establecer la velocidad del jugador.
            Vector2 m_moveValue = m_moveAction.ReadValue<Vector2>();
            SetPlayerVelocity(new Vector2(m_moveValue.x, m_moveValue.y));
            }
		
        //Funciones privadas.
        private void SetCameraVelocity(Vector2 velocity) {

            if (m_cameraBrain == null) return;
            m_cameraBrain.SetRotationVelocity(velocity);
            }
        private void SetPlayerVelocity(Vector2 velocity) {

            if (m_playerMovement == null) return;
            m_playerMovement.SetHorizontalVelocity(velocity);
            }
		
        //Funciones publicas.
        public void ShowCursor(InputAction.CallbackContext context) {

            if (m_cursorController == null) return;

            bool m_temporalCursor = false;

            if (context.phase == InputActionPhase.Started) m_temporalCursor = true;
            else if (context.phase == InputActionPhase.Canceled) m_temporalCursor = false;
            else return; 

            m_isShowingCursor = m_temporalCursor;
            m_cursorController.SetTemporalVisible(m_temporalCursor);
            }
        public void Zoom(InputAction.CallbackContext context) {
            
            if (m_cameraBrain == null) return;

            m_cameraBrain.Zoom(-context.ReadValue<float>());
            }
        public void Run(InputAction.CallbackContext context) {

            if (m_playerMovement == null) return;

            bool m_run = false;

            if (context.phase == InputActionPhase.Started) m_run = true;
            else if (context.phase == InputActionPhase.Canceled) m_run = false;
            else return; 

            m_playerMovement.SetRun(m_run);
            }
        public void Jump(InputAction.CallbackContext context) {
            
            if (context.phase != InputActionPhase.Started) return;
            
            m_playerMovement.Jump();
            }
        public void Attack(InputAction.CallbackContext context) {
            
            if (context.phase != InputActionPhase.Started) return;
            
            m_playerAttack.Attack();
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
