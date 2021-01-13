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
            DataSystem m_dS = DataSystem.GetSingleton();
            InventoryData m_dI = DataSystem.GetSingleton().GetDungeonData().GetInventoryData();
            int m_probabilityIncrement = 4 * GenerateRuinsRooms.GetActualFloor();

            //Dar semillas de desbloqueo aleatorias dependiendo de una probabilidad.
            if (Random.Range(0f, 100f) < 20f) { 

                m_dI.AddSeed(m_dS.GetRandomSeedID(m_probabilityIncrement, Seed.SeedType.Unlock), 1);
                }
            
            //Añadir semillas.

                //Numero de semillas a dar.
                int m_count = (Random.Range(1, 5)); 

                //Añadir aleatoriamente semillas.
                for(int i = 0; i < m_count; i ++) {

                    Seed.SeedType m_type = Random.Range(0, 2) < 1 ? Seed.SeedType.Durability : Seed.SeedType.Potion;
                    int m_index = m_dS.GetRandomSeedID(m_probabilityIncrement, m_type);

                    if (m_index != -1) m_dI.AddSeed(m_index, 1);
                    }

            //Dar un arma al azar.
            int m_weaponIndex = m_dS.GetRandomWeaponID(m_probabilityIncrement);

            if (m_weaponIndex != -1) { 

                Weapon m_weapon = m_dS.GetWeapon(m_weaponIndex);
                m_dS.GetDungeonData().GetInventoryData().AddWeapon(m_weaponIndex, m_weapon.GetUses(), ref DataSystem.GetSingleton().GetGameData().GetInventoryData().m_lastWeaponID);
                Debug.Log("Added the weapon: " + m_weapon.GetName() + " to the inventory.");
                }

            //Destruir cofre.
            RoomController.GetSingleton().DestroyProp(gameObject);
            Destroy(gameObject);            
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
