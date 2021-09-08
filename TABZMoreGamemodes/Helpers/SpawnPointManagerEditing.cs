using UnityEngine;
using HarmonyLib;
namespace TABZMGamemodes
{
    public class SpawnPointManagerEditing
    {
        private static Vector3[] SpawnPositions = new Vector3[] { };
        private static bool useDefaultSpawns = true;
       
        public static void SetSpawnPoints(params Vector3[] spawnPositions)
        {
            SpawnPositions = spawnPositions;
            useDefaultSpawns = false;
        }
        public static void UseDefaultSpawnPoints()
        {
            SpawnPositions = new Vector3[] { };
            useDefaultSpawns = true;
        }

        public static void Patch(Harmony harmonyInstance)
        {
            var getRandomSpawnPointMethod = AccessTools.Method(typeof(SpawnPointManager), nameof(SpawnPointManager.GetRandomSpawnPoint));
            var getRandomSpawnPointPostfix = new HarmonyMethod(typeof(SpawnPointManagerEditing).GetMethod(nameof(SpawnPointManagerEditing.GetRandomSpawnPointPostfix)));
            harmonyInstance.Patch(getRandomSpawnPointMethod, postfix: getRandomSpawnPointPostfix);
        }
        public static void GetRandomSpawnPointPostfix(ref Vector3 __result)
        {
            if (!useDefaultSpawns && SpawnPositions.Length>0)
            {
                int randomIndex = Random.Range(0, SpawnPositions.Length);
                __result = SpawnPositions[randomIndex];
            }
        }
    }
}
