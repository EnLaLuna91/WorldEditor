﻿using UnityEngine;
using UnityEngine.UI;

public class loadTerrain : MonoBehaviour {
     public GameObject Terrain;

     private Button btn;

     private void Start() {
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
     }

     private void TaskOnClick() {
          //Debug.Log(string.Format("Terrain Is Load? {0}", GlobalVariables.TerrainIsLoad));
          if (GlobalVariables.TerrainIsLoad) RemoveTerrain();

          if (Terrain != null) {
               Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
               Instantiate(Terrain, pos, Quaternion.identity);
               GlobalVariables.TerrainIsLoad = true;
          }
     }

     private void RemoveTerrain() {
          GameObject map = GameObject.FindGameObjectWithTag("Terrain");
          //Debug.Log(string.Format("Terrain name: {0}", map.name));
          Destroy(map);
          GlobalVariables.TerrainIsLoad = false;
          RemoveAllItemsOnMap();
          RemoveAllFXsOnMap();
     }

     private void RemoveAllItemsOnMap() {
          GameObject[] objectives = GameObject.FindGameObjectsWithTag("Objectives");
          foreach(GameObject item in objectives) {
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

     private void RemoveAllFXsOnMap() {
          GameObject[] items = GameObject.FindGameObjectsWithTag("FX");
          foreach (GameObject item in items) {
               //Debug.Log(string.Format("FX name: {0}", item.name));
               Destroy(item);
          }
          GlobalVariables.FxIsSelected = false;
     }
}
