using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    public GameManager gameManager;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == null)
                {
                    Debug.Log("Object is null");
                }

                if (hit.collider.CompareTag("Brick"))
                { 
                    BreakBrick(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Coin"))
                { 
                    CollectCoin(hit.collider.gameObject);
                }
                else
                {
                    Debug.Log("Object not found");
                }
            }
        }
    }

    private void BreakBrick(GameObject brick)
    {
        gameManager.UpdateScore();
        Destroy(brick);
    }

    private void CollectCoin(GameObject coin)
    {
        gameManager.UpdateCoins();
        Destroy(coin);
    }
}
