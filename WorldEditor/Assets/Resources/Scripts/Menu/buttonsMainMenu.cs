using UnityEngine;
using UnityEngine.UI;

public class buttonsMainMenu : MonoBehaviour {

     public string Title;
     public Color textColor;
     public Color outlineColor;
     public Texture Image;
     public GameObject MenuToActive = null;

     private GameController gameController;
     private Button btn;

     private void Start() {
          InicializeGameController();
          InicializeComponents();
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

     #region Config Button

     private void InicializeComponents() {
          textColor = new Color(textColor.r, textColor.g, textColor.b, 1.0f); // Para que coja bien el color hay que darle el alpha
          outlineColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, 1.0f); // Para que coja bien el color hay que darle el alpha

          btn = GetComponent<Button>();

          if (Title != "") UpdateText();
          else ClearText();
          UpdateBackground();
          if (Image != null) UpdateImageBackground();

          btn.onClick.AddListener(TaskOnClick);
     }

     private void UpdateText() {
          Text text = btn.GetComponentInChildren<Text>();
          text.text = string.Format("{0}", Title);
          text.color = textColor;          
          Outline line = btn.GetComponentInChildren<Outline>();
          line.effectColor = outlineColor;          
     }

     private void ClearText() {
          Text text = btn.GetComponentInChildren<Text>();
          text.text = string.Format("");
     }

     private void UpdateBackground() {
          Image img = btn.GetComponent<Image>();
          img.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
     }

     private void UpdateImageBackground() {
          RawImage img = btn.GetComponentInChildren<RawImage>();
          img.texture = (Texture) Image;
     }

     #endregion

     #region Extra Functions
     
     private void DisableSubMenus() {
          gameController.MainMenuSetActive(false);
          gameController.SubMenuSetActive(true);
          gameController.DisableAllGameObjects();
     }

     private void DisablePanelRight() {
          gameController.BasePanelSetActive(true);
          gameController.PanelRightSetActive(false);
          gameController.PanelHideSetActive(false);
     }

     private void ActivePanelRight() {
          gameController.BasePanelSetActive(false);
          gameController.PanelRightSetActive(true);
          gameController.PanelHideSetActive(true);
     }
     
     #endregion

     #region Button Funcionality

     private void TaskOnClick() {
          if (MenuToActive != null) ActiveSubMenu();
          else if (gameObject.name == "BackButton") BackManu();
          else if (gameObject.name == "SaveButton") SaveButton();
          else if (gameObject.name == "LoadButton") LoadButton();
          else if (gameObject.name == "StartButton") StartButton();
          else if (gameObject.name == "ExitButton") ExitButton();
          else DisableSubMenus();
     }

     private void ActiveSubMenu() {
          if (MenuToActive.transform.parent.name == "PanelRight") ActivePanelRight();
          else {
               gameController.MainMenuSetActive(false);
               gameController.SubMenuSetActive(true);
               if (gameObject.transform.parent.tag == "UIActivable") gameController.PushGameObject(gameObject.transform.parent.gameObject);
          }

          gameController.ActiveGameObject(MenuToActive);
     }

     private void ActiveSubMenu(GameObject obj) {
          gameController.MainMenuSetActive(false);
          gameController.SubMenuSetActive(true);
          gameController.ActiveGameObject(obj);
     }

     private void BackManu() {    
          if (gameController.StackCount() > 0) {
               GameObject obj = gameController.PopGameObject();
               ActiveSubMenu(obj);
          } else {
               DisablePanelRight();
               DisableSubMenus();
               gameController.SubMenuSetActive(false);
               gameController.MainMenuSetActive(true);
          }
     }

     private void SaveButton() {
          Debug.Log("Save");
     }

     private void LoadButton() {
          Debug.Log("Load");
     }

     private void StartButton() {
          Debug.Log("Start");
     }

     private void ExitButton() {
          Debug.Log("Leave");
     }

     #endregion
     
}
