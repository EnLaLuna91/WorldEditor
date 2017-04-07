using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fixItem2 : MonoBehaviour {
     public GameObject itemToFix;
     public Vector3 RotationItem;
     public float YItemPosition;

     private Button btn;

     private void Start() {
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
     }

     private void TaskOnClick() {
          GlobalVariables.ItemToDrag = itemToFix;
          GlobalVariables.RotationItem = RotationItem;
          GlobalVariables.YItemPosition = YItemPosition;
          Debug.Log(string.Format("Activando {0}", gameObject.name));
     }
}
