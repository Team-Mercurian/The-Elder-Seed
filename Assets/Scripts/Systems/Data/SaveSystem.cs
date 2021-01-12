using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
        
        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public static void Save() {

			string m_rawData = JsonUtility.ToJson(DataSystem.GetMasterData(), true);
			File.WriteAllText(Application.persistentDataPath + "/save.json", m_rawData);

			Debug.Log("Saved");
			}

		//Funciones privadas.
		public static MasterData Load() {

			MasterData m_data = new MasterData();

			if (File.Exists(Application.persistentDataPath + "/save.json")) {

				string m_rawData = File.ReadAllText(Application.persistentDataPath + "/save.json");
				JsonUtility.FromJsonOverwrite(m_rawData, m_data);
				}

			return m_data;
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
