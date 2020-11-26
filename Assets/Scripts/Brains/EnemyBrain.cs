using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : EntityBrain {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
        
        
		//Private Functions
        protected void ConstantJump(float minRandomJump, float maxRandomJump) {

            StartCoroutine(ConstantJumpCoroutine(minRandomJump, maxRandomJump));
            }   
        
        
	//Coroutines
	private IEnumerator ConstantJumpCoroutine(float minSecs, float maxSecs) {
		
		while(true) {

			GetMovement().Jump();
			GetAttack().Attack();
			yield return new WaitForSeconds(Random.Range(minSecs, maxSecs));
			}
		}
	}
