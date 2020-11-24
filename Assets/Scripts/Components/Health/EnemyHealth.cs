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
            [SerializeField] private GameObject m_drop = null;

            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
		private void DropObject() {
            
            if (Random.Range(0f, 100f) > 75f) return;

            Seed.SeedType m_type = Random.Range(0, 2) < 1 ? Seed.SeedType.Durability : Seed.SeedType.Potion;
            DataSystem.GetSingleton().GetGameData().GetFarmData().AddSeed(m_type, Rarity.Common);

            if (m_drop == null) return;
            Instantiate(m_drop, transform.position, Quaternion.identity);
            }

        //Funciones publicas.
		protected override int SetActualHealth() {

            return m_health;
            }
		protected override void Dead() {
            
            DropObject();
            Destroy(gameObject);
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
