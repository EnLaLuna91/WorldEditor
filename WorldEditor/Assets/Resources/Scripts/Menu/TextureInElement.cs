using UnityEngine;

public class TextureInElement : MonoBehaviour {

     public string ItemName { get; set; }
     public int ItemID { get; set; }
     public Material ItemMaterial { get; set; }

     public override string ToString() {
          return string.Format("Item name: {0}\tItem ID: {1}\tMaterial name: {2}", ItemName, ItemID, ItemMaterial.name);
     }

}
