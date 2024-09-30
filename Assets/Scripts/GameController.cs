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
    public float currentClosestDistance;

    void Start () {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        for (int i = 0; i < pickupParent.childCount; i++) {
            allPickups.Add(pickupParent.GetChild(i).gameObject);
        }
        currentClosestDistance = Mathf.Infinity;
    }
    void Update () {
        getClosestPickup();
    }


    void getClosestPickup() {
        for (int i = 0; i < allPickups.Count; i++) {
            allPickups[i].GetComponent<Renderer>().material.color = Color.white;
            float distance = Vector3.distance(allPickups[i].transform.position, Player.transform.position);

            if (distance < currentClosestDistance) {
                closestPickup = allPickups[i];
            }
                
        }
        closestPickup.GetComponent <Renderer>().material.color = Color.blue;
        lineRenderer.SetPosition(0, Player.position);
        lineRenderer.SetPosition(1, closestPickup.position);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

}