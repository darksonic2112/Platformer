using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject coinPrefab;
    public Transform environmentRoot;
    public float animationDuration = 0.25f;

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
        CoinAnimation(coin);
        Destroy(coin);
    }

    private void CoinAnimation(GameObject coin)
    {
        Vector3 startPos = coin.transform.position;
        StartCoroutine(AnimateCoin(startPos));
    }

    private IEnumerator AnimateCoin(Vector3 startPos)
    {
        float elapsedTime = 0f;
        Vector3 endPos = startPos + new Vector3(0f, 1f, 0f);
        
        GameObject coinInstance = Instantiate(coinPrefab, startPos, Quaternion.identity, environmentRoot);

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            
            coinInstance.transform.position = Vector3.Lerp(startPos, endPos, t);
            
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        
        coinInstance.transform.position = endPos;
        
        Destroy(coinInstance);
    }
}