using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables {

     /// <summary>
     /// Determina si se puede o no insertar elementos en el mapa
     /// </summary>
     public static bool CanFixItem { get; set; }

     /// <summary>
     /// Este grupo determina si los elementos del Work Object han sido insertados o no
     /// </summary>
     public static bool TerrainIsLoad { get; set; }
     public static bool ObjectivesIsSelected { get; set; }
     public static bool ResourcesIsSelected { get; set; }
     public static bool FxIsSelected { get; set; }
     
     /// <summary>
     /// Este grupo deterimina si los elementos de la escena han sido insertados o no
     /// </summary>
     public static bool ItemsIsSelected { get; set; }
     public static bool NPCsIsSelected { get; set; }
     public static bool SkyIsSelected { get; set; }
     public static bool MaterialIsSelected { get; set; }

     /// <summary>
     /// Indican si hay una de las dos teclas, o las dos, activadas en un momento
     /// </summary>
     public static bool IsShiftPressed { get; set; }
     public static bool IsCtrlPressed { get; set; }

     /// <summary>
     /// Establece si el juegador a ganado o no
     /// </summary>
     public static bool YouWin { get; set; }
     public static bool GameOver { get; set; }

     /// <summary>
     /// Las camaras que se irán usando
     /// </summary>
     public static Camera EditorCamera { get; set; }
     public static Camera HeroCamera { get; set; }

     /// <summary>
     /// Si el juego esta en modo prueba de la escena o no
     /// </summary>
     public static bool inGameMode { get; set; }

     /// <summary>
     /// Nombre de la escena y objetivos del reto
     /// </summary>
     public static string SceneName { get; set; }
     public static string QuestDesc { get; set; }

     /// <summary>
     /// Nombre del SKyBox usado
     /// </summary>
     public static string SkyName { get; set; }

     /// <summary>
     /// Tamaño de la grid, ya que hay elementos que tienen su tamaño particular de la grid
     /// </summary>
     public static int GridWidth { get; set; }
     public static int GridHeight { get; set; }

     /// <summary>
     /// Configuración para el cambio de texturas y el guardado de estas
     /// </summary>
     public static bool FixTexture { get; set; }
     public static Material ColorMaterial { get; set; }
     public static List<TextureInElement> ElementsToChangeTexture { get; set; }
}
