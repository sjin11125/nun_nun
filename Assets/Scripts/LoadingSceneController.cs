using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{

    private static LoadingSceneController instance;

    public  void LoadLoginScene(string sceneaanme)
    {
        SceneManager.LoadScene(sceneaanme);
    }
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
        DontDestroyOnLoad(gameObject);
    }
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Image progressBar;
    
    private string loadSceneName;
    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());

    }
    

    private IEnumerator LoadSceneProcess()
    {
        //throw new NotImplementedException();
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));
        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
        op.allowSceneActivation = false;
        if (loadSceneName == "Main")
        {
            if (!TutorialButton.isTutoButton)
            {
                BuildingSave buildingsave = this.GetComponent<BuildingSave>();
                buildingsave.BuildingLoad();
            }
        }
       
        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;
            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }

        }
   
    }
    public void BuildingLoading()
    {
        
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) // 씬로드가 끝나는 지점
    {
        if(arg0.name == loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;
           
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
            gameObject.SetActive(false);
        }
    }
}
