using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBrain : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
            public static CameraBrain instance;         //Singleton.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Camera Values")]
            [SerializeField] private float m_defaultDistance = 5;
            [SerializeField] private Vector2 m_defaultAngle = new Vector2(0, 90);
            [SerializeField] private Vector2 m_cameraRotationSensitivity = new Vector2(0.1f, 0.05f);
            [Space]
            [SerializeField] private float m_positionSmoothness = 0.1f;

            [Header("Camera References")]
            [SerializeField] private Camera m_camera = null;
            [SerializeField] private Transform m_cameraHolder = null;
            [SerializeField] private Transform m_target = null;
			
            //Privadas.
            private Vector3 m_positionVelocity = new Vector3();
            private Vector2 m_actualAngle = new Vector2();

			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {
			
            //Establecer singleton.
            instance = this;
            }
        private void Update() {

            //Establecer el limite de la distancia y los angulos.
            m_defaultDistance = Mathf.Clamp(m_defaultDistance, 0, Mathf.Infinity);
            
            if (m_actualAngle.x >= 360) m_actualAngle = new Vector2(m_actualAngle.x - 360, m_actualAngle.y);
            if (m_actualAngle.x < 0) m_actualAngle = new Vector2(m_actualAngle.x + 360, m_actualAngle.y);

            m_actualAngle = new Vector2(m_actualAngle.x, Mathf.Clamp(m_actualAngle.y, -80, 80));

            //Establecer la rotacion de la camara variable por variable.
            float m_y = Mathf.Clamp(Mathf.Sin(m_actualAngle.y * Mathf.Deg2Rad), 0, Mathf.Infinity);
            float m_x = Mathf.Cos(m_actualAngle.x * Mathf.Deg2Rad) * Mathf.Cos(m_actualAngle.y * Mathf.Deg2Rad);
            float m_z = Mathf.Sin(m_actualAngle.x * Mathf.Deg2Rad) * Mathf.Cos(m_actualAngle.y * Mathf.Deg2Rad);
                
            //Establecer la posicion del target solo si este existe.
            Vector3 m_targetPosition = m_target == null ? transform.position : m_target.position;

            //Establecer posicion, y rotacion del padre e hijo.
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(m_targetPosition.x, 0, m_targetPosition.z), ref m_positionVelocity, m_positionSmoothness, Mathf.Infinity, Time.deltaTime);
            m_cameraHolder.localPosition = new Vector3(m_x, m_y, m_z) * m_defaultDistance;
            m_cameraHolder.LookAt(m_targetPosition, Vector3.up);
            }
        private void OnValidate() {

            m_actualAngle = m_defaultAngle;
            Update();
            }

        //Funciones privadas.
		
        //Funciones publicas.
		public void SetRotationVelocity(Vector2 velocity) {
            
            //Sumar la velocidad al angulo a utilizar.
            m_actualAngle += new Vector2(-velocity.x, -velocity.y) * m_cameraRotationSensitivity;
            }

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }

