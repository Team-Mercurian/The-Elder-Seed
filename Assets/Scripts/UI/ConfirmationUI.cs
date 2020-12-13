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
        
		//No Static
		[Header("References")]
		[SerializeField] private TextMeshProUGUI m_titleText = null;
		[SerializeField] private TextMeshProUGUI m_leftText = null;
		[SerializeField] private TextMeshProUGUI m_rightText = null;

		[Header("Events")]
		[SerializeField] private UnityEvent m_eventLeft = null;
		[SerializeField] private UnityEvent m_eventRight = null;
        
        
    //Functions
	
		//MonoBehaviour Functions
			
		//Public Functions
		public void SetData(string title, ButtonEvent left, ButtonEvent right) {
			
			m_titleText.text = title;
			m_leftText.text = left.GetText();
			m_rightText.text = right.GetText();

			m_eventLeft = left.GetEvent();
			m_eventRight = right.GetEvent();
			}

		public void LeftEvent() {
			
			if (m_eventLeft != null) m_eventLeft.Invoke();
			Close();
			}
		public void RightEvent() {
			
			if (m_eventRight != null) m_eventRight.Invoke();
			Close();
			}

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