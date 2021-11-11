using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Monument : MonoBehaviour
{
    /// <summary>
    /// Służy do aktywowania doków
    /// </summary>
    public MonumentColor monumentColor;
    [HideInInspector]
    public bool isFollowing = false;
    [HideInInspector]
    public bool isActive = false;
    [HideInInspector]
    public bool isDocked = false;
    GameObject currentDock;
    GameObject player;
    Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        animator.SetBool("isUnderUsage", isActive);
        animator.SetBool("isDocked", isDocked);
        if (isFollowing)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) > 0.2f || Mathf.Abs(player.transform.position.y - transform.position.y) > 0.2f)
            {
                Vector2 pos = Vector2.Lerp(transform.position, player.transform.position, 0.03f);
                transform.position = pos;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        switch (go.tag)
        {
            case "MonumentDock":
                MonumentDock monD = go.GetComponent<MonumentDock>();
                if (monD.GetDockColor() == monumentColor) isFollowing = false; currentDock = go ;  StartCoroutine("FlyingIntoTarget") ;
                break;
            case "Hero":
                player = go;
                break;
        }
           
    }

    public void Interaction()
    {
        if (isFollowing) isFollowing = false;
        else isFollowing = true;
    }
    void Dock()
    {
        CameraShaker.Instance.ShakeOnce(0.4f, 4f, .1f, 1f);
        isActive = true;
        isDocked = true;
        Hero hero = player.GetComponent<Hero>();
        hero.activeMonument = null;
    }

    IEnumerator FlyingIntoTarget()
    {
        while (Vector2.Distance(transform.position, currentDock.transform.position) > 0.005f)
        {
            transform.position = Vector2.Lerp(transform.position, currentDock.transform.position, 1.5f * Time.deltaTime);
            yield return null;
        }
        transform.position = currentDock.transform.position;
        Dock();
        MonumentDock monD = currentDock.GetComponent<MonumentDock>();
        monD.Dock(gameObject.GetComponent<Monument>());
    }
}
