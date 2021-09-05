using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TABZMGamemodes.Arena
{
    public class ArenaScoreBoardUI : MonoBehaviour
    {
        public Text Objective;
        public List<PlayerScoreUI> Scores = new List<PlayerScoreUI>();
        public ArenaScoreBoard arenaScoreBoard;
        private Vector3 distanceFromText = new Vector3(0f, -20f, 0f);
        public void Awake()
        {
            NetworkManagerEditing.OnPlayerSpawned += NetworkManagerEditing_OnPlayerSpawned;
        }
        public void Start()
        {
            arenaScoreBoard = GetComponent<ArenaScoreBoard>();
        }
            public void OnDestroy()
        {
            NetworkManagerEditing.OnPlayerSpawned -= NetworkManagerEditing_OnPlayerSpawned;
        }
        private void NetworkManagerEditing_OnPlayerSpawned(PhotonView playerView)
        {
            StartCoroutine("CreateUI");
        }
        public IEnumerable CreateUI()
        {
            yield return new WaitForSeconds(Time.deltaTime * 4f);
            Transform uiCanvas = NetworkManager.LocalPlayerPhotonView.transform.Find("UI_Canvas");
            Transform playersInLobby = uiCanvas.Find("PlayersInLobby");

            Objective = CreateText(uiCanvas, playersInLobby);
            Objective.transform.localPosition -= distanceFromText;
            Objective.text = "Wins at " + arenaScoreBoard.ObjectiveScore;

            for (int i =0; i< arenaScoreBoard.ScoreBoard.Count;i++)
            {
                PlayerPoints p = arenaScoreBoard.ScoreBoard[i];
                Text playerScore = CreateText(uiCanvas, playersInLobby);
                playerScore.transform.localPosition -= distanceFromText * (i + 2);
                playerScore.text = string.Format("{0} - {1}", p.Player, p.Points);
                Scores.Add(new PlayerScoreUI(playerScore, p.Player));
            }
        }
        public void UpdatePlayerScore(int player, int score)
        {
            var playerPoints = Scores.Find(s => s.Player == player);
            if(playerPoints != null)
                playerPoints.Score.text = string.Format("{0} - {1}", player, score);
            else
            {
                Transform uiCanvas = NetworkManager.LocalPlayerPhotonView.transform.Find("UI_Canvas");
                Transform playersInLobby = uiCanvas.Find("PlayersInLobby");

                Text playerScore = CreateText(uiCanvas, playersInLobby);
                playerScore.transform.localPosition -= distanceFromText * (Scores.Count + 1);

                playerScore.text = string.Format("{0} - {1}", player, score);
                Scores.Add(new PlayerScoreUI(playerScore, player));
            }
        }
        public void RemovePlayerScore(int player)
        {
            var playerPoints = Scores.Find(s => s.Player == player);
            if (playerPoints == null)
                return;

            Destroy(playerPoints.Score);
            Scores.Remove(playerPoints);
        }
        public Text CreateText(Transform parent, Transform reference)
        {
            Text text = Instantiate(reference).GetComponent<Text>();
            text.transform.SetParent(parent);
            text.transform.position = reference.position;
            text.transform.rotation = reference.rotation;
            text.transform.localScale = reference.localScale;
            text.color = Color.white;
            return text;
        }
    }
    public class PlayerScoreUI
    {
        public Text Score;
        public int Player;

        public PlayerScoreUI(Text Score, int Player)
        {
            this.Score = Score;
            this.Player = Player;
        }
    }
}
