using System.Collections.Generic;
using UnityEngine;

namespace TABZMGamemodes.Arena
{
    public class ArenaGamemode : MonoBehaviour
    {
        private static readonly Vector3[] SpawnPointSetOne = new Vector3[]
        {
            new Vector3(1024,73,663),
            new Vector3(976,83,662),
            new Vector3(978,83,686),
            new Vector3(975,85,707),
        };
        private static readonly ArenaKit[] KitSetOne = new ArenaKit[]
        {
            new ArenaKit
            {
                Weapons = new string[]{ "Items/Ak74", "Items/Makarov"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoMedium", 90), new KeyValuePair<string, int>("Items/AmmoSmall", 24) }
            },
            new ArenaKit
            {
                Weapons = new string[]{"Items/Axe","Items/HuntingKnife"},
                Ammo = new KeyValuePair<string, int>[]{}
            },
            new ArenaKit
            {
                Weapons = new string[]{ "Items/Revolver", "Items/Revolver"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoMedium", 18) }
            },
            new ArenaKit
            {
                Weapons = new string[]{ "Items/Musket"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoMedium", 3) }
            },
        };
        private static readonly float RespawnTime = 0.5f;
        public void Awake()
        {
            SpawnPointManagerEditing.SetSpawnPoints(SpawnPointSetOne);
            HealthHandlerEditing.ChangeRespawnTime(RespawnTime);
            NetworkManagerEditing.OnPlayerSpawned += NetworkManagerEditing_OnPlayerSpawned;

            SpawnBarrier();
            HealthHandlerEditing.KillLocalPlayer();
        }
        public void SpawnBarrier()
        {
            GameObject barrier = Instantiate(GameObject.Find("rusty_pipe_straight"));
            barrier.transform.parent = null;
            barrier.transform.position = new Vector3(1031f, 100f, 637f);
            barrier.transform.rotation = Quaternion.identity;
            barrier.transform.localScale = 538f * new Vector3(10f, 1, 10f);
        }
        public void OnDestroy()
        {
            SpawnPointManagerEditing.UseDefaultSpawnPoints();
            HealthHandlerEditing.ResetRespawnTime();
            NetworkManagerEditing.OnPlayerSpawned -= NetworkManagerEditing_OnPlayerSpawned;
        }

        private void NetworkManagerEditing_OnPlayerSpawned(PhotonView playerView)
        {
            InventoryService invService = ServiceLocator.GetService<InventoryService>();
            invService.ClearInventory();
            var items = GetRandomItems();
            foreach (var item in items)
                item.TryPickup();
        }
        private List<InventoryItem> GetRandomItems()
        {
            int randomIndex = Random.Range(0, KitSetOne.Length - 1);
            List<InventoryItem> items = new List<InventoryItem>();

            foreach (string s in KitSetOne[randomIndex].Weapons)
                items.Add(PhotonNetwork.Instantiate(s, Vector2.zero, Quaternion.identity, 0).GetComponent<InventoryItem>());

            foreach (var a in KitSetOne[randomIndex].Ammo)
            {
                InventoryItemAmmo ammo = PhotonNetwork.Instantiate(a.Key, Vector2.zero, Quaternion.identity, 0).GetComponent<InventoryItemAmmo>();
                ammo.Amount = a.Value;
                items.Add(ammo);
            }
            return items;
        }
    }
    struct ArenaKit
    {
        public string[] Weapons;
        public KeyValuePair<string, int>[] Ammo;
    }
}
