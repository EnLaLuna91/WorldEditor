using SerializerFree;
using SerializerFree.Serializers;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Saver : MonoBehaviour {

     private SaverData data = new SaverData();
     
     /// <summary>
     /// Serializa el escenario
     /// </summary>
     /// <returns>string con la escena serializada</returns>
     public string SerializeScenary() {
          //Debug.Log(string.Format("Path: {0}", path));

          data.SceneName = GlobalVariables.SceneName;
          data.QuestDesc = GlobalVariables.QuestDesc;
          
          data.Terrain = Terrain.activeTerrain.name.Split('(')[0];
          data.Sky = GlobalVariables.SkyName;
          GetObjectives();
          GetResources();
          GetEffects();
          GetItems();
          GetNPCs();

          return Serialize();

          //Debug.Log(string.Format("Serializer: \n{0}", output));

          

     }     

     private string Serialize() {
          return Serializer.Serialize(data, new UnityJsonSerializer());
     }

     //private void WriteJSON(string path, string output) {
     //     using (FileStream fs = new FileStream(path, FileMode.Create)) {
     //          using (StreamWriter writer = new StreamWriter(fs)) {
     //               writer.Write(output);
     //          }
     //     }
     //}

     #region Get Elements

     /// <summary>
     /// Obtiene los elementos de la escena, dependiendo del tipo de elemento
     /// </summary>

     private void GetObjectives() {
          List<ObjectivesData> newObjectives = new List<ObjectivesData>();
          GameObject[] objectives = GameObject.FindGameObjectsWithTag("Objectives");
          foreach (GameObject item in objectives) {
               ObjectivesData obj = new ObjectivesData {
                    Objective = item.name.Split('(')[0],
                    Tag = item.tag,
                    Position = item.transform.position,
                    Rotation = item.transform.rotation,
                    Material = MatData(item)
               };
               newObjectives.Add(obj);
          }
          data.Objectives = newObjectives;
     }
     
     private void GetResources() {
          List<ResourcesData> newResources = new List<ResourcesData>();
          GameObject[] resources = GameObject.FindGameObjectsWithTag("Resources");
          foreach (GameObject item in resources) {
               ResourcesData obj = new ResourcesData {
                    Resource = item.name.Split('(')[0],
                    Tag = item.tag,
                    Position = item.transform.position,
                    Rotation = item.transform.rotation,
                    Material = MatData(item)
               };
               newResources.Add(obj);
          }
          data.Resources = newResources;
     }

     private void GetEffects() {
          List<EffectsData> newEffects = new List<EffectsData>();
          GameObject[] effects = GameObject.FindGameObjectsWithTag("FX");
          foreach (GameObject item in effects) {
               EffectsData obj = new EffectsData {
                    Effect = item.name.Split('(')[0],
                    Position = item.transform.position,
                    Rotation = item.transform.rotation
               };
               newEffects.Add(obj);
          }
          data.Effects = newEffects;
     }
     
     private void GetItems() {
          List<ItemsData> newItems = new List<ItemsData>();
          GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
          foreach (GameObject item in items) {
               ItemsData obj = new ItemsData {
                    Item = item.name.Split('(')[0],
                    Tag = item.tag,
                    Position = item.transform.position,
                    Rotation = item.transform.rotation,
                    Material = MatData(item)
               };
               newItems.Add(obj);
          }
          data.Items = newItems;
     }

     private void GetNPCs() {
          List<NPCsData> newNPCs = new List<NPCsData>();
          GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPCs");
          foreach (GameObject item in npcs) {
               NPCsData obj = new NPCsData {
                    NPC = item.name.Split('(')[0],
                    Tag = item.tag,
                    Position = item.transform.position,
                    Rotation = item.transform.rotation
               };
               newNPCs.Add(obj);
          }
          data.NPCs = newNPCs;
     } 

     #endregion

     
     /// <summary>
     /// Obtiene el material de un objeto en concreto, para poder serializar ese material
     /// </summary>
     /// <param name="item">GameObjet elemento a obtener la textura</param>
     /// <returns>MaterialData</returns>
     private MaterialData MatData(GameObject item) {
          Material material = item.GetComponentInChildren<Renderer>().materials[0];
          MaterialData mat;
          if (ExistItem(item)) {
               if (material.name.Split('(')[0].TrimEnd() == "ColorMat") {
                    mat = new MaterialData {
                         Type = "Color",
                         Name = material.name.Split('(')[0].TrimEnd(),
                         MatColor = material.color
                    };
               } else {
                    mat = new MaterialData {
                         Type = "Texture",
                         Name = material.name.Split('(')[0].TrimEnd(),
                         MatColor = material.color
                    };
               }
               //Debug.Log(string.Format("Mat Type {0}\tName {1}\tColor {2}", mat.Type, mat.Name, mat.MatColor));
          } else mat = null;
          //Debug.Log(string.Format("Mat {0}", mat));
          return mat;
     }

     private bool ExistItem(GameObject item) {
          return GlobalVariables.ElementsToChangeTexture.Exists(x => x.ItemID == item.GetInstanceID());
     }
}

