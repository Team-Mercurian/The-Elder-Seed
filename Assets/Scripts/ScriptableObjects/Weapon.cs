using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[CreateAssetMenu(fileName = "Weapon", menuName = "Game/Items/Weapon")]
public class Weapon : Item {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
        public enum WeaponType {

            Melee,
            Range,
            }
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Weapon Values")]
            [SerializeField] private WeaponType m_type = WeaponType.Melee;
            [SerializeField] private int m_baseDamage = 120;
            [SerializeField] [Range(0, 100)] private int m_criticalProbability = 20;
            [SerializeField] private float m_range = 20;
            [SerializeField] private int m_baseUses = 50;

            [Header("Weapon Knockback")]
            [SerializeField] private float m_knockbackForce = 1f;
            [SerializeField] private float m_knockbackTime = 0.5f;

            [Header("Weapon Mesh")]
            [SerializeField] private Mesh m_weaponMesh = null;

            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.

        //Funciones publicas.
        public WeaponType GetWeaponType() => m_type;
        public int GetMinDamage() => m_baseDamage;
        public int GetUses() => m_baseUses;
        public int GetCriticalProbability() => m_criticalProbability;
        public float GetRange() => m_range;

        public float GetKnockbackForce() => m_knockbackForce;
        public float GetKnockbackTime() => m_knockbackTime;

        public Mesh GetWeaponMesh() => m_weaponMesh;
		
        public int GetCalculatedDamage(int uses, bool critical) {
            
            //(Daño base + (Daño base * (desgaste actual / desgaste máximo))) * multiplicador de daño crítico. 

            int m_damage = Mathf.RoundToInt(m_baseDamage + (m_baseDamage * (float) uses/ GetUses()));

            if (critical && Random.Range(0, 100) < m_criticalProbability) m_damage = Mathf.RoundToInt(m_damage * 1.5f); 

            return m_damage;
            }
        
        public int GetBaseDamage() => m_baseDamage * 2;

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
