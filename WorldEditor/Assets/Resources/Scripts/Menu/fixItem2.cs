using UnityEngine;
using UnityEngine.UI;

public class fixItem2 : MonoBehaviour {
     public GameObject ItemToFix;
     public string CustomTag = "";
     public Vector3 RotationItem;
     public float YItemPosition;

     private Button btn;
     private bool ReadyToFix = false;
     private GameObject obj = null;
     private string Layer = "";

     private void Start() {
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
     }

     private void TaskOnClick() {
          if (ItemToFix != null) {    
               ReadyToFix = true;
          }
     }

     private void Update() {

          if (ReadyToFix) ShowItem();
          if (Input.GetMouseButtonDown(1) && GlobalVariables.CanFixItem && ReadyToFix) RotateItem();
          if (Input.GetMouseButtonDown(0) && GlobalVariables.CanFixItem && ReadyToFix) InsertItem();
          
     }


     private void InsertItem() {

          obj.layer = LayerMask.NameToLayer(Layer);

          if (CustomTag != "") obj.tag = CustomTag;

          if (obj.tag == "Objectives") GlobalVariables.ObjectivesIsSelected = true;
          if (obj.tag == "Resources") GlobalVariables.ResourcesIsSelected = true;
          if (obj.tag == "Item") GlobalVariables.ItemsIsSelected = true;
          if (obj.tag == "NPCs") GlobalVariables.NPCsIsSelected = true;

          //Debug.Log(string.Format("Obj.layer: {0}\tLayer: {1}", LayerMask.LayerToName(obj.layer), Layer));

          if (GlobalVariables.IsCtrlPressed) ReadyToFix = true;
          else {
               ReadyToFix = false;
               RotationItem.y = 0;
               obj = null;
          }

          //Debug.Log(string.Format("ReadyToFix: {0}", ReadyToFix));
     }

     private void RotateItem() {

          if (GlobalVariables.IsShiftPressed) RotationItem.y -= 90;
          else RotationItem.y += 90;

          if (RotationItem.y >= 360 || RotationItem.y <= -360) RotationItem.y = 0;

          obj.transform.rotation = Quaternion.Euler(RotationItem);

          //Debug.Log(string.Format("Rotation: {0}", RotationItem));
     }
     
     private Vector3 RayCast() {
          RaycastHit hitInfo;
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain")))
               return new Vector3(hitInfo.point.x, hitInfo.point.y + YItemPosition, hitInfo.point.z);
          else return new Vector3(0, 0, 0);
     }

     private void ShowItem() {          
          Vector3 pos = RayCast();
          if (pos != new Vector3(0, 0, 0)) {
               if (obj == null) {
                    obj = Instantiate(ItemToFix, pos, Quaternion.Euler(RotationItem)) as GameObject;
                    Layer = LayerMask.LayerToName(obj.layer);
                    obj.layer = LayerMask.NameToLayer("Default");
               }
               obj.transform.position = pos;
          }          
     }

}
