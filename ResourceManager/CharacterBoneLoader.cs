using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XUtility;

/// <summary>
/// Character bone loader.
/// </summary>
public class CharacterBoneLoader : DownLoaderBase
{
    public override void LoadAsyn(string name, CustomerAction<Object> callback)
    {
//        WWW tem = new WWW(AssetbundleBaseURL+name + "_characterBase.assetbundle");
//        callback(tem.assetBundle.mainAsset);
//        tem.assetBundle.Unload(false);
//        return ;
        if(!CheckBuffer(name,callback))
            AddTask(new DownLoaderTask(AssetbundleBaseURL+name + "_characterBase.assetbundle",1),name);
    }

    protected override AssetRequestBase LoadAsset(ResourceLoadTask task)
    {
        return new AssetWWWRequestTask(task.WWWTask.Asset.LoadAsync("characterBase",typeof(GameObject)));
    }
}

