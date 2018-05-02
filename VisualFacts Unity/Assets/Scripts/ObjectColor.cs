using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColor : MonoBehaviour {

    public Material white;
    public Material gray;
    public Material sand;
    private Material org;

    public void setOrgMat(Material m)
    {
        org = m;
    }

    public void setMaterial(int i)
    {
        MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();

        switch (i)
        {
            case 1:
                
                foreach (MeshRenderer renderer in renderers)
                {
                    renderer.material = white;
                }
                break;
            case 2:
                foreach (MeshRenderer renderer in renderers)
                {
                    renderer.material = gray;
                }
                break;
            case 3:
                foreach (MeshRenderer renderer in renderers)
                {
                    renderer.material = sand;
                }
                break;
            case 4:
                foreach (MeshRenderer renderer in renderers)
                {
                    renderer.material = org;
                }
                break;
        }
    }
}
