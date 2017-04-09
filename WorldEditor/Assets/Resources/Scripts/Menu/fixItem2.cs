using UnityEngine;
using UnityEngine.UI;

public class fixItem2 : MonoBehaviour {
     public GameObject itemToFix;
     public string CustomTag = "";
     public Vector3 RotationItem;
     public float YItemPosition;

     private Button btn;

     private void Start() {
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
     }

     private void TaskOnClick() {
          if (itemToFix != null) {
               GlobalVariables.ItemToDrag = itemToFix;
               if (CustomTag != "") GlobalVariables.CustomTag = CustomTag;
               GlobalVariables.RotationItem = RotationItem;
               GlobalVariables.YItemPosition = YItemPosition;         
          }
          //Debug.Log(string.Format("Activando {0}", gameObject.name));
     }
}
