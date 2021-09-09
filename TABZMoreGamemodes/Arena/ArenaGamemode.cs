using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TABZMGamemodes.Arena
{
    public class ArenaGamemode : MonoBehaviour
    {
        private Vector3 ArenaPosition = ArenaGamemodeData.ArenaPositionTwo;
        private Vector3 ArenaScale = ArenaGamemodeData.ArenaScaleTwo;
        private Vector3[] SpawnPointSet = ArenaGamemodeData.SpawnPointSetTwo;
        private ArenaKit[] KitSet = ArenaGamemodeData.KitSetOne;

        private static readonly float RespawnTime = 0.5f;
        public static void GamemodeInnit()
        {
            ArenaGamemode arenaGamemode = new GameObject("ArenaHandler").AddComponent<ArenaGamemode>();
            arenaGamemode.gameObject.AddComponent<ArenaScoreBoardUI>().gamemodeData = arenaGamemode;

            GameObject.Find("CitadelSpawns").SetActive(false);
            //GameObject.Find("ItemSpawns").SetActive(false);

            GameObject networkManager = GameObject.Find("NetworkManager");
            networkManager.GetComponent<NetworkZombieSpawner>().enabled = false;
        }
        public void Awake()
        {
            SpawnPointManagerEditing.SetSpawnPoints(SpawnPointSet);
            HealthHandlerEditing.ChangeRespawnTime(RespawnTime);
            NetworkManagerEditing.OnPlayerSpawned += NetworkManagerEditing_OnPlayerSpawned;
            HealthHandlerEditing.OnKill += HealthHandlerEditing_OnKill;

            SpawnBarrier(ArenaPosition, ArenaScale);
            HealthHandlerEditing.KillLocalPlayer();
        }
        public void Update()
        {
            if (hasFinishedCleaning)
            {
                StartCoroutine("DestroyAllDropedItems");
                hasFinishedCleaning = false;
            }
        }
        private bool hasFinishedCleaning = true;
        private IEnumerator DestroyAllDropedItems()
        {
            yield return new WaitForSeconds(1f);
            InventoryItem[] allItems = FindObjectsOfType<InventoryItem>();

            foreach (var item in allItems)
                if (item.enabled && item.photonView.isMine)
                    PhotonNetwork.Destroy(item.photonView);

            hasFinishedCleaning = true;
        }
        public void SpawnBarrier(Vector3 position, Vector3 scale)
        {
            GameObject barrier = Instantiate(GameObject.Find("rusty_pipe_straight"));
            barrier.transform.parent = null;
            barrier.transform.position = position;
            barrier.transform.rotation = Quaternion.identity;
            barrier.transform.localScale = scale;
        }
        public void OnDestroy()
        {
            SpawnPointManagerEditing.UseDefaultSpawnPoints();
            HealthHandlerEditing.ResetRespawnTime();
            NetworkManagerEditing.OnPlayerSpawned -= NetworkManagerEditing_OnPlayerSpawned;
            HealthHandlerEditing.OnKill -= HealthHandlerEditing_OnKill;
        }

        private void NetworkManagerEditing_OnPlayerSpawned(PhotonView playerView)
        {
            GivePlayerItems();
        }

        public int KillsMadeByLocalPlayer { get; private set; } = 0;
        public int KillsNeededToChangeWeapons { get; private set; } = 2;
        private void HealthHandlerEditing_OnKill(HealthHandler damaged, float damage, PhotonPlayer damager)
        {
            ZombieBlackboard zB = damaged.GetComponent<ZombieBlackboard>();
            if (damager == null || zB == null)
                return;
            if (zB.Behaviour == BTType.ZOMBIE)
                return;
            if (damaged.photonView.viewID != NetworkManager.LocalPlayerPhotonView.viewID && damager.IsLocal)
            {
                GivePrizeForReachingGoal();
            }
        }
        private void GivePrizeForReachingGoal()
        {
            KillsMadeByLocalPlayer++;
            if (KillsMadeByLocalPlayer % KillsNeededToChangeWeapons == 0)
            {
                ItemRank++;
                if (ItemRank % KitSet.Length == 0)
                {
                    ItemRankCompletitions++;
                    KillsNeededToChangeWeapons++;
                }
                GivePlayerItems();
                NetworkManager.LocalPlayerPhotonView.RPC("RecieveHealth", null, 99.9f);//99.9 instead of 100 so the hud can update itself
            }
        }
        public int ItemRank { get; private set; } = 0;
        public int ItemRankCompletitions { get; private set; } = 0;
        private void GivePlayerItems(bool random = false, bool cleanInventory = true)
        {
            if (cleanInventory)
            {
                InventoryService invService = ServiceLocator.GetService<InventoryService>();
                invService.ClearInventory();
            }
            List<InventoryItem> items;
            if (random)
            {
                items = GetRandomItems();
            }
            else
            {
                items = GetItems(ItemRank % KitSet.Length);
            }

            foreach (var item in items)
                item.TryPickup();
        }
        private List<InventoryItem> GetItems(int index)
        {
            List<InventoryItem> items = new List<InventoryItem>();

            foreach (string s in KitSet[index].Weapons)
                items.Add(PhotonNetwork.Instantiate(s, Vector2.zero, Quaternion.identity, 0).GetComponent<InventoryItem>());

            foreach (var a in KitSet[index].Ammo)
            {
                InventoryItemAmmo ammo = PhotonNetwork.Instantiate(a.Key, Vector2.zero, Quaternion.identity, 0).GetComponent<InventoryItemAmmo>();
                ammo.Amount = a.Value;
                items.Add(ammo);
            }
            return items;
        }
        private List<InventoryItem> GetRandomItems()
        {
            int randomIndex = Random.Range(0, KitSet.Length);
            return GetItems(randomIndex);
        }
    }
    public struct ArenaKit
    {
        public string[] Weapons;
        public KeyValuePair<string, int>[] Ammo;
    }
}
