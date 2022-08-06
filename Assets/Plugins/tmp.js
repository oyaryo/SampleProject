Hello = function () {
  let openRequest = indexedDB.open("db", 1);

  openRequest.onerror = function () {
    console.error("Error: ", openRequest.error);
  };

  openRequest.onsuccess = function () {
    let db = openRequest.result;
    let transaction = db.transaction("tokens");
    let tokens = transaction.objectStore("tokens");
    let emailIndex = tokens.index("email_idx");

    let request = emailIndex.getAll("test@example.com");

    request.onsuccess = function () {
      if (request.result !== undefined) {
        console.log("tokens: ", request.result[0].uToken);
      } else {
        console.log("No such tokens");
      }
    };
  };

  setTimeout(() => {
    var returnStr = testText;
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  }, 5000);
}
