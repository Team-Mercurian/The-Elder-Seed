using System.Collections;
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
        [SerializeField] private GameObject[] m_chestRooms = null;
        [SerializeField] private GameObject[] m_enemies = null;
        [SerializeField] private Weapon[] m_weapons = null;
        [SerializeField] private Seed[] m_seedTypes = null;

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

        Debug.Log("Saved");
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

    public GameObject GetChestRoomPrefab(int index) => m_chestRooms[index];
    public int GetRandomChestRoomPrefabIndex() => Random.Range(0, m_rooms.Length);

    public GameObject GetEnemyPrefab(int index) => m_enemies[index];
    public int GetRandomEnemyPrefabIndex() => Random.Range(0, m_enemies.Length);

    public void SetTemporalData(TemporalData temporalData) => m_temporalData = temporalData;
    public TemporalData GetTemporalData() => m_temporalData;

    public void SetActualWeapon(int index) => m_gameData.SetActualWeapon(index);

    public Weapon GetActualWeapon() => m_weapons[GetActualWeaponData().GetIndex()];   
    public WeaponData GetActualWeaponData() => m_gameData.GetActualWeapon();   
    
    public Weapon GetWeapon(int index) => m_weapons[index];   

    public Seed GetSeed(int index) => m_seedTypes[index];   
    public Seed[] GetAllSeeds() => m_seedTypes;   

    public void AddGridData(GridData data) => m_gameData.AddGridData(data);
    public void RemoveGridData(Vector3Int position) {
        GridData m_d = null;

        foreach(GridData m_gd in m_gameData.GetGridDatas()) {

            if (position == m_gd.GetSeedPosition()) {

                m_d = m_gd;
                break;
                }
            }

        m_gameData.RemoveGridData(m_d);
        }    
    
    public void UseActualWeapon() => m_gameData.UseWeapon(0);

    public Weapon AddRandomWeapon() {
        
        int m_weaponIndex = Random.Range(0, m_weapons.Length);
        Weapon m_weapon = m_weapons[m_weaponIndex];

        m_gameData.AddWeapon(m_weaponIndex, m_weapon.GetUses());

        return m_weapon;
        }

    public bool GetIfGridIsUsed(Vector3Int position) {

        foreach(GridData m_gd in m_gameData.GetGridDatas()) {

            if (m_gd.GetSeedPosition() == position) return true;
            }

        return false;
        }    
    }

[System.Serializable]
public class MasterData {

    [SerializeField] private List<GameData> m_gameDatas;

    public MasterData() {

        m_gameDatas = new List<GameData>();
        m_gameDatas.Add(new GameData());
        }

    public GameData GetGameData(int file) {

        return m_gameDatas[file];
        }
    }

[System.Serializable]
public class GameData {

    [SerializeField] private int m_actualWeaponInventoryIndex;

    [SerializeField] private List<WeaponData> m_inventoryWeapons;
    [SerializeField] private List<GridData> m_gridDatas;

    [SerializeField] private List<int> m_harvestedSeeds;
    [SerializeField] private List<int> m_seedsInInventory;

    public GameData() { 
        
        m_seedsInInventory = new List<int>();

        for(int i = 0; i < 5; i ++) m_seedsInInventory.Add(0);
        for(int i = 0; i < 5; i ++) m_seedsInInventory.Add(1);

        m_inventoryWeapons = new List<WeaponData>();
        m_inventoryWeapons.Add(new WeaponData(0, DataSystem.GetSingleton().GetWeapon(0).GetUses()));

        m_gridDatas = new List<GridData>();

        m_harvestedSeeds = new List<int>();

        m_actualWeaponInventoryIndex = 0;
        }

    public void AddSeeds(int index) => m_seedsInInventory.Add(index);
    public int GetSeedCount(int index) {

        int m_count = 0;

        foreach(int m_i in m_seedsInInventory) {

            if (m_i == index) m_count ++;
            }

        return m_count;
        }

    public void SetActualWeapon(int index) => m_actualWeaponInventoryIndex = index;
    public WeaponData GetActualWeapon() => SearchInWeaponInventory(m_actualWeaponInventoryIndex);

    public void UseWeapon(int index) => m_inventoryWeapons[index].UseWeapon();

    public void AddWeapon(int index, int uses) => m_inventoryWeapons.Add(new WeaponData(index, uses));

    public void AddGridData(GridData data) => m_gridDatas.Add(data);
    public void RemoveGridData(GridData data) => m_gridDatas.Remove(data);

    public List<GridData> GetGridDatas() => m_gridDatas;

    public void AddHarvestedSeed(int index) => m_harvestedSeeds.Add(index);

    public WeaponData SearchInWeaponInventory(int index) {

        foreach(WeaponData m_w in m_inventoryWeapons) {

            if (m_w.GetIndex() == index) return m_w;
            }

        return new WeaponData(-1, 0);
        }
    }

[System.Serializable]
public class WeaponData {
    
    [SerializeField] private int m_weaponIndex;
    [SerializeField] private int m_uses;

    public WeaponData() {

        m_weaponIndex = -1;
        m_uses = 0;
        }
    public WeaponData(int weaponIndex, int uses) {

        m_weaponIndex = weaponIndex;
        m_uses = uses;
        }

    public int GetIndex() => m_weaponIndex;
    public void UseWeapon() => m_uses --;
    public void SetUses(int count) => m_uses = count;
    public int GetUses() => m_uses;
    }

[System.Serializable] 
public class GridData {

    [SerializeField] private int m_seedIndex;
    [SerializeField] private Vector3Int m_position;
    [SerializeField] private System.TimeSpan m_plantStartTime;

    public GridData(int seedIndex, Vector3Int position, System.TimeSpan plantStart) {

        m_seedIndex = seedIndex;
        m_position = position;

        m_plantStartTime = plantStart;
        }   

    public int GetSeedIndex() => m_seedIndex;
    public Vector3Int GetSeedPosition() => m_position;
    }

public class RoomData {
    
    public enum RoomType {

        Room,
        Chest,
        }

    [SerializeField] private Vector2Int m_roomPosition;
    [SerializeField] private int m_roomPrefabIndex;
    [SerializeField] private RoomType m_roomType;
    
    public RoomData(Vector2Int roomPosition, int prefabIndex, RoomType roomType) {
        
        m_roomPosition = roomPosition;
        m_roomPrefabIndex = prefabIndex;
        m_roomType = roomType;
        }
    public Vector2Int GetRoomPosition() {

        return m_roomPosition;
        }
    public int GetRoomPrefabIndex() {

        return m_roomPrefabIndex;
        }
    public RoomType GetRoomType() {

        return m_roomType;
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