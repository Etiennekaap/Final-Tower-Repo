using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeScript : MonoBehaviour
{
    public Color hoverColor;
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

        if (!buildmanager.Buildable)
            return;

        //Debug.Log(buildmanager.GetBuildThisTurret().ToString());

        if (turret != null)
        {
            Debug.Log("Can't Build Here. Already A Turret At This Location");
            return;
        }

        buildmanager.BuildTurretHere(this);

        //GameObject buildThisTurret = buildmanager.GetBuildThisTurret();
        //turret = Instantiate(buildThisTurret, transform.position + offsetter, transform.rotation);
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
        if (!buildmanager.Buildable)
            return;

        rend.material.color = hoverColor;
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
