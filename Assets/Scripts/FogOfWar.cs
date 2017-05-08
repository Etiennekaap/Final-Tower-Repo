using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{

    public Camera camera;
    public GameObject target;
    public GameObject plane;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit raycastHit;
        Ray ray = camera.ScreenPointToRay(target.transform.position);
        if (Physics.Raycast(ray, out raycastHit))
        {
            //Create area of light
            Texture2D texture = plane.GetComponent<Texture2D>();
            Debug.Log(texture.GetPixel((int)raycastHit.transform.position.x, (int)raycastHit.transform.position.y));
            texture.SetPixel((int)raycastHit.transform.position.x, (int)raycastHit.transform.position.y, Color.clear);

            for (int x = 0; x < 200; x++)
            {
                for (int y = 0; y < 200; y++)
                {
                    float distanceToCenter = Mathf.Sqrt(Mathf.Pow(100 - x, 2) + Mathf.Pow(100 - y, 2));
                    if (distanceToCenter > 100) texture.SetPixel(x, y, Color.black);
                    else texture.SetPixel(x, y, Color.clear);
                }
            }
        }
    }
}
