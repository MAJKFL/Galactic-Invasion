using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public enum MonumentColor
{
    Red,
    Green,
    Blue,
    nothing
}
public class MonumentDock : MonoBehaviour
{
    /// <summary>
    /// Odpowiada za dokowanie Monumentu odpowiedniego koloru
    /// </summary>
    public MonumentColor dockColor;
    [HideInInspector]
    public bool isActivated;
    Monument currentDockedMonument;

    void Awake()
    {
        SpriteRenderer marker = transform.GetChild(0).GetComponent<SpriteRenderer>();
        marker.color = Main.WhichColor(dockColor);
    }

    void Update()
    {
        if (currentDockedMonument != null && currentDockedMonument.isDocked == false) {
                currentDockedMonument = null;
                isActivated = false;
                CameraShaker.Instance.ShakeOnce(0.3f, 4f, .1f, 1f);
        }
        
    }

    public MonumentColor GetDockColor()
    {
        return (dockColor);
    }

    public void Dock(Monument mon)
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        currentDockedMonument = mon;
        isActivated = true;
    }
}
