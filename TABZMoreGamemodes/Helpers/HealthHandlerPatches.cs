using HarmonyLib;

namespace TABZMGamemodes
{
    public class HealthHandlerPatches
    {
        public static void Patches(Harmony harmonyInstance)
        {
            var healthHandlerDieMethod = AccessTools.Method(typeof(HealthHandler), "WaitForSecondsThenSpawn");
            var waitForSecondsThenSpawnPrefix = new HarmonyMethod(typeof(HealthHandlerPatches).GetMethod(nameof(HealthHandlerPatches.WaitForSecondsThenSpawnPrefix)));
            harmonyInstance.Patch(healthHandlerDieMethod, prefix: waitForSecondsThenSpawnPrefix);
        }

        public static void WaitForSecondsThenSpawnPrefix(ref float v)
        {
            v = HealthHandlerEditing.GetRespawnTime();
        }
    }
}
