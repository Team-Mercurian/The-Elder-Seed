using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes {

    Menu,
    House,
    Ruins,
    }

public class SceneController : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
            private static SceneController m_instance;
            private static Scenes m_actualScene;
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] private GameObject m_fastFade = null;
            [SerializeField] private GameObject m_transitionFade = null;
			
            //Privadas.
            private Coroutine m_coroutine;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {

            if (m_instance == null) {

                m_instance = this;
                DontDestroyOnLoad(gameObject);
                }

            else {

                Destroy(gameObject);
                return;
                }

            m_actualScene = (Scenes) SceneManager.GetActiveScene().buildIndex;
            }

        //Funciones privadas.
		
        //Funciones publicas.
        public void LoadScene(Scenes scene, bool fastFade) {

            if (m_coroutine == null) m_coroutine = StartCoroutine(LoadSceneCoroutine((int) scene, fastFade));
            }
        public static SceneController GetSingleton() => m_instance;
        public static Scenes GetActualScene() => m_actualScene;
            
        public void GoToHouse() => LoadScene(Scenes.House, false);
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        private IEnumerator LoadSceneCoroutine(int sceneNumber, bool fastFade) {
            
            AsyncOperation m_async = SceneManager.LoadSceneAsync(sceneNumber);
            m_async.allowSceneActivation = false;
            float m_time = 0.25f;

            FadeControllerUI m_fade = Instantiate(fastFade ? m_fastFade : m_transitionFade).GetComponent<FadeControllerUI>();

            for(float i = 0; i < m_time; i += Time.deltaTime) {

                m_fade.SetValue(Mathf.Lerp(0, 0.5f, i / m_time));
                yield return null;
                }

            yield return (m_async.progress > 0.9f);
            yield return null;
            
            m_actualScene = (Scenes) sceneNumber;
            m_async.allowSceneActivation = true;

            for(float i = 0; i < m_time; i += Time.deltaTime) {

                m_fade.SetValue(Mathf.Lerp(0.5f, 1f, i / m_time));
                yield return null;
                }

            Destroy(m_fade.gameObject);
            m_coroutine = null;
            }
        }
