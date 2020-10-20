using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSystem : MonoBehaviour {

    //Establecer variables.
    private static MasterData m_masterData;
    private static GameData m_gameData;
    private static DataSystem m_instance;

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
    }

[System.Serializable]
public class MasterData {

    private GameData[] m_gameDatas;

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