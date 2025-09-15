using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{

    private List<PowerCellScript> powerCellScripts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(var powerCell in FindObjectsByType<PowerCellScript>(FindObjectsSortMode.None))
        {
            powerCellScripts.Add(powerCell); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        FinishLevels();
    }

    void FinishLevels()
    {
        foreach (var correctPlace in powerCellScripts)
        {
            if (!correctPlace.inCorectPlace)
                return;

            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
    }

}
