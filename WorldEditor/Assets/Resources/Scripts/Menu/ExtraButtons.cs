using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExtraButtons : MonoBehaviour {

     private Button btn;

     private void Start() {
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
     }

     private void TaskOnClick() {
          if (gameObject.name == "SaveButton") SaveButton();
          else if (gameObject.name == "LoadButton") LoadButton();
          else if (gameObject.name == "StartButton") StartButton();
          else if (gameObject.name == "ExitButton") ExitButton();
     }

     private void SaveButton() {
          Debug.Log("Save");
          var path = EditorUtility.SaveFilePanel("Save scene as unity", "", "scene.unity", "unity");
          Debug.Log(string.Format("Ruta: {0}", path));
          bool saveOK = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), path);
          Debug.Log("Saved Scene " + (saveOK ? "OK" : "Error!"));
     }

     private void LoadButton() {
          Debug.Log("Load");
          string path = EditorUtility.OpenFilePanel("Open scene", "", "unity");
          SceneManager.LoadScene(path, LoadSceneMode.Additive);
     }

     private void StartButton() {
          Debug.Log("Start");
     }

     private void ExitButton() {
          Debug.Log("Leave");
     }
}
