using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : EntityHealth {
	
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
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		protected override int SetActualHealth() {

            int m_h = m_health;

            TemporalData m_p = DataSystem.GetSingleton().GetTemporalData();
            if (m_p != null) m_h = m_p.GetPlayer().GetHealth();

            return m_health;
            }
		protected override void Dead() {
            
            CameraController.ResetCameraInStart();
            DataSystem.GetSingleton().SetTemporalData(null);
            SceneController.GetSingleton().LoadScene(SceneController.Scenes.House);
            }

        //Funciones ha heredar.
		
        //Corotinas.
		
        }
