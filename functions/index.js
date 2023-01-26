// The Cloud Functions for Firebase SDK to create Cloud Functions and set up triggers.
const functions = require('firebase-functions');
var bodyParser = require('body-parser')
// The Firebase Admin SDK to access Firestore.
const admin = require('firebase-admin');
const { firestore } = require('firebase-admin/firestore');
const { json } = require('body-parser');
admin.initializeApp();


var message={
  name:"",
  message:""
};

// Take the text parameter passed to this HTTP endpoint and insert it into 
// Firestore under the path /messages/:documentId/original
exports.getGameData=functions.https.onCall(async(req,res)=>{
  const db=admin.firestore();
  const gameData=[];
  const achieveDataColor = await db.collection('gameData').doc("achieve").collection('Color').get();
  const achieveDataEct = await db.collection('gameData').doc("achieve").collection('Ect').get();
  const achieveDataShape = await db.collection('gameData').doc("achieve").collection('Shape').get();
  // buildingData = await db.collection('gameData').doc("achieve").collection('Color').get();
  //const nuniData = await db.collection('gameData').doc("achieve").collection('Color').get();

  const achieveDatas=[];


  for (var i in achieveDataColor.docs) {
    const element = achieveDataColor.docs[i].data();
    achieveDatas.push(element);
  } 
  for (var i in achieveDataEct.docs) {
    const element = achieveDataEct.docs[i].data();
    achieveDatas.push(element);
  }
  for (var i in achieveDataShape.docs) {
    const element = achieveDataShape.docs[i].data();
    achieveDatas.push(element);
  }
  
  gameData.push(achieveDatas);
  console.log(JSON.stringify(gameData));
  
return JSON.stringify(gameData);
});

exports.addMessage = functions.https.onCall(async (req, res) => {
  // Grab the text parameter.
  
  console.log("req: "+ req);
  var test=JSON.parse(req);

  // Push the new message into Firestore using the Firebase Admin SDK.
  
  const writeResult = await admin.firestore().collection('users').doc('vicky').
  collection('building').add(test);
  
  // Send back a message that we've successfully written the message
 // res.json({result: `Message with ID: ${writeResult.id} added.`});
});

exports.findUser=functions.https.onCall(async (req, res) => {
  const db=admin.firestore();
  console.log("req: "+req);
  const idToken=JSON.parse(req);
  
  console.log("req to json: "+idToken.message);
  const user = db.collection('user').doc(idToken.message);
  const doc = await user.get();
  if (!doc.exists) {
    console.log('No such document!');
    const data={
      BestScore:"0",
      Message:"",
      Image:"",
      Money:"2000",
      ShinMoney:"0",
      Tuto:"0",
      Version:"1.3.6",
      Id:"",
      NickName:""
    };

    const res =await user.set(data);
    return JSON.stringify(data);
  
    console.log('New document!');
  } else {
    console.log('Document data:', JSON.stringify(doc.data()));
    return JSON.stringify(doc.data());
  }
});

exports.addBuilding=functions.https.onCall(async(req,res)=>{
  const buildingData=JSON.parse(req);
    //const buildingData=JSON.parse(req);
    console.log("res: "+JSON.stringify(buildingData));
    const db=admin.firestore();
    const resbuilding =await db.collection('user').doc(buildingData.Uid).
    collection('building').doc(buildingData.Id).set({
      BuildingPosition_x:buildingData.BuildingPosition_x,
      BuildingPosition_y:buildingData.BuildingPosition_y,
      Building_Image:buildingData.Building_Image,
      Level:buildingData.Level,
      isFliped:buildingData.isFliped,
      isLock:buildingData.isLock,
      Id:buildingData.Id,
    });
});

exports.deleteBuilding=functions.https.onCall(async(req,res)=>{

    const buildingData=JSON.parse(req);
    const db=admin.firestore();

    const buildingRef = db.collection('user').doc(buildingData.Uid).collection('building').doc(buildingData.Id).delete();      //Uid 고치기

//return JSON.stringify( buildingData);
});

exports.getBuilding=functions.https.onCall(async(req,res)=>{

  const idToken=JSON.parse(req);
  console.log("idToken: "+JSON.stringify( idToken));
    //const buildingData=JSON.parse(req);
    const db=admin.firestore();

    const buildingRef = db.collection('user').doc(idToken.message).collection('building');      //Uid 고치기
    const snapshot = await buildingRef.get();

  
  const buildingData=[];
  snapshot.forEach(doc => {
    buildingData.push(doc.data());
    
  });
  console.log("buildingData: "+JSON.stringify( buildingData));
return JSON.stringify( buildingData);
});

exports.setUser=functions.https.onCall(async (req, res) => {
  const db=admin.firestore();
  console.log("req: "+req);
  const userData=JSON.parse(req);
  
  console.log("req to json: "+JSON.stringify(userData));
  const user = db.collection('user').doc(userData.Uid);
  const doc = await user.get();


  const data={
    BestScore:userData.BestScore,
    Message:userData.Message,
    Image:userData.Image,
    Money:userData.Money,
    ShinMoney:userData.ShinMoney,
    Tuto:userData.Tuto,
    Version:userData.Version,
    NickName:userData.NickName
  };

  if (!doc.exists) {        //문서가 존재하지 않으면
    console.log('No such document!');
    
    const res =await user.set(data);
    message.name="setUser";
    message.message="Success";
    return JSON.stringify(message);
  
  } else {                  //문서가 존재한다면

    const res =await user.set(data);
    message.name="setUser";
    message.message="Success";
    return JSON.stringify(message);
  }
});

//=====누니 관련=========
exports.setNuni=functions.https.onCall(async(req,res)=>{
  const nuniData=JSON.parse(req);
    //const buildingData=JSON.parse(req);
    console.log("res: "+JSON.stringify(nuniData));
    const db=admin.firestore();
    const resbuilding =await db.collection('user').doc(nuniData.Uid).
    collection('nuni').doc(nuniData.Id).set({
      isLock:nuniData.isLock,
      cardImage:nuniData.cardImage,
      Id:nuniData.Id,
    });
});

exports.getNuni=functions.https.onCall(async(req,res)=>{

  const idToken=JSON.parse(req);
  console.log("idToken: "+JSON.stringify( idToken));
    //const buildingData=JSON.parse(req);
    const db=admin.firestore();

    const nunigRef = db.collection('user').doc(idToken.message).collection('nuni');      
    const snapshot = await nunigRef.get();

  
  const nuniData=[];
  snapshot.forEach(doc => {
    nuniData.push(doc.data());
    
  });
  console.log("nuniData: "+JSON.stringify( nuniData));
return JSON.stringify( nuniData);
});

//=====친구 관련========

exports.getFriend=functions.https.onCall(async(req,res)=>{
  const idToken=JSON.parse(req);
  console.log("idToken: "+JSON.stringify( idToken));
    //const buildingData=JSON.parse(req);
    const friendData=[];

    const db=admin.firestore();

    const friendgRef = db.collection('user').doc(idToken.message).collection('friend');      
    const snapshot = await friendgRef.get();

    for(var i in snapshot.docs)
    {
      const doc=snapshot.docs[i];
      var friendImage=await db.collection('user').doc(doc.id).get();
     
        const friend={
          FriendName:doc.id,
          FriendImage:"",
          FriendMessage:""
        }
        friend.FriendImage=friendImage.data().Image;
        friend.FriendMessage=friendImage.data().Message;
        console.log("친구 정보: "+JSON.stringify(friendImage.data()));
        friendData.push(friend);
  
     
   
    }

  if(friendData.length==0)
  {
    console.log("FriendData: "+friendData.length);
    return null;
  }
  console.log("FriendData: "+JSON.stringify( friendData));
return JSON.stringify( friendData);
});


exports.getRequestFriend=functions.https.onCall(async(req,res)=>{
  const idToken=JSON.parse(req);
  console.log("idToken: "+JSON.stringify( idToken));
    //const buildingData=JSON.parse(req);
    const friendData=[];

    const db=admin.firestore();

    const friendgRef = db.collection('user').doc(idToken.message).collection('friendRequest');      
    const snapshot = await friendgRef.get();
console.log("snapshot: "+JSON.stringify(snapshot.docs));
    for(var i in snapshot.docs)
    {
      const doc=snapshot.docs[i];
      var friendImage=await db.collection('user').doc(doc.id).get();
     
        const friend={
          FriendName:doc.id,
          FriendImage:"",
          FriendMessage:""
        }
        friend.FriendImage=friendImage.data().Image;
        friend.FriendMessage=friendImage.data().Message;
        console.log("친구 정보: "+JSON.stringify(friendImage.data()));
        friendData.push(friend);
  
    }

  if(friendData.length==0)
  {
    console.log("FriendData: "+friendData.length);
    return null;
  }
  console.log("FriendData: "+JSON.stringify( friendData));
return JSON.stringify( friendData);
});
exports.plusFriend=functions.https.onCall(async(req,res)=>{ 

  const friendInfo=JSON.parse(req);
  const db=admin.firestore();

  //먼저 내 친구인지 확인
  const myfriend =db.collection('user').doc(friendInfo.Uid).
  collection('friend').doc(friendInfo.FriendName);

  const mydoc=await myfriend.get();

  if(!mydoc.exists)       //내 친구 목록에서 찾지 못했다면 친구 요청
 {
  const resfriend =db.collection('user').doc(friendInfo.FriendName).
    collection('friendRequest').doc(friendInfo.Uid);

    const doc=await resfriend.get();

    
      resfriend.set({
        FriendName:friendInfo.FriendName
      });
      return "success";
   
  }
  else{       //이미 추가된 친구라면 Null 리턴
    return "fail";
  }
});

exports.addFriend=functions.https.onCall(async(req,res)=>{ 

  const friendInfo=JSON.parse(req);
  const db=admin.firestore();

  const myfriend =db.collection('user').doc(friendInfo.Uid).
  collection('friend').doc(friendInfo.FriendName);
  
  
//const doc=await myfriend.get();

  console.log("doc: "+friendInfo);
  myfriend.set(
    {FriendName:friendInfo.FriendName}
    );

    const removeReqfriend =db.collection('user').doc(friendInfo.Uid).   //요청 친구 목록에서 지우기
    collection('friendRequest').doc(friendInfo.FriendName).delete();
    
});

exports.searchFriend=functions.https.onCall(async(req,res)=>{         //유저 검색
const friendInfo=JSON.parse(req);
const db=admin.firestore();
console.log("friendInfo: "+JSON.stringify( friendInfo));
  const resfriend = db.collection('user').doc(friendInfo.message);

    const doc=await resfriend.get();
    if(doc.exists)
    {
      console.log(doc.data());
      const res={
        FriendName:doc.id,
        FriendImage:doc.data().Image,
        FriendMessage:doc.data().Message
      }
      return JSON.stringify(res);
    }
    else
      return null;

    
});

exports.deleteFriend=functions.https.onCall(async(req,res)=>{         //친구 지우기
  const friendInfo=JSON.parse(req);
  const db=admin.firestore();
  console.log("friendInfo: "+JSON.stringify( friendInfo));
    const resfriend = db.collection('user').doc(friendInfo.message);
  
      const doc=await resfriend.get();
      if(doc.exists)
      {
        console.log(doc.data());
        const res={
          FriendName:doc.id,
          FriendImage:doc.data().Image,
          FriendMessage:doc.data().Message
        }
        return JSON.stringify(res);
      }
      else
        return null;
  
      
  });

  //===============방명록=======================
  exports.getVisitorBook=functions.https.onCall(async(req,res)=>{
    const idToken=JSON.parse(req);
    console.log("idToken: "+JSON.stringify( idToken));
      //const buildingData=JSON.parse(req);
      const friendData=[];
  
      const db=admin.firestore();
  
      const friendgRef = db.collection('user').doc(idToken.message).collection('visitorBook');      
      const snapshot = await friendgRef.get();
  console.log("snapshot: "+JSON.stringify(snapshot.docs));
      for(var i in snapshot.docs)
      {
        const doc=snapshot.docs[i];
        var visitor=await db.collection('user').doc(doc.data().name).get();
       console.log("doc: "+doc.data().name);
          const content={
            FriendName:doc.data().name,
            FriendImage:visitor.data().Image,
            FriendMessage:doc.data().message,
            FriendTime:doc.data().time,
          }
          content.FriendImage=visitor.data().Image;

          console.log("방명록 정보: "+JSON.stringify(content));

          friendData.push(content);
    
      }
  
    if(friendData.length==0)
    {
      console.log("FriendData: "+friendData.length);
      return null;
    }
    console.log("FriendData: "+JSON.stringify( friendData));
  return JSON.stringify( friendData);
  });

  exports.setVisitorBook=functions.https.onCall(async(req,res)=>{
    const newMessage=JSON.parse(req);
    console.log("newMessage: "+JSON.stringify( newMessage));

      const db=admin.firestore();
  
      const friendgRef = db.collection('user').doc(newMessage.Uid).collection('visitorBook').doc(newMessage.FriendTime).set({
        name:newMessage.FriendName,
        Image:newMessage.FriendImage,
        message:newMessage.FriendMessage,
        time:newMessage.FriendTime

      });      
  return JSON.stringify( "success");
  });
  //=======================업적===============================
  exports.getMyAchieveInfo=functions.https.onCall(async(req,res)=>{
    const idToken=JSON.parse(req);

    const achieveData=[];

    const db=admin.firestore();
  
    const achieveRef = db.collection('user').doc(idToken.message).collection('achieve');      
    const snapshot = await achieveRef.get();
console.log("snapshot: "+JSON.stringify(snapshot.docs));

    for(var i in snapshot.docs)
    {
      const doc=snapshot.docs[i].data();
      achieveData.push(doc);
    }
return JSON.stringify(achieveData);
  });

  exports.setMyAchieveInfo=functions.https.onCall(async(req,res)=>{
    const myAchieve=JSON.parse(req);
console.log(JSON.stringify(myAchieve));
    const achieveData=[];

    const db=admin.firestore();
  
      console.log("sfaf: "+JSON.stringify(myAchieve.Items));
      for (let index = 0; index < myAchieve.Items.length; index++) {
        console.log("index: "+JSON.stringify(myAchieve.Items[index]));
        const element = myAchieve.Items[index];
        const achieveRef = db.collection('user').doc(element.Uid).collection('achieve').doc(element.Id).set({
          Count:element.Count,
          Id:element.Id,
          Index:element.Index,
          isReward:element.isReward
          });
      }
      /*for (const key in myAchieve.Items) {
        console.log(JSON.stringify(key));
        const achieveRef = db.collection('user').doc(key.Uid).collection('achieve').doc(key.Id).set({
          Count:key.Count,
          Id:key.Id,
          Index:key.Index,
          isReward:key.isReward
          });
      }*/
      
     
return JSON.stringify(achieveData);
  });