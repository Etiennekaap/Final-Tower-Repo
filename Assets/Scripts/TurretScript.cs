using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour {

    [SerializeField]
    protected Transform target;
    [SerializeField]
    protected float newTargetingTimer = 0.7f;
    [SerializeField]

    [Header("Turret Attributes")]
    public float turretRange = 15f;
    public float fireRate = 1f;
    public float currentFireCooldown = 1f;
    public float buildCost = 100f;
    public float UpgradeCost = 50f;
    public float SellCost = 55f;
    public float FieldOfView = 3f;

    [Header("Unity Required Fields")]
    public Transform PivotPoint;
    public float rotationSpeed = 8f;
    public string enemyTag = "Enemy";

    public GameObject bulletPrefab;
    public Transform firePoint;


    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     Start();                                                               //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary Start
    /// <summary>
    /// Creates a seperate Update Loop based on a Time.DeltaTime that is NOT based on frames.
    /// Removes higher framerate computers possibility to call it more often than lower framerate computers.
    /// </summary>
    #endregion

    void Start ()
    {
        InvokeRepeating("UpdateTarget", 0f, newTargetingTimer);
	}

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     UpdateTarget();                                                        //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary UpdateTarget
    /// <summary>
    /// Called every newTargetingTimer's value in seconds.
    /// -----------
    /// Creates a list of enemies based on all enemies in the current scene with the tag enemyTag (Currently thats every enemy)
    /// -----------
    /// shortest distance to the enemy at the start is Infinity as there are no enemies in the scene yet (hence infinity)
    /// -----------
    /// nearest enemy is null for the same reasons
    /// -----------
    /// Iterates through the enemy list to check which enemy is closest to the turret. 
    /// when it finds an enemy closer than the last iterated enemy the nearest enemy is that iteration of enemy.
    /// similarly the distance to that enemy is the current shortestDistance
    /// -----------
    /// if there is a nearest enemy and the shortest distance between the closest enemy and the turret is within the turret's range...
    /// the target of the turret will be the transform of that iteration of enemy
    /// ----------- 
    /// if either... the nearest enemy does NOT exist or there is no enemy within the turrets range...
    /// the turrets target is set to null
    /// </summary>
    #endregion

    void UpdateTarget()
    {
        SeekTarget();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     Update();                                                              //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary Update
    /// <summary>
    /// if there is no target. it breaks from the loop
    /// -----------
    /// Rotates the turret's head (by the pivot point) to the direction of the target of the turret
    /// -----------
    /// once the Fire cooldown timer reaches 0 it calls ShootProjectile() and resets the Fire cooldown timer
    /// -----------
    /// decrements the fire cooldown timer by Time.DeltaTime
    /// </summary>
    #endregion

    void Update()
    {
        if (target == null)
        {
            return;
        }

        //Debug.Log(transform.position);


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

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     OnDrawGizmosSelected();                                                //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary OnDrawGizmosSelected
    /// <summary>
    /// Creates a red wire sphere in the scene (NOT IN THE ACTUAL GAME) to indicate the range of of the turret when turret is selected.
    /// </summary>
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     ShootProjectile();                                                     //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary ShootProjectile
    /// <summary>
    /// Called every time the fire cooldown timer reaches zero
    /// -----------
    /// Instantiates a bullet based on its prefab at the end of the turret's barrel
    /// -----------
    /// if the bullet is not null call bullet.Seek(target) where target is the current target of the turret
    /// </summary>
    #endregion

    virtual public void ShootProjectile()
    {
        GameObject bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletScript bullet = bulletGameObject.GetComponent<BulletScript>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     SeekTarget();                                                          //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//
    virtual public void SeekTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= turretRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }


    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     RotateTowardsTarget();                                                 //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//
    virtual public void RotateTowardsTarget()
    {
        
    }

}
