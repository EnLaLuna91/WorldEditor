using UnityEngine;
using UnityEngine.UI;

public class fixItem2 : MonoBehaviour {
     public GameObject ItemToFix;
     public string CustomTag = "";
     public Vector3 RotationItem;
     public float YItemPosition;

     private Button btn;
     private bool ReadyToFix = false;

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

          if (Input.GetMouseButtonDown(1) && GlobalVariables.CanFixItem && ReadyToFix) RotateItem();
          if (Input.GetMouseButtonDown(0) && GlobalVariables.CanFixItem && ReadyToFix) InsertItem();

     }


     private void InsertItem() {
          RaycastHit hitInfo;
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain"));
          Vector3 pos = new Vector3(hitInfo.point.x, hitInfo.point.y + YItemPosition, hitInfo.point.z);
          GameObject obj = Instantiate(ItemToFix, pos, Quaternion.Euler(RotationItem)) as GameObject;

          if (CustomTag != "") obj.tag = CustomTag;

          if (obj.tag == "Objectives") GlobalVariables.ObjectivesIsSelected = true;
          if (obj.tag == "Resources") GlobalVariables.ResourcesIsSelected = true;
          if (obj.tag == "Item") GlobalVariables.ItemsIsSelected = true;
          if (obj.tag == "NPCs") GlobalVariables.NPCsIsSelected = true;
          

          if (GlobalVariables.IsCtrlPressed) ReadyToFix = true;
          else {
               ReadyToFix = false;
               RotationItem.y = 0;
          }

          //Debug.Log(string.Format("ReadyToFix: {0}", ReadyToFix));
     }

     private void RotateItem() {

          if (GlobalVariables.IsShiftPressed) RotationItem.y -= 90;
          else RotationItem.y += 90;

          if (RotationItem.y >= 360 || RotationItem.y <= -360) RotationItem.y = 0;
          //Debug.Log(string.Format("Rotation: {0}", RotationItem));
     }
     

}
