using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRuinsRooms : GameBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
            private static int m_actualFloor = 0;

        //Establecer variables.
		
            //Publicas.
            [Header("Generate All Rooms")]
			[SerializeField] private int m_roomsCount = 10;       
			[SerializeField] private int m_chestsRooms = 2;       

            [Header("Debug")]
            [SerializeField] private GameObject m_testScene = null;

            [Header("Map")]
            [SerializeField] private MapController m_smallMapController = null;
            [SerializeField] private MapController m_bigMapController = null;

            //Privadas.
            private DataSystem m_dataSystem;

    //Funciones
		
        //Funciones de MonoBehaviour.
        private void Start() {

            m_dataSystem = DataSystem.GetSingleton();
            GenerateAllRooms();
            m_smallMapController.SetMap(DataSystem.GetSingleton().GetDungeonData());
            m_bigMapController.SetMap(DataSystem.GetSingleton().GetDungeonData());
            
            PlayerBrain.GetSingleton().GetAttack().SetWeapon(m_dataSystem.GetWeapon(m_dataSystem.GetDungeonData().GetActualWeapon().GetID()));
            }
		
        //Funciones publicas.
        public static void NextFloor() {
            
            RoomController.SetAppearDirection(Direction.Up);
            DataSystem.GetSingleton().GetDungeonData().NextFloor();
            SaveSystem.Save();

            SceneController.GetSingleton().LoadScene(Scenes.Ruins, false);
            }
        public static void ExitRuins(bool dead) {
            
            DataSystem m_dS = DataSystem.GetSingleton();
            FarmSpawnController.SetSpawn(dead ? FarmSpawnController.SpawnType.Altar : FarmSpawnController.SpawnType.Ruins);

            List<DeadPanelUI.LostItem> m_lostItems = new List<DeadPanelUI.LostItem>();

            InventoryData m_iD = DataSystem.GetSingleton().GetDungeonData().GetInventoryData();

            int m_losePercent = Random.Range(40, 61);

            foreach(WeaponEntityData m_ed in m_iD.GetWeaponList()) {
                
                if (dead) m_ed.SetUses(Mathf.RoundToInt(m_ed.GetUses() - (DataSystem.GetSingleton().GetWeapon(m_ed.GetID()).GetUses() * 0.35f)));
                m_lostItems.Add(new DeadPanelUI.LostItem((Item) m_dS.GetWeapon(m_ed.GetID()), false));
                }

            foreach(ItemData m_i in m_iD.GetPotionList()) {

                for(int i = 0; i < m_i.GetCount(); i ++) {
                    
                    m_lostItems.Add(new DeadPanelUI.LostItem(m_dS.GetPotion(m_i.GetID()), dead));
                    }
                if (dead) m_i.SetCount(0);
                }

            foreach(ItemData m_i in m_iD.GetPlantList()) {

                for(int i = 0; i < m_i.GetCount(); i ++) {
                    
                    m_lostItems.Add(new DeadPanelUI.LostItem(m_dS.GetPlant(m_i.GetID()), dead));
                    }
                if (dead) m_i.SetCount(0);
                }

            List<Seed> m_seeds = DataSystem.GetSingleton().GetSeeds();
            
            foreach(Seed m_s in m_seeds) {
                
                int m_totalCount = m_iD.GetSeedData(m_s.GetID()).GetCount();

                int m_lostCount = dead ? Mathf.RoundToInt(m_totalCount * (m_losePercent / 100f)) : 0;
                int m_finalCount = m_totalCount - m_lostCount;

                for(int i = 0; i < m_lostCount; i ++) m_lostItems.Add(new DeadPanelUI.LostItem(m_s, true));
                for(int i = 0; i < m_finalCount; i ++) m_lostItems.Add(new DeadPanelUI.LostItem(m_s, false));

                m_iD.AddSeed(m_s.GetID(), -m_lostCount);
                }

            DeadPanelUI.GetSingleton().SetData(m_lostItems, dead ? "¡Te has desmayado!" : "¡Has logrado salir con exito!");
            DeadPanelUI.GetSingleton().Open();
                
            RoomController.SetAppearDirection(Direction.Up);

            m_dS.GetGameData().GetInventoryData().AddDungeonInventory(m_dS.GetDungeonData().GetInventoryData());
            SaveSystem.Save();
            }
        public static int GetActualFloor() => m_actualFloor;

        //Funciones privadas.
        private void GenerateAllRooms() {

            if (m_dataSystem.GetDungeonData() == null) {
                
                DungeonData m_dD = new DungeonData();
                m_dD.GetPlayer().SetHealth(DataSystem.GetSingleton().GetPlayerHealth());
                
                InventoryData m_iD = DataSystem.GetSingleton().GetNewInventoryData(true);

                //Set best weapon to the default weapon.
                List<WeaponEntityData> m_weaponDatas = m_iD.GetWeaponList().OrderByDescending(c => c.GetUses()).ThenByDescending(c => DataSystem.GetSingleton().GetWeapon(c.GetID()).GetRarity()).ToList();
                int m_actualWeapon = m_weaponDatas[0].GetIndex();

                m_dD.SetInventoryData(m_iD);
                m_dD.SetActualWeapon(m_actualWeapon);

                m_dataSystem.SetDungeonData(m_dD);
                }
                
            m_actualFloor = m_dataSystem.GetDungeonData().GetFloor();

            List<RoomData> m_roomsDatas = m_dataSystem.GetDungeonData().GetRoomDatas();

            if (m_roomsDatas == null) {

                m_roomsDatas = new List<RoomData>();           
                int m_generatedRooms = 0;
                Vector2Int m_pos = new Vector2Int();

                List<Vector2Int> m_roomsPositions = new List<Vector2Int>();
                int m_maxRooms = (m_roomsCount + m_chestsRooms);

                while(m_generatedRooms <= m_maxRooms) {

                    if (!GetValueInList(m_pos, m_roomsPositions)) {

                        //Generar habitacion de escalera.
                        if (m_generatedRooms == m_maxRooms) {

                            int m_roomPrefabIndex = m_dataSystem.GetRandomStairsRoomPrefabIndex();
                            
                            m_roomsPositions.Add(m_pos);

                            List<RoomPropData> m_roomProps = new List<RoomPropData>();
                            List<GameObject> m_props = m_dataSystem.GetStairsRoomPrefab(m_roomPrefabIndex).GetComponent<RoomController>().GetProps();
                            
                            for(int i = 0; i < m_props.Count; i ++) {
                                
                                m_roomProps.Add(new RoomPropData(i, false));
                                }
                                
                            m_roomsDatas.Add(new RoomData(m_pos, m_roomPrefabIndex, RoomData.RoomType.Stairs, m_roomProps));
                            m_generatedRooms ++;
                            }
                        
                        else {
                                
                            bool m_generateChest = GetIfIsGenerateChest(m_generatedRooms);

                            //Generar habitacion de cofre.
                            if (m_generateChest) {

                                int m_roomPrefabIndex = m_dataSystem.GetRandomChestRoomPrefabIndex();
                                
                                m_roomsPositions.Add(m_pos);

                                List<RoomPropData> m_roomProps = new List<RoomPropData>();
                                List<GameObject> m_props = m_dataSystem.GetChestRoomPrefab(m_roomPrefabIndex).GetComponent<RoomController>().GetProps();

                                for(int i = 0; i < m_props.Count; i ++) {
                                    
                                    m_roomProps.Add(new RoomPropData(i, false));
                                    }

                                m_roomsDatas.Add(new RoomData(m_pos, m_roomPrefabIndex, RoomData.RoomType.Chest, m_roomProps));
                                m_generatedRooms++;
                                }
                            
                            //Generar habitacion normal.
                            else {

                                int m_roomPrefabIndex = m_dataSystem.GetRandomRoomPrefabIndex();
                                
                                m_roomsPositions.Add(m_pos);

                                List<RoomPropData> m_roomProps = new List<RoomPropData>();
                                List<GameObject> m_props = m_dataSystem.GetRoomPrefab(m_roomPrefabIndex).GetComponent<RoomController>().GetProps();

                                for(int i = 0; i < m_props.Count; i ++) {
                                    
                                    m_roomProps.Add(new RoomPropData(i, false));
                                    }

                                m_roomsDatas.Add(new RoomData(m_pos, m_roomPrefabIndex, RoomData.RoomType.Room, m_roomProps));
                                m_generatedRooms ++;
                                }
                            }
                        }

                    int m_directionRaw = Random.Range(0, 4);
                    Vector2Int m_direction = new Vector2Int();

                    switch(m_directionRaw) {

                        case 0 : m_direction = Vector2Int.left; break;
                        case 1 : m_direction = Vector2Int.right; break;
                        case 2 : m_direction = Vector2Int.up; break;
                        case 3 : m_direction = Vector2Int.down; break;
                        }
                        
                    m_pos = new Vector2Int(m_pos.x + m_direction.x, Mathf.Clamp(m_pos.y + m_direction.y, 0, 100));
                    }

                m_dataSystem.GetDungeonData().SetRoomDatas(m_roomsDatas);
                m_dataSystem.GetDungeonData().SetActualRoom(new Vector2Int(0, 0));
                }
                
            GenerateRoom(m_dataSystem.GetDungeonData().GetRoomData(m_dataSystem.GetDungeonData().GetActualRoom()));
            }
        private void GenerateRoom(RoomData roomData) {

            GameObject m_roomPrefab = null;

            if (roomData.GetRoomType() == RoomData.RoomType.Room) {

                if (m_testScene == null) m_roomPrefab = m_dataSystem.GetRoomPrefab(roomData.GetRoomPrefabIndex());
                else m_roomPrefab = m_testScene;
                }
            
            else if (roomData.GetRoomType() == RoomData.RoomType.Chest) {

                m_roomPrefab = m_dataSystem.GetChestRoomPrefab(roomData.GetRoomPrefabIndex());
                }
                
            else if (roomData.GetRoomType() == RoomData.RoomType.Stairs) {

                m_roomPrefab = m_dataSystem.GetStairsRoomPrefab(roomData.GetRoomPrefabIndex());
                }


            List<RoomData> m_rd = DataSystem.GetSingleton().GetDungeonData().GetRoomDatas();

            roomData.Unlock();
            roomData.Visit();
            
            RoomData m_r;

            m_r = m_rd.Find(c => c.GetRoomPosition() == roomData.GetRoomPosition() + Vector2Int.up);
            if (m_r != null) m_r.Unlock();

            m_r = m_rd.Find(c => c.GetRoomPosition() == roomData.GetRoomPosition() + Vector2Int.down);
            if (m_r != null) m_r.Unlock();

            m_r = m_rd.Find(c => c.GetRoomPosition() == roomData.GetRoomPosition() + Vector2Int.left);
            if (m_r != null) m_r.Unlock();

            m_r = m_rd.Find(c => c.GetRoomPosition() == roomData.GetRoomPosition() + Vector2Int.right);
            if (m_r != null) m_r.Unlock();

            RoomController m_roomController = Instantiate(m_roomPrefab, transform).GetComponent<RoomController>();
            m_roomController.SetData(roomData.GetRoomPosition());
            }		
        private bool GetIfIsGenerateChest(int actual) {

            int m_calc = Mathf.FloorToInt(((float) (m_roomsCount + m_chestsRooms) / m_chestsRooms));
            bool m_return = false;

            for(int i = m_calc - 1; i <= m_roomsCount + m_chestsRooms; i += m_calc) {
                
                if (i == actual) {
                    
                    m_return = true;
                    }
                }
            
            return m_return;
            }
        private bool GetValueInList(Vector2Int value, List<Vector2Int> list) {

            foreach(Vector2Int m_pos in list) {

                if (value == m_pos) return true;
                }
            
            return false;
            }

        //Funciones publicas.
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
