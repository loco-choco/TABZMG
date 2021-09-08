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
        public Text LocalPlayerDamageScoreText;
        public Text LocalPlayerItemRankScoreText;
        private Vector3 distanceFromText = new Vector3(0f, -20f, 0f);

        private int deathScore = 0;

        public ArenaGamemode gamemodeData;
        public void Awake()
        {
            NetworkManagerEditing.OnPlayerSpawned += NetworkManagerEditing_OnPlayerSpawned;
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
            Objective.text = string.Format("CHANGES WEAPON AT {0} DAMAGE", gamemodeData.DamageNeededToChangeWeapons.ToString("F0"));
            Vector3 previousPosition = Objective.transform.localPosition;


            LocalPlayerDeathScoreText = CreateText(uiCanvas, playersInLobby);
            LocalPlayerDeathScoreText.transform.localPosition = previousPosition + distanceFromText;
            LocalPlayerDeathScoreText.text = string.Format("{0} DEATHS", deathScore);

            previousPosition = LocalPlayerDeathScoreText.transform.localPosition;

            LocalPlayerDamageScoreText = CreateText(uiCanvas, playersInLobby);
            LocalPlayerDamageScoreText.transform.localPosition = previousPosition + distanceFromText;
            LocalPlayerDamageScoreText.text = string.Format("{0} DAMAGE DEALT", gamemodeData.DamageDeltByLocalPlayer.ToString("F0"));

            previousPosition = LocalPlayerDamageScoreText.transform.localPosition;

            LocalPlayerItemRankScoreText = CreateText(uiCanvas, playersInLobby);
            LocalPlayerItemRankScoreText.transform.localPosition = previousPosition + distanceFromText;
            LocalPlayerItemRankScoreText.text = string.Format("ITEM RANK {0}", gamemodeData.ItemRank + 1);
        }
        public void Update()
        {
            if (LocalPlayerDeathScoreText != null)
                LocalPlayerDeathScoreText.text = string.Format("{0} DEATHS", deathScore);

            if (LocalPlayerDamageScoreText != null)
                LocalPlayerDamageScoreText.text = string.Format("{0} DAMAGE DEALT", gamemodeData.DamageDeltByLocalPlayer.ToString("F0"));

            if (LocalPlayerItemRankScoreText != null)
                LocalPlayerItemRankScoreText.text = string.Format("ITEM RANK {0}", gamemodeData.ItemRank + 1);
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
