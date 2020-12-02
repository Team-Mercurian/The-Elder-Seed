using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSpawnController : MonoBehaviour {
	
	//Enumerators.
	public enum SpawnType {

		House,
		Altar,
		Ruins,
		}
	
	//Structs
	
	//Set Variables
	
		//Static
        private static SpawnType m_spawnType = SpawnType.House;
        
		//No Static
		[Header("References")]
		[SerializeField] private PlayerBrain m_player = null;
		[Space]
		[SerializeField] private Transform m_houseSpawn = null;
		[SerializeField] private Transform m_altarSpawn = null;
		[SerializeField] private Transform m_ruinsSpawn = null;

		private Vector3 m_position = Vector3.zero;
		private float m_angle = 0;

    //Functions
	
		//MonoBehaviour Functions
		private void Start() {

			SetSpawnVariables(m_spawnType, ref m_position, ref m_angle);
			m_player.GetMovement().SetPositionAndDirection(m_position, m_angle);
			}
        
		//Public Functions
        public static void SetSpawn(SpawnType spawnType) => m_spawnType = spawnType;
        
		//Private Functions
        private void SetSpawnVariables(SpawnType spawnType, ref Vector3 position, ref float angle) {

			switch(spawnType) {
				
				case SpawnType.House : position = m_houseSpawn.position; angle = 0; break;
				case SpawnType.Altar : position = m_altarSpawn.position; angle = 0; break;
				case SpawnType.Ruins : position = m_ruinsSpawn.position; angle = 180; break;
				}
			}	 
        
	//Coroutines
	
	}
