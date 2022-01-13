using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbzA34XMFQkRiYkVFmvNh-u4YzfdYK_Uv2DH8ekeqhONVfIB069vZ_6s57epyPw92xfZ4Q/exec";
    public InputField IDInput, PassInput;
    string id, pass;

    bool SetIDPass()
    {
        id = IDInput.text.Trim();
        pass = PassInput.text.Trim();

        if (id == "" || pass == "") return false;
        else return true;
    }

    public void Register()
    {
        if (!SetIDPass())
        {
            Debug.Log("아이디 또는 비번 비어잇음");
            return;
        }
        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }

    public void Login()
    {
        if (!SetIDPass())
        {
            print("아이디 또는 비번이 비었다고ㅗㅗㅗㅗㅗㅗ");
            return;
        }
        WWWForm form = new WWWForm();
        form.AddField("order", "login");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isDone) print(www.downloadHandler.text);
            else print("웹의 응답이 없다.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
