using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenSprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector2 scale = transform.localScale * (cameraSize.y / spriteSize.y);
        if (cameraSize.x >= spriteSize.x * scale.x)
        { // Landscape (or equal)
            scale = transform.localScale * (cameraSize.x / spriteSize.x);
        }

        transform.position = Vector2.zero; // Optional
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
    }
}
