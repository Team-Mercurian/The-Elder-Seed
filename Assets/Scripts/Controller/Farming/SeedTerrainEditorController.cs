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
            private int m_gridSize = 0;
			
    //Funciones
		
        //Funciones de MonoBehaviour.
        #if UNITY_EDITOR
        private void Update() {

            if (FarmingEnviromentController.GetCellSize() == 0) return; 

            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, 1, Mathf.Infinity), 1, Mathf.Clamp(transform.localScale.z, 1, Mathf.Infinity));
            m_gridSize = FarmingEnviromentController.GetCellSize();
            }
        private void OnDrawGizmos() {

            if (m_gridSize == 0) return; 

            //Establecer variables.
            Vector3 m_tp = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            Vector2 m_tls = new Vector2(transform.localScale.x - 1, transform.localScale.z - 1);

            float m_offset = 0.5f;
            float m_gridBlueOffset = 0;

            float m_xSraw = m_tls.x/2;
            float m_ySraw = m_tls.y/2;

            float m_xS = Mathf.RoundToInt((m_xSraw) + 0.01f) + m_offset;
            float m_yS = Mathf.RoundToInt((m_ySraw) + 0.01f) + m_offset;

            Vector3 m_uL;
            Vector3 m_uR;
            Vector3 m_dL;
            Vector3 m_dR;

            //Dibujar grid.
            Gizmos.color = Color.red;

            for(float m_x = -m_xS + 1; m_x <= m_xS - 1; m_x += 1) {

                float m_lXS = m_x * m_gridSize;
                float m_lYS = m_yS * m_gridSize;
                
                m_uL = m_tp + new Vector3(m_lXS, 0, -m_lYS);
                m_uR = m_tp + new Vector3(m_lXS, 0, -m_lYS);
                m_dL = m_tp + new Vector3(m_lXS, 0, m_lYS);
                m_dR = m_tp + new Vector3(m_lXS, 0, m_lYS);

                Gizmos.DrawLine(m_uL, m_uR);
                Gizmos.DrawLine(m_uR, m_dR);
                Gizmos.DrawLine(m_dR, m_dL);
                Gizmos.DrawLine(m_dL, m_uL);
                }

            
            for(float m_y = -m_yS + 1; m_y <= m_yS - 1; m_y += 1) {

                float m_lXS = m_xS * m_gridSize;
                float m_lYS = m_y * m_gridSize;
                
                m_uL = m_tp + new Vector3(-m_lXS, 0, m_lYS);
                m_uR = m_tp + new Vector3(m_lXS, 0, m_lYS);
                m_dL = m_tp + new Vector3(-m_lXS, 0, m_lYS);
                m_dR = m_tp + new Vector3(m_lXS, 0, m_lYS);

                Gizmos.DrawLine(m_uL, m_uR);
                Gizmos.DrawLine(m_uR, m_dR);
                Gizmos.DrawLine(m_dR, m_dL);
                Gizmos.DrawLine(m_dL, m_uL);
                }

            //Dibujar borde redondeado.
            Gizmos.color = Color.blue;

            m_uL = m_tp + new Vector3(-(m_xS * m_gridSize) - m_gridBlueOffset, 0, (m_yS * m_gridSize) + m_gridBlueOffset);
            m_uR = m_tp + new Vector3((m_xS * m_gridSize) + m_gridBlueOffset, 0, (m_yS * m_gridSize) + m_gridBlueOffset);
            m_dL = m_tp + new Vector3(-(m_xS * m_gridSize) - m_gridBlueOffset, 0, -(m_yS * m_gridSize) - m_gridBlueOffset);
            m_dR = m_tp + new Vector3((m_xS * m_gridSize) + m_gridBlueOffset, 0, -(m_yS * m_gridSize) - m_gridBlueOffset);

            Gizmos.DrawLine(m_uL, m_uR);
            Gizmos.DrawLine(m_uR, m_dR);
            Gizmos.DrawLine(m_dR, m_dL);
            Gizmos.DrawLine(m_dL, m_uL);
            
            //Dibujar borde.
            Gizmos.color = Color.green;

            float m_greenOffset = 0.5f;

            m_uL = m_tp + new Vector3(-(m_xSraw + m_greenOffset) * m_gridSize, 0, (m_ySraw + m_greenOffset) * m_gridSize);
            m_uR = m_tp + new Vector3((m_xSraw + m_greenOffset) * m_gridSize, 0, (m_ySraw + m_greenOffset) * m_gridSize);
            m_dL = m_tp + new Vector3(-(m_xSraw + m_greenOffset) * m_gridSize, 0, -(m_ySraw + m_greenOffset) * m_gridSize);
            m_dR = m_tp + new Vector3((m_xSraw + m_greenOffset) * m_gridSize, 0, -(m_ySraw + m_greenOffset) * m_gridSize);

            Gizmos.DrawLine(m_uL, m_uR);
            Gizmos.DrawLine(m_uR, m_dR);
            Gizmos.DrawLine(m_dR, m_dL);
            Gizmos.DrawLine(m_dL, m_uL);

            /*

            //Establecer variables.
            Vector3 m_tp = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            Vector2 m_tls = new Vector2(transform.localScale.x - 1, transform.localScale.z - 1);

            bool m_xIsEven = m_tls.x % 2 < 1;
            bool m_yIsEven = m_tls.y % 2 < 1;

            float m_xSraw = m_tls.x;
            float m_ySraw = m_tls.y;

            float m_offset = 0;
            float m_gridCountOffset = 1;
            float m_gridBlueOffset = 1;

            switch(m_gridSize) {
                
                case 1 : 
                    m_offset = 0.5f; 
                    m_gridCountOffset = 1f;
                    m_gridBlueOffset = 0;
                    break;

                case 2 : 
                    m_offset = 1; 
                    m_gridCountOffset = 2;
                    m_gridBlueOffset = 0;
                    break;

                case 3 : 
                    m_offset = 0; 
                    m_gridCountOffset = 3;
                    m_gridBlueOffset = 0;
                    break;

                case 4 : 
                    m_offset = -0.5f; 
                    m_gridCountOffset = 1;
                    m_gridBlueOffset = -0.5f;
                    break;

                case 5 : 
                    m_offset = -0.5f; 
                    m_gridCountOffset = 1;
                    m_gridBlueOffset = -0.5f;
                    break;
                }

            float m_xS = (Mathf.RoundToInt((m_xSraw / m_gridSize) + 0.01f) * m_gridSize) + m_offset;
            float m_yS = (Mathf.RoundToInt((m_ySraw / m_gridSize) + 0.01f) * m_gridSize) + m_offset;
            
            Vector3 m_uL;
            Vector3 m_uR;
            Vector3 m_dL;
            Vector3 m_dR;

            //Dibujar grid.
            Gizmos.color = Color.red;

            for(float m_x = -m_xS + m_gridCountOffset; m_x <= m_xS - m_gridCountOffset; m_x += m_gridSize) {

                float m_lXS = m_x;
                float m_lYS = m_yS;
                
                m_uL = m_tp + new Vector3(m_lXS - m_gridSize, 0, -m_lYS);
                m_uR = m_tp + new Vector3(m_lXS + m_gridSize, 0, -m_lYS);
                m_dL = m_tp + new Vector3(m_lXS + m_gridSize, 0, m_lYS);
                m_dR = m_tp + new Vector3(m_lXS - m_gridSize, 0, m_lYS);

                Gizmos.DrawLine(m_uL, m_uR);
                Gizmos.DrawLine(m_uR, m_dL);
                Gizmos.DrawLine(m_dL, m_dR);
                Gizmos.DrawLine(m_dR, m_uL);
                }

            
            for(float m_y = -m_yS + m_gridCountOffset; m_y <= m_yS - m_gridCountOffset; m_y += m_gridSize) {

                float m_lXS = m_xS;
                float m_lYS = m_y;
                
                m_uL = m_tp + new Vector3(-m_lXS, 0, m_lYS - m_gridSize);
                m_uR = m_tp + new Vector3(m_lXS, 0, m_lYS - m_gridSize);
                m_dL = m_tp + new Vector3(-m_lXS, 0, m_lYS + m_gridSize);
                m_dR = m_tp + new Vector3(m_lXS, 0, m_lYS + m_gridSize);

                Gizmos.DrawLine(m_uL, m_uR);
                Gizmos.DrawLine(m_uR, m_dR);
                Gizmos.DrawLine(m_dR, m_dL);
                Gizmos.DrawLine(m_dL, m_uL);
                }

            //Dibujar borde redondeado.
            Gizmos.color = Color.blue;

            m_uL = m_tp + new Vector3(-m_xS - m_gridBlueOffset, 0, m_yS + m_gridBlueOffset);
            m_uR = m_tp + new Vector3(m_xS + m_gridBlueOffset, 0, m_yS + m_gridBlueOffset);
            m_dL = m_tp + new Vector3(-m_xS - m_gridBlueOffset, 0, -m_yS - m_gridBlueOffset);
            m_dR = m_tp + new Vector3(m_xS + m_gridBlueOffset, 0, -m_yS - m_gridBlueOffset);

            Gizmos.DrawLine(m_uL, m_uR);
            Gizmos.DrawLine(m_uR, m_dR);
            Gizmos.DrawLine(m_dR, m_dL);
            Gizmos.DrawLine(m_dL, m_uL);
            
            //Dibujar borde.
            Gizmos.color = Color.green;

            float m_greenOffset = 0.5f;

            m_uL = m_tp + new Vector3(-m_xSraw - m_greenOffset, 0, m_ySraw + m_greenOffset);
            m_uR = m_tp + new Vector3(m_xSraw + m_greenOffset, 0, m_ySraw + m_greenOffset);
            m_dL = m_tp + new Vector3(-m_xSraw - m_greenOffset, 0, -m_ySraw - m_greenOffset);
            m_dR = m_tp + new Vector3(m_xSraw + m_greenOffset, 0, -m_ySraw - m_greenOffset);

            Gizmos.DrawLine(m_uL, m_uR);
            Gizmos.DrawLine(m_uR, m_dR);
            Gizmos.DrawLine(m_dR, m_dL);
            Gizmos.DrawLine(m_dL, m_uL);
            */
            }
		#endif

        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
