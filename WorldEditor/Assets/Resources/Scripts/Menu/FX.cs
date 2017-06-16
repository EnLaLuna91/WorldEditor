using UnityEngine;
using UnityEngine.UI;

public class FX : MonoBehaviour {

     public GameObject Effect;
     public Vector3 ScaleFx = new Vector3(0.0f, 0.0f, 0.0f);
     public bool Win, Lose;
     
     public float YItemPosition;

     private Button btn;
     private Vector3 pos;
     private bool isInsert = false;
     private bool ReadyToFix = false;

     private void Start() {
          btn = GetComponent<Button>();
          btn.onClick.AddListener(TaskOnClick);
     }

     private void TaskOnClick() {
          if (Effect != null) {
               ReadyToFix = true;
          }
          //Debug.Log(string.Format("Activando {0}", gameObject.name));
     }

     private void Update() {
          if (Input.GetMouseButtonDown(0) && GlobalVariables.CanFixItem && ReadyToFix) InsertFX();

          if (isInsert && (GlobalVariables.YouWin || GlobalVariables.GameOver)) ActiveFX();
     }

     /// <summary>
     /// Inserta un FX en el mapa
     /// </summary>
     private void InsertFX() {
          RaycastHit hitInfo;
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain"));
          pos = new Vector3(hitInfo.point.x, hitInfo.point.y + YItemPosition, hitInfo.point.z);

          isInsert = true;
          GlobalVariables.FxIsSelected = true;

          ReadyToFix = false;

          if (GlobalVariables.IsCtrlPressed) ReadyToFix = true;
     }

     /// <summary>
     /// Determina el tipo de efecto, cuando se activará
     /// </summary>
     private void ActiveFX() {
          if (Win) {
               GameObject obj = Instantiate(Effect, pos, Quaternion.identity) as GameObject;
               obj.transform.localScale = ScaleFx;
          }
          if (Lose) {
               GameObject obj = Instantiate(Effect, pos, Quaternion.identity) as GameObject;
               obj.transform.localScale = ScaleFx;
          }
     }
}
