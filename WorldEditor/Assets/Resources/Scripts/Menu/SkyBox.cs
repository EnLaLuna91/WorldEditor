using UnityEngine;
using UnityEngine.UI;

public class SkyBox : MonoBehaviour {
     public Material Skybox;

     private Button btn;

     private void Start() {
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
     }

     private void TaskOnClick() {
          RenderSettings.skybox = Skybox;
          GlobalVariables.SkyIsSelected = true;
     }
}
