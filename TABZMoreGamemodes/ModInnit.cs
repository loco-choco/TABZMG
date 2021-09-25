using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using HarmonyLib;
using BepInEx;
namespace TABZMGamemodes
{

    [BepInPlugin("Locochoco.plugins.TABZMoreGamemodes", "More Gamemodes Mod", "1.0.0.0")]
    [BepInProcess("GAME.exe")] //TABZ executable
    public class ModInnit : BaseUnityPlugin
    {
        public void Awake()
        {
                try
                {
                    Harmony harmony = new Harmony("Locochoco.TABZMoreGamemodes");
                    HealthHandlerPatches.Patches(harmony);
                    SpawnPointManagerEditing.Patch(harmony);
                    NetworkManagerEditing.Patch(harmony);

                SceneManager.sceneLoaded += SceneManager_sceneLoaded;
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
        }

        private static void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            if (scene.buildIndex == 0)
                RunOnMenu();
            else if (scene.buildIndex == 1)
                RunOnGame();
        }
        private static void RunOnMenu()
        {
            MenuCreator.CreateGamemodesMenu();
        }
        private static void RunOnGame()
        {
            GamemodeSelector.PlayChoosenGamemode();
        }
    }
}
