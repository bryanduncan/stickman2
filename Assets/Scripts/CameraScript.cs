using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    float yVel = 0.01f;
    float xVel = 0.01f;
    float smoothTime = 0.3f;
    float newY;
    float newX;
    float xAvg;
    float yAvg;
    // Start is called before the first frame update
    void Start()
    {
        xAvg = (0 + player1.transform.position.x);
        yAvg = (0 + player1.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        xAvg = (0 + player1.transform.position.x);
        yAvg = (0 + player1.transform.position.y);
        Camera.main.transform.position = new Vector3(newX, newY, Camera.main.transform.position.z);

        UpdateX();
        UpdateY();
    }

    public void UpdateY()
    {
        newY = Mathf.SmoothDamp(transform.position.y, yAvg, ref yVel, smoothTime);
        if (newY < 0)
            newY = 0;
        //print("newY: " + newY + "\n");
        //print("Camera Y: " + Camera.main.transform.position.y + "\n");
    }
    public void UpdateX()
    {
        newX = Mathf.SmoothDamp(transform.position.x, xAvg, ref xVel, smoothTime);
        if (newX < 0)
            newX = 0;
        else if (newX > 160)
            newX = 160;
    }

    /*
    void Start()
    {
        xAvg = (0 + player1.transform.position.x + player2.transform.position.x) / 3;
        yAvg = (0 + player1.transform.position.y + player2.transform.position.y) / 3;
    }

    // Update is called once per frame
    void Update()
    {
        xAvg = (0 + player1.transform.position.x + player2.transform.position.x) / 3;
        yAvg = (0 + player1.transform.position.y + player2.transform.position.y) / 3;
        Camera.main.transform.position = new Vector3(newX, newY, Camera.main.transform.position.z);
    }

    public void UpdateY()
    {
        newY = Mathf.SmoothDamp(transform.position.y, yAvg, ref yVel, smoothTime);
        if (newY < 0)
            newY = 0;
        //print("newY: " + newY + "\n");
        //print("Camera Y: " + Camera.main.transform.position.y + "\n");
    }
    public void UpdateX()
    {
        newX = Mathf.SmoothDamp(transform.position.x, xAvg, ref xVel, smoothTime);
    }
    */
}
