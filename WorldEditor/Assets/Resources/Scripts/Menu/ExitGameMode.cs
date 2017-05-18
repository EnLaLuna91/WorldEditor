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
          
     }

     private void ChangeCameras() {
          GlobalVariables.EditorCamera.enabled = !GlobalVariables.EditorCamera.enabled;
          GlobalVariables.HeroCamera.enabled = !GlobalVariables.HeroCamera.enabled;
     }

}
