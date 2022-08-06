mergeInto(LibraryManager.library, {

  // 関数呼び出し
  Hello: function () {
    window.alert("Hello");
  },

  // 数値型の引数と戻り値
  AddNumbers: function (x, y) {
    return x + y;
  },

  // 数値型以外の型の引数
  PrintFloatArray: function (array, size) {
    for(var i = 0; i < size; i++)
      console.log(HEAPF32[(array >> 2) + i]);
  },

  // 文字列型の引数
  HelloString: function (str) {
    window.alert(Pointer_stringify(str));
  },

  // 文字列の戻り値
  StringReturnValueFunction: function () {
    var returnStr = "bla2";
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  // WebGLテクスチャのバインド
  BindWebGLTexture: function (texture) {
    GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
  },

  // indexedDBの戻り値テスト
  TestIndexedDB: function () {
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
          //console.log("tokens: ", request.result[0].uToken);
          var returnStr = request.result[0].uToken;
          var bufferSize = lengthBytesUTF8(returnStr) + 1;
          var buffer = _malloc(bufferSize);
          stringToUTF8(returnStr, buffer, bufferSize);
          console.log("TEST: ", returnStr);
          return returnStr;
        } else {
          console.log("No such tokens");
        }
      };
    };

  },
});