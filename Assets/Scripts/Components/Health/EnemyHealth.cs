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
		
        //Funciones privadas.
		private void DropObject() {
            
            if (Random.Range(0f, 100f) > 75f) return;
            if (m_seedEntity == null) return;
            
            DataSystem m_ds = DataSystem.GetSingleton();

            Seed.SeedType m_type = Random.Range(0, 2) < 1 ? Seed.SeedType.Durability : Seed.SeedType.Potion;
            int m_index = m_ds.GetRandomSeedIndex(4 * GenerateRuinsRooms.GetActualFloor(), m_type);

            SeedEntityController m_s = Instantiate(m_seedEntity, transform.position, Quaternion.identity).GetComponent<SeedEntityController>();
            m_s.SetData(m_index);
            }

        //Funciones publicas.
        protected override void SetHealth(int value) {
            
            SetActualHealth(value);
            }
		protected override int GetSavedHealth() {

            return m_health;
            }
		protected override void Dead() {
            
            DropObject();
            RoomController.GetSingleton().DestroyProp(gameObject);
            Destroy(gameObject);
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
