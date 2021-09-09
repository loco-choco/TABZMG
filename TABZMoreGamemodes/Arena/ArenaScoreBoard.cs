//using System.Collections.Generic;
//using System.Linq;
//using Photon;
//using UnityEngine;

//namespace TABZMGamemodes.Arena
//{
//    public class ArenaScoreBoard : PunBehaviour
//    {
//        public Dictionary<int, PlayerPoints> ScoreBoard = new Dictionary<int, PlayerPoints>();
//        int LocalPlayerID;
//        public void Start()
//        {
//            LocalPlayerID = PhotonNetwork.player.ID;
//        }
//        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
//        {
//            base.OnPhotonPlayerConnected(newPlayer);
//            if (PhotonNetwork.isMasterClient)
//            {
//                ScoreBoard.Add(newPlayer.ID, new PlayerPoints(newPlayer.ID, 0));
//            }
//        }
//        public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
//        {
//            base.OnPhotonPlayerDisconnected(otherPlayer);
//            if (PhotonNetwork.isMasterClient)
//            {
//                ScoreBoard.Remove(otherPlayer.ID);
//            }
//        }
//        public PlayerPoints GetLocalPlayerScore()
//        {
//            ScoreBoard.TryGetValue(LocalPlayerID, out PlayerPoints playerPoints);
//            return playerPoints;
//        }
//        [PunRPC]
//        public void AddPoints(int owner, int points)
//        {
//            PlayerPoints playerPoints = ScoreBoard.Find(s => s.Player == owner);
//            if (playerPoints != null)
//            {
//                playerPoints.Points += points;
//            }
//            else
//            {
//                playerPoints = new PlayerPoints(owner, points);
//                ScoreBoard.Add(playerPoints);
//            }
//        }
        
//        public delegate void OnWin(int winner);
//        public event OnWin OnWinner;
//    }
//    public class PlayerPoints
//    {
//        public int Player;
//        public int Points;
//        public PlayerPoints(int Player, int InitialPoints)
//        {
//            this.Player = Player;
//            Points = InitialPoints;
//        }
//    }
//}
