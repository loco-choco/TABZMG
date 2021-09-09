using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TABZMGamemodes.Arena
{
    public class ArenaScoreBoardUI : MonoBehaviour
    {
        public Text Objective;
        public Text LocalPlayerDeathScoreText;
        public Text LocalPlayerKillScoreText;
        public Text LocalPlayerItemRankScoreText;
        private Vector3 distanceFromText = new Vector3(0f, -20f, 0f);

        private int deathScore = 0;

        public ArenaGamemode gamemodeData;
        public void Awake()
        {
            NetworkManagerEditing.OnPlayerSpawned += NetworkManagerEditing_OnPlayerSpawned;
            StartCoroutine("UpdatePerSecond");
        }
        public void OnDestroy()
        {
            NetworkManagerEditing.OnPlayerSpawned -= NetworkManagerEditing_OnPlayerSpawned;
        }
        private bool ignoreFirstPoint = true;
        private void NetworkManagerEditing_OnPlayerSpawned(PhotonView playerView)
        {
            Debug.Log("Criando UI");
            StartCoroutine("CreateUI");
            deathScore++;
            if (ignoreFirstPoint)
                deathScore--;

            ignoreFirstPoint = false;
        }
        public IEnumerator CreateUI()
        {
            yield return new WaitForSeconds(Time.deltaTime * 4f);
            Transform uiCanvas = NetworkManager.LocalPlayerPhotonView.transform.Find("UI_Canvas");
            Transform playersInLobby = uiCanvas.Find("PlayersInLobby");

            Objective = CreateText(uiCanvas, playersInLobby);
            Objective.transform.localPosition += distanceFromText;
            Objective.text = string.Format("CHANGES WEAPON AT EVERY {0} KILLS", gamemodeData.KillsNeededToChangeWeapons);
            Vector3 previousPosition = Objective.transform.localPosition;


            LocalPlayerDeathScoreText = CreateText(uiCanvas, playersInLobby);
            LocalPlayerDeathScoreText.transform.localPosition = previousPosition + distanceFromText;
            LocalPlayerDeathScoreText.text = string.Format("{0} DEATHS", deathScore);

            previousPosition = LocalPlayerDeathScoreText.transform.localPosition;

            LocalPlayerKillScoreText = CreateText(uiCanvas, playersInLobby);
            LocalPlayerKillScoreText.transform.localPosition = previousPosition + distanceFromText;
            LocalPlayerKillScoreText.text = string.Format("{0} KILLS", gamemodeData.KillsMadeByLocalPlayer);

            previousPosition = LocalPlayerKillScoreText.transform.localPosition;

            LocalPlayerItemRankScoreText = CreateText(uiCanvas, playersInLobby);
            LocalPlayerItemRankScoreText.transform.localPosition = previousPosition + distanceFromText;
            LocalPlayerItemRankScoreText.text = string.Format("ITEM RANK {0}", gamemodeData.ItemRank + 1);
        }
        public IEnumerator UpdatePerSecond()
        {
            if (Objective != null)
                Objective.text = string.Format("CHANGES WEAPON AT EVERY {0} KILLS", gamemodeData.KillsNeededToChangeWeapons);

            if (LocalPlayerDeathScoreText != null)
                LocalPlayerDeathScoreText.text = string.Format("{0} DEATHS", deathScore);

            if (LocalPlayerKillScoreText != null)
                LocalPlayerKillScoreText.text = string.Format("{0} KILLS", gamemodeData.KillsMadeByLocalPlayer);

            if (LocalPlayerItemRankScoreText != null)
            {
                LocalPlayerItemRankScoreText.text = string.Format("ITEM RANK {0}", gamemodeData.ItemRank + 1);
                if (gamemodeData.ItemRankCompletitions > 0)
                    LocalPlayerItemRankScoreText.text += string.Format(" x {0}", gamemodeData.ItemRankCompletitions);
            }

            yield return new WaitForSeconds(1f);
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
}
