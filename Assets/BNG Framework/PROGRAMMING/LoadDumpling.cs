using TMPro;
using UnityEngine;

/// <summary>
/// Handles spawning Dumpling prefabs in the gameplay scene.
/// </summary>
public class LoadDumpling : MonoBehaviour
{
    public GameObject[] dumplingPrefabs;
    public Transform spawnPoint;
    //public TMP_Text dumplingName;
    
    // Start is called before the first frame update
    void Start()
    {
        int selectedDumpling = PlayerPrefs.GetInt("selectedDumpling");
        GameObject prefab = dumplingPrefabs[selectedDumpling];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        //dumplingName.text = prefab.name;
    }
}
