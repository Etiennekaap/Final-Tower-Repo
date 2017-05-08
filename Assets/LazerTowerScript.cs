using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTowerScript : TurretScript
{

    public bool lazerActivated = true;
    public LineRenderer lineRenderer;
    [SerializeField]
    private ParticleSystem lightningEffect;

    void Start()
    {
        lightningEffect = GetComponentInChildren<ParticleSystem>();
        InvokeRepeating("UpdateTarget", 0f, newTargetingTimer);
    }

    void UpdateTarget()
    {
        SeekTarget();
    }

    void Update()
    {
        if (target == null)
        {
            if (lazerActivated)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    lightningEffect.Stop();
                }
            }
            return;
        }

        if (lazerActivated)
        {
            Lightning();
        }
        else
        {
            if (currentFireCooldown <= 0f)
            {
                ShootProjectile();
                currentFireCooldown = 1f / fireRate;
            }

            currentFireCooldown -= Time.deltaTime;
        }
        Vector3 direction = target.position - transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(PivotPoint.rotation, lookAtRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        PivotPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);

       
    }

    void Lightning()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            //lightningEffect.Simulate(10,true,true);
            //lightningEffect.Emit(20);
            lightningEffect.Play();
            Debug.Log("Line Renderer Enabled and lightning effect start");
        }
        //Debug.Log(lightningEffect.isPlaying);

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 delta = firePoint.position - target.position;
        lightningEffect.transform.position = target.position;
        lightningEffect.transform.rotation = Quaternion.LookRotation(delta);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }
}
