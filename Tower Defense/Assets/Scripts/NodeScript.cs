using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{

    [SerializeField] GameObject baseTower;
    private bool hasTower;
    [SerializeField] GameController gc;
    private void OnMouseOver()
    {
        //Check if node already has a tower present
        if (!hasTower)
        {
            //Set node to dark gray on hover
            gameObject.GetComponent<MeshRenderer>().material.color = new Color32(0x5A, 0x5A, 0x5A, 0xFF);
            if (Input.GetMouseButtonDown(0))
            {
                hasTower = true;
                Instantiate(baseTower, gameObject.transform.position, Quaternion.Euler(180,-90,-90));
            }
        }
    }

    private void OnMouseExit()
    {
        //Reset node color
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
