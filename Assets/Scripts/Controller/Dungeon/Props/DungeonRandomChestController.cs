using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRandomChestController : InteractableBehaviour {
	
	//Singleton
	//private static DungeonRandomChestController m_instance = null;
	//private void Awake() => m_instance = this;
	//public static DungeonRandomChestController GetSingleton() => m_instance;
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
		[Header("Values")]
		[SerializeField][Range(0, 100)] private int m_probabilityToSpawn = 25;
        
        
    //Functions
	
		//MonoBehaviour Functions
		private void Start() {
			
			if (Random.Range(0, 100) > m_probabilityToSpawn) {
				
           		RoomController.GetSingleton().DestroyProp(gameObject);
				Destroy(gameObject);
				}
			}
        
		//Public Functions
        public override void Interact() {

            //Establecer variables locales.
            DataSystem m_dS = DataSystem.GetSingleton();
            InventoryData m_dI = DataSystem.GetSingleton().GetDungeonData().GetInventoryData();
            int m_probabilityIncrement = 4 * GenerateRuinsRooms.GetActualFloor();

            //Dar semillas de desbloqueo aleatorias dependiendo de una probabilidad.
            if (Random.Range(0f, 100f) < 5f) { 

                int m_seedID = m_dS.GetRandomSeedID(m_probabilityIncrement, Seed.SeedType.Unlock);
                Seed m_seed = m_dS.GetSeed(m_seedID);
                m_dI.AddSeed(m_seedID, 1);
			    ObtainedObjectsUI.GetSingleton().AddItem(m_seed.GetIcon(), m_seed.GetName(), m_seed.GetRarity());
                }
            
            //Añadir semillas.

                //Numero de semillas a dar.
                int m_count = (Random.Range(1, 2)); 

                //Añadir aleatoriamente semillas.
                for(int i = 0; i < m_count; i ++) {

                    Seed.SeedType m_type = Random.Range(0, 2) < 1 ? Seed.SeedType.Durability : Seed.SeedType.Potion;
                    int m_index = m_dS.GetRandomSeedID(m_probabilityIncrement, m_type);

                    if (m_index != -1) {
                        
                        m_dI.AddSeed(m_index, 1);
			            ObtainedObjectsUI.GetSingleton().AddItem(m_dS.GetSeed(m_index).GetIcon(), m_dS.GetSeed(m_index).GetName(), m_dS.GetSeed(m_index).GetRarity());
                        }
                    }

            //Destruir cofre.
            RoomController.GetSingleton().DestroyProp(gameObject);
            Destroy(gameObject);            
            }
        
		//Private Functions
        
        
	//Coroutines
	
	}
