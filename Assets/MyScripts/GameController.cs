using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using LoginResult = PlayFab.ClientModels.LoginResult;

public class GameController : MonoBehaviour
{
    // 作品毎に異なるIDをプログラム内で共有する
    [SerializeField] string _pieceID;
    public string pieceID {get => _pieceID; set => _pieceID = value;}
    
    /// <summary>
    /// 現在の所持仮想通貨を表示するための処理
    /// </summary>

    // 現在の所持金を表示するためのオブジェクトを格納する変数
    public Text text_CurrentVC;
    int VC;
    public void GetCurrentVirtualCurency()
    {
        GetUserInventry();
        
        // 画面に現在の所持金を表示するための命令文
        text_CurrentVC.text = "Current Virtual Curency: " + VC + " SC";
    }

    void GetUserInventry()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest
        {

        },
        result =>
        {
            foreach (var virtualCurrency in result.VirtualCurrency)
            {
                if(virtualCurrency.Key == "SC")
                {
                    VC = virtualCurrency.Value;
                }
            }
        },
        error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }


    /// <summary>
    /// アイテム購入に使用する関数
    /// </summary>
    public List<CatalogItem> catalogItems { get; private set; }
    public List<StoreItem> storeItems { get; private set; }
    StoreItem purchaseItem;
    const string CATALOG_VERSION = "Main";
    const string TICKET_STORE_ID = "ticket_store";
    const string VC_SC = "SC";

    public Text purchaseText;

    public void GetCatalogData()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest()
        {
            CatalogVersion = CATALOG_VERSION,
        },
        result =>
        {
            Debug.Log("カタログデータ取得成功");
            catalogItems = result.Catalog;
            GetStoreData();
        },
        error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    void GetStoreData()
    {
        PlayFabClientAPI.GetStoreItems(new GetStoreItemsRequest()
        {
            CatalogVersion = CATALOG_VERSION,
            StoreId = TICKET_STORE_ID
        },
        result =>
        {
            Debug.Log("ストアデータ取得成功");
            storeItems = result.Store;
            purchaseItem = storeItems.Find(x => x.ItemId == "ticket-A");
            PurchaseItem();
        },
        error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    void PurchaseItem()
    {
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest()
        {
            CatalogVersion = CATALOG_VERSION,
            StoreId = TICKET_STORE_ID,
            ItemId = purchaseItem.ItemId,
            VirtualCurrency = VC_SC,
            Price = (int)purchaseItem.VirtualCurrencyPrices[VC_SC]
        },
        result =>
        {
            //Debug.Log($"{result.Items[0].DisplayName}:購入成功");
            purchaseText.text = "購入完了。ありがとうございました！";
        },
        error =>
        {
            if (error.Error == PlayFabErrorCode.InsufficientFunds)
            {
                purchaseText.text = "あらお金が足りないわね。";
            }
            Debug.Log(error.GenerateErrorReport());
        });
    }

    void ChangeText(string text)
    {
        Text txt = GetComponentInChildren<Text>();
        txt.text = text;
    }

    void Start()
    {
        GetCurrentVirtualCurency();
    }
}
