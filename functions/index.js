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

// Listens for new messages added to /messages/:documentId/original and creates an
// uppercase version of the message to /messages/:documentId/uppercase
exports.makeUppercase = functions.firestore.document('/messages/{documentId}')
    .onCreate((snap, context) => {
      // Grab the current value of what was written to Firestore.
      const original = snap.data().original;

      // Access the parameter `{documentId}` with `context.params`
      functions.logger.log('Uppercasing', context.params.documentId, original);
      
      const uppercase = original.toUpperCase();
      
      // You must return a Promise when performing asynchronous tasks inside a Functions such as
      // writing to Firestore.
      // Setting an 'uppercase' field in Firestore document returns a Promise.
      return snap.ref.set({uppercase}, {merge: true});
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
      Id:""
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
    Version:userData.Version
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