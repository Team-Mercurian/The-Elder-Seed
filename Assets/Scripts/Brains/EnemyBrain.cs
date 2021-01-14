using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : EntityBrain {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
		[SerializeField] private Transform m_indicator = null;
        private Coroutine m_movementRoutine = null;
        private Coroutine m_jumpRoutine = null;
        
    //Functions
	
		//MonoBehaviour Functions
        private void Update() {

			m_indicator.eulerAngles = new Vector3(0, CameraController.GetDirection().eulerAngles.y, 0);
			}

		//Public Functions
        
        
		//Private Functions
        protected void ConstantJump(float minRandomJump, float maxRandomJump) {
			
			if (m_jumpRoutine != null) StopCoroutine(m_jumpRoutine);
			m_jumpRoutine = StartCoroutine(ConstantJumpCoroutine(minRandomJump, maxRandomJump));
			}

		protected void ConstantMovement(float minTime, float maxTime) {
			
			if (m_movementRoutine != null) StopCoroutine(m_movementRoutine);
			m_movementRoutine = StartCoroutine(ConstantMovementCoroutine(minTime, maxTime));
			}
		protected void FollowPlayer() {

			if (m_movementRoutine != null) StopCoroutine(m_movementRoutine);
			m_movementRoutine = StartCoroutine(FollowPlayerCoroutine(false));
			}
		protected void FollowPlayerInverse() {

			if (m_movementRoutine != null) StopCoroutine(m_movementRoutine);
			m_movementRoutine = StartCoroutine(FollowPlayerCoroutine(true));
			}

		protected void JumpRoutine_Stop() {

			if (m_jumpRoutine == null) return;
			StopCoroutine(m_jumpRoutine);
			}
		protected void MovementRoutine_Stop() {

			m_movement.SetHorizontalVelocity(Vector2.zero);
			if (m_movementRoutine == null) return;
			StopCoroutine(m_movementRoutine);
			}

		protected void JumpAttack() {

			GetMovement().Jump();
			GetAttack().Attack();
			}
		

		protected float GetPlayerDistance() => Mathf.Abs(Vector3.Distance(PlayerBrain.GetSingleton().transform.position, transform.position));
        
	//Coroutines
	private IEnumerator ConstantJumpCoroutine(float minSecs, float maxSecs) {
		
		while(true) {

			JumpAttack();
			yield return new WaitForSeconds(Random.Range(minSecs, maxSecs));
			}
		}
	private IEnumerator ConstantMovementCoroutine(float minTime, float maxTime) {
		
		while(true) {

			float m_randomAngle = Random.Range(0, 360);
			GetMovement().SetHorizontalVelocity(new Vector2(Mathf.Cos(m_randomAngle * Mathf.Deg2Rad), Mathf.Sin(m_randomAngle * Mathf.Deg2Rad)));
			yield return new WaitForSeconds(Random.Range(minTime, maxTime));

			GetMovement().SetHorizontalVelocity(Vector2.zero);
			yield return new WaitForSeconds(Random.Range(minTime, maxTime));
			}
		}
	private IEnumerator FollowPlayerCoroutine(bool inverse) {
	
		while(true) {

			Vector2 m_velocity = Vector2.zero;

			if (GetPlayerDistance() > 2 || inverse) {

				Vector3 m_playerPos = PlayerBrain.GetSingleton().transform.position;

				float m_rad = 0;
				
				if (inverse) m_rad = Mathf.Atan2(transform.position.z - m_playerPos.z, transform.position.x - m_playerPos.x);
				else m_rad = Mathf.Atan2(m_playerPos.z - transform.position.z, m_playerPos.x - transform.position.x);

				m_velocity = new Vector2(Mathf.Cos(m_rad), Mathf.Sin(m_rad)); 
				}

			GetMovement().SetHorizontalVelocity(m_velocity);
			yield return null;
			}
		}
	}
