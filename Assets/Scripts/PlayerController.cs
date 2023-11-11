using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horsePower = 0;
    private const float turnSpeed = 50.0f;

    private float horizontalInput;
    private float verticalInput;

    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;

    [SerializeField] float speed;
    [SerializeField] float rpm;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;

    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;

    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (IsOnGround())
        {
            playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            transform.Rotate(Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);

            speed = Mathf.Round(playerRb.velocity.magnitude * 2.237f); //For kph, change 2.237 to 3.6
            speedometerText.SetText("Speed: " + speed + " mph");
            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.SetText("RPM: " + rpm);

        }
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
