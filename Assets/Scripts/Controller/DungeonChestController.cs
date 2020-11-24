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

            if (Random.Range(0f, 100f) < 20f) { 

                DataSystem.GetSingleton().GetGameData().GetFarmData().AddSeed(Seed.SeedType.Unlock, Rarity.Common);
                }
            
            int m_count = (Random.Range(1, 5)); 

            for(int i = 0; i < m_count; i ++) {

                Seed.SeedType m_type = Random.Range(0, 2) < 1 ? Seed.SeedType.Durability : Seed.SeedType.Potion;
                DataSystem.GetSingleton().GetGameData().GetFarmData().AddSeed(m_type, Rarity.Common);
                }

            Weapon m_weapon = DataSystem.GetSingleton().AddRandomWeapon();
            Debug.Log("Added the weapon: " + m_weapon.GetName() + " to the inventory.");
            Destroy(gameObject);            
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
