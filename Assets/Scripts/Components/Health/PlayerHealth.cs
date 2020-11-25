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

            DungeonData m_p = DataSystem.GetSingleton().GetDungeonData();
            if (m_p != null) m_h = m_p.GetPlayer().GetHealth();

            return m_health;
            }
		protected override void Dead() {
            
            CameraController.ResetCameraInStart();
            GenerateRuinsRooms.ExitRuins(true);
            }

        //Funciones ha heredar.
		
        //Corotinas.
		
        }
