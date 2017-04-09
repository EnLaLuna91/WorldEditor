using UnityEngine;

public static class GlobalVariables {

     public static bool CanFixItem { get; set; }
     public static GameObject ItemToDrag { get; set; }
     public static string CustomTag { get; set; }
     public static Vector3 RotationItem { get; set; }
     public static float YItemPosition { get; set; }
     
     public static bool TerrainIsLoad { get; set; }
     public static bool ObjectivesIsSelected { get; set; }
     public static bool ResourcesIsSelected { get; set; }
     public static bool FxIsSelected { get; set; }
     
     public static bool ItemsIsSelected { get; set; }
     public static bool NPCsIsSelected { get; set; }
     public static bool SkyIsSelected { get; set; }
     public static bool MaterialIsSelected { get; set; }
}
