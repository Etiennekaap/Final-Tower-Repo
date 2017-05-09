using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeScript : MonoBehaviour
{
    public Color buildColor;
    public Color cantBuildColor;
    public Color selectionColor;
    private Color startColor;

    public Vector3 offsetter;
    public GameObject turret;
    BuildManager buildmanager;
    private Renderer rend;


    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     Start();                                                               //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary Start
    /// <summary>
    /// Gets the component from Renderer and sets the rend material color to the default startColor
    /// </summary>
    #endregion

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildmanager = BuildManager.BuildManagerInstance;
    }

    public Vector3 GetBuildPos()
    {
        return transform.position + offsetter;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     OnMouseDown();                                                         //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary OnMouseDown
    /// <summary>
    /// Called once when you click the mouse
    /// -----------
    /// First checks if there is not a turret already on the clicked node. else it returns
    /// -----------
    /// instantiates a turret with the selected current turretprefab on the selected node
    /// </summary>
    #endregion

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildmanager.SelectNode(this);
            return;
        }

        if (!buildmanager.Buildable)
            return;

        buildmanager.BuildTurretHere(this);

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     OnMouseEnter();                                                        //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary OnMouseEnter
    /// <summary>
    /// Called when the mouse coördinates intersect with an instance of node
    /// -----------
    /// sets the color of the renderer for the node to the hover color
    /// </summary>
    #endregion

    void OnMouseEnter()
    {
        if (buildmanager.BuildThisTurret == null)
        {
            rend.material.color = selectionColor;
            return;
        }
        if (PlayerStats.resources < buildmanager.BuildThisTurret.buildCost && buildmanager.BuildThisTurret != null)
        {
            Debug.Log("Cant Build Here");
            rend.material.color = cantBuildColor;
        }
        else if (PlayerStats.resources >= buildmanager.BuildThisTurret.buildCost && turret == null && buildmanager.BuildThisTurret != null)
        {
            Debug.Log("Will Build Here");
            rend.material.color = buildColor;
        }
        else
        {
            rend.material.color = selectionColor;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                                                                                            //
    //                                                                     OnMouseExit();                                                         //
    //                                                                                                                                            //
    //--------------------------------------------------------------------------------------------------------------------------------------------//

    #region Code Summary OnMouseExit
    /// <summary>
    /// Called when the mouse coördinates NO LONGER intersect with an instance of node
    /// -----------
    /// sets the color of the renderer for the node to the start color
    /// </summary>
    #endregion

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
