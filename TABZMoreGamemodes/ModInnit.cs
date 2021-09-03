using System.Collections.Generic;
using UnityEngine;
using CAMOWA;
using HarmonyLib;
using System;

namespace TABZMGamemodes
{
    public class ModInnit : MonoBehaviour
    {
        private static bool HasThePatchesBeenRun = false;
        [IMOWAModInnit("TABZMoreGamemodes", -1, 1)]
        static public void Innit(string startingPoint)
        {
            Debug.Log("Mod innititif");
            if (!HasThePatchesBeenRun)
            {
                try
                {
                    Harmony harmony = new Harmony("Locochoco.TABZMoreGamemodes");
                    HealthHandlerPatches.Patches(harmony);
                    SpawnPointManagerEditing.Patch(harmony);
                    NetworkManagerEditing.Patch(harmony);
                    HasThePatchesBeenRun = true;

                    AccessTools.StaticFieldRefAccess<string>(typeof(MainMenuHandler), "mVersionString") += "-Arena";
                    PhotonNetwork.Disconnect();
                    PhotonNetwork.ConnectUsingSettings(MainMenuHandler.VERSION_NUMBER);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            if (SceneManagerHelper.ActiveSceneBuildIndex == 0)
                RunOnMenu();
            else if (SceneManagerHelper.ActiveSceneBuildIndex == 1)
                RunOnGame();
        }
        static private void RunOnMenu()
        {
        }
        static private void RunOnGame()
        {
            new GameObject("ArenaHandler").AddComponent<Arena.ArenaGamemode>();
        }
    }
}
