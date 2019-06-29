using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class Move : MonoBehaviour
{
    public string portName = "COM5";
    public int baundRate = 9600;
    private SerialPort mySerial;

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    void Start()
    {
        mySerial = new SerialPort(portName, baundRate);
        mySerial.Open();

        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            mySerial.Write("a");
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
    void OnDestroy()
    {
        mySerial.Close();
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 8)
        {
            winText.text = "You Win!";
        }
    }
}