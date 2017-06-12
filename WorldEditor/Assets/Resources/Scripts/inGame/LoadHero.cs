using UnityEditor;
using UnityEngine;

public class LoadHero : MonoBehaviour {
     
     private GameObject levelStartPoint;
     private Hero _hero;


     public void SetHero() {
          Debug.Log(string.Format("SetHero"));

          if (!GetStartPoint()) return;

          

          GameObject heroPrefab = Instantiate(Resources.Load<GameObject>("Fracsland/Media/UsingPrefabs/Hero"));
          GameObject h = (GameObject) Instantiate(heroPrefab, levelStartPoint.transform.position, levelStartPoint.transform.rotation);
          GameObject heroTarget = GameObject.Instantiate(Resources.Load<GameObject>("Fracsland/Media/UsingPrefabs/HeroTarget"));
                    
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
               Camera cam = Instantiate(Resources.Load<Camera>("Prefabs/Camera/Camera"));
               GlobalVariables.HeroCamera = Camera.Instantiate(cam, levelStartPoint.transform.position, levelStartPoint.transform.rotation);
               GlobalVariables.HeroCamera.enabled = false;
               Debug.Log(string.Format("LoadCamera\nHeroCamera.Pos: {0}\tHeroCamera.Rot: {1}", GlobalVariables.HeroCamera.transform.position, GlobalVariables.HeroCamera.transform.rotation));
          }          
     }

     private void ChangeCameras() {
          GlobalVariables.EditorCamera.enabled = !GlobalVariables.EditorCamera.enabled;          
          GlobalVariables.HeroCamera.transform.position = new Vector3(levelStartPoint.transform.position.x, levelStartPoint.transform.position.y + 70, levelStartPoint.transform.position.z + 50);
          GlobalVariables.HeroCamera.transform.rotation = levelStartPoint.transform.rotation;
          GlobalVariables.HeroCamera.enabled = !GlobalVariables.HeroCamera.enabled;
          Debug.Log(string.Format("ChangeCameras\nHeroCamera.Pos: {0}\tHeroCamera.Rot: {1}", GlobalVariables.HeroCamera.transform.position, GlobalVariables.HeroCamera.transform.rotation));
     }
}
