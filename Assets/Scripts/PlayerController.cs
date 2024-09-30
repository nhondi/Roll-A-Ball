using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {

    public Vector2 moveValue;
    public float speed;
    private int count;
    private int numPickups = 5;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI velocityText;
    public float velocityValue;
    public Vector3 lastPosition;


    void Start() {
        count = 0;
        winText.text = "";
        SetCountText ();
        velocityText.text = velocityValue.ToString("0.00");
        velocityValue = 0;
        lastPosition = Vector3.zero;
    }

    void OnMove (InputValue value) {
        moveValue = value.Get<Vector2 >();
    }

    void FixedUpdate() {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y) ;

        GetComponent <Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);

        PositionAndVelocity();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Pickup") {
            other.gameObject.SetActive(false);
            count+=1;
            SetCountText();
        }
    }

    private void SetCountText() {
        scoreText.text = "Score: " + count.ToString();
        if(count >= numPickups) {
            winText.text = "You win!";
        }
    }

    private void PositionAndVelocity() {
        currentPosition = transform.position
        if (currentPosition != lastPosition) {
            velocityValue = Vector3.Magnitude((currentPosition - lastPosition) / Time.deltaTime);
        }
        velocityText.text = velocityValue.ToString("0.00");
    }
}
