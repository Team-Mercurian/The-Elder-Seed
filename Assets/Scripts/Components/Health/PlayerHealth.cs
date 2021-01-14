using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : EntityHealth {
	
	private static PlayerHealth m_instance = null;
	private void Awake() => m_instance = this;
	public static PlayerHealth GetSingleton() => m_instance;

    [SerializeField] Animator m_animator = null;

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
        protected override void Start() {

            m_health = DataSystem.GetSingleton().GetPlayerHealth();
            base.Start();
            }

        public void UsePotion() {

            if (m_actualHealth >= m_health) return;
            if (DataSystem.GetSingleton().GetDungeonData().GetActualPotion().GetCount() <= 0) return;

            DataSystem.GetSingleton().GetDungeonData().UsePotion();
            
            Potion m_potion = DataSystem.GetSingleton().GetPotion(DataSystem.GetSingleton().GetDungeonData().GetActualPotionIndex());
            float m_healPercent = m_potion.GetHealPercent() / 100f;
            int m_intHeal = m_actualHealth + Mathf.RoundToInt(m_healPercent * m_health);

            OverrideHealth(m_intHeal);

            SelectedPotionUI.GetSingleton().SetData(m_potion, DataSystem.GetSingleton().GetDungeonData().GetActualPotion().GetCount());
            }
		
        //Funciones heredadas.
		protected int GetSavedHealth() {

            int m_h = m_health;

            DungeonData m_p = DataSystem.GetSingleton().GetDungeonData();
            if (m_p != null) m_h = m_p.GetPlayer().GetHealth();

            return m_h;
            }
        protected override void SaveHealth(int health) {

            DataSystem.GetSingleton().GetDungeonData().GetPlayer().SetHealth(health);
            }
        protected override int SetActualHealth() {
            
            return GetSavedHealth();
            }
		protected override void Dead() {
            
            PlayerBrain.GetSingleton().GetMovement().SetIfCanMove(false);
            CameraController.ResetCameraInStart();
            GenerateRuinsRooms.ExitRuins(true);
            m_animator.SetTrigger("dead");
            }
        protected override void HealthBarDeadAction() {

            
            }

        //Funciones ha heredar.
		
        //Corotinas.
		
        }
