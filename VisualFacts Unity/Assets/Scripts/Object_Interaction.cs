using UnityEngine;
using System.Collections.Generic;
using TouchScript.Gestures.TransformGestures;
using System.Collections;
using UnityEngine.UI;

public class Object_Interaction : MonoBehaviour {
    public Transform camControl;
    public GameObject objectCamera;
    public GameObject shaderCamera;
    public ScreenTransformGesture einFingerGeste;
    public ScreenTransformGesture zweiFingerGeste;
    public float turnSpeed;
    public float zoomSpeed;
    public float moveSpeed;
    public float rotationSpeed;
    public GameObject objectTafel;
    public GameObject kreuz;
    public GameObject frontButton;
    public Material plane_dots;

    private Transform camTransform;
    private Transform tafel;
    private List<Vector3> punkte = new List<Vector3>();
    private bool frontActive = false;
    private Quaternion camRot = Quaternion.Euler(0,0,0);

    public bool frontMode = false;

    //für Rotation
    private float animationLength = 1.5f;
    private bool animating = false;
    private Quaternion Left = Quaternion.FromToRotation(Vector3.up, Vector3.left);
    private Quaternion Right = Quaternion.FromToRotation(Vector3.up, Vector3.right);
    private Quaternion Up = Quaternion.FromToRotation(Vector3.up, Vector3.forward);
    private Quaternion Down = Quaternion.FromToRotation(Vector3.up, Vector3.back);
    private Quaternion LastTarget;
    private float timer;
    private Vector3 lastPos = Vector3.zero;
    

    void Start()
    {
        timer = Time.time;
        camTransform = objectCamera.transform;
        tafel = objectTafel.transform;
        zentrum();
        frontButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
    }

    private void OnEnable()
    {
        einFingerGeste.Transformed += einFinger;
        zweiFingerGeste.Transformed += zweiFinger;
        
    }

    private void OnDisable()
    {
        einFingerGeste.Transformed -= einFinger;
        zweiFingerGeste.Transformed -= zweiFinger;
        
    }

    public void resetFront()
    {
        frontActive = false;
        frontButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        punkte = new List<Vector3>();
        frontMode = true;
    }

    public void RotateMitWuerfel(string dir)
    {
        if (animating)
        {
            StopAllCoroutines();
            camControl.rotation = LastTarget;
        }

        animating = true;
        switch (dir)
        {
            case "left":
                LastTarget = (Quaternion.FromToRotation(camControl.forward, camControl.right) * camControl.rotation);
                break;
            case "right":
                LastTarget = (Quaternion.FromToRotation(camControl.right, camControl.forward) * camControl.rotation);
                break;
            case "up":
                LastTarget = (Quaternion.FromToRotation(camControl.up, camControl.forward) * camControl.rotation);
                break;
            case "down":
                LastTarget = (Quaternion.FromToRotation(camControl.forward, camControl.up) * camControl.rotation);
                break;
            default:
                //animating = false;
                return;
        }

        StartCoroutine(Turn(LastTarget));
    }

    public IEnumerator Turn(Quaternion target)
    {
        float starttime = Time.time;
        float t = 0f;

        while (Time.time - starttime <= animationLength-1.0f)
        {
            t = (Time.time - starttime) / animationLength;
            camControl.rotation = Quaternion.Lerp(camControl.rotation,target, t);
            yield return null;
        }
        camControl.rotation = target;
        animating = false;
    }

    private void einFinger(object sender, System.EventArgs e)
    {
		/*RaycastHit hit;
		var dist = 1f;
		Ray ray = shaderCamera.GetComponent<Camera> ().ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit))
		{
			dist = hit.distance;
            
		}
		else
		{
			dist = 8f;
		}*/

		if (Input.GetMouseButton(0))
        {
            camControl.localRotation *= Quaternion.Euler(
				
				-einFingerGeste.DeltaPosition.y / Screen.height * turnSpeed,einFingerGeste.DeltaPosition.x / Screen.width * turnSpeed,0) ;
            //
            
        }
        if (Input.GetMouseButton(2))
        {

            var mouse = Input.mousePosition;
            var rot = 0f;
            if(mouse.x > Screen.width/2)
            {
                if (mouse.y > Screen.height / 2)
                {
                    rot = (-einFingerGeste.DeltaPosition.x / Screen.width + einFingerGeste.DeltaPosition.y / Screen.height);
                }
                else
                {
                    rot = (einFingerGeste.DeltaPosition.x / Screen.width + einFingerGeste.DeltaPosition.y / Screen.height);
                }
            }
            else
            {
                if (mouse.y > Screen.height / 2)
                {
                     rot = (-einFingerGeste.DeltaPosition.x / Screen.width - einFingerGeste.DeltaPosition.y / Screen.height);
                }
                else
                {
                    rot = (einFingerGeste.DeltaPosition.x / Screen.width - einFingerGeste.DeltaPosition.y / Screen.height);
                }
            }
            var radius = Mathf.Sqrt(Mathf.Pow(mouse.x - Screen.width / 2,2)+ Mathf.Pow(mouse.y - Screen.height / 2, 2));

            camControl.localRotation *= Quaternion.Euler(0,0,rot / radius * -rotationSpeed);
            
        }
        if (Input.GetMouseButton(1))
        {
            //:::::::::::::::::::Zwischenlösung::::::::::::::::::::
            /*camControl.transform.Translate (
                new Vector3 (0,
                    -einFingerGeste.DeltaPosition.y / Screen.height * moveSpeed,
                    -einFingerGeste.DeltaPosition.x / Screen.width * moveSpeed ),
                Space.Self);*/

            var plane = new Plane(objectCamera.transform.forward, objectTafel.transform.position);
            camControl.localPosition += camControl.localRotation * einFingerGeste.DeltaPosition * -moveSpeed / Screen.width * Mathf.Abs(plane.GetDistanceToPoint(objectCamera.transform.position))  ;
        }
        
    }

    private void zweiFinger(object sender, System.EventArgs e)
    {
        camControl.localRotation *= Quaternion.Euler(zweiFingerGeste.DeltaRotation,
                                                 zweiFingerGeste.DeltaPosition.x / Screen.width * rotationSpeed,
                                                 -zweiFingerGeste.DeltaPosition.y / Screen.height * rotationSpeed);
        camTransform.localPosition += Vector3.right * (zweiFingerGeste.DeltaScale - 1f) * -zoomSpeed;
    }

    public void ebene()
    {
        if (frontActive && !animating)
        {
            var plane = new Plane(punkte[0], punkte[1], punkte[2]);
            for (int i = 1; i <= 3; i++)
            {
                Destroy(GameObject.Find("" + i));
            }

            if (Vector3.Angle(plane.normal, -Vector3.forward) > 90)
            {
                tafel.rotation = Quaternion.FromToRotation(plane.normal, -Vector3.forward);
            }
            else
            {
                tafel.rotation = Quaternion.FromToRotation(plane.normal, Vector3.forward);
            }
            camControl.rotation = camRot;
            frontMode = false;
            Debug.Log("Objekt wurde begradigt");
        }
    }

    void save_rotation()
    {
        camRot = camControl.rotation;
    }

    public void zentrum()
    {
        var mittelwert = Vector3.zero;
        var count = 1;
        var pos = objectTafel.transform.position;
        Debug.Log("Mittelpunkt des Objekts festlegen");
        Debug.Log("Berechnung startet...");

        List<Matrix4x4> matrices = new List<Matrix4x4>();
        foreach (Transform transform in objectTafel.GetComponentsInChildren<Transform>())
        {
            matrices.Add(transform.localToWorldMatrix);
        }
        var meshes = objectTafel.GetComponentsInChildren<MeshFilter>();
        var meshCount = 0;
        foreach (MeshFilter meshFilter in meshes)
        {
            var vertecies = meshFilter.mesh.vertices;
            foreach (Vector3 vertecy in vertecies)
            {
                var nextPoint = matrices[meshCount].MultiplyPoint3x4(vertecy);
                mittelwert = (mittelwert * (count - 1) + nextPoint) / count;
                count++;
            }
            meshCount++;
        }
        Debug.Log("Abgeschlossen: "+ count + " Knoten registriert");
        objectTafel.transform.position -= mittelwert;
        camControl.position = Vector3.zero;

        set_camera_distance();
    }

    void set_camera_distance()
    {
        var bounds = new Bounds(Vector3.zero, Vector3.one);
        MeshRenderer[] renderers = objectTafel.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        var zoom = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        objectCamera.transform.localPosition = Vector3.forward * -zoom;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            kreuz.SetActive(true);
            timer = Time.time;
        }
        else
        {
            if(timer + 0.8f > Time.time)
            {
                kreuz.SetActive(true);
            }
            else
            {
                kreuz.SetActive(false);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (frontMode)
            {
                if (Vector3.Distance(Input.mousePosition, lastPos) < 0.01f)
                {
                    setFrontDot();
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
			RaycastHit hit;
			Ray ray = objectCamera.GetComponent<Camera> ().ScreenPointToRay(new Vector2(Screen.width/2.0f,Screen.height/2.0f));//mousePos
			if (Physics.Raycast (ray, out hit))
			{
                if (hit.distance > 0.05f)
                {
                    camTransform.localPosition += Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * hit.distance;
                }
                else
                {
                    if(Input.GetAxis("Mouse ScrollWheel") < 0)
                    {
                        camTransform.localPosition += Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * hit.distance;
                    }
                }
			}
			else
			{
				camTransform.localPosition += Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
			}
        }
        
		if (Input.GetKeyDown ("space")&&frontMode)
        {
            setFrontDot();
            
        }
        if (Input.GetKeyDown("f"))
        {
            ebene();
            //zentrum();
        }
        if (Input.GetKeyDown("b"))
        {
            zentrum();
        }
        if (Input.GetKeyDown("r"))
        {
            save_rotation();
        }
    }

    void setFrontDot()
    {
        RaycastHit hit;
        Ray ray = objectCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (punkte.Count < 3)
                {
                    var bounds = new Bounds(Vector3.zero, Vector3.one);
                    MeshRenderer[] renderers = objectTafel.GetComponentsInChildren<MeshRenderer>();
                    foreach (MeshRenderer renderer in renderers)
                    {
                        bounds.Encapsulate(renderer.bounds);
                    }
                    var sphere_size = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) * 0.05f;
                    punkte.Add(hit.point);
                    GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    dot.name = "" + punkte.Count;
                    dot.transform.position = hit.point;
                    dot.GetComponent<MeshRenderer>().material = plane_dots;
                    dot.transform.localScale *= sphere_size;
                    Destroy(dot.GetComponent<SphereCollider>());
                    Debug.Log("Position registriert");

                    if (punkte.Count == 3)
                    {
                        frontButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        frontActive = true;
                    }
                }
            }
        }
    }
}