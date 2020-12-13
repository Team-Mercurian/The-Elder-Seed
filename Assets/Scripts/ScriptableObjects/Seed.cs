using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[CreateAssetMenu(fileName = "Seed", menuName = "Game/Items/Seed")]
public class Seed : Item {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
        public enum SeedType {

            Durability,
            Potion,
            Unlock,
            }
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("Seed Values")]
            [SerializeField] private SeedType m_type = SeedType.Durability;
            [SerializeField] private Mesh m_seedMesh = null;
            [SerializeField] private Mesh m_plantMesh = null;			

            [SerializeField] private Plant m_plant = null;

            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
		
        //Funciones publicas.
        public SeedType GetSeedType() => m_type;
        public Mesh GetSeedMesh() => m_seedMesh;
        public Mesh GetPlantMesh() => m_plantMesh;
        public Plant GetPlant() => m_plant;

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
