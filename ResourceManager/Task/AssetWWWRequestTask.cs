using UnityEngine;
using System.Collections;

public class AssetWWWRequestTask : AssetRequestBase
{
    #region implemented abstract members of AssetRequestBase
    public override AsyncOperation Request
    {
        get
        {
            return assetBundleRequest;
        }
    }
    public override Object Asset
    {
        get
        {
            if(Request != null && assetBundleRequest.isDone)
                return assetBundleRequest.asset;
            return null;
        }
    }
    #endregion
    private AssetBundleRequest assetBundleRequest;

    public AssetWWWRequestTask(AssetBundleRequest request)
    {
        assetBundleRequest = request;
    }
}

