using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonChestController : InteractableBehaviour {
	
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
        public override void Interact() {

            Weapon m_weapon = DataSystem.GetSingleton().AddRandomWeapon();
            Debug.Log("Added the weapon: " + m_weapon.GetName() + " to the inventory.");
            Destroy(gameObject);            
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
