using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBrain : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Privadas
            private static CameraBrain m_instance;                                                              //Singleton.
			
        //Establecer variables.
		
            //Publicas.
            [Header("Camera Values")]
            [SerializeField] private Vector2 m_defaultAngle = new Vector2(0, 90);                               //Angulo por defecto en X e Y de la camara.
            [SerializeField] private Vector2 m_cameraRotationSensitivity = new Vector2(0.1f, 0.05f);            //Multiplicador de sensibilidad de la rotacion de la camara.
            [Space]
            [SerializeField] private float m_positionSmoothness = 0.1f;                                         //Fluidez del cambio de posicion de la camara.
            [SerializeField] private float m_rotationSmoothness = 0.05f;                                         //Fluidez del cambio de posicion de la camara.
            [SerializeField] private Vector2 m_positionMultiplier = new Vector2(1, 1);                          //Multiplicador de la distancia X e Y de la camara.
            
            [Header("Distance")]
            [SerializeField] private float m_defaultDistance = 5;                                               //Distancia de la camara hacia el jugador por defecto.
            [SerializeField] private float m_zoomSensitivity = 1;
            [SerializeField] private float m_zoomSmoothness = 0.1f;
            [SerializeField] private float m_minDistance = 1; 
            [SerializeField] private float m_maxDistance = 6; 

            [Header("Limits")]
            [SerializeField] private float m_elevationUpMultiplier = 3;                                                  //Limite del suelo del jugador.
            [SerializeField] private float m_elevationDownMultiplier = 1;                                                  //Limite del suelo del jugador.
            [Space]
            [SerializeField] private float m_minVerticalAngle = -80;                                            //Limite del suelo del jugador.
            [SerializeField] private float m_maxVerticalAngle = 80;                                            //Limite del suelo del jugador.

            [Header("Camera References")]
            [SerializeField] private Transform m_cameraHolder = null;                                           //Referencia del sostenedor de la camara (Hijo).
            [SerializeField] private Transform m_target = null;                                                 //Referencia al objetivo a seguir de la camara.
			
            //Privadas.
            private Vector3 m_positionVelocity = new Vector3();                                                 //Velocidad de la posicion.
            private Vector2 m_actualAngle = new Vector2();                                                      //Angulo de la camara.
            private Vector3 m_direction = new Vector3();                                                        //Direccion de la camara (X y Z).
            private Vector3 m_targetPosition = new Vector3();                                                   //Posicion a guardar del objetivo.
            private Vector2 m_rotationVelocity = new Vector2();

            private Vector2 m_reachAngle = new Vector2();

            private float m_reachDistance = 0;
            private float m_smoothedDistance = 0;
            private float m_zoomVelocity = 0;
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Awake() {
			
            //Establecer singleton.
            m_instance = this;
            m_actualAngle = m_defaultAngle;

            m_reachAngle = m_actualAngle;

            m_reachDistance = m_defaultDistance;
            m_smoothedDistance = m_defaultDistance;
            }     
        private void Update() {
            
            //Direccion

                //Establecer la direccion en X y Z de la camara.
                m_direction = new Vector3(-Mathf.Cos(m_actualAngle.x * Mathf.Deg2Rad), 0, -Mathf.Sin(m_actualAngle.x * Mathf.Deg2Rad));
            
            //Distancia
                m_smoothedDistance = Mathf.SmoothDamp(m_smoothedDistance, m_reachDistance, ref m_zoomVelocity, m_zoomSmoothness);

            //Posicion 

                //Establecer la posicion del objetivo solo si este existe, si no, se mantendra en su posicion.
                SetTargetPosition();

                //Establecer las posiciones del hijo.
                SetCameraChildPosition(m_targetPosition, new Vector2(m_smoothedDistance, m_smoothedDistance));
                }
        private void FixedUpdate() {

            //Mover la posicion de la camara de manera fluida frente a objetos con fisicas.
            MoveToATarget(new Vector3(m_targetPosition.x, 0, m_targetPosition.z));
            }
        private void LateUpdate() {

            //Girar la camara dependiendo de la posicion del objetivo.
            SetCameraRotation(m_targetPosition);
            }

        private void OnValidate() {

            m_defaultAngle = GetProcessedAngle(m_defaultAngle);
            m_actualAngle = m_defaultAngle;

            m_minVerticalAngle = Mathf.Clamp(m_minVerticalAngle, -90, 0);
            m_maxVerticalAngle = Mathf.Clamp(m_maxVerticalAngle, 0, 90);

            m_defaultDistance = Mathf.Clamp(m_defaultDistance, m_minDistance, m_maxDistance);

            SetTargetPosition();
            SetCameraChildPosition(m_targetPosition, new Vector2(m_defaultDistance, m_defaultDistance));

            MoveToATarget(new Vector3(m_targetPosition.x, 0, m_targetPosition.z));
            SetCameraRotation(m_targetPosition);
            }
        
        #if UNITY_EDITOR
        private void OnDrawGizmosSelected() {
            
            //Dibujar linea de direccion.
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + m_direction);

            //Dibujar objetivo.
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_targetPosition, 0.5f);

            //Dibujar distancia por defecto de la camara.
            Gizmos.color = Color.blue;
            int m_presition = 32;
            Vector3 m_targetPos = m_targetPosition;
            float m_multiplier = m_positionMultiplier.x;
            float m_dis = m_defaultDistance * m_multiplier;

            for(int i = 0; i < m_presition; i ++) {

                float m_radA = (i * ((Mathf.PI * 2) / m_presition));
                float m_radB = ((i + 1) * ((Mathf.PI * 2) / m_presition));

                Vector3 m_posA = new Vector3(Mathf.Sin(m_radA), 0, Mathf.Cos(m_radA)) * m_dis;
                Vector3 m_posB = new Vector3(Mathf.Sin(m_radB), 0, Mathf.Cos(m_radB)) * m_dis;

                Gizmos.DrawLine(m_targetPos + m_posA, m_targetPos + m_posB);
                }

            //Dibujar rango minimo de la camara.
            Gizmos.color = Color.green;
            m_dis = m_minDistance * m_multiplier;

            for(int i = 0; i < m_presition; i ++) {

                float m_radA = (i * ((Mathf.PI * 2) / m_presition));
                float m_radB = ((i + 1) * ((Mathf.PI * 2) / m_presition));

                Vector3 m_posA = new Vector3(Mathf.Sin(m_radA), 0, Mathf.Cos(m_radA)) * m_dis;
                Vector3 m_posB = new Vector3(Mathf.Sin(m_radB), 0, Mathf.Cos(m_radB)) * m_dis;

                Gizmos.DrawLine(m_targetPos + m_posA, m_targetPos + m_posB);
                }

            //Dibujar rango maximo de la camara.
            Gizmos.color = Color.red;
            m_dis = m_maxDistance * m_multiplier;

            for(int i = 0; i < m_presition; i ++) {

                float m_radA = (i * ((Mathf.PI * 2) / m_presition));
                float m_radB = ((i + 1) * ((Mathf.PI * 2) / m_presition));

                Vector3 m_posA = new Vector3(Mathf.Sin(m_radA), 0, Mathf.Cos(m_radA)) * m_dis;
                Vector3 m_posB = new Vector3(Mathf.Sin(m_radB), 0, Mathf.Cos(m_radB)) * m_dis;

                Gizmos.DrawLine(m_targetPos + m_posA, m_targetPos + m_posB);
                }

            //Dibujar arco vertical.
            Gizmos.color = Color.red;
            m_multiplier = m_positionMultiplier.y;

            float m_disX = m_defaultDistance * m_positionMultiplier.x;
            float m_disY = m_defaultDistance * m_positionMultiplier.y;

            float m_initAngle = m_minVerticalAngle;
            float m_angleSize = -m_initAngle + m_maxVerticalAngle;

            float m_limitsSize = 0.2f;

            for(int i = 0; i < m_presition; i ++) {

                float m_angleA = m_initAngle + (i * (m_angleSize / m_presition));
                float m_angleB = m_initAngle + ((i + 1) * (m_angleSize / m_presition));

                int m_signA = Mathf.RoundToInt(Mathf.Sign(m_angleA));
                int m_signB = Mathf.RoundToInt(Mathf.Sign(m_angleB));

                float m_angleDisMultiplierA = 0;
                float m_angleDisMultiplierB = 0;

                if (m_signA == -1) m_angleDisMultiplierA = m_elevationDownMultiplier;
                else if (m_signA == 1) m_angleDisMultiplierA = m_elevationUpMultiplier;

                if (m_signB == -1) m_angleDisMultiplierB = m_elevationDownMultiplier;
                else if (m_signB == 1) m_angleDisMultiplierB = m_elevationUpMultiplier;

                float m_elevationValueA = (Mathf.Sin(m_angleA * Mathf.Deg2Rad) * m_disY) * m_angleDisMultiplierA;
                float m_elevationValueB = (Mathf.Sin(m_angleB * Mathf.Deg2Rad) * m_disY) * m_angleDisMultiplierB;
                
                float m_yA = m_targetPos.y + m_elevationValueA;
                float m_yB = m_targetPos.y + m_elevationValueB;

                float m_xA = ((Mathf.Cos(m_defaultAngle.x * Mathf.Deg2Rad) * m_disX) * m_positionMultiplier.x) * (Mathf.Cos(m_angleA * Mathf.Deg2Rad));
                float m_xB = ((Mathf.Cos(m_defaultAngle.x * Mathf.Deg2Rad) * m_disX) * m_positionMultiplier.x) * (Mathf.Cos(m_angleB * Mathf.Deg2Rad));

                float m_zA = ((Mathf.Sin(m_defaultAngle.x * Mathf.Deg2Rad) * m_disX) * m_positionMultiplier.x) * (Mathf.Cos(m_angleA * Mathf.Deg2Rad));
                float m_zB = ((Mathf.Sin(m_defaultAngle.x * Mathf.Deg2Rad) * m_disX) * m_positionMultiplier.x) * (Mathf.Cos(m_angleB * Mathf.Deg2Rad));

                Vector3 m_posA = new Vector3(m_xA, m_yA, m_zA);
                Vector3 m_posB = new Vector3(m_xB, m_yB, m_zB);

                Gizmos.DrawLine(m_posA, m_posB);

                if (i == 0) Gizmos.DrawLine(m_posA + (Vector3.down * m_limitsSize), m_posA + (Vector3.up * m_limitsSize));
                else if(i == m_presition - 1) Gizmos.DrawLine(m_posB + (Vector3.down * m_limitsSize), m_posB + (Vector3.up * m_limitsSize));

                }
            }
        #endif

        //Funciones privadas.
        private void MoveToATarget(Vector3 positionToFollow) {

            transform.position = Vector3.SmoothDamp(transform.position, positionToFollow, ref m_positionVelocity, m_positionSmoothness);
            }
        private void SetCameraRotation(Vector3 positionToLook) {

            m_cameraHolder.LookAt(positionToLook, Vector3.up);
            }
        private void SetTargetPosition() {

            m_targetPosition = m_target == null ? transform.position : m_target.position;
            }
        private void SetCameraChildPosition(Vector3 targetPosition, Vector2 distance) {

            //Calculo matematico que encierra un arco vertical dependiendo de el angulo actual.
            int m_sign = Mathf.RoundToInt(Mathf.Sign(m_actualAngle.y));
            float m_multiplier = 0;

            if (m_sign == -1) m_multiplier = m_elevationDownMultiplier;
            else if (m_sign == 1) m_multiplier = m_elevationUpMultiplier;

            float m_elevationValue = (Mathf.Sin(m_actualAngle.y * Mathf.Deg2Rad) * distance.y) * m_positionMultiplier.y * m_multiplier;

            float m_y = targetPosition.y + m_elevationValue;

            //Calculo matematico que representa la horizontalidad dependiendo de la verticalidad.
            float m_x = ((Mathf.Cos(m_actualAngle.x * Mathf.Deg2Rad)) * m_positionMultiplier.x) * Mathf.Cos(m_actualAngle.y * Mathf.Deg2Rad);
            float m_z = ((Mathf.Sin(m_actualAngle.x * Mathf.Deg2Rad)) * m_positionMultiplier.x) * Mathf.Cos(m_actualAngle.y * Mathf.Deg2Rad);
            
            //Establecer la posicion del hijo multiplicando la distancia.
            m_cameraHolder.localPosition = new Vector3(m_x * distance.x, m_y, m_z * distance.x);
            }

        //Funciones publicas.
        public void SetRotationVelocity(Vector2 velocity) {
            
            m_reachAngle += new Vector2(-velocity.x, -velocity.y) * m_cameraRotationSensitivity;

            m_actualAngle = new Vector2(Mathf.SmoothDampAngle(m_actualAngle.x, m_reachAngle.x, ref m_rotationVelocity.x, m_rotationSmoothness), Mathf.SmoothDampAngle(m_actualAngle.y, m_reachAngle.y, ref m_rotationVelocity.y, m_rotationSmoothness));// Vector2.SmoothDamp(m_actualAngle, m_reachAngle, ref m_rotationVelocity, m_rotationSmoothness);
            m_actualAngle = GetProcessedAngle(m_actualAngle);
            }
		public Vector2 GetProcessedAngle(Vector2 angle) {
            
            Vector2 m_angles = angle;

            //Sumar la velocidad al angulo a utilizar.
            if (m_angles.x >= 360) m_angles = new Vector2(m_angles.x - 360, m_angles.y);
            if (m_angles.x < 0) m_angles = new Vector2(m_angles.x + 360, m_angles.y);

            m_angles = new Vector2(m_angles.x, Mathf.Clamp(m_angles.y, m_minVerticalAngle, m_maxVerticalAngle));

            return m_angles;
            }
        public Quaternion GetDirection() {

            return Quaternion.Euler(0, Mathf.Atan2(m_direction.x, m_direction.z) * Mathf.Rad2Deg, 0);
            }
        public void Zoom(float value) {

            m_reachDistance = Mathf.Clamp(m_reachDistance + ((value/120f) * m_zoomSensitivity), m_minDistance, m_maxDistance);   
            }

        public static CameraBrain GetSingleton() {

            return m_instance;
            }

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }

