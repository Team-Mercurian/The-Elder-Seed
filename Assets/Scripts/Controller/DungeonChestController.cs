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

            //Establecer variables locales.
            DataSystem m_dataSystem = DataSystem.GetSingleton();

            //Dar semillas de desbloqueo aleatorias dependiendo de una probabilidad.
            if (Random.Range(0f, 100f) < 20f) { 

                m_dataSystem.GetDungeonData().AddSeed(m_dataSystem.GetRandomSeedIndex(0, Seed.SeedType.Unlock));
                }
            
            //Añadir semillas.

                //Numero de semillas a dar.
                int m_count = (Random.Range(1, 5)); 

                //Añadir aleatoriamente semillas.
                for(int i = 0; i < m_count; i ++) {

                    Seed.SeedType m_type = Random.Range(0, 2) < 1 ? Seed.SeedType.Durability : Seed.SeedType.Potion;
                    int m_index = m_dataSystem.GetRandomSeedIndex(0, m_type);

                    if (m_index != -1) m_dataSystem.GetDungeonData().AddSeed(m_index);
                    }

            //Dar un arma al azar.
            int m_weaponIndex = m_dataSystem.GetRandomWeaponIndex(0);

            if (m_weaponIndex != -1) { 

                Weapon m_weapon = m_dataSystem.GetWeapon(m_weaponIndex);
                m_dataSystem.GetGameData().GetInventoryData().AddWeapon(m_weaponIndex, m_weapon.GetUses());
                Debug.Log("Added the weapon: " + m_weapon.GetName() + " to the inventory.");
                }

            //Destruir cofre.
            Destroy(gameObject);            
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
