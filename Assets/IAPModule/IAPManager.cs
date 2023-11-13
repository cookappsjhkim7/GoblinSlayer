using System;
using System.Globalization;
using System.Linq;
using CookApps.AnalyticsLite;
using IAPModule;
using UnityEngine.Purchasing;
// using CookApps.AnalyticsLite;


public class IAPManager : SingletonMonoBehaviour<IAPManager>
{
    protected IAPManager()
    {
    }

    private bool isReady = false;

    public bool IsReady
    {
        get { return isReady; }
    }

    protected Action<IAPModule.IAPSuccessResult> OnIapSuccessed = null;
    protected Action<IAPModule.IAPFailResult> OnIapFailed = null;
    //
    // protected Action OnIapComplete = null;
    // protected Action OnIapFailed = null;

    private IAPModule.IAPModule iapModule = null;

    // 초기화중 재진입 방지용.
    private bool isInitializing = false;

    public bool IsInitializing
    {
        get { return isInitializing; }
    }

    private string currencySymbol = null;

    //  private string[] productList = null;

    private Action<bool> _OnRetryInitComplete;

    // 인앱 관리자 초기화 
    //public void Initialize(string [] productList)
    public void Initialize()
    {
        //  this.productList = productList;
        if (iapModule != null && IsReady) // 이미 초기화 된 경우
        {
            return;
        }

        if (isInitializing) return;

        isInitializing = true;
        iapModule = new IAPModule.IAPUnity();
        iapModule.Initialize(OnInitializeResult);
    }

    public void ReInitialize(Action<bool> OnRetryInitComplete_)
    {
        _OnRetryInitComplete = OnRetryInitComplete_;

        if (isInitializing) return;

        isReady = false;
        isInitializing = true;
        if (iapModule == null)
        {
            iapModule = new IAPModule.IAPUnity();
        }

        iapModule.Initialize(OnInitializeResult);
    }

    public void RetryToInit(Action<bool> OnRetryInitComplete_)
    {
        _OnRetryInitComplete = OnRetryInitComplete_;

        Initialize();
    }

    // 인앱 관리자 초기화 결과 
    private void OnInitializeResult(bool result)
    {
        isInitializing = false;
        isReady = result;

        if (_OnRetryInitComplete != null)
        {
            _OnRetryInitComplete(isReady);
            _OnRetryInitComplete = null;
        }

        CheckRestoreTransactions(); //unconsumed item check
    }

    // 상품의 가격 문자 가져오기 
    public string GetProductMoneyString(string idString)
    {
        if (iapModule == null)
            return "none";

        return iapModule.GetProductPriceString(idString);
    }

    // 상품의 가격 숫자만 가져오기 
    public decimal GetProductPrice(string idString)
    {
        if (iapModule == null)
            return 0m;

        return iapModule.GetProductPrice(idString);
    }

    // 상품의 통화 코드 가져오기 
    public string GetProductCurrencyCode(string idString)
    {
        if (iapModule == null)
            return "";

        return iapModule.GetProductCurrencyCode(idString);
    }

    public string GetProductTitle(string idString)
    {
        if (iapModule == null)
            return "Title";

        return iapModule.GetProductTitle(idString);
    }

    public string GetProductCurrencySymbol(string idString)
    {
        if (iapModule == null)
            return "";
        if (currencySymbol != null)
        {
            return currencySymbol;
        }
        else if (TryGetCurrencySymbol(GetProductCurrencyCode(idString), out currencySymbol))
        {
            return currencySymbol;
        }
        else
        {
            return "";
        }
    }

    private bool TryGetCurrencySymbol(string ISOCurrencySymbol, out string symbol)
    {
        symbol = CultureInfo
            .GetCultures(CultureTypes.AllCultures)
            .Where(c => !c.IsNeutralCulture)
            .Select(culture =>
            {
                try
                {
                    return new RegionInfo(culture.Name);
                }
                catch
                {
                    return null;
                }
            })
            .Where(ri => ri != null && ri.ISOCurrencySymbol == ISOCurrencySymbol)
            .Select(ri => ri.CurrencySymbol)
            .FirstOrDefault();
        return symbol != null;
    }
    
    //아이템 구입하기.
    public void PurchaseItem(string productID, Action<IAPModule.IAPSuccessResult> OnPurchaseSuccess,
        Action<IAPModule.IAPFailResult> OnPurchaseFail = null)
    {
#if UNITY_EDITOR
        OnPurchaseSuccess?.Invoke(null);
        return;
#endif
        
        OnIapSuccessed = OnPurchaseSuccess;
        OnIapFailed = OnPurchaseFail;
    
        iapModule.PurchaseItem(productID, OnPurchaseItemSuccess,
            (result) => { OnPurchaseItemFail(productID, result); });
    }
    
    private void OnPurchaseItemSuccess(IAPModule.IAPSuccessResult result)
    {
        Success(result.GetProduct());
        
        if (OnIapSuccessed != null)
            OnIapSuccessed(result);
    }

    private void OnPurchaseItemFail(string id, IAPModule.IAPFailResult result)
    {
        
        if (OnIapFailed != null)
            OnIapFailed(result);
        
    }

    public void UpdateOnPurchase(IAPModule.IAPSuccessResult result, int usdPrice, int amount)
    {
        //  PlayerDataManager.MyInfo.UpdatePurchaseInfo(usdPrice);
        // AppEventManager.AppEvent_PURCHASES(usdPrice, amount, type, result.GetProductID());
        // AppEventForSingular.SendPurchaseEvent(result);
    }

    
    void Success(UnityEngine.Purchasing.Product product)
    {
        string order_id = product.transactionID;
        string product_id = product.definition.id;
        string currency_code = product.metadata.isoCurrencyCode;
        double currency_price = (double)product.metadata.localizedPrice;
        string receipt = product.receipt;

        CAppEventLite.ReportInAppPurchase(order_id, 
            product_id,
            currency_code,
            currency_price,
            receipt);
    }
    
    
    #region Restore Purchase

    public void ConsumePurchase(Product p)
    {
        iapModule.ConsumePurchase(p);
    }

    public void ConsumePurchase(string productId)
    {
        iapModule.ConsumePurchase(productId);
    }

    public void CheckRestoreTransactions()
    {
        iapModule.RegCallbackEvents(OnRestoreSuccess, OnRestoreFailed);
        iapModule.RestorePurchase((restoreSuccess) => { });
    }

    private void OnRestoreSuccess(IAPSuccessResult result)
    {
        string productId = result.GetProductID();
    }

    private void OnRestoreFailed(IAPFailResult result)
    {
    }

    #endregion
}