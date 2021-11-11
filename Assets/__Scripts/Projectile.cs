using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 2f;
    public GameObject projectileImpactEffect;
    public GameObject wallDestroyEffect;
    [HideInInspector]
    public MonumentColor projectileColor;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Main.WhichColor(projectileColor);
        projectileImpactEffect.GetComponent<ParticleSystem>().startColor = Main.WhichColor(projectileColor);
        gameObject.GetComponent<ParticleSystem>().startColor = Main.WhichColor(projectileColor);
        Invoke("Die", lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        switch (go.tag)
        {
            case ("GreenWall"):
                if (projectileColor == MonumentColor.Green)
                {
                    CameraShaker.Instance.ShakeOnce(0.7f, 4f, .1f, 1f);
                    wallDestroyEffect.GetComponent<ParticleSystem>().startColor = Main.WhichColor(projectileColor);
                    GameObject dF = Instantiate(wallDestroyEffect);
                    dF.transform.position = go.transform.position;
                    dF.GetComponent<ParticleSystem>().Play();
                    Destroy(dF, 0.20f);
                    Destroy(go);
                }
                Die();
                break;

            case ("BlueWall"):
                if (projectileColor == MonumentColor.Blue) 
                {
                    CameraShaker.Instance.ShakeOnce(0.7f, 4f, .1f, 1f);
                    wallDestroyEffect.GetComponent<ParticleSystem>().startColor = Main.WhichColor(projectileColor);
                    GameObject bY = Instantiate(wallDestroyEffect);
                    bY.transform.position = go.transform.position;
                    bY.GetComponent<ParticleSystem>().Play();
                    Destroy(go);
                }
                Die();
                break;

            case ("RedWall"):
                if (projectileColor == MonumentColor.Red) 
                {
                    CameraShaker.Instance.ShakeOnce(0.7f, 4f, .1f, 1f);
                    wallDestroyEffect.GetComponent<ParticleSystem>().startColor = Main.WhichColor(projectileColor);
                    GameObject bA = Instantiate(wallDestroyEffect);
                    bA.transform.position = go.transform.position;
                    bA.GetComponent<ParticleSystem>().Play();
                    Destroy(go);
                }
                Die();
                break;

            default:
                Die();
                break;
        }
    }

    void Die()
    {
        if(gameObject.tag == "ProjectileEnemy") CameraShaker.Instance.ShakeOnce(0.01f, 4f, .1f, 1f);
        else CameraShaker.Instance.ShakeOnce(0.05f, 4f, .1f, 1f);
        GameObject dF = Instantiate(projectileImpactEffect);
        dF.transform.position = gameObject.transform.position;
        dF.GetComponent<ParticleSystem>().Play();
        Destroy(dF, 0.20f);
        Destroy(gameObject);
    }
}
