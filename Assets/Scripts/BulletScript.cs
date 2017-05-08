using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private Transform target;
    public float bulletSpeed = 65f;
    public float AOERadius = 0f;
    public int bulletDamage = 20;

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     Seek();                                                                //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary Seek
    /// <summary>
    /// Called when bullet is instantiated in turretScript
    /// -----------
    /// sets target to the currently iterated enemy
    /// </summary>
    /// <param name="_target">
    /// _target is the currently iterated enemy which is selected in TurretScript
    /// </param>
    #endregion

    public void Seek(Transform _target)
    {
        target = _target;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     Update();                                                              //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary Update
    /// <summary>
    /// If the bullet does NOT have a target it destroys itself
    /// -----------
    /// Moves toward the current target with a bullet speed in world space
    /// -----------
    /// if the distance between the bullet and the enemy is lower than the Delta Magnitude. Call HitTarget() and return out of this function
    /// </summary>
    #endregion

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float currentDistance = bulletSpeed * Time.deltaTime;

        if (direction.magnitude <= currentDistance)
        {
            HitTarget();
            DamageTarget(target);
            return;
        }

        transform.Translate(direction.normalized * currentDistance, Space.World);
        transform.LookAt(target);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     HitTarget();                                                           //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary HitTarget
    /// <summary>
    /// Called when bullet hits its target
    /// -----------
    /// Destroys the bullet GameObject
    /// -----------
    /// TODO: Destroy &| Damage the enemy
    /// </summary>
    #endregion

    void HitTarget()
    {
        Destroy(gameObject);

        if (AOERadius > 0f)
        {
            Explode();
        }
        else
        {
            DamageTarget(target);
        }
    }

    void Explode ()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, AOERadius);

        foreach (Collider collider in collisions)
        {
            if (collider.tag == "Enemy")
            {
                DamageTarget(collider.transform);
            }
        }
    }

    void DamageTarget(Transform enemy)
    {
        EnemyScript thisEnemy = enemy.GetComponent<EnemyScript>();

        if (thisEnemy != null)
        thisEnemy.TakeDamage(bulletDamage);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AOERadius);
    }
}
