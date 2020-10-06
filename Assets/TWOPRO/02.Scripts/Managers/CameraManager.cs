using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    private void FixedUpdate()
    {

        MoveValue();

    }
    void MoveValue()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, speed * Time.deltaTime);  // Quaternion.Euler(0, Mathf.Lerp(transform.eulerAngles.y,player.eulerAngles.y,Time.deltaTime*2), 0);
    }
}
