using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speedMultiplier = 1;
    float bannedAngle;
    Joystick joystick;
    Transform camera;
    Vector2 movement;
    Rigidbody2D rb;

    void Awake()
    {
        joystick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
        camera = GameObject.Find("CameraHolder").GetComponent<Transform>();
        bannedAngle = Vector3.Angle(joystick.Direction, new Vector2(0, 1));
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        bool isRotating = true;
        if (joystick.Horizontal >= 0.2f || joystick.Vertical >= 0.2f)
        {
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;
        } 
        else if (joystick.Horizontal <= -0.2f || joystick.Vertical <= -0.2f)
        {
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
            isRotating = false;
        }
        
        float  characterAngle = Vector3.Angle(joystick.Direction, new Vector2(0,1));
        if (bannedAngle != characterAngle && Main.IsPlaying)
        {
            if (isRotating)
            {
                if (joystick.Horizontal > 0) 
                {
                    Quaternion rotation = Quaternion.AngleAxis(characterAngle, -transform.forward);
                    rotation = Quaternion.Lerp(transform.rotation, rotation, 0.2f);
                    transform.rotation = rotation;
                }
                else
                {
                    Quaternion rotation = Quaternion.AngleAxis(characterAngle, transform.forward);
                    rotation = Quaternion.Lerp(transform.rotation, rotation, 0.2f);
                    transform.rotation = rotation;
                }
            }
            
        }
        transform.position = pos;
        camera.transform.position = new Vector3(pos.x, pos.y, -10f);
        if (Main.IsPlaying) rb.MovePosition(rb.position + movement * speedMultiplier * Time.fixedDeltaTime);
    }

    public void Boost()
    {
        if (speedMultiplier == 1&& Main.IsPlaying) speedMultiplier = 2.5f;
        else if(Main.IsPlaying) speedMultiplier = 1;
    }
}
