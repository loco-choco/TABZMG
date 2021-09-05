using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TABZMGamemodes
{
    public struct GamemodeData
    {
        public string GamemodeName;
        public string UniqueVersionIdentifierPostfix;
        public GamemodeSelector.GamemodeInnit GamemodeInnit;
    }
    public class GamemodeSelector
    {
        public delegate void GamemodeInnit();
        public static int ChosenGamemodeIndex { private set; get; } = 0;
        private static readonly string OriginalVersionNumber = AccessTools.StaticFieldRefAccess<string>(typeof(MainMenuHandler), "mVersionString");
        public static GamemodeData[] Gamemodes = new GamemodeData[]
        {
            new GamemodeData(){ GamemodeName = "Survival",
            UniqueVersionIdentifierPostfix = "",
            GamemodeInnit = null},
            new GamemodeData(){ GamemodeName = "Arena",
            UniqueVersionIdentifierPostfix = "Arena",
            GamemodeInnit = Arena.ArenaGamemode.GamemodeInnit},
        };
        public static void ChangeGamemode(int index)
        {
            ChosenGamemodeIndex = index;
            if (ChosenGamemodeIndex < Gamemodes.Length && ChosenGamemodeIndex > -1)
            {
                MenuCreator.SelectedGamemodeText.text = "Reload Menu";
                MenuCreator.ReloadSceneButton.interactable = true;
            }
        }
        public static void ReloadMenuForConnect()
        {
            string postFix = "";
            if (ChosenGamemodeIndex < Gamemodes.Length && ChosenGamemodeIndex > -1)
                postFix = Gamemodes[ChosenGamemodeIndex].UniqueVersionIdentifierPostfix;

            Debug.Log(string.Format("Changing to {0} ({1})", postFix, ChosenGamemodeIndex));
            AccessTools.StaticFieldRefAccess<string>(typeof(MainMenuHandler), "mVersionString") = OriginalVersionNumber + postFix;
            MusicHandler.StopMusic();
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(0);
        }
        public static void PlayChoosenGamemode()
        {
            if (ChosenGamemodeIndex < Gamemodes.Length && ChosenGamemodeIndex > -1)
                Gamemodes[ChosenGamemodeIndex].GamemodeInnit?.Invoke();
        }
    }
}
