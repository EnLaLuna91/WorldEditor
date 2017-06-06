using UnityEngine;
using UnityEngine.UI;

public class ChangeTexture : MonoBehaviour {
     public Material Texure;

     private Button btn;
     private bool ReadyToFix = false;

     private void Start() {
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
     }

     private void TaskOnClick() {
          if (Texure != null) {
               ReadyToFix = true;
               GlobalVariables.FixTexture = true;
          }
          //Debug.Log(string.Format("ReadyToFix: {0}", ReadyToFix));
     }

     private void Update() {
          if (ReadyToFix && Input.GetMouseButtonDown(0)) ChangeMaterial();    
     }

     private void ChangeMaterial() {
          GameObject item = RayCast();
          Debug.Log(string.Format("Item: {0}", item));
          if (item != null) {
               Debug.Log(string.Format("Item.name: {0}", item.name));
               item.GetComponent<Renderer>().material = Texure;
               ReadyToFix = false;
               GlobalVariables.FixTexture = false;
          }
     }

     private GameObject RayCast() {
          RaycastHit hitInfo;
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          Debug.Log(string.Format("RayCast: {0}\tpos: {1}", Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Buildings")), hitInfo.point));
          if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Buildings")))
               return hitInfo.transform.gameObject;
          else return null;
     }

}
