//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;

//namespace TABZMGamemodes.Arena.Lobby
//{
//    public class ArenaLobby : MonoBehaviour
//    {
//        public ArenaMapSelector arenaMapSelector;
//        public ArenaGamemode gamemodeData;
//        public void Start()
//        {
//            CreateLobby();
//        }
//        public void CreateLobby()
//        {
//            GameObject Lobby = new GameObject("Lobby");
//            Lobby.transform.position = LobbyCreateData.LobbyPosition;
//            //Lobby voting panel
//        }
//        public void GoToLobby()
//        {
//            SpawnPointManagerEditing.SetSpawnPoints(LobbyCreateData.SpawnPoints);
//            HealthHandlerEditing.KillLocalPlayer();
//        }
//    }
//}
