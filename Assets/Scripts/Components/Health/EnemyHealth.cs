using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : EntityHealth {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Drops")]
            [SerializeField] private GameObject m_seedEntity = null;

            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void Update() {

            if (m_healthBar != null) m_healthBar.RotateToCamera(CameraController.GetSingleton().GetCameraTransform());
            }
		
        //Funciones privadas.
		private void DropObject() {
            
            if (Random.Range(0f, 100f) > 75f) return;
            if (m_seedEntity == null) return;
            
            DataSystem m_ds = DataSystem.GetSingleton();

            Seed.SeedType m_type = Random.Range(0, 2) < 1 ? Seed.SeedType.Durability : Seed.SeedType.Potion;
            int m_index = m_ds.GetRandomSeedID(4 * GenerateRuinsRooms.GetActualFloor(), m_type);

            SeedEntityController m_s = Instantiate(m_seedEntity, transform.position, Quaternion.identity).GetComponent<SeedEntityController>();
            m_s.SetData(m_index);
            }

        //Funciones publicas.
        protected override int SetActualHealth() {
            
            m_health = Mathf.RoundToInt(m_health * (1 + (0.25f * DataSystem.GetSingleton().GetDungeonData().GetFloor())));
            return m_health;
            }
		protected override void Dead() {
            
            if(m_animator != null)
            {
                m_animator.SetBool("dead", true);
            }

            GetComponent<EnemyMovement>().SetHorizontalVelocity(Vector2.zero);
            DropObject();
            GetComponent<EnemyBrain>().StopAllCoroutines();
            Invoke("FinishDead", 1.5f);
            }
        protected override void HealthBarDeadAction() {

            Destroy(m_healthBar.gameObject);
            }

        private void FinishDead() {

            RoomController.GetSingleton().DestroyProp(gameObject);
            RoomController.GetSingleton().CheckAndOpenPassages(false);
            Destroy(gameObject);
            }
        protected override void SaveHealth(int health) {


            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
