using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[CreateAssetMenu(fileName = "Weapon", menuName = "Game/Items/Weapon")]
public class Weapon : ScriptableObject {
	
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
            [SerializeField] private string m_name = "";
            [SerializeField] private WeaponType m_type = WeaponType.Melee;
            [SerializeField] private Rarity m_rarity = Rarity.Common;

            [Space]
            [SerializeField] private int m_minDamage = 4;
            [SerializeField] private int m_maxDamage = 10;
            [SerializeField] private int m_uses = 100;
			
            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
		
        //Funciones publicas.
        public string GetName() => m_name;
        public WeaponType GetWeaponType() => m_type;
        public Rarity GetRarity() => m_rarity;
        public int GetMinDamage() => m_minDamage;
        public int GetMaxDamage() => m_maxDamage;
        public int GetUses() => m_uses;
		
        public int GetCalculatedDamage(int uses) => Mathf.RoundToInt(Mathf.Lerp(m_minDamage, m_maxDamage, (float) uses/m_uses));

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
