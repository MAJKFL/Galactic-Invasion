using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEmmiter : MonoBehaviour
{
    /// <summary>
    /// Odpowiada za wyłączanie elementów za które odpowiada dok dock
    /// </summary>
    public bool isActive;
    public GameObject[] shieldsArray;
    public MonumentDock dock;
    GameObject marker;
    MonumentColor color;
    ParticleSystem particleSystem;
    
    void Awake()
    {
        color = dock.dockColor;
        marker = transform.GetChild(0).gameObject;
        SpriteRenderer markerC = marker.GetComponent<SpriteRenderer>();
        markerC.color = Main.WhichColor(color);
        particleSystem = gameObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (isActive != !dock.isActivated) particleSystem.Play();
        isActive = !dock.isActivated;
        for (int i = 0; shieldsArray.Length > i; i++)
        {
            shieldsArray[i].SetActive(isActive);
        }
        marker.SetActive(isActive);
    }
}
