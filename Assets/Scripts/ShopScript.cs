using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{

    public TurretBlueprints basicTurret;
    public TurretBlueprints missileTurret;
    public TurretBlueprints acidTurret;
    public TurretBlueprints lazerTower;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.BuildManagerInstance;
    }

    public void BuyNoTurret()
    {
        buildManager.SetBuildThisTurret(null);
    }

	public void BuyBasicTurret()
    {
        buildManager.SetBuildThisTurret(basicTurret);
    }

    public void BuyMissileTurret()
    {
        buildManager.SetBuildThisTurret(missileTurret);
    }

    public void BuyAcidTurret()
    {
        buildManager.SetBuildThisTurret(acidTurret);
    }

    public void BuyLazerTower()
    {
        buildManager.SetBuildThisTurret(lazerTower);
    }
}
