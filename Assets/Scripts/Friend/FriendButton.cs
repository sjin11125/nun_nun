using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class FriendButton : MonoBehaviour
{
    public InputField FriendNickname;
    Button SearchButton;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag=="FriendSearch")
        {
            SearchButton = gameObject.GetComponent<Button>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterFriend()               //친구네 마을로 가기
    {
        string friendNickname = gameObject.name;


    }
    public void SearchFriend()
    {
       // SearchButton.OnSubmit();

        WWWForm form1 = new WWWForm();
        form1.AddField("order", "SearchFriend");
        form1.AddField("friend_nickname", FriendNickname.text);

        StartCoroutine(ImagePost(form1));                        
    }


    
}
