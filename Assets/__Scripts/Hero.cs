using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class Hero : MonoBehaviour
{
    public float projectileSpeed;
    public float fireRate;
    public MonumentColor currentAmmoColor;
    public GameObject projectile;
    public ParticleSystem dieEffect;
    float lastShoot;
    bool rotate;
    [HideInInspector]
    public Monument activeMonument;
    ControlPanel nerbyPanel;
    Transform firePointLeft;
    Transform firePointRight;
    Image fireButton;
    Main main;
    GameObject target;

    void Awake()
    {
        fireButton = GameObject.Find("FireButton").GetComponent<Image>();
        firePointRight = transform.Find("FirePoint_right").GetComponent<Transform>();
        firePointLeft = transform.Find("FirePoint_left").GetComponent<Transform>();
        main = Camera.main.GetComponent<Main>();
    }

    void Update()
    {
        fireButton.color = Main.WhichColor(currentAmmoColor);
        if (rotate) transform.Rotate(Vector3.forward *5* Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        switch (go.tag)
        {
            case "ProjectileEnemy":
                Projectile proj = go.GetComponent<Projectile>();
                TakeDamage();
                break;
            case "AmmoBox":
                AmmoBox am = go.GetComponent<AmmoBox>();
                currentAmmoColor = am.ammoColor;
                am.Take();
                break;
            case "Controler":
                nerbyPanel = go.GetComponent<ControlPanel>();
                break;
            case "Monument":
                Monument mon = go.GetComponent<Monument>();
                if (activeMonument != null) if (activeMonument != mon)
                    {
                        activeMonument.isActive = false;
                        activeMonument.isFollowing = false;
                    }
                activeMonument = mon;
                activeMonument.isActive = true;
                activeMonument.isDocked = false;
                break;
            case "Target":
                main.EndLevel();
                go.GetComponent<ParticleSystem>().Play();
                Main.IsPlaying = false;
                Destroy(GetComponent<Rigidbody2D>());
                rotate = true;
                target = go;
                StartCoroutine("FlyingIntoTarget");
                break;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        switch (go.tag)
        {
            case "Controler":
                nerbyPanel = null;
                break;
        }
    }

    public void Shoot()
    {
        if (Time.time - lastShoot > fireRate)
        {
            if (Main.IsPlaying)
            {
                gameObject.GetComponent<Animator>().Play("ShotAnimation");
                CameraShaker.Instance.ShakeOnce(0.1f, 4f, .1f, 1f);
                GameObject go1 = Instantiate<GameObject>(projectile, firePointLeft.position, transform.rotation);
                GameObject go2 = Instantiate<GameObject>(projectile, firePointRight.position, transform.rotation);
                Rigidbody2D rb1 = go1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = go2.GetComponent<Rigidbody2D>();
                SpriteRenderer sp1 = go1.GetComponent<SpriteRenderer>();
                SpriteRenderer sp2 = go2.GetComponent<SpriteRenderer>();
                Projectile proj1 = go1.GetComponent<Projectile>();
                Projectile proj2 = go2.GetComponent<Projectile>();
                proj1.projectileColor = currentAmmoColor;
                proj2.projectileColor = currentAmmoColor;
                rb1.AddForce(firePointLeft.up * projectileSpeed, ForceMode2D.Impulse);
                rb2.AddForce(firePointRight.up * projectileSpeed, ForceMode2D.Impulse);
                firePointLeft.GetComponent<ParticleSystem>().Play();
                firePointRight.GetComponent<ParticleSystem>().Play();
                lastShoot = Time.time;
            }
        }
    }

    public void TakeDamage()
    {
        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        ParticleSystem dF = Instantiate(dieEffect);
        dF.transform.position = gameObject.transform.position;
        dF.Play();
        main.defeat();
        Destroy(gameObject);
    }

    public void Interact()
    {
        if (nerbyPanel != null && Main.IsPlaying) nerbyPanel.Interact();
        else if (activeMonument != null && Main.IsPlaying) activeMonument.Interaction() ;
    }

    IEnumerator FlyingIntoTarget()
    {
        while (Vector2.Distance(transform.position, target.transform.position) > 0.002f)
        {
            transform.position = Vector2.Lerp(transform.position, target.transform.position, 0.5f * Time.deltaTime);
            yield return null;
        }
        transform.position = target.transform.position;
    }
}
