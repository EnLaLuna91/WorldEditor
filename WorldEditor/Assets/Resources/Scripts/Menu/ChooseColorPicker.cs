using UnityEngine;
using UnityEngine.UI;

public class ChooseColorPicker : MonoBehaviour {

     public new RawImage renderer;
     public ColorPicker picker;
     public Material material;

     private GameController gameController;
     private Button btn;
     private bool ShowPicker = false;

     private void Start() {
          InicializeGameController();
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);

          if (renderer == null) {
               renderer = gameObject.GetComponentInChildren<RawImage>();
          }

          //Debug.Log(string.Format("Renderer: {0}", renderer));
          

          picker.onValueChanged.AddListener(color =>
          {
               renderer.color = color;
          });
          renderer.color = picker.CurrentColor;

          material.color = renderer.color;
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
          if (ShowPicker) {
               gameController.PickerActive(false);
          } else {
               gameController.PickerActive(true);
          }
          ShowPicker = !ShowPicker;
     }

     private void Update() {
          material.color = renderer.color;
          GlobalVariables.ColorMaterial = material;
     }

}
