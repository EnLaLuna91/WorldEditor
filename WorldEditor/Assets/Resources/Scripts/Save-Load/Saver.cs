using System;
using SerializerFree;
using SerializerFree.Serializers;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Saver : MonoBehaviour {

     private SaverData data = new SaverData();
     
     public void StoreData(string path) {
          //Debug.Log(string.Format("Path: {0}", path));

          data.SceneName = GlobalVariables.SceneName;
          data.QuestDesc = GlobalVariables.QuestDesc;

          data.Terrain = GameObject.FindGameObjectWithTag("Terrain");
          data.Sky = RenderSettings.skybox;
          GetObjectives();
          GetResources();
          GetEffects();
          GetItems();
          GetNPCs();

          string output = Serializer.Serialize(data, new UnityJsonSerializer());

          //Debug.Log(string.Format("Serializer: \n{0}", output));

          WriteJSON(path, output);

     }     

     private void WriteJSON(string path, string output) {
          using (FileStream fs = new FileStream(path, FileMode.Create)) {
               using (StreamWriter writer = new StreamWriter(fs)) {
                    writer.Write(output);
               }
          }
     }

     private void GetObjectives() {
          List<ObjectivesData> newObjectives = new List<ObjectivesData>();
          GameObject[] objectives = GameObject.FindGameObjectsWithTag("Objectives");
          foreach (GameObject item in objectives) {
               ObjectivesData obj = new ObjectivesData {
                    Objective = item,
                    Position = item.transform.position,
                    Rotation = item.transform.rotation
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
                    Resouce = item,
                    Position = item.transform.position,
                    Rotation = item.transform.rotation
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
                    Effect = item,
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
                    Item = item,
                    Position = item.transform.position,
                    Rotation = item.transform.rotation
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
                    NPC = item,
                    Position = item.transform.position,
                    Rotation = item.transform.rotation
               };
               newNPCs.Add(obj);
          }
          data.NPCs = newNPCs;
     }     
}

