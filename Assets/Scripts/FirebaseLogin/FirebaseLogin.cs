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

public class FirebaseLogin : MonoBehaviour
{	// Auth 용 instance
    public Text infoText;
    public string webClientId = "494831558708-2dq0fqt5ut11d37l24139nad54it8h04.apps.googleusercontent.com";

    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;

    FirebaseFirestore db;
    FirebaseFunctions functions;

    public FirebaseLogin()
    {
       
    }

    private void Awake()
    {
        
        Debug.Log("Awake");
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
        Debug.Log("Configuration");
        CheckFirebaseDependencies();

    }
    private void Start()
    {
        
        Debug.Log("Start");
    }

    public void AddMessageToDB()
    {
        addMessage("아싸").ContinueWith((task) => {
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
                }
           
        }
         else
            Debug.Log(task.Result);
        });
    }
    private Task<string> addMessage(string text)
    {
        functions = FirebaseFunctions.GetInstance(FirebaseApp.DefaultInstance);
        // Create the arguments to the callable function.
        var data = text;

        // Call the function and extract the operation from the result.
        var function = functions.GetHttpsCallable("addMessage");
        return function.CallAsync(data).ContinueWith((task) => {
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
        Debug.Log("task: "+task);
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
                Debug.Log("Sign In Successful.");
                OnGetPlayerInfo();
            }
        });
    }
    public Task<string> OnGetPlayerInfo()           //플레이어 정보 가져오기
    {
        var data = new Dictionary<string, object>();
        data["user"] = GameManager.NickName;

        var function = functions.GetHttpsCallable(FirebaseDef.Login.ToString());
        return function.CallAsync(data).ContinueWith((task) => {
            return (string)task.Result.Data;
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
