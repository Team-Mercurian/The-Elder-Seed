using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonData {

    private PlayerData m_playerData; 
    private List<RoomData> m_rooms;
    private Vector2Int m_actualRoom;
    private int m_floor;
	private int m_actualWeaponIndex;

    private InventoryData m_dungeonInventory;

    public DungeonData() {

        m_rooms = null;
        m_actualRoom = Vector2Int.zero;
        m_floor = 0;
        m_actualWeaponIndex = -1;
        
        m_playerData = new PlayerData();
        m_dungeonInventory = new InventoryData();
        }   
        
    public PlayerData GetPlayer() => m_playerData;

    public RoomData GetRoomData(Vector2Int position) => m_rooms.Find(c => c.GetRoomPosition() == position);
    
    public void SetRoomDatas(List<RoomData> rooms) => m_rooms = rooms;
    public List<RoomData> GetRoomDatas() => m_rooms;

    public int GetFloor() => m_floor;

    public void SetActualRoom(Vector2Int position) => m_actualRoom = position;
    public Vector2Int GetActualRoom() => m_actualRoom;

    public WeaponEntityData GetActualWeapon() => m_dungeonInventory.SearchInWeaponInventory(m_actualWeaponIndex);
    public void UseWeapon() => m_dungeonInventory.SearchInWeaponInventory(m_actualWeaponIndex).UseWeapon();

    public void NextFloor() {

        m_floor ++;
        m_rooms = null;
        m_actualRoom = Vector2Int.zero;
        }

    public int GetActualWeaponIndex() => m_actualWeaponIndex;
    public void SetActualWeapon(int index) => m_actualWeaponIndex = index; 

    public void SetInventoryData(InventoryData inventory) => m_dungeonInventory = inventory;
    public InventoryData GetInventoryData() => m_dungeonInventory;
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

    public PlayerData() {

        m_playerHealth = 0;
        }
    
    public void SetHealth(int health) => m_playerHealth = health; 
    public int GetHealth() => m_playerHealth;
    }
