using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaverData {
     public string SceneName;
     public string QuestDesc;

     //public GameObject Terrain;
     public string Terrain;

     public List<ObjectivesData> Objectives;
     public List<ResourcesData> Resources;
     public List<EffectsData> Effects;

     public List<ItemsData> Items;
     public List<NPCsData> NPCs;

     //public Material Sky;
     public string Sky;
}

[Serializable]
public class ObjectivesData {
     //public GameObject Objective;
     public string Objective;
     public string Tag;
     public Vector3 Position;
     public Quaternion Rotation;
}

[Serializable]
public class ResourcesData {
     //public GameObject Resouce;
     public string Resource;
     public string Tag;
     public Vector3 Position;
     public Quaternion Rotation;
     public Material Material;
}

[Serializable]
public class EffectsData {
     //public GameObject Effect;
     public string Effect;
     public Vector3 Position;
     public Quaternion Rotation;
}

[Serializable]
public class ItemsData {
     //public GameObject Item;
     public string Item;
     public string Tag;
     public Vector3 Position;
     public Quaternion Rotation;
     public Material Material;
}

[Serializable]
public class NPCsData {
     //public GameObject NPC;
     public string NPC;
     public string Tag;
     public Vector3 Position;
     public Quaternion Rotation;
     public Material Material;
}