using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.Json;
using System.Linq;

public class PlayFabController : MonoBehaviour
{
    void Start()
    {
        // PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    //    PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
    //    {
    //        TitleId = PlayFabSettings.TitleId,
    //        CustomId = "100",
    //        CreateAccount = true
    //    }, result =>
    //    {
    //        Debug.Log("ログイン成功！");
    //    }, error =>
    //    {
    //        Debug.Log(error.GenerateErrorReport());
    //    });
    }

    #region プレイヤーデータの更新・削除
    private void SetUserData()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>()
            {
                {"Name", "nekojoker1234"},
                {"Exp", "1000"}
            }
        }, result =>
        {
            Debug.Log("プレイヤーデータの登録成功！");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    private void DeleteUserData()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>()
            {
                {"Name", "nekojoker1234"},
                //{"Exp", "500"}
            },
            KeysToRemove = new List<string> { "Exp" }
        }, result =>
        {
            Debug.Log("プレイヤーデータの登録成功！");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    #endregion

    #region プレイヤーデータの更新・取得（JSON）
    void SetUserDataJson()
    {
        var questInfos = new List<QuestInfo>
        {
            new QuestInfo { Id = 1, ClearTime = 20, Score = 100 },
            new QuestInfo { Id = 2, ClearTime = 30, Score = 200 },
        };

        PlayFabClientAPI.UpdateUserData(
            new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>()
                {
                    { "QuestInfo", PlayFab.Json.PlayFabSimpleJson.SerializeObject(questInfos) }
                }
            },
        result => { Debug.Log("プレイヤーデータの登録成功！！"); },
        error => { Debug.Log(error.GenerateErrorReport()); });
    }

    public class QuestInfo
    {
        public int Id { get; set; }
        public int ClearTime { get; set; }
        public int Score { get; set; }
    }

    void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
        result =>
        {
            var questInfos = PlayFabSimpleJson.DeserializeObject<List<QuestInfo>>(result.Data["QuestInfo"].Value);
            foreach (var quest in questInfos)
            {
                Debug.Log($"Id: {quest.Id}");
                Debug.Log($"ClearTime: {quest.ClearTime}");
                Debug.Log($"Score: {quest.Score}");
            }
        },
        error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    #endregion

    #region タイトルデータの取得
    void GetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
            result =>
            {
                if (result.Data.ContainsKey("DeveloperWebSite"))
                {
                    Debug.Log("DeveloperWebSite: "
                        + result.Data["DeveloperWebSite"]);

                }
                if (result.Data.ContainsKey("QuestMaster"))
                {
                    var questMaster = PlayFabSimpleJson.DeserializeObject<List<QuestMaster>>(result.Data["QuestMaster"]);
                    foreach (var quest in questMaster)
                    {
                        Debug.Log($"Id: {quest.Id}");
                        Debug.Log($"Title: {quest.Title}");
                        Debug.Log($"Gold: {quest.Gold}");
                    }
                }
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }
    public class QuestMaster
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Gold { get; set; }
    }
    #endregion

    #region アイテム購入
    public List<CatalogItem> CatalogItems { get; private set; }
    public List<StoreItem> StoreItems { get; private set; }
    StoreItem purchaseItem;
    const string CATALOG_VERSION = "Main";
    const string GOLD_STORE_ID = "gold_store";
    const string VC_GD = "GD";

    public void GetCatalogData()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest()
        {
            CatalogVersion = CATALOG_VERSION,
        }
        , result =>
        {
            Debug.Log("カタログデータ取得成功！");
            CatalogItems = result.Catalog;
        }
        , error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    public void GetStoreData()
    {
        PlayFabClientAPI.GetStoreItems(new GetStoreItemsRequest()
        {
            CatalogVersion = CATALOG_VERSION,
            StoreId = GOLD_STORE_ID
        }
        , result =>
        {
            Debug.Log("ストアデータ取得成功！");
            StoreItems = result.Store;
            purchaseItem = StoreItems.Find(x => x.ItemId == "apple");
            PurchaseItem();
        }
        , error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    public void PurchaseItem()
    {
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest()
        {
            CatalogVersion = CATALOG_VERSION,
            StoreId = GOLD_STORE_ID,
            ItemId = purchaseItem.ItemId,
            VirtualCurrency = VC_GD,
            Price = (int)purchaseItem.VirtualCurrencyPrices[VC_GD]
        }, result =>
        {
            Debug.Log($"{result.Items[0].DisplayName}:購入成功！");
        }, error =>
        {
            if (error.Error == PlayFabErrorCode.InsufficientFunds)
            {
                Debug.Log("金額が不足しているため購入できません。");
            }
            Debug.Log(error.GenerateErrorReport());
        });
    }
    #endregion

    #region インベントリの取得
    public List<ItemInstance> UserInventry { get; private set; }
    private void GetUserInventry()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest
        {
        }, result =>
        {
            Debug.Log("インベントリの取得成功！");
            UserInventry = result.Inventory;
            foreach (var item in UserInventry)
            {
                Debug.Log("アイテムID:" + item.ItemId);
                Debug.Log("表示名:" + item.DisplayName);
            }

            var consumeItem = UserInventry.Find(x => x.ItemId == "banana");
            ConsumeItem(consumeItem.ItemInstanceId);
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    #endregion

    #region アイテム消費
    private void ConsumeItem(string itemInstanceId)
    {
        PlayFabClientAPI.ConsumeItem(new ConsumeItemRequest
        {
            ItemInstanceId = itemInstanceId,
            ConsumeCount = 2
        }, result =>
        {
            Debug.Log("アイテムの消費成功! ");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    #endregion

    #region 仮想通貨の操作
    private void AddUserVirtualCurrency()
    {
        PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = VC_GD,
            Amount = 1000
        }, result =>
        {
            Debug.Log("仮想通貨をふやしました! ");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    private void SubtractUserVirtualCurrency()
    {
        PlayFabClientAPI.SubtractUserVirtualCurrency(new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = VC_GD,
            Amount = 1000
        }, result =>
        {
            Debug.Log("仮想通貨を減らしました! ");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    #endregion

    #region バンドルの購入
    public void PurchaseBundle()
    {
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest()
        {
            CatalogVersion = CATALOG_VERSION,
            StoreId = GOLD_STORE_ID,
            ItemId = "gem_small_bundle",
            VirtualCurrency = VC_GD,
            Price = 200000
        }, result =>
        {
            Debug.Log($"{result.Items[0].DisplayName}:購入成功! ");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    #endregion

    #region コンテナの消費
    public List<ItemInstance> ContainersAndKeys { get; private set; }
    private void GetUserInventryContainer()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest
        {
        }, result =>
        {
            Debug.Log("インベントリの取得成功！");
            ContainersAndKeys = result.Inventory.Where(x => x.ItemClass == "Treasure" || x.ItemClass == "Key").ToList();

            UnlockContainerInstance();
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    public void UnlockContainerInstance()
    {
        PlayFabClientAPI.UnlockContainerInstance(new UnlockContainerInstanceRequest
        {
            CatalogVersion = CATALOG_VERSION,
            ContainerItemInstanceId = ContainersAndKeys.Find(x => x.ItemId == "con_treasure_a").ItemInstanceId,
            KeyItemInstanceId = ContainersAndKeys.Find(x => x.ItemId == "key_a").ItemInstanceId
        }, result =>
        {
            Debug.Log("コンテナの消費成功! ! ");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    #endregion

    #region ランキング更新
    public void SubmitScore(int playerScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = playerScore
                }
            }
        }, result =>
        {
            Debug.Log("スコア送信完了! ");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    #endregion

    #region ランキング取得
    public void RequestLeaderboard()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "HighScore",
            StartPosition = 0,
            MaxResultsCount = 10,
            Version = 0
        }, result =>
        {
            result.Leaderboard.ForEach(x => Debug.Log($"{x.Position + 1}位:{x.DisplayName} " + $"スコア {x.StatValue}"));
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    #endregion

    #region ログイン処理の簡略化
    //void Start()
    //{
    //    PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    //}
    //void OnEnable()
    //{
    //    PlayFabAuthService.OnLoginSuccess += PlayFabLogin_OnLoginSuccess;
    //}
    //private void OnDisable()
    //{
    //    PlayFabAuthService.OnLoginSuccess -= PlayFabLogin_OnLoginSuccess;
    //}
    //private void PlayFabLogin_OnLoginSuccess(LoginResult result)
    //{
    //    Debug.Log("Login Success!");

    //    if (result.NewlyCreated)
    //    {
    //        PlayFabAuthService.Instance.SaveCustomId();
    //    }
    //}
    #endregion

    #region ログイン時に各種データ取得
    public GetPlayerCombinedInfoRequestParams InfoRequestParams;
    // void Start()
    // {
    //     PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
    //     {
    //         TitleId = PlayFabSettings.TitleId,
    //         CustomId = "100",
    //         CreateAccount = true,
    //         InfoRequestParameters = InfoRequestParams
    //     }, result =>
    //     {
    //         Debug.Log("ログイン成功！");

    //         Debug.Log("プレイヤーデータ: " + result.InfoResultPayload.UserData["Name"].Value);
    //         Debug.Log("タイトルデータ: " + result.InfoResultPayload.TitleData["DeveloperWebSite"]);
    //         Debug.Log("仮想通貨(ジェム): " + result.InfoResultPayload.UserVirtualCurrency["GM"]);
    //     }, error =>
    //     {
    //         Debug.Log(error.GenerateErrorReport());
    //     });
    // }
    #endregion
}

