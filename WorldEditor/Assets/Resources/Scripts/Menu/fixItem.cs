using UnityEngine;
using UnityEngine.UI;

public class fixItem : MonoBehaviour {
     public GameObject ItemToFix;
     public string CustomTag = "";
     public Vector3 RotationItem;
     public float YItemPosition;
     public int gridWidth = 0;
     public int gridHeight = 0;

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
               if (gridHeight != 0) GlobalVariables.GridHeight = gridHeight;
               if (gridWidth != 0) GlobalVariables.GridWidth = gridWidth;
          }
          //Debug.Log(string.Format("ReadyToFix: {0}", ReadyToFix));
     }

     private void Update() {

          if (ReadyToFix) ShowItem();
          if (Input.GetMouseButtonDown(1) && GlobalVariables.CanFixItem && ReadyToFix) RotateItem();
          if (Input.GetMouseButtonDown(0) && GlobalVariables.CanFixItem && ReadyToFix) InsertItem();

     }

     /// <summary>
     /// Deja el elemento seleccionado fijo en el mapa
     /// </summary>
     private void InsertItem() {
          Debug.Log(string.Format("Insert Obj: {0}\tLayer: {1}", obj.name, Layer));

          obj.layer = LayerMask.NameToLayer(Layer);
          ChangeLayerRecursive(obj.transform, Layer);

          if (CustomTag != "") obj.tag = CustomTag;

          if (obj.tag == "Objectives") GlobalVariables.ObjectivesIsSelected = true;
          if (obj.tag == "Resources") GlobalVariables.ResourcesIsSelected = true;
          if (obj.tag == "Item") GlobalVariables.ItemsIsSelected = true;
          if (obj.tag == "NPCs") GlobalVariables.NPCsIsSelected = true;

          //Debug.Log(string.Format("Obj.layer: {0}\tLayer: {1}", LayerMask.LayerToName(obj.layer), Layer));

          if (GlobalVariables.IsCtrlPressed) ReadyToFix = true;
          else {
               Debug.Log(string.Format("No continue insert"));
               ReadyToFix = false;
               RotationItem.y = 0;
               obj = null;
               GlobalVariables.GridHeight = 0;
               GlobalVariables.GridWidth = 0;
          }

          //Debug.Log(string.Format("ReadyToFix: {0}", ReadyToFix));
     }

     /// <summary>
     /// Permite rotar el elemento seleccionado
     /// </summary>
     private void RotateItem() {

          if (GlobalVariables.IsShiftPressed) RotationItem.y -= 90;
          else RotationItem.y += 90;

          if (RotationItem.y >= 360 || RotationItem.y <= -360) RotationItem.y = 0;

          obj.transform.rotation = Quaternion.Euler(RotationItem);

          //Debug.Log(string.Format("Rotation: {0}", RotationItem));
     }

     /// <summary>
     /// Lanza un RayCast para obtener la posición del terreno a la que apunta el ratón
     /// </summary>
     /// <returns>Vector3</returns>
     private Vector3 RayCast() {
          RaycastHit hitInfo;
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          //Debug.Log(string.Format("RayCast: {0}\tpos: {1}", Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain")), hitInfo.point));
          if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain")))
               return new Vector3(hitInfo.point.x, hitInfo.point.y + YItemPosition, hitInfo.point.z);
          else return new Vector3(0, 0, 0);
     }

     /// <summary>
     /// Inicializa el elemento a insertar y lo va desplazando por el mapa, según donde apunte el ratón
     /// </summary>
     private void ShowItem() {
          Vector3 pos = RayCast();
          //Debug.Log(string.Format("ShowItem\tpos: {0}\tobj; {1}", pos, obj));
          if (pos != new Vector3(0, 0, 0)) {
               if (obj == null) {
                    obj = Instantiate(ItemToFix, pos, Quaternion.Euler(RotationItem)) as GameObject;
                    Layer = LayerMask.LayerToName(obj.layer);
                    obj.layer = LayerMask.NameToLayer("Default");
                    ChangeLayerRecursive(obj.transform, "Default");
                    //Debug.Log(string.Format("Create Obj: {0}\tLayer: {1}\tNew layer: {2}", obj.name, Layer, LayerMask.LayerToName(obj.layer)));
               }
               Debug.Log(string.Format("Pos: {0}\tYpos: {1}", pos, YItemPosition));
               obj.transform.position = pos;
          }
     }

     /// <summary>
     /// Modifica los layers de un elemento de forma recursa
     /// </summary>
     /// <param name="trans">Elemento a transformar</param>
     /// <param name="name">Nombre del nuevo layer</param>
     private void ChangeLayerRecursive(Transform trans, string name) {
          foreach (Transform child in trans) {
               if (child.gameObject.layer != LayerMask.NameToLayer("Water")) {
                    child.gameObject.layer = LayerMask.NameToLayer(name);
                    ChangeLayerRecursive(child, name);
               }
          }
     }
}
