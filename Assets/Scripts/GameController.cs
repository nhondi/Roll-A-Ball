using System.Collections;
using System.Collections.Generic ;
using UnityEngine ;
using TMPro;

public enum DEBUGMODES
{
    NORMAL = 0,
    DISTANCE = 1,
    VISION = 2
}

public class GameController : MonoBehaviour {
    public static GameController instance;

    private LineRenderer lineRenderer;
    public TextMeshProUGUI DistanceToPickup;
    public List<GameObject> allPickups = new List<GameObject>(); // holds all pickups
    public Transform pickupParent;
    public Transform closestPickup;
    public Transform Player;

    public DEBUGMODES currentDebugModes = DEBUGMODES.NORMAL;

    private void Awake()
    {
        instance = this;
    }
    void Start () {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        for (int i = 0; i < pickupParent.childCount; i++) {
            allPickups.Add(pickupParent.GetChild(i).gameObject);
        }
    }
    void Update ()
    {
        if(lineRenderer)
        {
            lineRenderer.enabled = (currentDebugModes != DEBUGMODES.NORMAL);
        }

        switch (currentDebugModes)
        {
            case DEBUGMODES.NORMAL:
                return;

            case DEBUGMODES.DISTANCE:
                getClosestPickup();
                break;

            case DEBUGMODES.VISION:
                getIncomingPickup();
                break;
        }   
    }

    public void SwitchDebugModes()
    {
        int val = (int)currentDebugModes;
        if(val++ == (int)DEBUGMODES.VISION)
        {
            val = 0;
        }
        currentDebugModes = (DEBUGMODES)val++;
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

    void getIncomingPickup()
    {
        Rigidbody playerRigidbody = Player.GetComponent<Rigidbody>();
        Vector3 playerVelocity = playerRigidbody.velocity;

        if (playerVelocity.magnitude == 0)
        {
            return;
        }

        Transform incomingPickup = null;
        float minDistance = Mathf.Infinity;
        float onTrackThreshold = 0.95f; // Threshold for being "on track"

        for (int i = 0; i < allPickups.Count; i++)
        {
            allPickups[i].GetComponent<Renderer>().material.color = Color.white;

            if (allPickups[i] != incomingPickup)
            {
                allPickups[i].transform.Rotate(Vector3.up * 50f * Time.deltaTime);
            }

            Vector3 directionToPickup = (allPickups[i].transform.position - Player.position).normalized;
            float dotProduct = Vector3.Dot(playerVelocity.normalized, directionToPickup);

            if (dotProduct >= onTrackThreshold)
            {
                float distance = Vector3.Distance(Player.position, allPickups[i].transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    incomingPickup = allPickups[i].transform;
                }
            }
        }

        if (incomingPickup != null)
        {
            incomingPickup.GetComponent<Renderer>().material.color = Color.green;
            incomingPickup.LookAt(Player);

            lineRenderer.SetPosition(0, Player.position);
            lineRenderer.SetPosition(1, Player.position + playerVelocity);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }
    }


}