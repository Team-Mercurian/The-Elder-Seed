using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantClockController : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] private LineRenderer m_clockBorder = null;
            [SerializeField] private LineRenderer m_clockHourHand = null;
            [SerializeField] private LineRenderer m_clockHand = null;
			
            [Header("Values")]
            [SerializeField] [Range(0, 1)] private float m_value = 0;

            [Space]

            [SerializeField] [Range(0, 1)] private float m_handDistanceMultiplier = 0.5f;
            [SerializeField] private float m_distance = 0.25f;
            [SerializeField] private int m_circleQuality = 16;


            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Start() {
			
            float m_angleDifference = (360f / m_circleQuality);
            m_clockBorder.positionCount = m_circleQuality;

            for(int i = 0; i < m_circleQuality; i ++) {

                m_clockBorder.SetPosition(i, new Vector3(Mathf.Cos((m_angleDifference * i) * Mathf.Deg2Rad), Mathf.Sin((m_angleDifference * i) * Mathf.Deg2Rad)) * m_distance);
                }

            float m_dis = m_distance * m_handDistanceMultiplier;

            m_clockHourHand.SetPosition(0, new Vector3(Mathf.Cos(90 * Mathf.Deg2Rad), Mathf.Sin(90 * Mathf.Deg2Rad)) * m_dis);
            m_clockHourHand.SetPosition(1, Vector3.zero);

            SetHandsPosition();
            }
        
        private void Update() {

            SetHandsPosition();
            transform.eulerAngles = new Vector3(0, CameraController.GetDirection().eulerAngles.y, 0);
            }
		
        //Funciones privadas.
        private void SetHandsPosition() {

            float m_dis = m_distance * m_handDistanceMultiplier;
            float m_angle = (360f * -m_value) + 90;

            m_clockHand.SetPosition(0, Vector3.zero);
            m_clockHand.SetPosition(1, new Vector3(Mathf.Cos(m_angle * Mathf.Deg2Rad), Mathf.Sin(m_angle * Mathf.Deg2Rad)) * m_dis);
            }
		
        //Funciones publicas.
        public void SetValue(float value) {

            m_value = value;
            SetHandsPosition();
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
