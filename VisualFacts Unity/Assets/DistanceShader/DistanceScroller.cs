using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceScroller : MonoBehaviour {

    [HideInInspector]
    public float f1;
    [HideInInspector]
    public float f2;
    [HideInInspector]
    public bool on = false;
    public float delta = 0.001f;
    public Transform CamTransform;

    private Vector4 color;

    public Material mat;

    float t = 0;
    float d = 1;

    float lastFloat = 0f;

	// Use this for initialization
	void Start () {
        setSize();
        setColor(3);
 	}

    public void setColor(int c)
    {
        switch(c)
        {
            case 1:
                color = new Vector4(1f, 0f, 0f, 1f);
                break;
            case 2:
                color = new Vector4(0f, 1f, 0f, 1f);
                break;
            case 3:
                color = new Vector4(0f, 0f, 1f, 1f);
                break;
        }
    }

    public void setSize()
    {
        var bounds = new Bounds(Vector3.zero, Vector3.one);
        MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        mat.SetFloat("_Size",Mathf.Min(bounds.size.x, bounds.size.y, bounds.size.z));
    }

    public void setValue(Slider slide)
    {
        var value = slide.value;
        lastFloat = value;
        mat.SetFloat("_Distance", lastFloat);
    }

        // Update is called once per frame
    void Update ()
    {
        Vector3 v = CamTransform.position.normalized;
        mat.SetVector("_CamPos", new Vector4(-v.x, -v.y, -v.z, 0f));
        mat.SetVector("_ColorShader", color);

        if (on)
        {
            t += d * delta;
            d *= (t > 1f || t < 0f) ? -1 : 1;

            lastFloat = Mathf.Lerp(f1, f2, t);
            mat.SetFloat("_Distance", lastFloat);
            
            lastFloat = mat.GetFloat("_Distance");
        }
	}

    public void ChangePlaneLevel(int d)
    {
        switch (d)
        {
            case 0:
                lastFloat = 0f;
                break;
            case 1:
                lastFloat += 0.01f;
                break;
            case -1:
                lastFloat -= 0.01f;
                break;
        }

        mat.SetFloat("_Distance", lastFloat);
    }


}
