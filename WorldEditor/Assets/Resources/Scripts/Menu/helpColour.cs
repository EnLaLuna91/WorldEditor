using UnityEngine;
using UnityEngine.UI;

public class helpColour : MonoBehaviour {

     public Color DoneColor;
     public Color RecomendedColor;

     private Button btn;
     private Color transparent = new Color(0.0f, 0.0f, 0.0f, 0.0f);

     private void Start() {
          btn = GetComponent<Button>();
     }

     private void Update() {
          if (gameObject.name == "TerrainButton") UpdateTerrainButton();
          if (gameObject.name == "ObjectivesButton") UpdateObjectivesButton();
          if (gameObject.name == "ResourcesButton") UpdateResourcesButton();
          if (gameObject.name == "EffectsButton") UpdateEffectsButton();
          if (gameObject.name == "WorkObjectsButton") UpdateWorkButton();
     }

     private void UpdateTerrainButton() {
          Image img = btn.GetComponent<Image>();
          img.color = (GlobalVariables.TerrainIsLoad) ? DoneColor : transparent;
     }

     private void UpdateObjectivesButton() {
          Image img = btn.GetComponent<Image>();
          img.color = (GlobalVariables.ObjectivesIsSelected) ? DoneColor : transparent;
     }

     private void UpdateResourcesButton() {
          Image img = btn.GetComponent<Image>();
          img.color = (GlobalVariables.ResourcesIsSelected) ? DoneColor : transparent;
     }

     private void UpdateEffectsButton() {
          Image img = btn.GetComponent<Image>();
          img.color = (GlobalVariables.FxIsSelected) ? DoneColor : transparent;
     }

     private void UpdateWorkButton() {
          Image img = btn.GetComponent<Image>();
          img.color = (GlobalVariables.TerrainIsLoad &&
               GlobalVariables.ObjectivesIsSelected &&
               GlobalVariables.ResourcesIsSelected &&
               GlobalVariables.FxIsSelected) ? DoneColor : transparent;
     }
     
}
