using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinsEntryController : InteractableBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] private Inventory_DungeonItemSelectionUI m_dungeonInventory = null;
			
            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour

        //Funciones privadas.
		
        //Funciones publicas.
        public override void Interact() {

            DungeonData m_dD = new DungeonData();
            m_dD.GetPlayer().SetHealth(DataSystem.GetSingleton().GetPlayerHealth());

            InventoryData m_iD = DataSystem.GetSingleton().GetNewDungeonInventoryData(false);
            m_dD.SetInventoryData(m_iD);

            DataSystem.GetSingleton().SetDungeonData(m_dD);

            m_dungeonInventory.Open();
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
