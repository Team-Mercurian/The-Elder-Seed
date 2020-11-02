using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
        public enum Scenes {

            House,
            Ruins,
            }
		
        //Establecer variables estaticas.
		
            //Publicas.
            private static SceneController m_instance;
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
			
            //Privadas.
            private Coroutine m_coroutine;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {

            m_instance = this;
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
        public void LoadScene(Scenes scene) {

            if (m_coroutine == null) m_coroutine = StartCoroutine(LoadSceneCoroutine((int) scene));
            }
        public static SceneController GetSingleton() {

            return m_instance;
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        private IEnumerator LoadSceneCoroutine(int sceneNumber) {
            
            AsyncOperation m_async = SceneManager.LoadSceneAsync(sceneNumber);
            m_async.allowSceneActivation = false;
            Debug.Log("Loading");

            yield return (m_async.progress > 0.9f);
            yield return null;

            m_async.allowSceneActivation = true;
            m_coroutine = null;
            }
        }
