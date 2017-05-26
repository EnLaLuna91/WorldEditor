
using UnityEngine;
using UnityEngine.UI;

public class InputFields : MonoBehaviour {

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

     private void ClearFields() {
          Name.text = GlobalVariables.SceneName;
          Desc.text = GlobalVariables.QuestDesc;
          gameController.EnableFieldName(false);
     }

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
