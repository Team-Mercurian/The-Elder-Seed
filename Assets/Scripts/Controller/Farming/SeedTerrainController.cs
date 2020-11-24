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

            if (FarmingEnviromentController.GetCellSize() == 0) return; 

            m_renderer.gameObject.SetActive(true);

            int m_cellSize = FarmingEnviromentController.GetCellSize();

            m_renderer.transform.localScale = new Vector3(1 * m_cellSize,  1 * m_cellSize, 1) ; 

            Vector2 m_tls = new Vector2(transform.localScale.x - 1, transform.localScale.z - 1);
            Vector3 m_tp = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            Vector3Int m_tpInt = new Vector3Int(Mathf.CeilToInt(transform.position.x), Mathf.CeilToInt(transform.position.y), Mathf.CeilToInt(transform.position.z));

            float m_xSraw = m_tls.x * m_cellSize;
            float m_ySraw = m_tls.y * m_cellSize;

            int m_xS = Mathf.CeilToInt((m_xSraw) / 2);
            int m_yS = Mathf.CeilToInt((m_ySraw) / 2);
            
            m_material = m_renderer.material;
            m_material.SetTextureScale("_UnlitColorMap", new Vector2(((m_xS * 2) / m_cellSize) + 1, ((m_yS * 2) / m_cellSize) + 1));

            FarmingEnviromentController.FarmSections m_farmSection = new FarmingEnviromentController.FarmSections(m_tpInt, new Vector2Int(m_xS + 1, m_yS + 1));
            FarmingEnviromentController.GetSingleton().AddSection(m_farmSection);
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
