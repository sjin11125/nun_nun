using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Google;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Functions;
using UnityEngine.SceneManagement;



public class FirebaseLogin : MonoBehaviour
{	// Auth 용 instance
    public Text infoText;
    public string webClientId = "494831558708-2dq0fqt5ut11d37l24139nad54it8h04.apps.googleusercontent.com";

    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;

    FirebaseFirestore db;
    FirebaseFunctions functions;

    public static FirebaseLogin _Instance;
    public static FirebaseLogin Instance
    {
        get
        {
            if (_Instance == null)
            {
                return null;
            }
            return _Instance;
        }
    }

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (_Instance == null)
        {
            _Instance = this;
        }
        else if (_Instance != this) 
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);  // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
       
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };

        CheckFirebaseDependencies();

    }


   /* public void GetBuilding()
    {
        addMessage("아싸").ContinueWith((task) => {
            Debug.Log(task);
            Debug.Log(task.IsFaulted);
        if (task.IsFaulted)
        {
                foreach (var inner in task.Exception.InnerExceptions)
                {
                    if (inner is FunctionsException)
                    {
                        var e = (FunctionsException)inner;
                        // Function error code, will be INTERNAL if the failure
                        // was not handled properly in the function call.
                        var code = e.ErrorCode;
                        var message = e.Message;

                        Debug.LogError(code);
                        Debug.LogError(message);
                    }
                    Debug.LogError("예외: "+inner.Message);
                }
           
        }
         else
            Debug.Log("통신성공: "+task.Result);
        });
    }*/
    public Task<string> GetBuilding(string uid)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        // Create the arguments to the callable function.
        //Buildingsave test = new Buildingsave("7.349999", "6.875","T", "bunsu_level(Clone)0", "bunsu_level(Clone)","1","F");
        // var data = JsonUtility.ToJson(test);
        SendMessage IdToken = new SendMessage("Send IdToken", uid);

        var function = functions.GetHttpsCallable("getBuilding");
        return function.CallAsync(JsonUtility.ToJson(IdToken)).ContinueWithOnMainThread((task) => {
            return (string)task.Result.Data;
        });
    }  
    public Task<string>AddBuilding(Buildingsave building)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        // Create the arguments to the callable function.
        // Buildingsave test = new Buildingsave("7.349999", "6.875","T", "bunsu_level(Clone)0", "bunsu_level(Clone)","1","F","sd25hr");
        building.Uid = GameManager.Instance.PlayerUserInfo.Uid;
        var data = JsonUtility.ToJson(building);

        var function = functions.GetHttpsCallable("addBuilding");

        return function.CallAsync(data).ContinueWithOnMainThread((task) => {
            Debug.Log("task.Result: " + task.Result);
            return (string)task.Result.Data;
        });
    }
    public Task<string>RemoveBuilding(Buildingsave building)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        // Create the arguments to the callable function.
        // Buildingsave test = new Buildingsave("7.349999", "6.875","T", "bunsu_level(Clone)0", "bunsu_level(Clone)","1","F","sd25hr");
        building.Uid = GameManager.Instance.PlayerUserInfo.Uid;
        var data = JsonUtility.ToJson(building);

        var function = functions.GetHttpsCallable("deleteBuilding");

        return function.CallAsync(data).ContinueWithOnMainThread((task) => {
            Debug.Log("task.Result: " + task.Result);
            return (string)task.Result.Data;
        });
    }
    private void CheckFirebaseDependencies()
    {
        Debug.Log("CheckFirebaseDependencies Start ");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                    auth = FirebaseAuth.DefaultInstance;
                //else
                   // AddToInformation("Could not resolve all Firebase dependencies: " + task.Result.ToString());
            }
            else
            {
                //AddToInformation("Dependency check was not completed. Error : " + task.Exception.Message);
            }
            //AddToInformation("왜안돼");
            Debug.Log("Done " + task.Result.ToString());
        });
       
    }

    public void SignInWithGoogle() { OnSignIn(); }
    public void SignOutFromGoogle() { OnSignOut(); }
    public void WirteButton()
    {
        Write();
    }
    public void GetGameData()
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        var function = functions.GetHttpsCallable("getGameData");

        function.CallAsync(null).ContinueWithOnMainThread((task) => {
            Debug.Log("res: " + task.Result.Data);

           return (string)task.Result.Data;



        });
    }
    public void GetUserInfo(string idToken)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        var function = functions.GetHttpsCallable("findUser");

        SendMessage IdToken = new SendMessage("Send IdToken",idToken);
        Debug.Log("Getuser");
        function.CallAsync(JsonUtility.ToJson(IdToken)).ContinueWithOnMainThread((task) => {
            Debug.Log("res: "+ task.Result.Data);

            if (!task.IsFaulted)
            {
                try { 
                GameManager.Instance.PlayerUserInfo = JsonUtility.FromJson<UserInfo>((string)task.Result.Data);     //유저 정보 세팅
                GameManager.Instance.PlayerUserInfo.Uid = idToken;
                    LoadingSceneController.Instance.LoadScene(SceneName.Main);
                    //return true;   
                }
                catch (Exception e )
                {
                    Debug.LogError(e.Message);
                    throw;
                }
            }
            else
            {
                Debug.LogError(task.Result);
            }
        });
    }
    public Task<string> GetVisitorBook(string uid)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        var function = functions.GetHttpsCallable("getVisitorBook");

        SendMessage IdToken = new SendMessage("Send IdToken", uid);
        
       return function.CallAsync(JsonUtility.ToJson(IdToken)).ContinueWithOnMainThread((task) => {
            Debug.Log("res: "+ task.Result.Data);

           return (string)task.Result.Data;



        });
    }
    public void SetVisitorBook(VisitorBookInfo message)
    {
        functions=FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        var function = functions.GetHttpsCallable("setVisitorBook");

        function.CallAsync(JsonUtility.ToJson(message)).ContinueWithOnMainThread((task) => {
            Debug.Log("task: "+task.Result.Data);
        });
    }
    public void SetUserInfo(UserInfo userInfo)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        var function = functions.GetHttpsCallable("setUser");

        function.CallAsync(JsonUtility.ToJson(userInfo)).ContinueWithOnMainThread((task) => {
            Debug.Log("res: "+ task.Result.Data);

        });
    }
    public void SetNuni(Cardsave nuni)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        var function = functions.GetHttpsCallable("setNuni");

        function.CallAsync(JsonUtility.ToJson(nuni)).ContinueWithOnMainThread((task) => {
            Debug.Log("res: "+ task.Result.Data);

        });
    }

    public Task<string> GetNuni(string uid)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        // Create the arguments to the callable function.
       // Cardsave test = new Buildingsave("7.349999", "6.875","T", "bunsu_level(Clone)0", "bunsu_level(Clone)","1","F");
        // var data = JsonUtility.ToJson(test);
        SendMessage IdToken = new SendMessage("Send IdToken", uid);

        var function = functions.GetHttpsCallable("getNuni");
        return function.CallAsync(JsonUtility.ToJson(IdToken)).ContinueWithOnMainThread((task) => {
            return (string)task.Result.Data;
        });
    }
    public Task<string> GetFriend(string uid)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        // Create the arguments to the callable function.
       // Cardsave test = new Buildingsave("7.349999", "6.875","T", "bunsu_level(Clone)0", "bunsu_level(Clone)","1","F");
        // var data = JsonUtility.ToJson(test);
        SendMessage IdToken = new SendMessage("Send IdToken", uid);

        var function = functions.GetHttpsCallable("getFriend");
        return function.CallAsync(JsonUtility.ToJson(IdToken)).ContinueWithOnMainThread((task) => {
            return (string)task.Result.Data;
        });
    }
    public Task<string> GetRequestFriend(string uid)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        // Create the arguments to the callable function.
       // Cardsave test = new Buildingsave("7.349999", "6.875","T", "bunsu_level(Clone)0", "bunsu_level(Clone)","1","F");
        // var data = JsonUtility.ToJson(test);
        SendMessage IdToken = new SendMessage("Send IdToken", uid);

        var function = functions.GetHttpsCallable("getRequestFriend");
        return function.CallAsync(JsonUtility.ToJson(IdToken)).ContinueWithOnMainThread((task) => {
            return (string)task.Result.Data;
        });
    }
    public Task<string> PlusFriend(string uid)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);

       // SendMessage IdToken = new SendMessage("Send IdToken", uid);
        FriendAddInfo AddInfo = new FriendAddInfo(GameManager.Instance.PlayerUserInfo.Uid,uid);

        var function = functions.GetHttpsCallable("plusFriend");
        return function.CallAsync(JsonUtility.ToJson(AddInfo)).ContinueWithOnMainThread((task) => {
            return (string)task.Result.Data;
        });
    } public Task<string> AddFriend(string uid)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);

       // SendMessage IdToken = new SendMessage("Send IdToken", uid);
        FriendAddInfo AddInfo = new FriendAddInfo(GameManager.Instance.PlayerUserInfo.Uid,uid);

        var function = functions.GetHttpsCallable("addFriend");
        return function.CallAsync(JsonUtility.ToJson(AddInfo)).ContinueWithOnMainThread((task) => {
            return (string)task.Result.Data;
        });
    }
    public Task<string> GetSearchFriend(string friendName)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        // Create the arguments to the callable function.
        // Cardsave test = new Buildingsave("7.349999", "6.875","T", "bunsu_level(Clone)0", "bunsu_level(Clone)","1","F");
        // var data = JsonUtility.ToJson(test);
        SendMessage IdToken = new SendMessage("Send IdToken", friendName);

        var function = functions.GetHttpsCallable("searchFriend");
        return function.CallAsync(JsonUtility.ToJson(IdToken)).ContinueWithOnMainThread((task) => {
            return (string)task.Result.Data;
        });
    }
    public Task<string> Write()
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        var function = functions.GetHttpsCallable("addBuilding");
        return function.CallAsync().ContinueWith((task) => {
            return (string)task.Result.Data;
        });
    }
    private void OnSignIn()
    {
        Debug.Log("OnSignIn Start ");
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        //("Calling SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
        
        Debug.Log("OnSignIn End ");
    }

    private void OnSignOut()
    {
        //AddToInformation("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect()
    {
        //AddToInformation("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    //AddToInformation("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    //AddToInformation("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            //AddToInformation("Canceled");
        }
        else            //로그인 성공
        {
            Debug.Log("Welcome: " + task.Result.DisplayName + "!");
            Debug.Log("Email = " + task.Result.Email);
            Debug.Log("Google ID Token = " + task.Result.IdToken);
            Debug.Log("Email = " + task.Result.Email);
            SignInWithGoogleOnFirebase(task.Result.IdToken);
            
        }
    }
    void SetUserInfo(Task<string> task)
    {
        Debug.Log((string)task.Result);
        try
        {
            Debug.Log("(string)task.Result: "+ (string)task.Result);
            GameManager.Instance.PlayerUserInfo = JsonUtility.FromJson<UserInfo>((string)task.Result);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            throw;
        }
    }
    private void SignInWithGoogleOnFirebase(string idToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null)
            {
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                    Debug.Log("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
            }
            else
            {
                Debug.Log("IDToken: "+task.Result.UserId);
                Debug.Log("Sign In Successful.");

                //GetUserInfo(task.Result.UserId);
                //    GameManager.Instance.StateList.Add("Login");
            }
        });
    }
   
    public void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        Debug.Log("Calling SignIn Silently");

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    }

    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        Debug.Log("Calling Games SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void AddToInformation(string str) { infoText.text += "\n" + str; }
    public void LoginLink(LoginState _t)
    {
        switch (_t)
        {
            case LoginState.GOOGLE_ACCOUNT:
                //GooleLogin();
                break;
            case LoginState.APPLE_ACCOUNT:
                break;
            case LoginState.EMAIL_ACCOUNT:
                break;
            default:
                break;
        }
    }
    
}
