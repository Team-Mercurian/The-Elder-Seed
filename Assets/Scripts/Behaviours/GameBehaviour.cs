using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBehaviour : MonoBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
        public enum Axis {

            X,
            Y,
            Z
            }
        public enum Direction {

            Left,
            Right,
            Up,
            Down,
            }
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
			
        //Establecer variables.
		
            //Publicas.
			
            //Privadas.
			
			
    //Funciones
		
        //Funciones de MonoBehaviour.

        //Funciones privadas.
		
        //Funciones publicas.
        public Vector2Int GetDirection(Direction direction) {
            
            Vector2Int m_pos = new Vector2Int();

            switch(direction) {
                
                case Direction.Left : m_pos = Vector2Int.left; break;
                case Direction.Right : m_pos = Vector2Int.right; break;
                case Direction.Down : m_pos = Vector2Int.down; break;
                case Direction.Up : m_pos = Vector2Int.up; break;
                }
            
            return m_pos;
            }
        public Direction GetDirection(Vector2Int direction) {
            
            if (direction == Vector2Int.up) return Direction.Up;   
            else if (direction == Vector2Int.down) return Direction.Down;  
            else if (direction == Vector2Int.left) return Direction.Left;  
            else return Direction.Right;  
            }
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
public abstract class GravityValues : GameBehaviour{

    //Establecer variables.
    private readonly float m_upGravity = 15;
    private readonly float m_downGravity = 35;

    //Establecer funciones.
    protected float GetGravityUpIntensity(float multiplier) {

        return m_upGravity * multiplier;
        }
    protected float GetGravityDownIntensity(float multiplier) {

        return m_downGravity * multiplier;
        }
    }
