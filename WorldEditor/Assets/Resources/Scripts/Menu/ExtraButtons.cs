using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ExtraButtons : MonoBehaviour {

     private GameController gameController;
     private Button btn;
     private LoadHero hero;

     private void Start() {
          InicializeGameController();
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
     }

     #region Game Controller

     private void InicializeGameController() {
          GameObject gameControllerObject = GameObject.FindWithTag("GameController");
          if (gameControllerObject != null) {
               gameController = gameControllerObject.GetComponent<GameController>();
          }
          if (gameController == null) {
               Debug.Log("Cannot find 'GameController' script");
          }
     }

     #endregion

     private void TaskOnClick() {
          if (gameObject.name == "SaveButton") SaveButton();
          else if (gameObject.name == "LoadButton") LoadButton();
          else if (gameObject.name == "StartButton") StartButton();
          else if (gameObject.name == "ExitButton") ExitButton();
     }

     private void SaveButton() {
          Debug.Log("Save");
          string path = EditorUtility.SaveFilePanel("Save scene as json", "", "scene.json", "json");
          Saver save = new Saver();
          save.StoreData(path);
     }

     private void LoadButton() {
          Debug.Log("Load");
          string path = EditorUtility.OpenFilePanel("Open scene", "", "json");
          //SceneManager.LoadScene(path, LoadSceneMode.Additive);
     }

     private void StartButton() {
          Debug.Log("Start");
          gameController.DisableEditorMenus();
          gameController.EnableInGameMenus();

          //LoadHero hero = gameObject.GetComponent<LoadHero>();
          hero.SetHero();
     }

     private void ExitButton() {
          Debug.Log("Leave");
     }
}
