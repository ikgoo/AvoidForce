using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerProduct : MonoBehaviour
{

    public Transform target;
    public float angle;
    public Vector3 closs;
    void Start()
    {
        
    }

    public bool bmove = false;
    public Quaternion q;

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.up);
        Vector3 toOther = target.position - transform.position;
        //angle = (Vector2.Dot(forward, toOther));
        angle = -(90 - Vector3.Angle(forward, toOther));
        DebugX.Log(angle);
        closs = Vector3.Cross(forward, toOther);
        DebugX.Log(closs);
        //Debug.Log((Vector2.Dot(forward, toOther)));

        if(bmove)
        {
            q = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 3f);
        }
    }

    public void bttclac()
    {
        Vector3 forward = transform.TransformDirection(Vector3.up);
        Vector3 toOther = target.position - transform.position;

        closs = Vector3.Cross(forward, toOther);
        if (closs.z < 0)
            angle = -1 * angle;

        bmove = true;

    }


}
