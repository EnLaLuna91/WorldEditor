using UnityEngine;
using UnityEngine.UI;

public class SceneNameQuestDesc : MonoBehaviour {

     private Text SceneName;
     private Text QuestDesc;
     

     private void Inicialize() {
          SceneName = GameObject.Find("ZoneName").GetComponent<Text>();
          QuestDesc = GameObject.Find("MainGoal").GetComponent<Text>();
     }

     public void UpdateText() {
          Inicialize();

          //Debug.Log(string.Format("Set name in game\tScne.name: {0}\tDesc: {1}", GlobalVariables.SceneName, GlobalVariables.QuestDesc));
          //Debug.Log(string.Format("SceneName: {0}\tQuestDesc: {1}", SceneName, QuestDesc));
          SceneName.text = GlobalVariables.SceneName;
          QuestDesc.text = GlobalVariables.QuestDesc;
          //Debug.Log(string.Format("Set name in game: {0}", SceneName.text));
     }
}
