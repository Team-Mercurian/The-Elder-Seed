using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedTerrainController : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] private MeshRenderer m_renderer = null;
			
            //Privadas.
            private Material m_material;
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {

            m_renderer.gameObject.SetActive(true);

            Vector2 m_tls = new Vector2(transform.localScale.x - 1, transform.localScale.z - 1);
            Vector3 m_tp = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            float m_xSraw = m_tls.x;
            float m_ySraw = m_tls.y;

            float m_xS = Mathf.CeilToInt(m_xSraw + 0.01f);
            float m_yS = Mathf.CeilToInt(m_ySraw + 0.01f);
            
            m_material = m_renderer.material;
            m_material.SetTextureScale("_UnlitColorMap", new Vector2(m_xS, m_yS));
            }
		private void OnValidate() {

            m_renderer.gameObject.SetActive(false);
            }

        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
