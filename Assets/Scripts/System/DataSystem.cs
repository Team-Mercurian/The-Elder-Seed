﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSystem : MonoBehaviour {

    //Establecer variables.
    private static MasterData m_masterData;
    private static GameData m_gameData;
    private static DataSystem m_instance;

    private static List<RoomData> m_runRoomsData;
    private static Vector2Int m_actualRoom;
        
    private static TemporalData m_temporalData;

        //Publicas.
        [Header("Persistent Room Holder")]
        [SerializeField] private GameObject[] m_rooms = null;
        [SerializeField] private GameObject[] m_enemies = null;

    //Funciones de MonoBehaviour.
    private void Awake() {

        if (m_instance == null) {

            m_instance = this;
            DontDestroyOnLoad(gameObject);

            #if UNITY_ANDROID && !UNITY_EDITOR
            Application.targetFrameRate = 60;
            #endif
            }

        else {

            Destroy(gameObject);
            return;
            }

        m_masterData = Load();
        m_gameData = m_masterData.GetGameData(0);
        }

    //Funciones estaticas.
    public static void Save() {

        string m_rawData = JsonUtility.ToJson(m_masterData, true);
        File.WriteAllText(Application.persistentDataPath + "/save.json", m_rawData);
        }

    private MasterData Load() {

        MasterData m_data = new MasterData();

        if (File.Exists(Application.persistentDataPath + "/save.json")) {

            string m_rawData = File.ReadAllText(Application.persistentDataPath + "/save.json");
            JsonUtility.FromJsonOverwrite(m_rawData, m_data);
            }

        return m_data;
        }

    public void SetRoomsData(List<RoomData> runRoomsData) {

        m_runRoomsData = runRoomsData;
        }

    public RoomData GetRoomData(Vector2Int roomPosition) {

        foreach(RoomData m_room in m_runRoomsData) {

            if (m_room.GetRoomPosition() == roomPosition) return m_room;
            }
        
        return null;
        }

    public static DataSystem GetSingleton() => m_instance;

    public GameData GetGameData() => m_gameData;    
    public List<RoomData> GetRoomsData() => m_runRoomsData;

    public Vector2Int GetActualRoom() => m_actualRoom;
    public void SetActualRoom(Vector2Int actualRoom) => m_actualRoom = actualRoom;

    public GameObject GetRoomPrefab(int index) => m_rooms[index];
    public int GetRandomRoomPrefabIndex() => Random.Range(0, m_rooms.Length);

    public GameObject GetEnemyPrefab(int index) => m_enemies[index];
    public int GetRandomEnemyPrefabIndex() => Random.Range(0, m_enemies.Length);

    public void SetTemporalData(TemporalData temporalData) => m_temporalData = temporalData;
    public TemporalData GetTemporalData() => m_temporalData;
    }

[System.Serializable]
public class MasterData {

    [SerializeField] private GameData[] m_gameDatas;

    public MasterData() {

        m_gameDatas = new GameData[4];
        m_gameDatas[0] = new GameData();
        }

    public GameData GetGameData(int file) {

        return m_gameDatas[file];
        }
    }

[System.Serializable]
public class GameData {

    [SerializeField] private int m_seedsCount;

    public GameData() { 
        
        m_seedsCount = 0;
        }

    public void AddSeeds(int value) => m_seedsCount += value;
    public int GetSeedCount() => m_seedsCount;
    }

public class RoomData {
    
    [SerializeField] private Vector2Int m_roomPosition;
    [SerializeField] private int m_roomPrefabIndex;
    
    public RoomData(Vector2Int roomPosition, int prefabIndex) {
        
        m_roomPosition = roomPosition;
        m_roomPrefabIndex = prefabIndex;
        }
    public Vector2Int GetRoomPosition() {

        return m_roomPosition;
        }
    public int GetRoomPrefabIndex() {

        return m_roomPrefabIndex;
        }
    }

public class TemporalData {

    [SerializeField] private PlayerData m_playerData; 

    public TemporalData(PlayerData playerData) {

        m_playerData = playerData;
        }   
        
    public void SetPlayer(PlayerData playerData) => m_playerData = playerData; 
    public PlayerData GetPlayer() => m_playerData;
    }

public class PlayerData {

    [SerializeField] private int m_playerHealth;

    public PlayerData(int playerHealth) {

        m_playerHealth = playerHealth;
        }   

    public void SetHealth(int health) => m_playerHealth = health; 
    public int GetHealth() => m_playerHealth;
    }

/* Deprecated generation.
public class RoomData {
    
    [SerializeField] private Vector2Int m_roomPosition;
    [SerializeField] private List<Vector2Int> m_tilePositions;

    [SerializeField] private Vector2Int? m_leftPassage = null;
    [SerializeField] private Vector2Int? m_rightPassage = null;
    [SerializeField] private Vector2Int? m_upPassage = null;
    [SerializeField] private Vector2Int? m_downPassage = null;
    
    public RoomData(Vector2Int roomPosition, List<Vector2Int> tilePositions) {
        
        m_roomPosition = roomPosition;
        m_tilePositions = tilePositions;
        }
    public Vector2Int GetRoomPosition() {

        return m_roomPosition;
        }
    public List<Vector2Int> GetTilePositions() {

        return m_tilePositions;
        }

    public void SetPassages(Vector2Int left, Vector2Int right, Vector2Int up, Vector2Int down) {

        m_leftPassage = new Vector2Int?(left);
        m_rightPassage = new Vector2Int?(right);;
        m_upPassage = new Vector2Int?(up);;
        m_downPassage = new Vector2Int?(down);;
        }
    
    public Vector2Int? GetPassagePosition(GameBehaviour.Direction direction) {

        switch(direction) {

            case GameBehaviour.Direction.Left : return m_leftPassage;
            case GameBehaviour.Direction.Right : return m_rightPassage;
            case GameBehaviour.Direction.Up : return m_upPassage;
            case GameBehaviour.Direction.Down : return m_downPassage;
            }
        
        return Vector2Int.zero;
        }
    }*/