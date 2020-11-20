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

            private Seed m_seed;
			
			
    //Funciones
		
        //Funciones de MonoBehaviour.
		
        //Funciones privadas.
		
        //Funciones publicas.
        public void SetData(Seed seed, Vector3Int position) {

            m_seed = seed;

            m_meshFilter.mesh = seed.GetSeedMesh();
            m_position = position;

            StartCoroutine(GrowCoroutine(seed.GetTimeToGrow()));
            }
        public bool GetIfCanHarvest() => m_canHarvest;
		public Vector3Int GetPosition() => m_position;

        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
        private IEnumerator GrowCoroutine(float timeToGrow) {

            for(float i = 0; i < timeToGrow; i += Time.deltaTime) {

                m_plantClock.SetValue(Mathf.Lerp(0, 1, i / timeToGrow));
                yield return null;
                }
            
            m_canHarvest = true;
            m_meshFilter.mesh = m_seed.GetPlantMesh();
            m_plantClock.gameObject.SetActive(false);
            }
        }
