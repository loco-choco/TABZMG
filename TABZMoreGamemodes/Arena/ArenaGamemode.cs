using System.Collections;
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
                Weapons = new string[]{ "Items/Makarov"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoSmall", 12) }
            },
            new ArenaKit
            {
                Weapons = new string[]{"Items/Scorpion"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoSmall", 24) }
            },
            new ArenaKit
            {
                Weapons = new string[]{ "Items/Revolver", "Items/Revolver"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoMedium", 3) }
            },

            new ArenaKit
            {
                Weapons = new string[]{ "Items/Ak74"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoMedium", 25)}
            },
			new ArenaKit
            {
                Weapons = new string[]{ "Items/Mp5"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoSmall", 35)}
            },			            
                       			
			new ArenaKit
            {
                Weapons = new string[]{"Items/HuntingSniper"},
                Ammo = new KeyValuePair<string, int>[]{new KeyValuePair<string, int>("Items/AmmoBig", 1)}
            },
             new ArenaKit
            {
                Weapons = new string[]{ "Items/Musket"},
                Ammo = new KeyValuePair<string, int>[]{ new KeyValuePair<string, int>("Items/AmmoMedium", 1) }
            },

            new ArenaKit
            {
                Weapons = new string[]{"Items/Axe"},
                Ammo = new KeyValuePair<string, int>[]{}
            },
            new ArenaKit
            {
                Weapons = new string[]{"Items/HuntingKnife"},
                Ammo = new KeyValuePair<string, int>[]{}
            },
            new ArenaKit
            {
                Weapons = new string[]{},
                Ammo = new KeyValuePair<string, int>[]{}
            },
        };
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
            SpawnPointManagerEditing.SetSpawnPoints(SpawnPointSetOne);
            HealthHandlerEditing.ChangeRespawnTime(RespawnTime);
            NetworkManagerEditing.OnPlayerSpawned += NetworkManagerEditing_OnPlayerSpawned;
            HealthHandlerEditing.OnTakeDamage += HealthHandlerEditing_OnTakeDamage;

            SpawnBarrier(new Vector3(1031f, 100f, 637f));
            HealthHandlerEditing.KillLocalPlayer();
        }
        public void Update()
        {
            if(hasFinishedCleaning)
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
                    if(item.enabled && item.photonView.isMine)
                        PhotonNetwork.Destroy(item.photonView);

            hasFinishedCleaning = true;
        }
        public void SpawnBarrier(Vector3 position)
        {
            GameObject barrier = Instantiate(GameObject.Find("rusty_pipe_straight"));
            barrier.transform.parent = null;
            barrier.transform.position = position;
            barrier.transform.rotation = Quaternion.identity;
            barrier.transform.localScale = 540f * new Vector3(10f, 1, 10f);
        }
        public void OnDestroy()
        {
            SpawnPointManagerEditing.UseDefaultSpawnPoints();
            HealthHandlerEditing.ResetRespawnTime();
            NetworkManagerEditing.OnPlayerSpawned -= NetworkManagerEditing_OnPlayerSpawned;
        }

        private void NetworkManagerEditing_OnPlayerSpawned(PhotonView playerView)
        {
            GivePlayerItems();
        }

        public float DamageDeltByLocalPlayer {get;private set;}= 0;
        public float DamageNeededToChangeWeapons {get;private set;} = 200f;
        private void HealthHandlerEditing_OnTakeDamage(HealthHandler damaged, float damage, PhotonPlayer damager, bool isKillingBlow)
        {
            if (damager == null)
                return;
            if(damaged.photonView != NetworkManager.LocalPlayerPhotonView && damager.IsLocal)
            {
                DamageDeltByLocalPlayer += damage;
                if(DamageDeltByLocalPlayer> DamageNeededToChangeWeapons)
                {
                    ItemRank = (ItemRank + 1) % KitSetOne.Length;
                    GivePlayerItems();
                    NetworkManager.LocalPlayerPhotonView.RPC("RecieveHealth", damager, 98f);//Doing this so the HealthHandler can give the last two health points and update the hud xP
                    DamageDeltByLocalPlayer = 0f;
                }
            }
        }

        public int ItemRank { get; private set; } = 0;
        private void GivePlayerItems(bool random = false,bool cleanInventory = true)
        {
            if (cleanInventory)
            {
                InventoryService invService = ServiceLocator.GetService<InventoryService>();
                invService.ClearInventory();
            }
            List<InventoryItem> items;
            if (random)
                items = GetRandomItems();
            else
                items = GetItems(ItemRank % KitSetOne.Length);

            foreach (var item in items)
                item.TryPickup();
        }
        private List<InventoryItem> GetItems(int index)
        {
            List<InventoryItem> items = new List<InventoryItem>();

            foreach (string s in KitSetOne[index].Weapons)
                items.Add(PhotonNetwork.Instantiate(s, Vector2.zero, Quaternion.identity, 0).GetComponent<InventoryItem>());

            foreach (var a in KitSetOne[index].Ammo)
            {
                InventoryItemAmmo ammo = PhotonNetwork.Instantiate(a.Key, Vector2.zero, Quaternion.identity, 0).GetComponent<InventoryItemAmmo>();
                ammo.Amount = a.Value;
                items.Add(ammo);
            }
            return items;
        }
        private List<InventoryItem> GetRandomItems()
        {
            int randomIndex = Random.Range(0, KitSetOne.Length);
            return GetItems(randomIndex);
        }
    }
    struct ArenaKit
    {
        public string[] Weapons;
        public KeyValuePair<string, int>[] Ammo;
    }
}
