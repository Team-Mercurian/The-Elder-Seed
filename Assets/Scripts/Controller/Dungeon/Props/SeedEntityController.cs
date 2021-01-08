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

        private int m_ID;
        
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
		public void SetData(int id) {
			
			m_ID = id;
			}
		public override void Interact() {
			
            DataSystem.GetSingleton().GetDungeonData().GetInventoryData().AddSeed(m_ID, 1);  //GetFarmData().AddSeed(m_type, Rarity.Common);
			Seed m_sd = DataSystem.GetSingleton().GetSeed(m_ID);
			ObtainedObjectsUI.GetSingleton().AddItem(m_sd.GetIcon(), m_sd.GetName(), m_sd.GetRarity());
			Destroy(gameObject);
			}
        
		//Private Functions
        
        
	//Coroutines
	
	}
