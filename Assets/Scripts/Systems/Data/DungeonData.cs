using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonData {

    private PlayerData m_playerData; 
    private List<RoomData> m_rooms;
    private Vector2Int m_actualRoom;

    private int m_floor;

    private List<int> m_recolectedSeedsIndex;

    public DungeonData(PlayerData playerData) {

        m_playerData = playerData;
        m_rooms = null;
        m_actualRoom = Vector2Int.zero;

        m_recolectedSeedsIndex = new List<int>();

        m_floor = 0;
        }   
        
    public PlayerData GetPlayer() => m_playerData;
    public RoomData GetRoomData(int index) => m_rooms[index];
    public RoomData GetRoomData(Vector2Int position) {

        foreach(RoomData m_d in m_rooms) {

            if (m_d.GetRoomPosition() == position) return m_d;
            }

        return null;
        }    

    public List<RoomData> GetRoomsDatas() => m_rooms;

    public void SetRoomDatas(List<RoomData> rooms) => m_rooms = rooms;
    public int GetFloor() => m_floor;

    public void SetActualRoom(Vector2Int position) => m_actualRoom = position;
    public Vector2Int GetActualRoom() => m_actualRoom;

    public void NextFloor() {

        m_floor ++;
        m_rooms = null;
        m_actualRoom = Vector2Int.zero;
        }

    public void AddSeed(int index) => m_recolectedSeedsIndex.Add(index);
    public void LoseInventoryPart(int percent) {

        Debug.Log("Inventario antes de morir: " + m_recolectedSeedsIndex.Count);

        List<int> m_newSeeds = m_recolectedSeedsIndex;
        int m_actualPercentIndex = 0;
        int m_maxPercentIndex = Mathf.FloorToInt(m_recolectedSeedsIndex.Count * (percent/100f));

        while(m_actualPercentIndex < m_maxPercentIndex) {

            m_newSeeds.RemoveAt(Random.Range(0, m_newSeeds.Count));
            m_actualPercentIndex ++;
            }

        Debug.Log("Inventario despues de morir: " + m_newSeeds.Count);
        }
    }
public class RoomData {
    
    public enum RoomType {

        Room,
        Chest,
        Stairs,
        }

    private Vector2Int m_roomPosition;
    private int m_roomPrefabIndex;
    private RoomType m_roomType;

    private List<RoomPropData> m_roomProps;
    
    public RoomData(Vector2Int roomPosition, int prefabIndex, RoomType roomType, List<RoomPropData> roomProps) {
        
        m_roomPosition = roomPosition;
        m_roomPrefabIndex = prefabIndex;
        m_roomType = roomType;
        m_roomProps = roomProps;
        }

    public Vector2Int GetRoomPosition() => m_roomPosition;
    public int GetRoomPrefabIndex() => m_roomPrefabIndex;
    public RoomType GetRoomType() => m_roomType;
    public List<RoomPropData> GetPropDatas() => m_roomProps;
    public void DestroyProp(int index) {
        
        m_roomProps[index].SetIfIsDestroyed(true);
        }
    }
public class RoomPropData {

    private int m_index;
    private bool m_destroyed;

    public RoomPropData(int index, bool destroyed) {

        m_index = index;
        m_destroyed = destroyed;
        }

    public int GetIndex() => m_index;
    public bool GetIfIsDestroyed() => m_destroyed;

    public void SetIfIsDestroyed(bool destroyed) => m_destroyed = destroyed; 
    }

public class PlayerData {

    private int m_playerHealth;

    public PlayerData(int playerHealth) {

        m_playerHealth = playerHealth;
        }   

    public void SetHealth(int health) => m_playerHealth = health; 
    public int GetHealth() => m_playerHealth;
    }
