using UnityEngine;

public class LoadTerrain : MonoBehaviour {

     /// <summary>
     /// Añade un terreno a la escena
     /// </summary>
     /// <param name="Terrain">GameObject del terreno a insertar</param>
     public void SetTerrain(GameObject Terrain) {
          Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
          Instantiate(Terrain, pos, Quaternion.identity);
          GlobalVariables.TerrainIsLoad = true;
     }

     /// <summary>
     /// Elimna todos los componentes que tiene un mapa y el mapa que hay en la escena
     /// </summary>
     public void RemoveTerrain() {
          GameObject[] maps = GameObject.FindGameObjectsWithTag("Terrain");
          foreach (GameObject map in maps) {
               //Debug.Log(string.Format("Terrain name: {0}", map.name));
               Destroy(map);
          }
          GlobalVariables.TerrainIsLoad = false;
          RemoveAllItemsOnMap();
          RemoveAllFXsOnMap();
     }

     /// <summary>
     /// Elimina los componentes del mapa
     /// </summary>
     private void RemoveAllItemsOnMap() {
          GameObject[] objectives = GameObject.FindGameObjectsWithTag("Objectives");
          foreach (GameObject item in objectives) {
               Destroy(item);
          }
          GameObject[] resources = GameObject.FindGameObjectsWithTag("Resources");
          foreach (GameObject item in resources) {
               Destroy(item);
          }
          GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
          foreach (GameObject item in items) {
               Destroy(item);
          }
          GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPCs");
          foreach (GameObject item in npcs) {
               Destroy(item);
          }
          GlobalVariables.ObjectivesIsSelected = false;
          GlobalVariables.ResourcesIsSelected = false;
          GlobalVariables.ItemsIsSelected = false;
          GlobalVariables.NPCsIsSelected = false;
     }

     /// <summary>
     /// Elimina los efectos que hay en el mapa
     /// </summary>
     private void RemoveAllFXsOnMap() {
          GameObject[] items = GameObject.FindGameObjectsWithTag("FX");
          foreach (GameObject item in items) {
               //Debug.Log(string.Format("FX name: {0}", item.name));
               Destroy(item);
          }
          GlobalVariables.FxIsSelected = false;
     }
}
