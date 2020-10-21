using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestPassageController : GameBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
			
            //Privadas.
			private Vector2Int m_positionToMove;
            private Direction m_direction;
			
    //Funciones
		
        //Funciones de MonoBehaviour
		
        //Funciones privadas.
		
        //Funciones publicas.
        public void SetData(Vector2Int positionToMove, Direction direction) {
            
            m_positionToMove = positionToMove;
            m_direction = direction;
            }
        public Vector2Int GetPositionToMove() {

            return m_positionToMove;
            }
        public Direction GetDirectionToMove() {

            return m_direction;
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
