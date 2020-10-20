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
    public GameData GetGameData() {

        return m_gameData;
        }
    
    public List<RoomData> GetRoomsData() {

        return m_runRoomsData;
        }
    public void SetRoomsData(List<RoomData> runRoomsData) {

        m_runRoomsData = runRoomsData;
        }

    public Vector2Int GetActualRoom() {

        return m_actualRoom;
        }
    public void SetActualRoom(Vector2Int actualRoom) {
        
        m_actualRoom = actualRoom;
        }
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

    public GameData() { 
        
        
        }
    }

public class RoomData {
    
    [SerializeField] private Vector2Int m_roomPosition;
    [SerializeField] private List<Vector2Int> m_tilePositions;
    
    public RoomData(Vector2Int roomPosition, List<Vector2Int> tilePositions) {
        
        m_roomPosition = roomPosition;
        m_tilePositions = tilePositions;
        }
    }