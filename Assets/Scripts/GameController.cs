using System.Collections;
using System.Collections.Generic ;
using UnityEngine ;
using TMPro;

public class GameController : MonoBehaviour {

    private LineRenderer lineRenderer;
    public TextMeshProUGUI DistanceToPickup;
    public List<GameObject> allPickups = new List<GameObject>(); // holds all pickups
    public Transform pickupParent;
    public Transform closestPickup;
    public Transform Player;

    void Start () {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        for (int i = 0; i < pickupParent.childCount; i++) {
            allPickups.Add(pickupParent.GetChild(i).gameObject);
        }
    }
    void Update () {
        getClosestPickup();
    }


    void getClosestPickup() {
        for (int i = 0; i < allPickups.Count; i++) {
            allPickups[i].GetComponent<Renderer>().material.color = Color.white;
            float distance = Vector3.Distance(allPickups[i].transform.position, Player.position);

            if( closestPickup == null )
            {
                closestPickup = allPickups[i].transform;
            }

            if (distance < Vector3.Distance(Player.position, closestPickup.position)) {
                closestPickup = allPickups[i].transform;
            }
                
        }
        closestPickup.GetComponent <Renderer>().material.color = Color.blue;
        lineRenderer.SetPosition(0, Player.position);
        lineRenderer.SetPosition(1, closestPickup.position);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

}