using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ControlPanel : MonoBehaviour
{
    public bool startAsActive = true;
    public bool isActive = true;
    public Sentry[] sentryArray;
    public MonumentDock dock;
    Animator animator;
    ParticleSystem particleSystem;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        SpriteRenderer marker = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (dock != null) marker.color = Main.WhichColor(dock.dockColor);
        else marker.color = Color.white;
        particleSystem = gameObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (dock != null)
        {
            if (startAsActive) isActive = !dock.isActivated;
            else isActive = dock.isActivated;
        }

        if (isActive) animator.SetFloat("speedMultiplier", 1);
        else animator.SetFloat("speedMultiplier", 0);

        
    }

    public void Interact()
    {
        if (isActive)
        {
            CameraShaker.Instance.ShakeOnce(0.1f, 4f, .1f, 1f);
            particleSystem.Play();
            for (int i = 0; sentryArray.Length > i; i++)
            {
                sentryArray[i].gameObject.GetComponent<ParticleSystem>().Play();
                sentryArray[i].ChangeShooting();
            }

        }
    }
}
