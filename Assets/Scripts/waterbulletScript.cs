using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterbulletScript : MonoBehaviour
{
    public float speed = 0.15f;
    float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= 3)
            Destroy(gameObject);

        transform.Translate(speed,0,0);
    }
}
