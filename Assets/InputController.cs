﻿using System.Collections;
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
            [SerializeField] private CursorVisibilityController m_cursorController = null;
			
            //Privadas.
            private InputAction m_moveAction = null;                                //Referencia a la accion de mover del jugador.
            private InputAction m_lookAction = null;                                //Referencia a la accion de mirar del jugador.
			
            //Privadas.
            private CameraBrain m_cameraBrain;
            private PlayerBrain m_playerBrain;

            private bool m_isShowingCursor = false;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {
			
            //Establecer referencias a componentes.
            m_cameraBrain = CameraBrain.GetSingleton();
            m_playerBrain = PlayerBrain.GetSingleton();

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
            SetPlayerVelocity(new Vector3(m_moveValue.x, 0, m_moveValue.y));
            }
		
        //Funciones privadas.
        private void SetCameraVelocity(Vector2 velocity) {

            if (m_cameraBrain == null) return;
            m_cameraBrain.SetRotationVelocity(velocity);
            }
        private void SetPlayerVelocity(Vector3 velocity) {

            if (m_playerBrain == null) return;
            m_playerBrain.SetVelocity(velocity);
            }
		
        //Funciones publicas.
        public void ShowCursor(InputAction.CallbackContext context) {

            bool m_temporalCursor = false;

            if (context.phase == InputActionPhase.Started) m_temporalCursor = true;
            else if (context.phase == InputActionPhase.Canceled) m_temporalCursor = false;
            else return; 

            m_isShowingCursor = m_temporalCursor;
            m_cursorController.SetTemporalVisible(m_temporalCursor);
            }
        public void Zoom(InputAction.CallbackContext context) {
            
            m_cameraBrain.Zoom(-context.ReadValue<float>());
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
