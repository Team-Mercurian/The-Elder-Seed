using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[CreateAssetMenu(fileName = "Seed", menuName = "Game/Items/Seed")]
public class Seed : ScriptableObject {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
        public enum SeedType {

            A,
            B,
            }
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Seed Values")]
            [SerializeField] private string m_name = "";
            [SerializeField] private SeedType m_type = SeedType.A;
            [SerializeField] private Rarity m_rarity = Rarity.Common;
            [SerializeField] private Sprite m_UIIcon = null;
            [SerializeField] private Mesh m_seedMesh = null;
            [SerializeField] private Mesh m_plantMesh = null;
            [SerializeField] private float m_timeToGrow = 5;
			
            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
		
        //Funciones publicas.
        public string GetName() => m_name;
        public SeedType GetSeedType() => m_type;
        public Rarity GetRarity() => m_rarity;
        public Sprite GetIcon() => m_UIIcon;
        public Mesh GetSeedMesh() => m_seedMesh;
        public Mesh GetPlantMesh() => m_plantMesh;
        public float GetTimeToGrow() => m_timeToGrow;

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
