using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarming : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] private Transform m_playerCell = null;
			
            //Privadas.
            private Vector2Int? m_actualGrid;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {
			
            StartCoroutine(CheckGrid(0.025f));
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        private IEnumerator CheckGrid(float presition) {

            while(true) {
                
                Vector2Int m_pos = new Vector2Int(Mathf.CeilToInt(transform.position.x - 0.5f), Mathf.CeilToInt(transform.position.z - 0.5f));
                
                m_actualGrid = null;

                foreach(FarmingEnviromentController.FarmSections m_section in FarmingEnviromentController.GetSingleton().GetGrid()) {

                    if (m_pos.x > m_section.GetMinPosition().x && m_pos.x < m_section.GetMaxPosition().x && m_pos.y > m_section.GetMinPosition().y && m_pos.y < m_section.GetMaxPosition().y) {// new Vector2Int(m_cell.x, m_cell.z) == m_pos) {

                        m_actualGrid = new Vector2Int(m_pos.x, m_pos.y);

                        m_playerCell.gameObject.SetActive(true);
                        m_playerCell.position = new Vector3(m_actualGrid.Value.x, m_section.GetPosition().y + 0.01f, m_actualGrid.Value.y);
                        }
                    }

                if (m_actualGrid == null) m_playerCell.gameObject.SetActive(false);

                yield return new WaitForSeconds(presition);
                }
            }
		
        }
