using UnityEngine;
using TouchScript.Gestures.TransformGestures;

public class Rotation : MonoBehaviour {
    public Transform camCtrl;
    public Transform cam;
    public ScreenTransformGesture einFingerGeste;
    public ScreenTransformGesture zweiFingerGeste;
    public float zoomSpeed;
    public float moveSpeed;
    public float speed;

    private void OnEnable() {
        einFingerGeste.Transformed += einFinger;
        zweiFingerGeste.Transformed += zweiFinger;
    }

    private void OnDisable() {
        einFingerGeste.Transformed -= einFinger;
        zweiFingerGeste.Transformed -= zweiFinger;
    }

    private void einFinger(object sender, System.EventArgs e) {
        if (Input.GetMouseButton(0))
        {
            camCtrl.localRotation *= Quaternion.Euler(0,
                                                     einFingerGeste.DeltaPosition.x / Screen.width * speed,
                                                    -einFingerGeste.DeltaPosition.y / Screen.height * speed);
        } 
        if (Input.GetMouseButton(1))
        {
            camCtrl.localRotation *= Quaternion.Euler((-einFingerGeste.DeltaPosition.x / Screen.width + einFingerGeste.DeltaPosition.y / Screen.height) * speed, 0, 0);
        }  
        if (Input.GetMouseButton(2))
        {
            var camPos = cam.position.normalized;
            camCtrl.localPosition += new Vector3(0, -einFingerGeste.DeltaPosition.y / Screen.height * moveSpeed, -einFingerGeste.DeltaPosition.x / Screen.width * moveSpeed);
        }  
    }

    private void zweiFinger(object sender, System.EventArgs e) {
        camCtrl.localRotation *= Quaternion.Euler(zweiFingerGeste.DeltaRotation,
                                                 zweiFingerGeste.DeltaPosition.x / Screen.width * speed,
                                                 -zweiFingerGeste.DeltaPosition.y / Screen.height * speed);
        cam.transform.localPosition += Vector3.right * (zweiFingerGeste.DeltaScale - 1f) * - zoomSpeed;
    }

   

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cam.transform.localPosition += Vector3.right * Input.GetAxis("Mouse ScrollWheel") * - zoomSpeed;
        }
        //camCtrl.localPosition = cam.position.normalized;
    }
}
