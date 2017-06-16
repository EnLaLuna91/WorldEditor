using UnityEngine;
using UnityEngine.UI;

public class ButtonAddTerrain : MonoBehaviour {
     public GameObject Terrain;

     private Button btn;
     private LoadTerrain terrainConf = new LoadTerrain();
     
     private void Start() {
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
     }

     private void TaskOnClick() {
          //Debug.Log(string.Format("Terrain Is Load? {0}", GlobalVariables.TerrainIsLoad));
          if (GlobalVariables.TerrainIsLoad) terrainConf.RemoveTerrain();

          if (Terrain != null) {
               terrainConf.SetTerrain(Terrain);
          }
     }

     
}
