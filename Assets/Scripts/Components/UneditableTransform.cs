using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Miscellaneous/Uneditable Transform")]
public class UneditableTransform : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
            public Vector3 position;
            public Vector3 rotation;
            public Vector3 scale;
			
            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour
        private void OnEnable() {

            transform.hideFlags = HideFlags.HideInInspector;
            }
        private void Update() {
            
            position = transform.position;
            rotation = transform.eulerAngles;
            scale = transform.localScale;
            }
        private void OnDestroy() {
            
            transform.hideFlags = HideFlags.None;
            }
		
        //Funciones privadas.
		
        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
