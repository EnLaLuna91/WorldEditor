using UnityEngine;

public class LoadHero : MonoBehaviour {
     
     private GameObject levelStartPoint;
     private Hero _hero;

     /// <summary>
     /// Añade el heroe en la escena y cambia las camaras
     /// </summary>
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

          Destroy(heroPrefab);

          LoadCamera();
          ChangeCameras();

     }

     /// <summary>
     /// Obtiene la posición del "HeroSpamPoint"
     /// </summary>
     /// <returns>Booleano, dependiendo si lo ha encontrado o no</returns>
     private bool GetStartPoint() {
          levelStartPoint = GameObject.Find("HeroSpamPoint");
          //Debug.Log(string.Format("HeroSpamPoint\nPosition: {0}\tRotation: {1}", levelStartPoint.transform.position, levelStartPoint.transform.rotation));
          if (levelStartPoint != null) {
               return true;
          }
          return false;
     }

     /// <summary>
     /// Carga la camara del Herore
     /// </summary>
     private void LoadCamera() {
          //GlobalVariables.EditorCamera = Camera.main;
          if (GlobalVariables.HeroCamera == null) {
               GlobalVariables.HeroCamera = Camera.Instantiate(Resources.Load<Camera>("Prefabs/Camera/HeroCamera"), levelStartPoint.transform.position, levelStartPoint.transform.rotation);
               GlobalVariables.HeroCamera.enabled = false;
               //Debug.Log(string.Format("LoadCamera\nHeroCamera.Pos: {0}\tHeroCamera.Rot: {1}", GlobalVariables.HeroCamera.transform.position, GlobalVariables.HeroCamera.transform.rotation));
          }          
     }

     /// <summary>
     /// Cambia la camara, para ver al heroe
     /// </summary>
     private void ChangeCameras() {
          GlobalVariables.EditorCamera.enabled = !GlobalVariables.EditorCamera.enabled;
          GlobalVariables.HeroCamera.enabled = !GlobalVariables.HeroCamera.enabled;
          GlobalVariables.HeroCamera.transform.position = new Vector3(levelStartPoint.transform.position.x, levelStartPoint.transform.position.y + 70, levelStartPoint.transform.position.z + 50);
          GlobalVariables.HeroCamera.transform.rotation = levelStartPoint.transform.rotation;
          
          //Debug.Log(string.Format("ChangeCameras\nHeroCamera.Pos: {0}\tHeroCamera.Rot: {1}", GlobalVariables.HeroCamera.transform.position, GlobalVariables.HeroCamera.transform.rotation));
     }
}
