using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaverData {
     public GameObject Terrain;
     public List<ObjectivesData> Objectives;
     public List<ResourcesData> Resources;
     public List<EffectsData> Effects;

     public List<ItemsData> Items;
     public List<NPCsData> NPCs;
     public Material Sky;
}

[Serializable]
public class ObjectivesData {
     public GameObject Objective;
     public Vector3 Position;
     public Quaternion Rotation;
}

[Serializable]
public class ResourcesData {
     public GameObject Resouce;
     public Vector3 Position;
     public Quaternion Rotation;
     public Material Material;
}

[Serializable]
public class EffectsData {
     public GameObject Effect;
     public Vector3 Position;
     public Quaternion Rotation;
}

[Serializable]
public class ItemsData {
     public GameObject Item;
     public Vector3 Position;
     public Quaternion Rotation;
     public Material Material;
}

[Serializable]
public class NPCsData {
     public GameObject NPC;
     public Vector3 Position;
     public Quaternion Rotation;
     public Material Material;
}