// The Cloud Functions for Firebase SDK to create Cloud Functions and set up triggers.
const functions = require('firebase-functions');
var bodyParser = require('body-parser')
// The Firebase Admin SDK to access Firestore.
const admin = require('firebase-admin');
const { firestore } = require('firebase-admin/firestore');
admin.initializeApp();

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
      Version:"1.3.6"
    };

    const res =await user.set(data);
    return JSON.stringify(data);
  
    console.log('New document!');
  } else {
    console.log('Document data:', doc.data());
  }
});

exports.addBuilding=functions.https.onCall(async(req,res)=>{
  const buildingData=JSON.parse(req);
    //const buildingData=JSON.parse(req);
    const db=admin.firestore();
    const resbuilding =await db.collection('user').doc('2YDwe89Wf6aKOvc0EtQYzHKMW2r1').
    collection('building').doc('buildingid').set({
      BuildingPosition_X:"",
      BuildingPosition_y:"",
      Building_Image:"",
      Level:"",
      isFliped:"",
      isLock:"",
    });
    
});
