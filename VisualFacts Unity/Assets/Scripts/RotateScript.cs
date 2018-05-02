using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour {

    private float animationLength = 1.5f;
    private bool animating = false;
    private Quaternion Left = Quaternion.FromToRotation(Vector3.up, Vector3.left);
    private Quaternion Right = Quaternion.FromToRotation(Vector3.up, Vector3.right);
    private Quaternion Up = Quaternion.FromToRotation(Vector3.up, Vector3.forward);
    private Quaternion Down = Quaternion.FromToRotation(Vector3.up, Vector3.back);
    private Quaternion LastTarget;

    public bool animate_active()
    {
        return animating;
    }

    public void Rotate(string dir)
    {
        if (animating)
        {
            StopAllCoroutines();
            transform.rotation = LastTarget;
        }

        animating = true;
        switch (dir)
        {
            case "left":
                LastTarget = (Left * transform.rotation);
                break;
            case "right":
                LastTarget = (Right * transform.rotation);
                break;
            case "up":
                LastTarget = (Up * transform.rotation);
                break;
            case "down":
                LastTarget = (Down * transform.rotation);
                break;
            default:
                animating = false;
                return;
        }

        StartCoroutine(Turn(LastTarget));
    }

    public IEnumerator Turn(Quaternion target)
    {
        float starttime = Time.time;
        float t = 0f;

        while(Time.time - starttime <= animationLength)
        {
            t = (Time.time - starttime) / animationLength;
            transform.rotation = Quaternion.Lerp(transform.rotation, target, t);
            yield return null;
        }

        animating = false;
    }

}
