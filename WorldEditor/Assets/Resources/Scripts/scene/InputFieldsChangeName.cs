
using UnityEngine;
using UnityEngine.UI;

public class InputFieldsChangeName : MonoBehaviour {

     private InputField Name;
     private InputField Desc;
     private Button btn;
     private GameController gameController;
     private bool FirstTimeEnter = true;

     private void Start() {
          InicializeGameController();
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
          Name = GameObject.Find("Name").GetComponentInChildren<InputField>();
          Desc = GameObject.Find("Description").GetComponentInChildren<InputField>();
          Name.text = GlobalVariables.SceneName;
          Desc.text = GlobalVariables.QuestDesc;
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
          if (gameObject.name == "Cancel") ClearFields();
          else if (gameObject.name == "Ok") SaveNameDesc();
     }

     /// <summary>
     /// Deja el nombre de la escena y objetivos de la misión como estaban antes de entrar y cierra la venta de cambiar nombre
     /// </summary>
     private void ClearFields() {
          Name.text = GlobalVariables.SceneName;
          Desc.text = GlobalVariables.QuestDesc;
          gameController.EnableFieldName(false);
     }

     /// <summary>
     /// Guarda el nombre de la escena y los objetivos de la misión, actualiza el nombre en el recuadro superior izquierda y
     /// cierra la ventana de cambio de nombre
     /// </summary>
     private void SaveNameDesc() {
          GlobalVariables.SceneName = Name.text.ToUpper();
          GlobalVariables.QuestDesc = Desc.text;
          gameController.SetSceneName();          
          gameController.EnableFieldName(false);
     }


     private void Update() {
          if (FirstTimeEnter) {
               FirstTimeEnter = false;
               Name.text = GlobalVariables.SceneName;
               Desc.text = GlobalVariables.QuestDesc;
          }
     }

}
