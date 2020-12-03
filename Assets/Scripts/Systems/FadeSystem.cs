using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSystem : MonoBehaviour {

    //Establecer variables.
		
        //Establecer variables publicas.
        public static FadeSystem instance;
			
        [Header("Animation")]
            public RectTransform rect;
            public AnimationCurve animationCurve;
            public float animationTime;


        //Establecer variables locales.
        private bool m_isLoading = false;
			
		
    //Funciones.
		
        //Funciones de MonoBehaviour.
        private void Awake() {
            
            rect.gameObject.SetActive(false);
            
            if (instance == null) {

                instance = this;
                DontDestroyOnLoad(gameObject);
                }

            else {

                Destroy(gameObject);
                }
            }

        //Funciones propias publicas.
        public void Fade(int roomIndex) {

            if (m_isLoading)    return;

            m_isLoading = true;

            StartCoroutine(FadeAnimation(roomIndex));
            }

        //Funciones propias privadas.
			
        //Corotinas.
        private IEnumerator FadeAnimation(int roomIndex) {

            rect.gameObject.SetActive(true);

            AsyncOperation m_async = SceneManager.LoadSceneAsync(roomIndex);
            m_async.allowSceneActivation = false;

            float m_defPos = 500;

            rect.offsetMin = new Vector2(m_defPos, rect.offsetMin.y);
            rect.offsetMax = new Vector2(m_defPos, rect.offsetMax.y);

            float m_reachPos = 0;
            float m_evaluation;
            float m_value = 500;

            for(float i = 0; i < animationTime; i += Time.unscaledDeltaTime) {

                m_evaluation = animationCurve.Evaluate(i / animationTime);
                m_value = Mathf.Lerp(m_defPos, m_reachPos, m_evaluation);

                rect.offsetMin = new Vector2(m_value, rect.offsetMin.y);
                rect.offsetMax = new Vector2(m_value, rect.offsetMax.y);
     
                yield return null;
                }

            rect.offsetMin = new Vector2(m_reachPos, rect.offsetMin.y);
            rect.offsetMax = new Vector2(m_reachPos, rect.offsetMax.y);

            if (m_async.progress >= 0.9f)   m_async.allowSceneActivation = true;
            else    yield return null;

            //DataBase.AddDifficulty();
            //DataBase.Reset();

            m_defPos = 0;
            m_reachPos = -500;

            for(float i = 0; i < animationTime; i += Time.unscaledDeltaTime) {

                m_evaluation = animationCurve.Evaluate(i / animationTime);
                m_value = Mathf.Lerp(m_defPos, m_reachPos, m_evaluation);

                rect.offsetMin = new Vector2(m_value, rect.offsetMin.y);
                rect.offsetMax = new Vector2(m_value, rect.offsetMax.y);
     
                yield return null;
                }
                
            rect.offsetMin = new Vector2(m_reachPos, rect.offsetMin.y);
            rect.offsetMax = new Vector2(m_reachPos, rect.offsetMax.y);
            rect.gameObject.SetActive(false);

            m_isLoading = false;
            }			
        }
