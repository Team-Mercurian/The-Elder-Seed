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
            [SerializeField] private PlantClockController m_plantClock = null;

            //Privadas.
            private bool m_canHarvest = false;
            private Vector3Int m_position;

            private int m_seedID;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour.
		
        //Funciones privadas.
		
        //Funciones publicas.
        public void SetData(int seedID, Vector3Int position, int count, int maxCount) {

            m_seedID = seedID;
            m_position = position;

            SetCount(count, maxCount);
            }
        public bool GetIfCanHarvest() => m_canHarvest;
		public Vector3Int GetPosition() => m_position;
        public int GetSeedID() => m_seedID;

        public void Finish() {

           SetCount(1, 1);
            }      
        public void SetCount(int actual, int max) {
            
            m_canHarvest = actual == max;
            m_plantClock.gameObject.SetActive(!m_canHarvest);
            if (!m_canHarvest) {

                m_plantClock.SetValue((float) actual / max);
                }
                
            Seed m_seed = DataSystem.GetSingleton().GetSeed(m_seedID);
            m_meshFilter.mesh = m_canHarvest ? m_seed.GetPlantMesh() : m_seed.GetSeedMesh();
            }

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        }
