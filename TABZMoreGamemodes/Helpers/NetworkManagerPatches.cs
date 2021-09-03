using HarmonyLib;

namespace TABZMGamemodes
{
    public class NetworkManagerEditing
    {
        public delegate void OnPlayerSpawn(PhotonView playerView);
        public static event OnPlayerSpawn OnPlayerSpawned;

        public static void Patch(Harmony harmonyInstance)
        {
            var onPlayerSpawnedMethod = AccessTools.Method(typeof(NetworkManager), nameof(NetworkManager.OnPlayerSpawned));
            var onPlayerSpawnedPrefix = new HarmonyMethod(typeof(NetworkManagerEditing).GetMethod(nameof(NetworkManagerEditing.OnPlayerSpawnedPrefix)));
            harmonyInstance.Patch(onPlayerSpawnedMethod, prefix: onPlayerSpawnedPrefix);
        }
        public static void OnPlayerSpawnedPrefix(PhotonView playerView)
        {
            OnPlayerSpawned?.Invoke(playerView);
        }
    }
}
