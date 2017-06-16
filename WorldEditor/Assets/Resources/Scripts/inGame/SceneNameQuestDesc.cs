using UnityEngine;
using UnityEngine.UI;

public class SceneNameQuestDesc : MonoBehaviour {

     private Text SceneName;
     private Text QuestDesc;
     

     /// <summary>
     /// Busca los componentes necesarios para el cambio de nombre y objetivos de la misión
     /// </summary>
     private void Inicialize() {
          SceneName = GameObject.Find("ZoneName").GetComponent<Text>();
          QuestDesc = GameObject.Find("MainGoal").GetComponent<Text>();
     }

     /// <summary>
     /// Actualiza el texto que aparecerá dentro del juego
     /// </summary>
     public void UpdateText() {
          Inicialize();

          //Debug.Log(string.Format("Set name in game\tScne.name: {0}\tDesc: {1}", GlobalVariables.SceneName, GlobalVariables.QuestDesc));
          //Debug.Log(string.Format("SceneName: {0}\tQuestDesc: {1}", SceneName, QuestDesc));
          SceneName.text = GlobalVariables.SceneName;
          QuestDesc.text = GlobalVariables.QuestDesc;
          //Debug.Log(string.Format("Set name in game: {0}", SceneName.text));
     }
}
