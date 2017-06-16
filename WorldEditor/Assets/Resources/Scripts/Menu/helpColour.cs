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

     /// <summary>
     /// Actualiza el color del botón de insertar terreno
     /// </summary>
     private void UpdateTerrainButton() {
          Image img = btn.GetComponent<Image>();
          img.color = (GlobalVariables.TerrainIsLoad) ? DoneColor : transparent;
     }

     /// <summary>
     /// Actualiza el color del botón de insertar objetivos
     /// </summary>
     private void UpdateObjectivesButton() {
          Image img = btn.GetComponent<Image>();
          img.color = (GlobalVariables.ObjectivesIsSelected) ? DoneColor : transparent;
     }

     /// <summary>
     /// Actualiza el color del botón de insertar recursos
     /// </summary>
     private void UpdateResourcesButton() {
          Image img = btn.GetComponent<Image>();
          img.color = (GlobalVariables.ResourcesIsSelected) ? DoneColor : transparent;
     }

     /// <summary>
     /// Actualiza el color del botón de insertar efectos
     /// </summary>
     private void UpdateEffectsButton() {
          Image img = btn.GetComponent<Image>();
          img.color = (GlobalVariables.FxIsSelected) ? DoneColor : transparent;
     }

     /// <summary>
     /// Actualiza el color del botón de Work Objects
     /// </summary>
     private void UpdateWorkButton() {
          Image img = btn.GetComponent<Image>();
          img.color = (GlobalVariables.TerrainIsLoad &&
               GlobalVariables.ObjectivesIsSelected &&
               GlobalVariables.ResourcesIsSelected &&
               GlobalVariables.FxIsSelected) ? DoneColor : transparent;
     }
     
}
