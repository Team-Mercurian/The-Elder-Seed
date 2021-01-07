using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputType {

    Keyboard,
    Gamepad
    }

[RequireComponent(typeof(PlayerInput))]
public class InputController : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas.
            private static IHasLookInput m_lookInput;
            private static InputType m_currentInput = InputType.Keyboard;
            private static InputController m_instance;
			
        //Establecer variables.
		
            //Publicas.
            [Header("Input References")]
            [SerializeField] private PlayerInput m_playerInput = null;              //Referencia al Input System del jugador.
			
            [Header("Input Values")]
            [SerializeField] private float m_secsToQuitDungeon = 3;

            //Privadas.
            private InputAction m_moveAction = null;                                //Referencia a la accion de mover del jugador.
            private InputAction m_lookAction = null;                                //Referencia a la accion de mirar del jugador.
			
            //Privadas.
            private CameraController m_cameraBrain;
            private CursorController m_cursorController;

            private bool m_isShowingCursor = false;
            private bool m_isInUIMode = false;

            private PlayerMovement m_playerMovement;
            private PlayerEvents m_playerEvents;
            private EntityAttack m_playerAttack;

            private Coroutine m_dungeonQuitCoroutine;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {

            m_instance = this;
            }
        private void Start() {
			
            //Establecer referencias a componentes.
            m_cameraBrain = CameraController.GetSingleton();
            m_lookInput = m_cameraBrain;

            m_playerMovement = PlayerBrain.GetSingleton().GetPlayerMovement();
            m_playerAttack = PlayerBrain.GetSingleton().GetAttack();
            m_playerEvents = PlayerBrain.GetSingleton().GetPlayerEvents();
            m_cursorController = CursorController.GetSingleton();

            //Establecer las referencias a las acciones.
            m_moveAction = m_playerInput.actions["Player/Move"];
            m_lookAction = m_playerInput.actions["Player/Look"];
            }
        private void Update() {

            //Establecer la velocidad de rotacion de la camara solo si no se esta mostrando el cursor.
            Vector2 m_lookValue = m_lookAction.ReadValue<Vector2>();
            Look(m_lookValue);

            //Mover al jugador.
            Vector2 m_moveValue = new Vector2();

            if (!m_isInUIMode) m_moveValue = m_moveAction.ReadValue<Vector2>();
                
            SetPlayerVelocity(new Vector2(m_moveValue.x, m_moveValue.y));

            if (m_playerInput.currentControlScheme == "Keyboard") m_currentInput = InputType.Keyboard;
            else if (m_playerInput.currentControlScheme == "Gamepad") m_currentInput = InputType.Gamepad;
            }
		
        //Funciones privadas.
        private void Look(Vector2 velocity) {

            if (m_lookInput == null) return;
            m_lookInput.Look(velocity);
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
            
            if (m_cameraBrain == null || m_isInUIMode) return;

            m_cameraBrain.Zoom(-context.ReadValue<float>());
            }
        /*public void Run(InputAction.CallbackContext context) {

            if (m_playerMovement == null) return;

            bool m_run = false;

            if (context.phase == InputActionPhase.Started) m_run = true;
            else if (context.phase == InputActionPhase.Canceled) m_run = false;
            else return; 

            m_playerMovement.SetRun(m_run);
            }*/
        public void Jump(InputAction.CallbackContext context) {
            
            if (context.phase != InputActionPhase.Started || m_isInUIMode) return;
            
            m_playerMovement.Jump();
            }
        public void Interact(InputAction.CallbackContext context) {
            
            if (context.phase != InputActionPhase.Started) return;
            
            m_playerEvents.Interact();
            }
        public void Attack(InputAction.CallbackContext context) {
            
            if (context.phase != InputActionPhase.Started || m_isInUIMode) return;
            
            m_playerAttack.Attack();
            }
        public void QuitDungeon(InputAction.CallbackContext context) {

            if (context.phase == InputActionPhase.Started) if (m_dungeonQuitCoroutine == null) m_dungeonQuitCoroutine = StartCoroutine(DungeonQuitCoroutine(m_secsToQuitDungeon));
            else if (context.phase == InputActionPhase.Canceled) {

                if (m_dungeonQuitCoroutine != null) StopCoroutine(m_dungeonQuitCoroutine); 
                }
            }
        public void OpenPause(InputAction.CallbackContext context) {

            if (context.phase != InputActionPhase.Started) return;
            
            PausePanelUI.GetSingleton().Pause();
            }
		
        public static void SetLookObject(IHasLookInput lookInput) => m_lookInput = lookInput;
            
        public static InputController GetSingleton() => m_instance;
        public static InputType GetInputType() => m_currentInput;
            
        public void SetUIMode(bool active) => m_isInUIMode = active;
        public bool GetUIMode() => m_isInUIMode;

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        private IEnumerator DungeonQuitCoroutine(float quitTime) {

            float m_count = 0;

            while(m_count < quitTime) {

                m_count += Time.deltaTime;
                yield return null;
                }

            GenerateRuinsRooms.ExitRuins(false);
            }
        }
