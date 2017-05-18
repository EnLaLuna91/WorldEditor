using UnityEditor;
using UnityEngine;

public class LoadHero : MonoBehaviour {
     
     private GameObject levelStartPoint;
     private Hero _hero;


     public void SetHero() {
          Debug.Log(string.Format("SetHero"));

          if (!GetStartPoint()) return;

          GameObject heroPrefab = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Fracsland/Resources/Media/UsingPrefabs/Hero.prefab", typeof(GameObject));          
          GameObject h = (GameObject) Instantiate(heroPrefab, levelStartPoint.transform.position, levelStartPoint.transform.rotation);
          GameObject heroTarget = GameObject.Instantiate((GameObject) AssetDatabase.LoadAssetAtPath("Assets/Fracsland/Resources/Media/UsingPrefabs/HeroTarget.prefab", typeof(GameObject)));
                    
          _hero = h.GetComponent<Hero>();
          _hero.target = heroTarget.GetComponent<HeroTarget>();
          heroTarget.transform.position = _hero.transform.position;
          //StatsManager.instance.Setup();
          _hero.SetHealth(Hero.CurrentHealth);

          LoadCamera();
          ChangeCameras();

     }

     private bool GetStartPoint() {
          levelStartPoint = GameObject.Find("HeroSpamPoint");
          Debug.Log(string.Format("HeroSpamPoint\nPosition: {0}\tRotation: {1}", levelStartPoint.transform.position, levelStartPoint.transform.rotation));
          if (levelStartPoint != null) {
               return true;
          }
          return false;
     }

     private void LoadCamera() {
          //GlobalVariables.EditorCamera = Camera.main;
          if (GlobalVariables.HeroCamera == null) {
               GlobalVariables.HeroCamera = Camera.Instantiate((Camera) AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefabs/Camera/Camera.prefab", typeof(Camera)));
               //GlobalVariables.HeroCamera = (Camera) AssetDatabase.LoadAssetAtPath("Assets/Fracsland/Prefabs/Camera.prefab", typeof(Camera));
               //GameObject cameraPrefab = (Camera) AssetDatabase.LoadAssetAtPath("Assets/Fracsland/Prefabs/Camera.prefab", typeof(Camera);
               //HeroCamera = (Camera) Instantiate(cameraPrefab, levelStartPoint.transform.position, levelStartPoint.transform.rotation);
               GlobalVariables.HeroCamera.enabled = false;
               Debug.Log(string.Format("LoadCamera\nEditorCamera: {0}\tHeroCamera: {1}", GlobalVariables.EditorCamera, GlobalVariables.HeroCamera));
          }          
     }

     private void ChangeCameras() {
          GlobalVariables.EditorCamera.enabled = !GlobalVariables.EditorCamera.enabled;
          GlobalVariables.HeroCamera.enabled = !GlobalVariables.HeroCamera.enabled;
          Debug.Log(string.Format("ChangeCamera\nEditorCamera: {0}\tHeroCamera: {1}", GlobalVariables.EditorCamera.enabled, GlobalVariables.HeroCamera.enabled));
     }
}
