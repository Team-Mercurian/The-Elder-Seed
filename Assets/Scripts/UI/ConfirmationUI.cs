using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ConfirmationUI : PanelUI, IHasTwoOptionsUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
		private static ConfirmationUI m_instance;
        
		//No Static
		[Header("References")]
		[SerializeField] private TextMeshProUGUI m_titleText = null;
		[SerializeField] private TextMeshProUGUI m_leftText = null;
		[SerializeField] private TextMeshProUGUI m_rightText = null;
		[SerializeField] private GameObject m_closeButton = null;

		private UnityEvent m_eventLeft = null;
		private UnityEvent m_eventRight = null;
        
        private PanelUI m_savedPanel;

    //Functions
	
		//MonoBehaviour Functions
		protected override void Start() {
			
			m_instance = this;
			SetDeltaScaledTime(false);
			base.Start();
			}
			
		//Public Functions
		public override void Open() {

			m_savedPanel = GameSystem.GetUI();
			base.Open();
            InputController.SetLookObject(null);
			GameSystem.SetUI(this);
			}
		public override void Close() {
 
			base.Close();
            InputController.SetLookObject(CameraController.GetSingleton());
			GameSystem.SetUI(m_savedPanel);
			}

		public void SetData(string title, ButtonEvent left, ButtonEvent right, bool hasCloseButton) {
			
			m_titleText.text = title;
			m_leftText.text = left.GetText();
			m_rightText.text = right.GetText();

			m_eventLeft = left.GetEvent();
			m_eventRight = right.GetEvent();
			
			m_closeButton.SetActive(hasCloseButton);
			}

		public void LeftEvent() {
			
			if (m_eventLeft != null) m_eventLeft.Invoke();
			Close();
			}
		public void RightEvent() {
			
			if (m_eventRight != null) m_eventRight.Invoke();
			Close();
			}

		public static ConfirmationUI GetSingleton() => m_instance; 

		//Private Functions
		
	
	}

public struct ButtonEvent {

	private string m_text;
	private UnityEvent m_event;

	public ButtonEvent(string text, UnityEvent buttonEvent) {

		m_text = text;
		m_event = buttonEvent;
		}
	
	public string GetText() => m_text;
	public UnityEvent GetEvent() => m_event;
	}