using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[CreateAssetMenu(fileName = "Miscellaneous", menuName = "Game/Items/Miscellaneous")]
public class Item : ScriptableObject {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Base Values")]
            [SerializeField] private int m_id = 0;
            [SerializeField] private string m_name = "";
            [SerializeField][TextArea] private string m_desc = "";
            [SerializeField] private Rarity m_rarity = Rarity.Common;
            [SerializeField] private Sprite m_UIIcon = null;

            [Space]
            [SerializeField] private int m_defaultCount = 0;
            [SerializeField] private bool m_defaultUnlocked = false;

            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
		
        //Funciones publicas.
        public int GetID() => m_id;
        public string GetName() => m_name;
        public string GetDescription() => m_desc;
        public Rarity GetRarity() => m_rarity;
        public Sprite GetIcon() => m_UIIcon;
        public int GetDefaultCount() => m_defaultCount;
        public bool GetDefaultUnlocked() => m_defaultUnlocked;

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
