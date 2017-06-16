using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

     private GameObject basePanel;
     private GameObject panelRight;
     private GameObject panelHide;

     private GameObject mainMenu;
     private GameObject subMenu;
     private GameObject[] UIActivables;
     private GameObject[] InGameMenus;
     private GameObject NameField;
     private GameObject SceneName;
     private GameObject ColorPicker;

     private Camera MainCamera;
     private Camera HeroCamera;

     private Stack<GameObject> levels;

     /// <summary>
     /// Inicializacion del juego
     /// </summary>
     private void Start() {
          InicialiceGlobalVariables();
          InicializePanels();
          InicializeMenus();
          DisableInGameMenus();
          SetSceneName();

          levels = new Stack<GameObject>();
     }

     #region Inicialice Global Variables

     /// <summary>
     /// Inicializa todas la variables globales, para evitar posibles fallos.
     /// </summary>
     private void InicialiceGlobalVariables() {
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

          GlobalVariables.inGameMode = false;

          GlobalVariables.SceneName = "Scene 0";
          GlobalVariables.QuestDesc = "Quest Objectives ...";

          GlobalVariables.SkyName = "";

          GlobalVariables.GridWidth = 0;
          GlobalVariables.GridHeight = 0;

          GlobalVariables.FixTexture = false;          
          GlobalVariables.ColorMaterial = null;
          GlobalVariables.ElementsToChangeTexture = new List<TextureInElement>();
     } 

     #endregion

     #region Menus Behavior

     /// <summary>
     /// Busca todos los paneles que se irán ocultando a lo largo de la ejecución del juego
     /// </summary>
     private void InicializePanels() {
          basePanel = GameObject.Find("BasePanel");
          panelRight = GameObject.Find("PanelRight");
          panelHide = GameObject.Find("PanelHide");

          mainMenu = GameObject.FindGameObjectWithTag("mainMenu");
          subMenu = GameObject.FindGameObjectWithTag("subMenu");
          UIActivables = GameObject.FindGameObjectsWithTag("UIActivable");

          InGameMenus = GameObject.FindGameObjectsWithTag("inGame");

          NameField = GameObject.Find("SceneInputName");
          SceneName = GameObject.Find("NameScene");

          ColorPicker = GameObject.Find("Picker");
     }

     /// <summary>
     /// Establece si van a estar, o no, activos los paneles, al iniciar el juego.
     /// </summary>
     public void InicializeMenus() {
          basePanel.SetActive(true);
          panelRight.SetActive(false);
          panelHide.SetActive(false);

          mainMenu.SetActive(true);
          subMenu.SetActive(false);

          EnableFieldName(false);
          SceneName.SetActive(true);

          ColorPicker.SetActive(false);
     }

     /// <summary>
     /// Permite modifcar el estado del menú principal
     /// </summary>
     /// <param name="isActive">Booleano con el valor de si se quiere activar o no</param>
     public void MainMenuSetActive(bool isActive) {
          mainMenu.SetActive(isActive);
     }

     /// <summary>
     /// Permite modificar el estado del menú de Work Objects
     /// </summary>
     /// <param name="isActive">Booleano con el valor de si se quiere activar o no</param>
     public void SubMenuSetActive(bool isActive) {
          subMenu.SetActive(isActive);
     }

     /// <summary>
     /// Permite activar o desactivar un GameObject y desactiva los demás paneles
     /// </summary>
     /// <param name="gameObject"></param>
     public void ActiveGameObject(GameObject gameObject) {
          foreach (var obj in UIActivables) {
               if (obj.name != gameObject.name) obj.SetActive(false);
          }
          gameObject.SetActive(true);
     }

     /// <summary>
     /// Desactiva todos los paneles
     /// </summary>
     public void DisableAllGameObjects() {
          foreach (var obj in UIActivables) {
               obj.SetActive(false);
          }
     }

     /// <summary>
     /// Modifica el estado de la interfaz inferior principal, es decir, todos los botones del menú principal
     /// </summary>
     /// <param name="isActive">Booleano con el valor de si se quiere activar o no</param>
     public void BasePanelSetActive(bool isActive) {
          basePanel.SetActive(isActive);
     }

     /// <summary>
     /// Activa o descativa el menú lateral
     /// </summary>
     /// <param name="isActive">Booleano con el valor de si se quiere activar o no</param>
     public void PanelRightSetActive(bool isActive) {
          panelRight.SetActive(isActive);
     }

     /// <summary>
     /// Modifica el estado de los botónes de configuración que aparecen junto al menú lateral.
     /// </summary>
     /// <param name="isActive">Booleano con el valor de si se quiere activar o no</param>
     public void PanelHideSetActive(bool isActive) {
          panelHide.SetActive(isActive);
     }
     

     #endregion

     #region InGame Menus

     /// <summary>
     /// Desactiva todos los panales relacionados con el testeo de la escena
     /// </summary>
     public void DisableInGameMenus() {
          foreach (var obj in InGameMenus) {
               obj.SetActive(false);
          }
     }

     /// <summary>
     /// Desactiva todos los paneles relacionados con la edición del mundo.
     /// </summary>
     public void DisableEditorMenus() {
          basePanel.SetActive(false);
          panelRight.SetActive(false);
          panelHide.SetActive(false);
          SceneName.SetActive(false);
     }

     /// <summary>
     /// Activa los paneles del testeo de la escena
     /// </summary>
     public void EnableInGameMenus() {
          foreach (var obj in InGameMenus) {
               obj.SetActive(true);
          }
     }

     /// <summary>
     /// Activa los paneles del editor de mundo
     /// </summary>
     public void EnableEditorMenus() {
          basePanel.SetActive(true);
          panelRight.SetActive(true);
          panelHide.SetActive(true);
          SceneName.SetActive(true);
     }


     #endregion
     
     #region Menu Levels

     /// <summary>
     /// Añade un elemento a la pila, estos elementos son los estados del menú
     /// </summary>
     /// <param name="gameObject">GameObject a insertar</param>
     public void PushGameObject(GameObject gameObject) {
          levels.Push(gameObject);
     }

     /// <summary>
     /// Extrae el último elemento insertado a la pila
     /// </summary>
     /// <returns>GameObject extraido</returns>
     public GameObject PopGameObject() {
          return levels.Pop();
     }

     /// <summary>
     /// Informa de la cantidad de elementos que hay en la pila
     /// </summary>
     /// <returns>Int con el número de elementos en la pila</returns>
     public int StackCount() {
          return levels.Count;
     }

     #endregion

     #region Input Fields Name

     /// <summary>
     /// Activa o desactiva la ventana que permite modificar el nombre de la escena y el objetivo del reto
     /// </summary>
     /// <param name="active">Booleano con el valor de si se quiere activar o no</param>
     public void EnableFieldName(bool active) {
          NameField.SetActive(active);
     }

     /// <summary>
     /// Modifica el texto con el nombre de la escena especificado, en el recuadro superior izquierda
     /// </summary>
     public void SetSceneName() {
          Text txt = SceneName.GetComponentInChildren<Text>();
          txt.text = GlobalVariables.SceneName;
     }

     #endregion

     /// <summary>
     /// Activa o desactiva el color picker, el selector de colores
     /// </summary>
     /// <param name="active">Booleano con el valor de si se quiere activar o no</param>
     public void PickerActive(bool active) {
          ColorPicker.SetActive(active);
     }

     /// <summary>
     /// Eventos de teclado que hay durante la ejecución del juego
     /// </summary>
     void OnGUI() {
          Event e = Event.current;
          if (e.shift) GlobalVariables.IsShiftPressed = true;
          else GlobalVariables.IsShiftPressed = false;

          if (e.control) GlobalVariables.IsCtrlPressed = true;
          else GlobalVariables.IsCtrlPressed = false;
     }
}
