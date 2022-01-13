using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;

    public void OnClick()
    {
        GameManager.BuildingArray = GameManager.BuildingList.ToArray();
        Debug.Log("Yahoo");
        for (int i = 0; i < GameManager.BuildingArray.Length; i++)
        {
            Debug.Log(i);
            Debug.Log("Building_name: " + GameManager.BuildingArray[i].Building_name);
            Debug.Log("BuildingPosition: " + GameManager.BuildingArray[i].BuildingPosition);
            Debug.Log("Placed: " + GameManager.BuildingArray[i].Placed);
            Debug.Log("Level: " + GameManager.BuildingArray[i].level);


        }


        SceneManager.LoadScene(transferMapName);

    }
}