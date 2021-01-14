using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryoSlimeProyectile : MonoBehaviour {
	
	//Singleton
	//private static CryoSlimeProyectile m_instance = null;
	//private void Awake() => m_instance = this;
	//public static CryoSlimeProyectile GetSingleton() => m_instance;
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
        [Header("Values")]
		[SerializeField] private float m_speed = 2;
		[SerializeField] private float m_timeToDisappear = 3;

		private Weapon m_weapon = null;
        private Transform m_parent = null;

		private Vector2 m_velocity;

    //Functions
	
		//MonoBehaviour Functions
        private void Start() {

			Vector3 m_playerPos = PlayerBrain.GetSingleton().transform.position;
			float m_rad = Mathf.Atan2(m_playerPos.z - transform.position.z, m_playerPos.x - transform.position.x);
			m_velocity = new Vector2(Mathf.Cos(m_rad), Mathf.Sin(m_rad)); 

			transform.eulerAngles = new Vector3(90, 0, m_rad * Mathf.Rad2Deg + 90);

			StartCoroutine(Brain());
			}
        private void Update() {

			transform.position += new Vector3(m_velocity.x, 0, m_velocity.y) * Time.deltaTime * m_speed;
			}
		private void OnTriggerEnter(Collider other) {

			if (other.CompareTag("Player")) {

				Vector3 dir1 = (other.transform.position - m_parent.position).normalized;
				Vector2 dir2 = new Vector2(dir1.x, dir1.z);
				Knockback m_knockback = new Knockback(dir2, m_weapon.GetKnockbackForce(), m_weapon.GetKnockbackTime());
				other.gameObject.GetComponent<EntityHealth>().GetDamage(m_weapon.GetCalculatedDamage(m_weapon.GetUses()), m_knockback);
				}
			}

		//Public Functions
		public void SetData(Weapon weapon, Transform parent) {

			m_weapon = weapon;
			m_parent = parent;
			}
        
	//Coroutines
	private IEnumerator Brain() {

		yield return new WaitForSeconds(m_timeToDisappear);
		Vector3 m_savedSize = transform.localScale;
		
		for(float i = 0; i < 0.5f; i += Time.deltaTime) {
			
			transform.localScale = m_savedSize * Mathf.Lerp(1f, 0, i / 0.5f);
			yield return null;
			}
		
		Destroy(gameObject);
		}
	}
