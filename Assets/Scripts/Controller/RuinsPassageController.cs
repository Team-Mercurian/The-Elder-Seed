using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinsPassageController : GameBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] private GameObject m_passageOpenPrefab = null;
            [SerializeField] private GameObject m_passageClosedPrefab = null;
            [Space]
            [SerializeField] private BoxCollider m_teleportTrigger = null;
			
            //Privadas.
			private Vector2Int m_positionToMove;
			
    //Funciones
		
        //Funciones de MonoBehaviour.
        private void OnDrawGizmos() {

            Gizmos.color = Color.red;

            Vector3 m_tp = transform.position;
            Vector3 m_tls = transform.localScale;
            Quaternion m_rot = transform.rotation;

            Gizmos.DrawLine(m_tp + (m_rot * Vector3.Scale(new Vector3(0.5f, 0, -0.5f), m_tls)), m_tp + (m_rot * Vector3.Scale(new Vector3(0.5f, 2, -0.5f), m_tls)));
            Gizmos.DrawLine(m_tp + (m_rot * Vector3.Scale(new Vector3(0.5f, 2, -0.5f), m_tls)), m_tp + (m_rot * Vector3.Scale(new Vector3(0.5f, 2, 0.5f), m_tls)));
            Gizmos.DrawLine(m_tp + (m_rot * Vector3.Scale(new Vector3(0.5f, 2, 0.5f), m_tls)), m_tp + (m_rot * Vector3.Scale(new Vector3(0.5f, 0, 0.5f), m_tls)));
            Gizmos.DrawLine(m_tp + (m_rot * Vector3.Scale(new Vector3(0.5f, 0, 0.5f), m_tls)), m_tp + (m_rot * Vector3.Scale(new Vector3(0.5f, 0, -0.5f), m_tls)));
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
        public void SetData(bool opened, Vector2Int teleportPosition) {
            
            if (teleportPosition == new Vector2Int(0, -1)) opened = true;
            m_teleportTrigger.enabled = opened;

            GameObject m_passage = opened ? m_passageOpenPrefab : m_passageClosedPrefab;
            Instantiate(m_passage, transform.position, transform.rotation * Quaternion.Euler(0, 180, 0), transform);

            m_positionToMove = teleportPosition;
            }
        public Vector2Int GetPositionToMove() {

            return m_positionToMove;
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
