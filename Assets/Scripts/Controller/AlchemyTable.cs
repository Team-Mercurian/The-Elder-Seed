using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyTable : InteractableBehaviour {
	
	//Singleton
	//private static AlchemyTableUI m_instance = null;
	//private void Awake() => m_instance = this;
	//public static AlchemyTableUI GetSingleton() => m_instance;
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
        [Header("References")]
		[SerializeField] private PotionCraftUI m_potionCraft = null;

        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void Interact() {
			
			m_potionCraft.Open();
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
