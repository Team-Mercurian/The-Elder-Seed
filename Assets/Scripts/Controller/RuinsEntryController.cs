using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            InventoryData m_iD = DataSystem.GetSingleton().GetNewInventoryData(false);

            //Set best weapon to the default weapon.
            List<WeaponEntityData> m_weaponDatas = DataSystem.GetSingleton().GetGameData().GetInventoryData().GetWeaponList().OrderByDescending(c => c.GetUses()).ThenByDescending(c => DataSystem.GetSingleton().GetWeapon(c.GetID()).GetRarity()).ToList();
            int m_actualWeapon = m_weaponDatas[0].GetIndex();

            m_iD.AddWeapon(DataSystem.GetSingleton().GetGameData().GetInventoryData().GetWeaponData(m_actualWeapon));
            m_dD.SetActualWeapon(m_actualWeapon);
            m_dD.SetInventoryData(m_iD);

            DataSystem.GetSingleton().SetDungeonData(m_dD);

            m_dungeonInventory.Open();
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
