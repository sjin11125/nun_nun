using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadingSceneController : MonoBehaviour
{

    private static LoadingSceneController instance;
    public static LoadingSceneController Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<LoadingSceneController>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }
    }

    private static LoadingSceneController Create()
    {
        return Instantiate(Resources.Load<LoadingSceneController>("loadingUI"));
    }

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
            return;

        }
        DontDestroyOnLoad(this);
    }
    [SerializeField]
    private CanvasGroup canvasGroup;

    //[SerializeField]
   // private Image progressBar;
    
    private string loadSceneName;
    private string uid;
    public void LoadScene(SceneName sceneName,string uid=null)
    {
        gameObject.SetActive(true);

        if (uid!=null)
        this.uid = uid;

        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName.ToString();
        StartCoroutine(LoadSceneProcess());


    }
    

    private IEnumerator LoadSceneProcess()
    {
        //throw new NotImplementedException();
       // progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));
        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
        op.allowSceneActivation = false;

        //FirebaseLogin.Instance.GetBuilding();
       /* if (loadSceneName=="Main")
        {
            LoadManager.Instance.buildingsave.BuildingReq(BuildingDef.getMyBuilding);
        }*/
       
        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;
            if(op.progress < 0.9f)
            {
               // progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                // progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                op.allowSceneActivation = true;
                yield break;
                /* if (progressBar.fillAmount >= 1f)
                 {
                       op.allowSceneActivation = true;
                 yield break;
                 }*/
            }
        }

    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) // 씬로드가 끝나는 지점
    {
        if (arg0.name == loadSceneName)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            switch (loadSceneName)
            {
                case "Main":
                    GetBuildingNuniInfo(GameManager.Instance.PlayerUserInfo.Uid);
                    break;
                case "FriendMain":
                    LoadManager.Instance.FriendUid = uid;
                    Debug.Log("uid: "+ uid);
                    GetBuildingNuniInfo(LoadManager.Instance.FriendUid);
                    break;
                case "Game":
                    FirebaseLogin.Instance.GetNuni(uid).ContinueWith((task) =>
                    {
                        if (!task.IsFaulted)
                        {
                            if (task.Result != null)//누니 넣기
                            {
                                Debug.Log("누니 받아온 결과: " + task.Result);

                                try
                                {

                                    Newtonsoft.Json.Linq.JArray Result = Newtonsoft.Json.Linq.JArray.Parse(task.Result);

                                    foreach (var item in Result)
                                    {
                                        Debug.Log("item: " + item.ToString());
                                        Cardsave itemNuni = JsonUtility.FromJson<Cardsave>(item.ToString());
                                        //Debug.Log("item: " + JsonUtility.ToJson(item));
                                        Card tempNuni = new Card(itemNuni);
                                        if (!GameManager.Instance.CharacterList.ContainsKey(tempNuni.Id))
                                            GameManager.Instance.CharacterList.Add(tempNuni.Id, tempNuni);
                                    }

                                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                                    {
                                        StartCoroutine(Fade(false));

                                    }); //BuildingLoad();
                                        //  UnityMainThreadDispatcher.Instance().Enqueue(NuniLoad); //BuildingLoad();
                                }
                                catch (Exception e)
                                {
                                    Debug.LogError(e.Message);
                                    throw;
                                }

                            }
                            else
                            {
                                Debug.Log("task is null");
                            }
                        }

                    });
                    break;

                default:
                    break;
            }

        }
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while(timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 3f;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
        }
        if(!isFadeIn)
        {
            Debug.Log("씬 로드 끝");
            gameObject.SetActive(false);
        }
    }

    public void GetBuildingNuniInfo(string uid)
    {
        FirebaseLogin.Instance.GetBuilding(uid).ContinueWith((task) =>      //건물 불러오기
        {
            Debug.Log("task.Result: " + task.Result);
            if (!task.IsFaulted)
            {
                if (task.Result != null)//건물 넣기
                {
                    Debug.Log("task.Result: " + task.Result);

                    try
                    {

                        /*Building[] Result = JsonHelper.FromJson<Building>(task.Result);

                        for (int i = 0; i < Result.Length; i++)
                        {
                            //Debug.Log("item: " + JsonUtility.ToJson(Result[i]));

                            MyBuildings.Add(Result[i].Id, Result[i]);
                        }*/
                        Newtonsoft.Json.Linq.JArray Result = Newtonsoft.Json.Linq.JArray.Parse(task.Result);

                        LoadManager.Instance.MyBuildings.Clear();

                        foreach (var item in Result)
                        {
                            Debug.Log("item: " + item.ToString());
                            Buildingsave itemBuilding = JsonUtility.FromJson<Buildingsave>(item.ToString());
                            //Debug.Log("item: " + JsonUtility.ToJson(item));
                            Building tempBuilding = new Building(itemBuilding);
                            LoadManager.Instance.MyBuildings.Add(tempBuilding.Id, tempBuilding);
                        }

                        FirebaseLogin.Instance.GetNuni(uid).ContinueWith((task) =>          //누니 받아오기
                        {
                            if (!task.IsFaulted)
                            {
                                if (task.Result != null)//누니 넣기
                                {
                                    Debug.Log("누니 받아온 결과: " + task.Result);

                                    try
                                    {

                                        Newtonsoft.Json.Linq.JArray Result = Newtonsoft.Json.Linq.JArray.Parse(task.Result);

                                        foreach (var item in Result)
                                        {
                                            Debug.Log("item: " + item.ToString());
                                            Cardsave itemNuni = JsonUtility.FromJson<Cardsave>(item.ToString());
                                            //Debug.Log("item: " + JsonUtility.ToJson(item));
                                            Card tempNuni = new Card(itemNuni);
                                            if (!GameManager.Instance.CharacterList.ContainsKey(tempNuni.Id))
                                                GameManager.Instance.CharacterList.Add(tempNuni.Id, tempNuni);
                                        }

                                        UnityMainThreadDispatcher.Instance().Enqueue(() =>
                                        {
                                            if (SceneManager.GetActiveScene().name!=SceneName.Game.ToString())
                                            {
                                                LoadManager.Instance.BuildingLoad();
                                                LoadManager.Instance.NuniLoad();
                                            }
                                           

                                            StartCoroutine(Fade(false));

                                        }); //BuildingLoad();
                                            //  UnityMainThreadDispatcher.Instance().Enqueue(NuniLoad); //BuildingLoad();
                                    }
                                    catch (Exception e)
                                    {
                                        Debug.LogError(e.Message);
                                        throw;
                                    }

                                }
                                else
                                {
                                    Debug.Log("task is null");
                                }
                            }

                        });



                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                        throw;
                    }

                }
                else
                {
                    Debug.Log("task is null");
                }
            }
        });
    }
}
