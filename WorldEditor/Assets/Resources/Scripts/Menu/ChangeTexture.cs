using UnityEngine;
using UnityEngine.UI;

public class ChangeTexture : MonoBehaviour {
     public Material Texture;

     private Button btn;
     private bool ReadyToFix = false;
     private string TempTexture;
     private RawImage image;

     private void Start() {
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
          if (gameObject.name == "Color") {
               Texture = GlobalVariables.ColorMaterial;
          }
          image = gameObject.GetComponentInChildren<RawImage>();
     }

     private void TaskOnClick() {
          if (Texture != null) {
               ReadyToFix = true;
               GlobalVariables.FixTexture = true;
          }
          //Debug.Log(string.Format("ReadyToFix: {0}", ReadyToFix));
     }

     private void Update() {
          if (gameObject.name == "Color") {
               image.color = GlobalVariables.ColorMaterial.color;
               Texture = GlobalVariables.ColorMaterial;
          }
          if (ReadyToFix && Input.GetMouseButtonDown(0)) ChangeMaterial();    
     }

     private void ChangeMaterial() {
          GameObject item = RayCast();
          Debug.Log(string.Format("Item: {0}\t Material: {1}", item, Texture));
          if (item != null) {
               //Debug.Log(string.Format("Item.name: {0}\tItem.GetComponentInChildren<Renderer>().materials.Length: {1}", item.name, item.GetComponentInChildren<Renderer>().materials.Length));
               //int i = 0;
               //foreach (Material mat in item.GetComponentInChildren<Renderer>().materials) {
               //     Debug.Log(string.Format("Mat[{0}].name: {1}",i++, mat.name));
               //}
               Material[] mats = item.GetComponentInChildren<Renderer>().materials;
               mats[0] = Texture;
               item.GetComponentInChildren<Renderer>().materials = mats;
               //item.GetComponentInChildren<Renderer>().materials[0].shader = Texure.shader;
               ReadyToFix = false;
               GlobalVariables.FixTexture = false;
          }
     }

     private GameObject RayCast() {
          RaycastHit hitInfo;
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          //Debug.Log(string.Format("RayCast: {0}\tpos: {1}", Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Buildings")), hitInfo.point));
          if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Buildings"))) {
               return hitInfo.transform.gameObject;
          } else return null;
     }

}
