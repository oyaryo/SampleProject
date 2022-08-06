
mergeInto(LibraryManager.library, {
  // 関数呼び出し
  Hello: function () {
    window.alert("Hello, world!");
  },
  Firestore: function() {
      // firebaseのconfig
      var firebaseConfig = {
          apiKey: "AIzaSyCe-0sXMZ59zyh4NjKyy0pUzUCBQbRtdwk",
          authDomain: "meta-gallary.firebaseapp.com",
          databaseURL: "https://meta-gallary-default-rtdb.firebaseio.com",
          projectId: "meta-gallary",
          storageBucket: "meta-gallary.appspot.com",
          messagingSenderId: "239972272231",
          appId: "1:239972272231:web:9b00a0bdf3c5398d5c2cc2"

      }
      // firebaseの初期化
      firebase.initializeApp(firebaseConfig);
      var db = firebase.firestore();
      db.collection("tickets").doc("FJsZ4xxQ1MfcVEVTquil")
      .onSnapshot(function(doc) {
        console.log("Current data: ", doc.data().paymentID);
        SendMessage('TextFromFirestore', 'UpdateText', doc.data().paymentID);
    });
  }
});