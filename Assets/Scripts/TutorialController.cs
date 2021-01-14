using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour {
	
	//Singleton
	private static TutorialController m_instance = null;
	private void Awake() => m_instance = this;
	public static TutorialController GetSingleton() => m_instance;
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
        [Header("References")]
		[SerializeField] private CanvasGroup m_canvasGroup = null;
		[SerializeField] private TextMeshProUGUI m_tutorialText = null;
        
    //Functions
	
		//MonoBehaviour Functions
		private void Start() {

        	SetTutorialText(DataSystem.GetSingleton().GetGameData().GetTutorialIndex());
			}
        
        
		//Public Functions
        public void SetTutorialText(int index) {

			DataSystem.GetSingleton().GetGameData().SetTutorialIndex(index);
			SaveSystem.Save();

			switch(index) {

				case 0 : 
					m_tutorialText.text = "Acercate a la granja";
					break;

				case 1 : 
					m_tutorialText.text = "Abre tu inventario presionando el Tabulador";
					break;

				case 2 : 
					m_tutorialText.text = "Selecciona una semilla de regeneracion";
					break;

				case 3 : 
					m_tutorialText.text = "Planta una semilla en la tierra disponible para plantar.";
					break;

				case 4 : 
					m_tutorialText.text = "Vuelve a abrir tu inventario";
					break;

				case 5 : 
					m_tutorialText.text = "Selecciona la pestaña de otros (estrella)";
					break;

				case 6 : 
					m_tutorialText.text = "Equipa los fragmentos magicos";
					break;
				
				case 7 : 
					m_tutorialText.text = "Usa los fragmentos magicos para hacer crecer tu planta";
					break;
				
				case 8 : 
					m_tutorialText.text = "Cosecha tu planta haciendo click al estar encima de ella";
					break;
				
				case 9 : 
					m_tutorialText.text = "Ve a craftear una pocion en la mesa que se encuentra al lado de la puerta de tu casa";
					break;
				
				case 10 : 
					m_tutorialText.text = "Ve a buscar mas semillas en las ruinas, para acceder acercate a la gran puerta que hay cerca de la granja";
					break;

				case 11 : 
					m_canvasGroup.alpha = 0;
					break;
				}
			}
        
		//Private Functions
        
        
	//Coroutines
	
	}
