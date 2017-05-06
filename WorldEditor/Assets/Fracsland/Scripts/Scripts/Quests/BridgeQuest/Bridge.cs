using UnityEngine;
using System.Collections;
using Items;
using Utils;

/// <summary>
/// Handles the behaivoiur of the bridge
/// </summary>
public class Bridge : MonoBehaviour {

    public GameObject bridgeWoodPrefab;
    public static float BRIDGE_WIDTH = 6.5f;
    public static float BRIDGE_OFFSET = 0.2f;
    public GameObject[] drownNodes;

     public void constructBridge(int numerator, int denominator) {
          ////Vector3 bridgePosition = new Vector3(57.2f, 10.328f, 59.1f);
          //Vector3 bridgePosition = RayCast();
          //if (bridgePosition != new Vector3(0, 0, 0)) {
          //     float woodWidth = (BRIDGE_WIDTH - (denominator - 1) * BRIDGE_OFFSET) / denominator;
          //     bridgePosition.z -= woodWidth / 2;
          //     int numActiveParts = denominator - numerator;

          //     for (int i = 0; i < denominator; i++) {
          //          bridgePosition.z += (woodWidth + BRIDGE_OFFSET);
          //          GameObject wood = (GameObject) Instantiate(bridgeWoodPrefab, bridgePosition, this.transform.rotation);
          //          wood.transform.localScale = new Vector3(wood.transform.localScale.x, wood.transform.localScale.y, woodWidth);
          //          if (i >= numActiveParts) {
          //               wood.GetComponent<BridgeWood>().setTransparent(true);
          //          }
          //     }
          //}

     }

     public void DisableColliders()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

     private Vector3 RayCast() {
          RaycastHit hitInfo;
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          //Debug.Log(string.Format("RayCast: {0}", Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain"))));
          if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain")))
               return new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
          else return new Vector3(0, 0, 0);
     }
}
