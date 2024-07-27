using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject movePoint;

    float minFOV = 20f;
    float maxFOV = 160f;
    float sensitivity = 10f;

    Vector3 resetCam;

    // Start is called before the first frame update
    void Start()
    {
        resetCam = movePoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float fov = Camera.main.fieldOfView;
        fov += -Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFOV, maxFOV);
        Camera.main.fieldOfView = fov;

        MoveCamera();
    }

    void MoveCamera() {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(x,y);
        movePoint.transform.Translate(movement * 30 * Time.deltaTime);
    }
}
