using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedEntityController : InteractableBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
		[Header("Values")]
		[SerializeField] private float m_moveToSpeed = 1;

        private int m_index;
        
		private Transform m_player;

    //Functions
	
		//MonoBehaviour Functions
		private void Start() {

			m_player = PlayerBrain.GetSingleton().transform;
			}
		private void Update() {

			transform.position = Vector3.MoveTowards(transform.position, m_player.position + (Vector3.up), m_moveToSpeed * Time.deltaTime);
			}
        
		//Public Functions
		public void SetData(int index) {
			
			m_index = index;
			}
		public override void Interact() {
			
            DataSystem.GetSingleton().GetDungeonData().AddSeed(m_index);  //GetFarmData().AddSeed(m_type, Rarity.Common);
			Seed m_sd = DataSystem.GetSingleton().GetSeed(m_index);

			Debug.Log("Added a seed to the dungeon loot: " + m_index + ", " + m_sd.GetSeedType() + ", " + m_sd.GetRarity());
			Destroy(gameObject);
			}
        
		//Private Functions
        
        
	//Coroutines
	
	}
