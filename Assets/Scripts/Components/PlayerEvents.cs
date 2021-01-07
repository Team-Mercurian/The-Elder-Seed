using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : GameBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
			[Header("References")]
            [SerializeField] private SpriteRenderer m_interactKey = null;

            //Privadas.
            private InteractableBehaviour m_interactable = null;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {

            m_interactKey.gameObject.SetActive(false);
            }
        private void Update() {

            m_interactKey.transform.eulerAngles = new Vector3(0, CameraController.GetDirection().eulerAngles.y, 0);
            }
        private void OnTriggerEnter(Collider collider) {
			
            if (collider.CompareTag("Interactable")) {

                InteractableBehaviour m_iB = collider.GetComponent<InteractableBehaviour>();

                if (m_iB.GetInstant()) m_iB.Interact();
                else {

                    m_interactable = collider.GetComponent<InteractableBehaviour>();
                    m_interactKey.gameObject.SetActive(true);
                    }
                }
            }
        private void OnTriggerExit(Collider collider) {
			
            if (collider.CompareTag("Interactable") && m_interactable == collider.GetComponent<InteractableBehaviour>()) {

                m_interactable = null;
                m_interactKey.gameObject.SetActive(false);
                }
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
        public void Interact() {
            
            if (m_interactable == null) return;
            m_interactable.Interact();
            m_interactable = null;
            m_interactKey.gameObject.SetActive(false);
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
