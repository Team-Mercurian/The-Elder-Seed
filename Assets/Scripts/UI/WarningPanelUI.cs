using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class WarningPanelUI : PanelUI {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
		private static WarningPanelUI m_instance;
        
		//No Static
		[Header("References")]
		[SerializeField] private TextMeshProUGUI m_titleText = null;
		[SerializeField] private TextMeshProUGUI m_closeText = null;
        
        private PanelUI m_savedPanel;

    //Functions
	
		//MonoBehaviour Functions
		protected override void Start() {
			
			m_instance = this;
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

		public void SetData(string title, string closeText) {
			
			m_titleText.text = title;
			m_closeText.text = closeText;
			}

		public static WarningPanelUI GetSingleton() => m_instance; 

		//Private Functions
		
	
	}