using UnityEngine;
using UnityEngine.UI;

public class ExitGameMode : MonoBehaviour {

     private GameController gameController;
     private Button btn;

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
          gameController.DisableInGameMenus();
          gameController.InicializeMenus();
          ChangeCameras();
          GameObject hero = GameObject.Find("Hero(Clone)");
          Destroy(hero);
          GlobalVariables.inGameMode = false;
          
     }

     /// <summary>
     /// Modifica las camaras
     /// </summary>
     private void ChangeCameras() {
          GlobalVariables.EditorCamera.enabled = !GlobalVariables.EditorCamera.enabled;
          GlobalVariables.HeroCamera.enabled = !GlobalVariables.HeroCamera.enabled;
     }

}
