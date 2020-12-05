﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinsPassageController : InteractableBehaviour {
	
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
			private Vector2Int m_roomPositionToMove;
            private Vector2Int m_directionToMove;
			
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

            Gizmos.DrawWireSphere(m_tp + (m_rot * (Vector3.Scale(Vector3.right, m_tls))), 0.25f);
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
        public void SetData(bool opened, Vector2Int teleportPosition, Vector2Int direction) {
            
            if (teleportPosition + direction == new Vector2Int(0, -1) && DataSystem.GetSingleton().GetDungeonData().GetFloor() == 0) opened = true;
            m_teleportTrigger.enabled = opened;

            GameObject m_passage = opened ? m_passageOpenPrefab : m_passageClosedPrefab;
            Instantiate(m_passage, transform.position, transform.rotation * Quaternion.Euler(0, 180, 0), transform);

            m_roomPositionToMove = teleportPosition + direction;
            m_directionToMove = direction;
            }
        public Vector2Int GetPositionToMove() {

            return m_roomPositionToMove;
            }
        public Vector2Int GetDirectionToMove() {

            return m_directionToMove;
            }
        public Vector3 GetPlayerAppearPosition() {
            
            return transform.position - (Vector3.Scale(new Vector3(m_directionToMove.x, 0, m_directionToMove.y), transform.localScale));
            }
        public override void Interact() {

            Vector2Int m_pos = GetPositionToMove();

            if (m_pos == new Vector2Int(0, -1)) {
                
                GenerateRuinsRooms.ExitRuins(false);
                }
                
            else {
                
                Direction m_direction = GetDirection(GetDirectionToMove());
                RoomController.SetAppearDirection(m_direction);

                DataSystem.GetSingleton().GetDungeonData().SetActualRoom(m_pos);
                SceneController.GetSingleton().LoadScene(Scenes.Ruins, true);
                }
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }