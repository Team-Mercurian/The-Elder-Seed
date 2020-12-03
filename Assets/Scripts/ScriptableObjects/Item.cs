using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
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
            [SerializeField] private string m_name = "";
            [SerializeField] private Rarity m_rarity = Rarity.Common;
            [SerializeField] private Sprite m_UIIcon = null;

            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
		
        //Funciones publicas.
        public string GetName() => m_name;
        public Rarity GetRarity() => m_rarity;
        public Sprite GetIcon() => m_UIIcon;

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
