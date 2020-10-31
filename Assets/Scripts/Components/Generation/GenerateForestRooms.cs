using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateForestRooms : GameBehaviour {
	
    //Establecer variables.
		
        //Establecer estructuras.
		
        //Establecer enumeradores.
		
        //Establecer variables estaticas.
		
            //Publicas.
			
            //Privadas
            private static Direction m_appearDirection = Direction.Up;
            
        //Establecer variables.
		
            //Publicas.
            [Header("Generate All Rooms")]
			[SerializeField] private int m_roomsCount = 10;                                             //

            [Header("Generate Tiles")]
			[SerializeField] private int m_generationCount = 100;
            [SerializeField] private int m_tileSize = 4;

            [Header("Props")]
            [SerializeField] private GameObject m_floorProp = null;
            [SerializeField] private GameObject m_wallProp = null;

            [Header("Passages")] 
            [SerializeField] private GameObject m_passagePropOpen = null;
            [SerializeField] private GameObject m_passagePropClose = null;
            [SerializeField] private int m_passageLength = 4;

            //Privadas.
            private DataSystem m_dataSystem;
			
            private ForestPassageController m_passageLeft;
            private ForestPassageController m_passageRight;
            private ForestPassageController m_passageUp;
            private ForestPassageController m_passageDown;

            private PlayerMovement m_player;

    //Funciones
		
        //Funciones de MonoBehaviour.
        private void Start() {

            m_player = PlayerMovement.GetSingleton();
            m_dataSystem = DataSystem.GetSingleton();
            GenerateAllRooms();
            CameraBrain.GetSingleton().SetTransformTargetPositions();
            }
		
        //Funciones privadas.
        private void GenerateAllRooms() {

            List<RoomData> m_roomsDatas = m_dataSystem.GetRoomsData();

            if (m_roomsDatas == null) {

                m_roomsDatas = new List<RoomData>();           
                int m_generatedRooms = 0;
                Vector2Int m_pos = new Vector2Int();

                List<Vector2Int> m_roomsPositions = new List<Vector2Int>();

                while(m_generatedRooms < m_roomsCount) {

                    if (!GetValueInList(m_pos, m_roomsPositions)) {

                        List<Vector2Int> m_roomTiles = GenerateRoom();

                        m_roomsPositions.Add(m_pos);
                        m_roomsDatas.Add(new RoomData(m_pos, m_roomTiles));
                        m_generatedRooms ++;
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

                foreach(RoomData m_rd in m_roomsDatas) {
                    
                    Vector2Int m_leftPassage = Vector2Int.zero;
                    Vector2Int m_rightPassage = Vector2Int.zero;
                    Vector2Int m_upPassage = Vector2Int.zero;
                    Vector2Int m_downPassage = Vector2Int.zero;

                    List<Vector2Int> m_rooms = GetAllRoomsPositions(m_roomsDatas);

                    bool m_generateLeftPassage = GetValueInList(m_rd.GetRoomPosition() + Vector2Int.left, m_rooms);
                    bool m_generateRightPassage = GetValueInList(m_rd.GetRoomPosition() + Vector2Int.right, m_rooms);
                    bool m_generateUpPassage = GetValueInList(m_rd.GetRoomPosition() + Vector2Int.up, m_rooms);
                    bool m_generateDownPassage = m_rd.GetRoomPosition() == Vector2Int.zero ? true : GetValueInList(m_rd.GetRoomPosition() + Vector2Int.down, m_rooms);

                    foreach(Vector2Int m_tile in m_rd.GetTilePositions()) {
                        
                        if (m_generateLeftPassage && m_tile.x <= m_leftPassage.x) m_leftPassage = m_tile; 
                        if (m_generateRightPassage && m_tile.x >= m_rightPassage.x) m_rightPassage = m_tile; 
                        if (m_generateUpPassage && m_tile.y >= m_upPassage.y) m_upPassage = m_tile; 
                        if (m_generateDownPassage && m_tile.y <= m_downPassage.y) m_downPassage = m_tile; 
                        }

                    Vector2Int m_lPP = Vector2Int.zero;
                    Vector2Int m_rPP = Vector2Int.zero;
                    Vector2Int m_uPP = Vector2Int.zero;
                    Vector2Int m_dPP = Vector2Int.zero;

                    if (m_generateLeftPassage) m_lPP = GeneratePassage(m_leftPassage, Vector2Int.left, m_rd.GetTilePositions());
                    if (m_generateRightPassage) m_rPP = GeneratePassage(m_rightPassage, Vector2Int.right, m_rd.GetTilePositions());
                    if (m_generateUpPassage) m_uPP = GeneratePassage(m_upPassage, Vector2Int.up, m_rd.GetTilePositions());
                    if (m_generateDownPassage) m_dPP = GeneratePassage(m_downPassage, Vector2Int.down, m_rd.GetTilePositions());

                    m_rd.SetPassages(m_lPP, m_rPP, m_uPP, m_dPP);
                    }

                m_dataSystem.SetRoomsData(m_roomsDatas);
                m_dataSystem.SetActualRoom(new Vector2Int(0, 0));
                }
                
            GenerateProps(m_dataSystem.GetRoomData(m_dataSystem.GetActualRoom()));

            Vector3 m_playerPos = new Vector3();
            float m_playerRot = 0;

            switch(m_appearDirection) {

                case Direction.Left : 
                    m_playerPos = m_passageRight.transform.position + new Vector3(-1 * m_tileSize, 0, 0); 
                    m_playerRot = 270;
                    break;

                case Direction.Right : 
                    m_playerPos = m_passageLeft.transform.position + new Vector3(1 * m_tileSize, 0, 0);
                    m_playerRot = 90;
                    break;

                case Direction.Up : 
                    m_playerPos = m_passageDown.transform.position + new Vector3(0, 0, 1 * m_tileSize);
                    m_playerRot = 0;
                    break;

                case Direction.Down : 
                    m_playerPos = m_passageUp.transform.position + new Vector3(0, 0, -1 * m_tileSize);
                    m_playerRot = 180;
                    break;
                }

            m_player.transform.position = m_playerPos;
            m_player.transform.rotation = Quaternion.Euler(0, m_playerRot, 0);
            }
        private List<Vector2Int> GenerateRoom() {
            
            List<Vector2Int> m_positions = new List<Vector2Int>();
            int m_generatedTiles = 0;

            Vector2Int m_generatorPosition = new Vector2Int();

            while(m_generatedTiles < m_generationCount) {
                
                for(int x = 0; x < 2; x ++) {

                    for(int y = 0; y < 2; y ++) {

                        Vector2Int m_pos = m_generatorPosition + new Vector2Int(x, y);

                        if (!GetValueInList(m_pos, m_positions)) {

                            m_positions.Add(m_pos);
                            m_generatedTiles ++;
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
                
                m_generatorPosition += m_direction;
                }             

            return m_positions;
            }		
		
        private void GenerateProps(RoomData roomData) {
            
            List<Vector2Int> m_positions = roomData.GetTilePositions();
            List<Vector2Int> m_walls = new List<Vector2Int>();

            foreach(Vector2Int m_pos in m_positions) {

                InstantiateScaledProp(new Vector3(m_pos.x, 0, m_pos.y), m_floorProp);    

                if (!GetValueInList(m_pos + Vector2Int.down, m_positions)) CreateWallProp(m_pos + Vector2Int.down, m_walls);
                if (!GetValueInList(m_pos + Vector2Int.left, m_positions)) CreateWallProp(m_pos + Vector2Int.left, m_walls);
                if (!GetValueInList(m_pos + Vector2Int.right, m_positions)) CreateWallProp(m_pos + Vector2Int.right, m_walls);
                if (!GetValueInList(m_pos + Vector2Int.up, m_positions)) CreateWallProp(m_pos + Vector2Int.up, m_walls);
                }

            m_passageLeft = CreatePassageProp(roomData, Direction.Left, true);
            m_passageRight = CreatePassageProp(roomData, Direction.Right, true);
            m_passageUp = CreatePassageProp(roomData, Direction.Up, true);
            m_passageDown = CreatePassageProp(roomData, Direction.Down, roomData.GetRoomPosition() == new Vector2Int(0, 0) ? false : true);
            }
        private GameObject InstantiateScaledProp(Vector3 position, GameObject prop) {
            
            return Instantiate(prop, position * m_tileSize, Quaternion.identity, transform);
            }
        private GameObject InstantiateScaledProp(Vector3 position, GameObject prop, Vector3 rotation) {
            
            return Instantiate(prop, position * m_tileSize, Quaternion.Euler(rotation), transform);
            }

        private List<Vector2Int> GetAllRoomsPositions(List<RoomData> rooms) {

            List<Vector2Int> m_list = new List<Vector2Int>();

            foreach(RoomData m_rd in rooms) {
                
                m_list.Add(m_rd.GetRoomPosition());
                }
                
            return m_list;
            }

        private void CreateWallProp(Vector2Int position, List<Vector2Int> walls) {
            
            if (GetValueInList(position, walls)) return;

            InstantiateScaledProp(new Vector3(position.x, 0, position.y), m_wallProp);
            walls.Add(position);
            }
        private ForestPassageController CreatePassageProp(RoomData roomData, Direction direction, bool open) {

            Vector2Int? m_position = roomData.GetPassagePosition(direction);
            if (m_position == Vector2Int.zero) return null;

            GameObject m_prop = open ? m_passagePropOpen : m_passagePropClose;

            float m_angle = 0;

            switch(direction) {

                case Direction.Left : m_angle = 0; break;
                case Direction.Right : m_angle = 180; break;
                case Direction.Up : m_angle = 90; break;
                case Direction.Down : m_angle = 270; break;
                }

            GameObject m_go = InstantiateScaledProp(new Vector3(m_position.Value.x, 0, m_position.Value.y), m_prop, new Vector3(0, m_angle, 0));
            ForestPassageController m_passage = m_go.GetComponent<ForestPassageController>();
            m_passage.SetData(roomData.GetRoomPosition() + GetDirection(direction), direction);
            return m_passage;
            }
        
        private Vector2Int GeneratePassage(Vector2Int position, Vector2Int direction, List<Vector2Int> floorPositions) {
            
            Vector2Int m_propPosition = Vector2Int.zero;

            for(int i = 0; i < m_passageLength; i ++) {

                Vector2Int m_pos = position + (direction * i);

                if (i == 1) m_propPosition = m_pos;

                if (!GetValueInList(m_pos, floorPositions)) {

                    floorPositions.Add(m_pos);
                    }
                }

            return m_propPosition;
            }

        //Funciones publicas.
        public int GetGenerationCount() {

            return m_generationCount;
            }
        public bool GetValueInList(Vector2Int value, List<Vector2Int> list) {

            foreach(Vector2Int m_pos in list) {

                if (value == m_pos) return true;
                }
            
            return false;
            }
        public static Direction GetAppearDirection() {

            return m_appearDirection;
            }
        public static void SetAppearDirection(Direction direction) {

            m_appearDirection = direction;
            }
		
        //Funciones heredadas.
		
        //Funciones ha heredar.
		
        //Corotinas.
		
        }
