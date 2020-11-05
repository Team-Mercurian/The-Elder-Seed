using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class SeedTerrainEditorController : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
			
            //Privadas.
			
    //Funciones
		
        //Funciones de MonoBehaviour.
        #if UNITY_EDITOR
        private void Update() {

            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, 1, Mathf.Infinity), 1, Mathf.Clamp(transform.localScale.z, 1, Mathf.Infinity));
            }
        private void OnDrawGizmos() {

            //Establecer variables.
            Vector2 m_tls = new Vector2(transform.localScale.x - 1, transform.localScale.z - 1);
            Vector3 m_tp = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            float m_xSraw = m_tls.x/2f;
            float m_ySraw = m_tls.y/2f;

            float m_xS = Mathf.CeilToInt(m_xSraw + 0.01f) - 0.5f;
            float m_yS = Mathf.CeilToInt(m_ySraw + 0.01f) - 0.5f;
            
            Vector3 m_uL;
            Vector3 m_uR;
            Vector3 m_dL;
            Vector3 m_dR;

            //Dibujar grid.
            Gizmos.color = Color.red;

            for(float m_x = -m_xS; m_x <= m_xS; m_x ++) {

                for(float m_y = -m_yS; m_y <= m_yS; m_y ++) {
                                    
                    float m_lXS = m_x;
                    float m_lYS = m_y;
                    
                    m_uL = m_tp + new Vector3(-m_lXS, 0, m_lYS);
                    m_uR = m_tp + new Vector3(m_lXS, 0, m_lYS);
                    m_dL = m_tp + new Vector3(m_lXS, 0, -m_lYS);
                    m_dR = m_tp + new Vector3(-m_lXS, 0, -m_lYS);

                    Gizmos.DrawLine(m_uL, m_uR);
                    Gizmos.DrawLine(m_uR, m_dL);
                    Gizmos.DrawLine(m_dL, m_dR);
                    Gizmos.DrawLine(m_dR, m_uL);
                    }
                }
                
            //Dibujar borde.
            Gizmos.color = Color.green;

            float m_offset = 0.5f;

            m_uL = m_tp + new Vector3(-m_xSraw - m_offset, 0, m_ySraw + m_offset);
            m_uR = m_tp + new Vector3(m_xSraw + m_offset, 0, m_ySraw + m_offset);
            m_dL = m_tp + new Vector3(-m_xSraw - m_offset, 0, -m_ySraw - m_offset);
            m_dR = m_tp + new Vector3(m_xSraw + m_offset, 0, -m_ySraw - m_offset);

            Gizmos.DrawLine(m_uL, m_uR);
            Gizmos.DrawLine(m_uR, m_dR);
            Gizmos.DrawLine(m_dR, m_dL);
            Gizmos.DrawLine(m_dL, m_uL);

            //Dibujar borde redondeado.
            Gizmos.color = Color.blue;

            m_uL = m_tp + new Vector3(-m_xS, 0, m_yS);
            m_uR = m_tp + new Vector3(m_xS, 0, m_yS);
            m_dL = m_tp + new Vector3(-m_xS, 0, -m_yS);
            m_dR = m_tp + new Vector3(m_xS, 0, -m_yS);

            Gizmos.DrawLine(m_uL, m_uR);
            Gizmos.DrawLine(m_uR, m_dR);
            Gizmos.DrawLine(m_dR, m_dL);
            Gizmos.DrawLine(m_dL, m_uL);
            }
		#endif

        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
