using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class FriendInfoUI : MonoBehaviour
{
    // Start is called before the first frame update
   // FriendInfo FriendInfo;
    public Button GoBtn;          //친구 마을 가기 버튼
    public Button RemoveBtn;          //친구 제거 버튼
    

    public Text FriendName;     //친구 닉넴
    public Text FriendMessage;     //친구 메세지
    public Image FriendImage;       //친구 프사

    public void SetFriendInfo(FriendInfo friendInfo)
    {
        FriendName.text = friendInfo.FriendName;
        //FriendImage.sprite=GameManager.Instance.ima
        //이미지 넣기

    }
    public void Start()
    {
        GoBtn.OnClickAsObservable().Subscribe(_=> { 

        });
    }
    
}
