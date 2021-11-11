using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Sentry : MonoBehaviour
{
    public bool isShooting = true;
    public float shootDelay = 1;
    public float projectileSpeed = 5;
    public GameObject projectile;
    bool shot = true;
    Transform firePoint;
    Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        firePoint = transform.GetChild(0).GetComponent<Transform>();
    }

    void Update()
    {
        if (isShooting && shot)
        {
            Invoke("Shoot", shootDelay);
            shot = false;
        }
    }

    public void Shoot()
    {
        CameraShaker.Instance.ShakeOnce(0.01f, 4f, .1f, 1f);
        GameObject go1 = Instantiate<GameObject>(projectile, firePoint.position, transform.rotation);
        Rigidbody2D rb1 = go1.GetComponent<Rigidbody2D>();
        rb1.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
        animator.Play("Anim_Sentry_Shoot");
        muzzleEffect();
        shot = true;
    }

    void muzzleEffect()
    {
        ParticleSystem particles = firePoint.GetComponent<ParticleSystem>();
        particles.Play();
    }

    public void ChangeShooting()
    {
        if (isShooting) isShooting = false;
        else isShooting = true;
    }
}
