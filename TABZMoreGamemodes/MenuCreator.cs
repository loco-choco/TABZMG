using UnityEngine;
using UnityEngine.UI;

namespace TABZMGamemodes
{
    public class MenuCreator
    {
        public static Text SelectedGamemodeText;
        public static Button ReloadSceneButton;
        public static void CreateGamemodesMenu()
        {
            GameObject canvas = GameObject.Find("Canvas");
            GameObject gamemodesList = new GameObject("GamemodesList");
            gamemodesList.AddComponent<RectTransform>();
            gamemodesList.AddComponent<ScrollRect>();
            gamemodesList.transform.SetParent(canvas.transform);
            gamemodesList.transform.localScale = Vector3.one;
            gamemodesList.transform.localPosition = Vector3.zero;
            gamemodesList.transform.rotation = canvas.transform.rotation;
            Vector3 redTogglePos = GameObject.Find("ToggleRed").transform.position;

            Vector3 distanceFromRedToggle = new Vector3(0f, 50f, 0f);
            Vector3 distanceBettwenButton = new Vector3(0f, 40f, 0f);

            for (int i = 0; i < GamemodeSelector.Gamemodes.Length; i++)
            {
                var b = CreateButton(gamemodesList.transform);
                b.transform.position = redTogglePos;
                b.transform.localPosition -= distanceFromRedToggle + distanceBettwenButton * (i + 1);
                b.GetComponentInChildren<Text>().text = GamemodeSelector.Gamemodes[i].GamemodeName;
                int index = i;
                b.onClick.AddListener(() => GamemodeSelector.ChangeGamemode(index));
            }

            var selectedGamemode = CreateButton(gamemodesList.transform);
            selectedGamemode.onClick.AddListener(() => GamemodeSelector.ReloadMenuForConnect());
            ReloadSceneButton = selectedGamemode;
            selectedGamemode.transform.position = redTogglePos;
            selectedGamemode.transform.localPosition -= distanceFromRedToggle + distanceBettwenButton * (GamemodeSelector.Gamemodes.Length + 1);
            SelectedGamemodeText = selectedGamemode.GetComponentInChildren<Text>();
            SelectedGamemodeText.text = GamemodeSelector.Gamemodes[GamemodeSelector.ChosenGamemodeIndex].GamemodeName;
            selectedGamemode.interactable = false;
        }
        private static GameObject ReferenceButton;
        public static Font GetServerCellFont() { return Resources.Load<Font>("Font/eurof55"); }
        public static Button CreateButton(Transform parent)
        {
            if (ReferenceButton == null)
                ReferenceButton = GameObject.Find("QUIT");
            GameObject button = Object.Instantiate(ReferenceButton);
            Button buttonScript = button.GetComponent<Button>();
            buttonScript.transform.SetParent(parent);
            buttonScript.transform.rotation = parent.rotation;
            buttonScript.transform.localScale = Vector3.one*2f;
            return buttonScript;
        }
    }
}
