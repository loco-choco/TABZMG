using System;
using UnityEngine;

using HarmonyLib;
using CAMOWA;
namespace TABZMGamemodes
{
    public class ModInnit : MonoBehaviour
    {
        private static bool HasThePatchesBeenRun = false;
        [IMOWAModInnit("TABZMoreGamemodes", -1, 1)]
        static public void Innit(string startingPoint)
        {
            if (!HasThePatchesBeenRun)
            {
                try
                {
                    Harmony harmony = new Harmony("Locochoco.TABZMoreGamemodes");
                    HealthHandlerPatches.Patches(harmony);
                    SpawnPointManagerEditing.Patch(harmony);
                    NetworkManagerEditing.Patch(harmony);
                    HasThePatchesBeenRun = true;
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
            MenuCreator.CreateGamemodesMenu();
        }
        static private void RunOnGame()
        {
            GamemodeSelector.PlayChoosenGamemode();
        }
    }
}
