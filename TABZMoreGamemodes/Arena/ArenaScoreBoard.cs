//using System.Collections.Generic;
//using UnityEngine;

//namespace TABZMGamemodes.Arena
//{
//    public class ArenaScoreBoard : MonoBehaviour
//    {
//        public List<PlayerPoints> ScoreBoard = new List<PlayerPoints>();
//        public int ObjectiveScore { get; private set; }
//        public ArenaScoreBoardUI arenaUI;

//        public void Start()
//        {
//            arenaUI = GetComponent<ArenaScoreBoardUI>();
//            ObjectiveScore = 10;
//        }
//        public void OnDestroy()
//        {
//            gameObject.GetPhotonView().RPC("PlayerLeft", PhotonTargets.Others, gameObject.GetPhotonView().ownerId);
//        }
//        public PlayerPoints GetLocalPlayerScore()
//        {
//            PlayerPoints playerPoints = ScoreBoard.Find(s => s.Player == NetworkManager.LocalPlayerPhotonView.ownerId);
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
//            arenaUI.UpdatePlayerScore(owner, points);

//            if (playerPoints.Points >= ObjectiveScore)
//                OnWinner?.Invoke(owner);
//        }
        
//        [PunRPC]
//        public void PlayerLeft(int owner)
//        {
//            PlayerPoints playerPoints = ScoreBoard.Find(s => s.Player == owner);
//            if (playerPoints != null)
//            {
//                ScoreBoard.Remove(playerPoints);
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
