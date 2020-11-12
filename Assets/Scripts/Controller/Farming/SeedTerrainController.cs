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

            Vector3Int m_tpInt = new Vector3Int(Mathf.CeilToInt(transform.position.x), Mathf.CeilToInt(transform.position.y), Mathf.CeilToInt(transform.position.z));

            float m_xSraw = m_tls.x;
            float m_ySraw = m_tls.y;

            int m_xS = Mathf.CeilToInt((m_xSraw) / 2);
            int m_yS = Mathf.CeilToInt((m_ySraw) / 2);
            
            m_material = m_renderer.material;
            m_material.SetTextureScale("_UnlitColorMap", new Vector2((m_xS * 2) + 1, (m_yS * 2) + 1));

            List<Vector3Int> m_cells = new List<Vector3Int>();
            
            for(int m_x = -m_xS; m_x < m_xS + 1; m_x ++) {

                for(int m_y = -m_yS; m_y < m_yS + 1; m_y ++) {

                    m_cells.Add(m_tpInt + new Vector3Int(m_x, 0, m_y));
                    }
                }
            
            FarmingEnviromentController.GetSingleton().AddCells(m_cells);
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
