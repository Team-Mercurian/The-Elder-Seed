using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PausePanelUI : PanelUI {
	
	//Singleton
	private static PausePanelUI m_instance = null;
	private void Awake() => m_instance = this;
	public static PausePanelUI GetSingleton() => m_instance;
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
		[Header("Buttons")]
		[SerializeField] private Button m_resumeButton = null;
		[SerializeField] private bool m_resumeIsActive = true;

		[Space]
		[SerializeField] private Button m_goToFarmButton = null;
		[SerializeField] private bool m_goToFarmIsActive = true;
		
		[Space]
		[SerializeField] private Button m_exitButton = null;
		[SerializeField] private bool m_exitIsActive = true;
        
		private bool m_isPaused = false;
		private Coroutine m_timeScaleRoutine = null;
        
    //Functions
	
		//MonoBehaviour Functions
		protected override void Start() {

			m_resumeButton.gameObject.SetActive(m_resumeIsActive);
			m_goToFarmButton.gameObject.SetActive(m_goToFarmIsActive);
			m_exitButton.gameObject.SetActive(m_exitIsActive);
			SetDeltaScaledTime(false);

			base.Start();
			}
        
		//Public Functions
		public void Pause() {

			Pause(!m_isPaused);
			}
		public void GoToFarm() {

			Pause(false);
			GenerateRuinsRooms.ExitRuins(false);
			}
		public void Exit() {
			
			UnityEvent m_rEvent = new UnityEvent();
			m_rEvent.AddListener(() => CloseGame());

			ButtonEvent m_leftButton = new ButtonEvent("No", null);
			ButtonEvent m_rightButton = new ButtonEvent("Si", m_rEvent);
			
			ConfirmationUI.GetSingleton().SetData("¿Quieres ir al escritorio?", m_leftButton, m_rightButton, true);
			ConfirmationUI.GetSingleton().Open();
			}
			
        public void Pause(bool active) {
			
			if (m_isPaused == active || GetIsInAnimation()) return;

			m_isPaused = active;
			if (m_isPaused) InternalPause();
			else InternalUnPause();
			}
        
		//Private Functions
        private void InternalPause() {

			if (InputController.GetSingleton().GetUIMode()) return;

			if (m_goToFarmIsActive) {

				if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0) m_goToFarmButton.interactable = false;
				else m_goToFarmButton.interactable = true;
				}

			Open();
			ChangeTimeScale(false);
            InputController.SetLookObject(null);
			GameSystem.SetUI(this);
			}	 
		private void InternalUnPause() {

			Close();
			ChangeTimeScale(true);
            InputController.SetLookObject(CameraController.GetSingleton());
			GameSystem.SetUI(null);
			}
		
		private void ChangeTimeScale(bool active) {

			if (m_timeScaleRoutine != null) StopCoroutine(m_timeScaleRoutine);
			m_timeScaleRoutine = StartCoroutine(SmoothTimeScaleActivation(active));
			}

		private void CloseGame() => Application.Quit(); 

	//Coroutines
	private IEnumerator SmoothTimeScaleActivation(bool active) {

		float m_valueA = Time.timeScale;
		float m_valueB = active ? 1 : 0;

		for(float i = 0; i < GetAnimationTime(); i += Time.unscaledDeltaTime) {

			Time.timeScale = Mathf.Lerp(m_valueA, m_valueB, GetAnimationCurveEvaluation(i / GetAnimationTime()));	
			yield return null;
			}
		
		Time.timeScale = m_valueB;
		}
	}
