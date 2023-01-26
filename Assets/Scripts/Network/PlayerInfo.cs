using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using UniRx;
public class PlayerInfo : MonoBehaviour                 //플레이어 프로필 스크립트
{
    public static string Id;            //플레이어 아이디
    public static string NickName;      //플레이어 닉네임
    public static string SheetsNum;     //플레이어 건물 정보 들어있는 스프레드 시트 id
    public static string Info;          //상태메세지



    public Image ProfileImage;      //내 프로필 이미지

    public InputField InfoInput;        //한줄소개 수정
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameManager.AllNuniArray.Length; i++)
        {
            if (GameManager.AllNuniArray[i].Image.name != GameManager.Instance.PlayerUserInfo.Image)
                continue;
            //GameManager.Instance.ProfileImage.Subscribe((value) => ProfileImage.sprite = value). ;// = GameManager.AllNuniArray[i].Image;
            GameManager.Instance.ProfileImage.AsObservable().Subscribe((value) => {
                ProfileImage.sprite = value;

            });
            ProfileImage.sprite = GameManager.AllNuniArray[i].Image;
        }
        
       


    }

  


    public void EditInfo()                  //한줄소개 수정
    {
        GameManager.Instance.PlayerUserInfo.Message = InfoInput.text;

        /*WWWForm form1 = new WWWForm();
        form1.AddField("order", "setProfileInfo");
        form1.AddField("player_nickname", GameManager.NickName);
        form1.AddField("profile_info", InfoInput.text);


        StartCoroutine(ImagePost(form1));*/

        FirebaseLogin.Instance.SetUserInfo(GameManager.Instance.PlayerUserInfo);
    }
    // Update is called once per frame
  /*  void Update()
    {
        if (gameObject.tag.Equals("Profile"))
        {
            gameObject.GetComponent<Image>().sprite = GameManager.ProfileImage;
        
            Profile[0].text = GameManager.Instance.PlayerUserInfo.Uid;
            Profile[1].text = GameManager.Instance.PlayerUserInfo.Message;
        }
        if (gameObject.tag .Equals( "Profile_Image"))
        {
            gameObject.GetComponent<Image>().sprite = GameManager.ProfileImage;
            /*  for (int i = 0; i < GameManager.Instance.CharacterList.Count; i++)
              {
                  if (GameManager.ProfileImage.name.Equals( GameManager.Instance.CharacterList[i].Image.name)
                  {
                      profile_image.sprite = GameManager.Instance.CharacterList[i].Image;
                  }
              }
        }
    }*/
}
