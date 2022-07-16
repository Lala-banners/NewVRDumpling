using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the dumpling selection in the selection scene before
/// spawning in the gameplay scene.
/// </summary>
public class DumplingSelection : MonoBehaviour
{
    [Header("Selection")] 
    [SerializeField] private GameObject[] dumplings;
    [SerializeField] private int selectedDumpling = 0;

    public void NextDumpling()
    {
        dumplings[selectedDumpling].SetActive(false);
        selectedDumpling = (selectedDumpling + 1) % dumplings.Length;
        dumplings[selectedDumpling].SetActive(true);
    }

    public void PreviousDumpling()
    {
        dumplings[selectedDumpling].SetActive(false);
        selectedDumpling--;

        if(selectedDumpling < 0)
        {
            selectedDumpling += dumplings.Length;
        }
        
        dumplings[selectedDumpling].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedDumpling", selectedDumpling);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
