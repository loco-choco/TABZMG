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

            var TakeDamage = AccessTools.Method(typeof(HealthHandler), "TakeDamage");
            var takeDamagePrefixMethod = new HarmonyMethod(typeof(HealthHandlerPatches).GetMethod(nameof(HealthHandlerPatches.TakeDamagePrefix)));
            harmonyInstance.Patch(TakeDamage, prefix: takeDamagePrefixMethod);
        }

        public static void WaitForSecondsThenSpawnPrefix(ref float v)
        {
            v = HealthHandlerEditing.GetRespawnTime();
        }

        public static void TakeDamagePrefix(HealthHandler __instance, float dmg, PhotonPlayer player, bool killingBlow)
        {
            HealthHandlerEditing.InvokeOnTakeDamage(__instance, dmg, player, killingBlow);
        }
    }
}
