using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class helpPopUp : MonoBehaviour, IPointerEnterHandler {

     public string textHelper;
     public Color textColor;
     public Color outlineColor;

     private GameObject Sandwich;
     private bool _mouseOver;

     private void OnGUI() {
          if (_mouseOver) {
               //Sandwich = Instantiate(Resources.Load("SandwichHelper"), Input.mousePosition, Quaternion.identity) as GameObject;
               Sandwich = Instantiate(Resources.Load("Prefabs/SandwichHelper", typeof(GameObject))) as GameObject;
               Debug.Log(Sandwich);

               InicializeComponents();
          } else Destroy(Sandwich);
     }

     //private void OnMouseEnter() {
     //     Debug.Log("Mouse Enter");
     //     _mouseOver = true;
     //}

     private void InicializeComponents() {
          textColor = new Color(textColor.r, textColor.g, textColor.b, 1.0f); // Para que coja bien el color hay que darle el alpha
          outlineColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, 1.0f); // Para que coja bien el color hay que darle el alpha

          UpdateText();
     }

     private void UpdateText() {
          Text text = Sandwich.GetComponentInChildren<Text>();
          text.text = textHelper;
          text.color = textColor;
          Outline line = Sandwich.GetComponentInChildren<Outline>();
          line.effectColor = outlineColor;
     }

     private void OnMouseExit() {
          Debug.Log("Mouse Exit");
          _mouseOver = false;
     }
     

     public void OnPointerEnter(PointerEventData eventData) {
          Debug.Log("Mouse Enter");
          _mouseOver = true;
     }
}
