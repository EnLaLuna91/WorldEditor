using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

     private GameObject basePanel;
     private GameObject panelRight;
     private GameObject panelHide;

     private GameObject mainMenu;
     private GameObject subMenu;
     private GameObject[] UIActivables;
     private GameObject[] InGameMenus;

     private Camera MainCamera;
     private Camera HeroCamera;


     private Stack<GameObject> levels;

     private void Start() {
          InicialiceGlobalVarialbes();
          InicializePanels();
          InicializeMenus();
          DisableInGameMenus();

          levels = new Stack<GameObject>();
     }

     private void InicialiceGlobalVarialbes() {
          GlobalVariables.CanFixItem = true;

          GlobalVariables.TerrainIsLoad = false;
          GlobalVariables.ObjectivesIsSelected = false;
          GlobalVariables.ResourcesIsSelected = false;
          GlobalVariables.FxIsSelected = false;

          GlobalVariables.ItemsIsSelected = false;
          GlobalVariables.NPCsIsSelected = false;
          GlobalVariables.SkyIsSelected = false;
          GlobalVariables.MaterialIsSelected = false;

          GlobalVariables.IsShiftPressed = false;
          GlobalVariables.IsCtrlPressed = false;

          GlobalVariables.YouWin = false;
          GlobalVariables.GameOver = false;

          GlobalVariables.EditorCamera = Camera.main;
          GlobalVariables.HeroCamera = null;
     }


     #region Menus Behavior

     private void InicializePanels() {
          basePanel = GameObject.Find("BasePanel");
          panelRight = GameObject.Find("PanelRight");
          panelHide = GameObject.Find("PanelHide");

          mainMenu = GameObject.FindGameObjectWithTag("mainMenu");
          subMenu = GameObject.FindGameObjectWithTag("subMenu");
          UIActivables = GameObject.FindGameObjectsWithTag("UIActivable");

          InGameMenus = GameObject.FindGameObjectsWithTag("inGame");                  
     }

     public void InicializeMenus() {
          basePanel.SetActive(true);
          panelRight.SetActive(false);
          panelHide.SetActive(false);

          mainMenu.SetActive(true);
          subMenu.SetActive(false);
     }

     public void MainMenuSetActive(bool isActive) {
          mainMenu.SetActive(isActive);
     }

     public void SubMenuSetActive(bool isActive) {
          subMenu.SetActive(isActive);
     }

     public void ActiveGameObject(GameObject gameObject) {
          foreach (var obj in UIActivables) {
               if (obj.name != gameObject.name) obj.SetActive(false);
          }
          gameObject.SetActive(true);
     }

     public void DisableAllGameObjects() {
          foreach (var obj in UIActivables) {
               obj.SetActive(false);
          }
     }

     public void BasePanelSetActive(bool isActive) {
          basePanel.SetActive(isActive);
     }

     public void PanelRightSetActive(bool isActive) {
          panelRight.SetActive(isActive);
     }

     public void PanelHideSetActive(bool isActive) {
          panelHide.SetActive(isActive);
     }




     #endregion

     #region InGame Menus

     public void DisableInGameMenus() {
          foreach (var obj in InGameMenus) {
               obj.SetActive(false);
          }
     }

     public void DisableEditorMenus() {
          basePanel.SetActive(false);
          panelRight.SetActive(false);
          panelHide.SetActive(false);
     }

     public void EnableInGameMenus() {
          foreach (var obj in InGameMenus) {
               obj.SetActive(true);
          }
     }

     public void EnableEditorMenus() {
          basePanel.SetActive(true);
          panelRight.SetActive(true);
          panelHide.SetActive(true);
     }


     #endregion


     #region Menu Levels

     public void PushGameObject(GameObject gameObject) {
          levels.Push(gameObject);
     }

     public GameObject PopGameObject() {
          return levels.Pop();
     }

     public int StackCount() {
          return levels.Count;
     }

     #endregion
     
     void OnGUI() {
          Event e = Event.current;
          if (e.shift) GlobalVariables.IsShiftPressed = true;
          else GlobalVariables.IsShiftPressed = false;

          if (e.control) GlobalVariables.IsCtrlPressed = true;
          else GlobalVariables.IsCtrlPressed = false;
     }
}
