using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour {

    private NodeScript targetNode;
    public GameObject UI; 

    public void SetTarget(NodeScript _targetNode)
    {
        targetNode = _targetNode;
        UI.SetActive(true);
    }

    public void DeactivateUI()
    {
        UI.SetActive(false);
    }
}
