using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public MonumentColor ammoColor;
    Color color;

    void Awake()
    {
        SpriteRenderer marker = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Color color = Main.WhichColor(ammoColor);
        marker.color = color;
    }
    
    public Color GetColor()
    {
        return (color);
    }

    public void Take()
    {
        Destroy(gameObject);
    }
}
