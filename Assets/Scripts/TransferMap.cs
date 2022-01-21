using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;

    public void OnClick()
    {
        //GameManager.BuildingArray = GameManager.BuildingList.ToArray();
        Debug.Log("Yahoo");
     


        LoadingSceneController.Instance.LoadScene(transferMapName);

    }
}