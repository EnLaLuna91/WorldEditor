using UnityEngine;

public static class GlobalVariables {

     public static bool CanFixItem { get; set; }

     public static bool TerrainIsLoad { get; set; }
     public static bool ObjectivesIsSelected { get; set; }
     public static bool ResourcesIsSelected { get; set; }
     public static bool FxIsSelected { get; set; }
     
     public static bool ItemsIsSelected { get; set; }
     public static bool NPCsIsSelected { get; set; }
     public static bool SkyIsSelected { get; set; }
     public static bool MaterialIsSelected { get; set; }

     public static bool IsShiftPressed { get; set; }
     public static bool IsCtrlPressed { get; set; }

     public static bool YouWin { get; set; }
     public static bool GameOver { get; set; }

     public static Camera EditorCamera { get; set; }
     public static Camera HeroCamera { get; set; }

     public static bool inGameMode { get; set; }

     public static string SceneName { get; set; }
     public static string QuestDesc { get; set; }

     public static string SkyName { get; set; }

     public static int GridWidth { get; set; }
     public static int GridHeight { get; set; }

     public static bool FixTexture { get; set; }
}
