using SFB;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Para poder ver la clase en el Inspector de Unity
[System.Serializable]
public class OpenFileBrowser {
     public string Title = "";
     public string FileName = "";
     public string Directory = "";
     public string Extension = "";
     public bool Multiselect = false;
}

// Para poder ver la clase en el Inspector de Unity
[System.Serializable]
public class SaveFileBrowser {
     public string Title = "";
     public string Directory = "";
     public string FileName = "";
     public string Extension = "";
}

public class ExtraButtons : MonoBehaviour {

     public OpenFileBrowser openFile;
     public SaveFileBrowser saveFile;

     private GameController gameController;
     private Button btn;
     private LoadHero hero = new LoadHero();
     private SceneNameQuestDesc NameInGame = new SceneNameQuestDesc();

     private void Start() {
          InicializeGameController();
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
     }

     #region Game Controller

     /// <summary>
     /// Busca el evento del GameController
     /// </summary>
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

     /// <summary>
     /// Guardar escena
     /// </summary>
     private void SaveButton() {
          //Debug.Log("Save");
          // https://github.com/gkngkc/UnityStandaloneFileBrowser
          var path = StandaloneFileBrowser.SaveFilePanel(saveFile.Title, saveFile.Directory, GlobalVariables.SceneName.ToLower() + ".json", saveFile.Extension);
          if (!string.IsNullOrEmpty(path)) {
               Saver save = new Saver();
               File.WriteAllText(path, save.SerializeScenary());
          }
          
          
     }

     /// <summary>
     /// Carga una escena
     /// </summary>
     private void LoadButton() {
          //Debug.Log("Load");

          // https://github.com/gkngkc/UnityStandaloneFileBrowser

          var paths = StandaloneFileBrowser.OpenFilePanel(openFile.Title, openFile.Directory, openFile.Extension, openFile.Multiselect);
          if (paths.Length > 0) {
               StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
          }
     }

     /// <summary>
     /// Ejecuta el juego
     /// </summary>
     private void StartButton() {
          //Debug.Log("Start");
          gameController.DisableEditorMenus();
          gameController.EnableInGameMenus();
          NameInGame.UpdateText();
          GlobalVariables.inGameMode = true;
          hero.SetHero();
     }

     /// <summary>
     /// Sale del juego
     /// </summary>
     private void ExitButton() {
          //Debug.Log("Leave");
          Application.Quit();
     }

     /// <summary>
     /// Se espera de forma asincrona, a que el usuario seleccione que elemento cargar
     /// </summary>
     /// <param name="url">Ruta que tendrá el visor de documentos</param>
     /// <returns>Devuelve el elemento cargado</returns>
     private IEnumerator OutputRoutine(string url) {
          var loader = new WWW(url);
          yield return loader;
          Loader load = new Loader(gameController);
          load.LoadData(loader.text);
     }
}
