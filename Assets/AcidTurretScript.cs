using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidTurretScript : TurretScript {

    void Start()
    {
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
            return;
        }

        Vector3 direction = target.position - transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(PivotPoint.rotation, lookAtRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        PivotPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (currentFireCooldown <= 0f)
        {
            ShootProjectile();
            currentFireCooldown = 1f / fireRate;
        }

        currentFireCooldown -= Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }
}
