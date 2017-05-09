using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager BuildManagerInstance;

    public NodeUI nodeUI;
    public TurretBlueprints BuildThisTurret;
    public NodeScript selectedNode;

    public bool Buildable { get { return BuildThisTurret != null; } }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     Awake();                                                               //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary Awake
    /// <summary>
    /// Awake is called before start. used to make sure that certain instances are instantiated before being used in Start()
    /// -----------
    /// Used to make sure there is only ONE instance of Buildmanager and sets the buildmanagerinstance to the current GameObject
    /// </summary>
    #endregion

    void Awake()
    {
        if (BuildManagerInstance != null)
        {
            Debug.LogError("More Than One BuildManagerInstance in this Scene");
            return;
        }
        BuildManagerInstance = this;

        DeselectNode();
        BuildThisTurret = null;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     Start();                                                               //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary Start
    /// <summary>
    /// Selected turret is the turret to build
    /// -----------
    /// TODO: be able to select different kind of turret prefabs
    /// </summary>
    #endregion

    void Start()
    {
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     GetBuildThisTurret();                                                  //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary GetBuildThisTurret
    /// <summary>
    /// Gets the selected turret to build
    /// </summary>
    /// <returns>
    /// selected turret prefab
    /// </returns>
    #endregion

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     SetBuildThisTurret();                                                  //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//
    public void SetBuildThisTurret(TurretBlueprints pTurret)
    {
        BuildThisTurret = pTurret;
        DeselectNode();
    }

    public void SelectNode(NodeScript node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        BuildThisTurret = null;
        selectedNode = node;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.DeactivateUI();
    }

    public void BuildTurretHere(NodeScript node)
    {
        if (PlayerStats.resources < BuildThisTurret.buildCost)
        {
            //TODO: POPUP on UI!
            Debug.Log("We require more minerals");
            return;
        }

       PlayerStats.resources -= BuildThisTurret.buildCost;
       GameObject turret = Instantiate(BuildThisTurret.prefab, node.GetBuildPos(), Quaternion.identity);
       node.turret = turret;

        //TODO: Show resources in UI!
        Debug.Log("Very Well. this turret will be conjured..... You have " + PlayerStats.resources + " left");
    }

}
