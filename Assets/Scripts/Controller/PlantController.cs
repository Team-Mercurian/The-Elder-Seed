using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            [Header("References")]
            [SerializeField] private MeshFilter m_meshFilter = null;

            //Privadas.
            private bool m_canHarvest = false;
            private Vector3Int m_position;

            private int m_seedID;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour.
		
        //Funciones privadas.
		
        //Funciones publicas.
        public void SetData(int seedID, Vector3Int position, bool canHarvest) {

            m_seedID = seedID;
            Seed m_seed = DataSystem.GetSingleton().GetSeed(seedID);

            m_meshFilter.mesh = m_seed.GetSeedMesh();
            m_position = position;

            m_canHarvest = canHarvest;

            m_meshFilter.mesh = m_canHarvest ? m_seed.GetPlantMesh() : m_seed.GetSeedMesh();
            }
        public bool GetIfCanHarvest() => m_canHarvest;
		public Vector3Int GetPosition() => m_position;
        public int GetSeedID() => m_seedID;

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        }
