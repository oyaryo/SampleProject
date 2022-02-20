using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using LoginResult = PlayFab.ClientModels.LoginResult;

public class GameController : MonoBehaviour
{
    [SerializeField] string _pieceID;
    public string pieceID {get => _pieceID; set => _pieceID = value;}
    
    void Start()
    {
        GetCurrentVirtualCurency();
        CheckTicket();
    }

    /// <summary>
    /// 現在の所持仮想通貨を表示するための処理
    /// </summary>
    public Text text_CurrentVC;    
    private string _VC;
    string VC{
        get{ return _VC; }
        set{ 
            _VC = value;
            text_CurrentVC.text = "Current Virtual Curency: " + VC + " SC";
        }
    } 
    void GetCurrentVirtualCurency()
    {
        GetUserInventry();
    }

    void GetUserInventry()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest
        {

        },
        result =>
        {
            VC = result.VirtualCurrency[VC_SC].ToString();
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

    public Text text_Purchase;

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
            text_Purchase.text = "購入完了。ありがとうございました！";
            ticketFlag = true;
            GetCurrentVirtualCurency();
        },
        error =>
        {
            if (error.Error == PlayFabErrorCode.InsufficientFunds)
            {
                text_Purchase.text = "あらお金が足りないわね。";
                return;
            }
            Debug.Log(error.GenerateErrorReport());
        });
    }

    // チケットを所持しているかどうかをチェックする
    public bool ticketFlag {get; set;}
    public List<ItemInstance> UserInventry { get; private set; }
    public void CheckTicket()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest
        {
            
        },
        result =>
        {
            UserInventry = result.Inventory;
            foreach (var item in UserInventry)
            {
                if (item.ItemId == "ticket-A")
                {
                    ticketFlag = true;
                }
            }
            //var consumeItem = UserInventry.Find(x => x.ItemId == "apple");
            //ConsumeItem(consumeItem.ItemInstanceId);
        },
        error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
