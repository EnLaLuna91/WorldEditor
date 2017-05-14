using UnityEditor;
using UnityEngine;

public class LoadHero : MonoBehaviour {
     
     private GameObject levelStartPoint;
     private Hero _hero;


     public void SetHero() {
          if (!GetStartPoint()) return;

          GameObject heroPrefab = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Fracsland/Resources/Media/UsingPrefabs/Hero.prefab", typeof(GameObject));          
          GameObject h = (GameObject) Instantiate(heroPrefab, levelStartPoint.transform.position, levelStartPoint.transform.rotation);
          GameObject heroTarget = GameObject.Instantiate((GameObject) AssetDatabase.LoadAssetAtPath("Assets/Fracsland/Resources/Media/UsingPrefabs/HeroTarget.prefab", typeof(GameObject)));
                    
          _hero = h.GetComponent<Hero>();
          _hero.target = heroTarget.GetComponent<HeroTarget>();
          heroTarget.transform.position = _hero.transform.position;
          StatsManager.instance.Setup();
          _hero.SetHealth(Hero.CurrentHealth);

     }

     private bool GetStartPoint() {
          levelStartPoint = GameObject.Find("HeroSpamPoint");
          if (levelStartPoint != null) {
               return true;
          }
          return false;
     }
}
