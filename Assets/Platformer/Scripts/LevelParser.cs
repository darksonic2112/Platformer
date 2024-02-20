using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject coinPrefab;
    public Transform environmentRoot;
    float offset = 0.5f;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();
        
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }
        int row = 0;
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (var column = 0; column < letters.Length; column++)
            {
                var letter = letters[column];
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
                if (letter == 'x')
                {
                    Vector3 newPos = new Vector3(column + offset, row + offset, 0f);
                    Instantiate(rockPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                if (letter == 'b')
                {
                    Vector3 newPos = new Vector3(column + offset, row + offset, 0f);
                    Instantiate(brickPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                if (letter == 's')
                {
                    Vector3 newPos = new Vector3(column + offset, row + offset, 0f);
                    Instantiate(stonePrefab, newPos, Quaternion.identity, environmentRoot);
                }
                if (letter == '?')
                {
                    Vector3 newPos = new Vector3(column + offset, row + offset, 0f);
                    Instantiate(questionBoxPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                if (letter == 'C')
                {
                    Vector3 newPos = new Vector3(column + offset, row + offset, 0f);
                    Instantiate(coinPrefab, newPos, Quaternion.identity, environmentRoot);
                }
            }
            row++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
