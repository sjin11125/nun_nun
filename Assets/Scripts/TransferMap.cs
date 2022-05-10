using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;
    public BuildingSave builinSave;
    public void OnClick()
    {
        builinSave = gameObject.GetComponent<BuildingSave>() ;
        //GameManager.BuildingArray = GameManager.BuildingList.ToArray();
        Debug.Log("Yahoo");

        

        LoadingSceneController.Instance.LoadScene(transferMapName,builinSave);
        //builinSave.BuildingLoad();
    }
}