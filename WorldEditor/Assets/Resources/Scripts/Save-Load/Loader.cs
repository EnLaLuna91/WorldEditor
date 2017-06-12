using SerializerFree;
using SerializerFree.Serializers;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Loader : MonoBehaviour {

     private GameController gameController;

     public Loader(GameController controller) {
          gameController = controller;
     }

     public void LoadData(string text) {
          //Debug.Log(string.Format("Path: {0}", path));
          //string text = ReadJson(path);

          //Debug.Log(string.Format("JSON:\n{0}", text));
          SaverData data = Deserialize(text);
          
          GlobalVariables.SceneName = data.SceneName;
          GlobalVariables.QuestDesc = data.QuestDesc;

          LoadTerrainFromJSON(data.Terrain);
          LoadSky(data.Sky);
          LoadObjectives(data.Objectives);
          LoadResources(data.Resources);
          LoadEffects(data.Effects);
          LoadItems(data.Items);
          LoadNPCs(data.NPCs);

          UpdateScene();
     }

     private string ReadJson(string path) {
          return System.IO.File.ReadAllText(path);
     }

     private SaverData Deserialize(string json) {
          return Serializer.Deserialize<SaverData>(json, new UnityJsonSerializer());
     }

     private void UpdateScene() {
          gameController.SetSceneName();
     }

     #region Load Elements

     #region Terrain
     private void LoadTerrainFromJSON(string terrainName) {
          LoadTerrain terrain = new LoadTerrain();
          terrain.RemoveTerrain();
          GameObject Terrain = Instantiate(Resources.Load<GameObject>("Prefabs/Maps/" + terrainName));
          terrain.SetTerrain(Terrain);
     }
     #endregion

     #region Sky
     private void LoadSky(string SkyName) {
          Material sky = FindSkyBox(SkyName);
          RenderSettings.skybox = sky;
          GlobalVariables.SkyIsSelected = true;
          GlobalVariables.SkyName = SkyName;
          //Debug.Log(string.Format("SkyName: {0}", SkyName));
     } 

     private Material FindSkyBox(string SkyName) {
          Material sky = Instantiate(Resources.Load<Material>("Materials/Skyboxes/" + SkyName));
          if (sky == null){
               sky = Instantiate(Resources.Load<Material>("Materials/Skyboxes/sky5X/sky5X_skyboxes/" + SkyName));             
          }
          //Debug.Log(string.Format("Sky: {0}", sky));
          return sky;
     }
     #endregion

     #region Objectives

     private void LoadObjectives(List<ObjectivesData> objectives) {
          foreach(var obj in objectives) {
               GameObject objective = Instantiate(Resources.Load<GameObject>("Prefabs/Buildings/" + obj.Objective));
               if (objective == null) {
                    objective = Instantiate(Resources.Load<GameObject>("Prefabs/" + obj.Objective));
               }
               GameObject objetivo = Instantiate(objective, obj.Position, obj.Rotation) as GameObject;
               objetivo.tag = obj.Tag;
               GlobalVariables.ObjectivesIsSelected = true;
               if (obj.Material.Type != "") LoadMaterial(objetivo, obj.Material);
          }
     }

     #endregion

     #region Resources

     private void LoadResources(List<ResourcesData> resources) {
          foreach (var res in resources) {
               GameObject resource = Instantiate(Resources.Load<GameObject>("Prefabs/Buildings/" + res.Resource));
               GameObject objetivo = Instantiate(resource, res.Position, res.Rotation) as GameObject;
               objetivo.tag = res.Tag;
               GlobalVariables.ResourcesIsSelected = true;
               if (res.Material.Type != "") LoadMaterial(objetivo, res.Material);
          }
     }

     #endregion

     #region Effects
     
     private void LoadEffects(List<EffectsData> effects) {
          foreach (var fx in effects) {
               GameObject resource = Instantiate(Resources.Load<GameObject>("Prefabs/FX/" + fx.Effect));
               GameObject objetivo = Instantiate(resource, fx.Position, fx.Rotation) as GameObject;
               GlobalVariables.FxIsSelected = true;
          }
     }

     #endregion

     #region Items

     private void LoadItems(List<ItemsData> items) {
          foreach (var it in items) {
               GameObject item = Instantiate(Resources.Load<GameObject>("Prefabs/Buildings/" + it.Item));
               GameObject elemento = Instantiate(item, it.Position, it.Rotation) as GameObject;
               elemento.tag = it.Tag;
               GlobalVariables.ItemsIsSelected = true;
               if (it.Material.Type != "") LoadMaterial(elemento, it.Material);
          }
     }

     #endregion

     #region NPCs

     private void LoadNPCs(List<NPCsData> npcs) {
          foreach (var n in npcs) {
               GameObject npc = Instantiate(Resources.Load<GameObject>("Prefabs/NPCs/" + n.NPC));
               GameObject pj = Instantiate(npc, n.Position, n.Rotation) as GameObject;
               pj.tag = n.Tag;
               GlobalVariables.NPCsIsSelected = true;
          }
     }

     #endregion

     private void LoadMaterial(GameObject obj, MaterialData mat) {
          if (mat.Type == "Color") {
               Material[] mats = obj.GetComponentInChildren<Renderer>().materials;
               Material material = Instantiate(Resources.Load<Material>("Materials/" + mat.Name));
               material.color = mat.MatColor;
               mats[0] = material;
               obj.GetComponentInChildren<Renderer>().materials = mats;
          } else if (mat.Type == "Texture") {
               Material[] mats = obj.GetComponentInChildren<Renderer>().materials;
               Material material = Instantiate(Resources.Load<Material>("Materials/" + mat.Name));
               mats[0] = material;
               obj.GetComponentInChildren<Renderer>().materials = mats;
          }
     }
     
     #endregion


          
}
