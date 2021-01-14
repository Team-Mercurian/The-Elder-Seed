using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

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
		private void ConfirmEvent() {
        
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

            if (DataSystem.GetSingleton().GetGameData().GetTutorialIndex() < 11) 
                TutorialController.GetSingleton().SetTutorialText(11);

            m_dungeonInventory.Open();
            }

        //Funciones publicas.
        public override void Interact() {

            if (DataSystem.GetSingleton().GetGameData().GetTutorialIndex() < 10) {

                UnityEvent m_event;

                m_event = new UnityEvent();
                m_event.AddListener(() => ConfirmEvent());
                ButtonEvent m_lEvent = new ButtonEvent("Si", m_event);
                ButtonEvent m_rEvent = new ButtonEvent("No", null);

                ConfirmationUI.GetSingleton().SetData("¿Quieres saltarte el tutorial?", m_lEvent, m_rEvent, false);
                ConfirmationUI.GetSingleton().Open();
                }
            
            else {

                ConfirmEvent();
                }
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
