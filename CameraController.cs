using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform playertransform;
    [SerializeField]
    Vector3 cameraPosition;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapsize;

    [SerializeField]
    float cameraspeed;
    float width;
    float height;

    void Start()
    {
        playertransform = GameObject.Find("Player").GetComponent<Transform>();

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;

        mapsize.x = width * 2;
        mapsize.y = height * 2;
    }

    void FixedUpdate()
    {
        CameraArea();
    }

    void CameraArea()
    {
        transform.position = Vector3.Lerp(transform.position, playertransform.position, cameraspeed * Time.deltaTime);

        float x = mapsize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -x + center.x, x + center.x);

        float y = mapsize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -y + center.y, y + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
