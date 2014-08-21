using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XUtility;

/// <summary>
/// Resources base.
///  需要提供的接口有：添加下载任务，移除下载任务，查询下载进度.
///  内部的功能包括：缓存，下载实时监控，资源的控制（动态清理不必要的垃圾资源）.
/// </summary>
public class DownLoaderBase
{
    public static string AssetbundleBaseURL
    {
        get
        {
            if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer)
                return Application.dataPath+"/assetbundles/";
            else
                return "file://" + Application.dataPath + "/assetbundles/";
        }
    }

    protected List<ResourceLoadTask> assetBundleLoadingBuffer = new List<ResourceLoadTask>();

    protected Dictionary<string, ResourceLoadTask> tasks = new Dictionary<string, ResourceLoadTask>();

    protected Dictionary<string, ResourceLoadTask> buffers = new Dictionary<string, ResourceLoadTask>();

    protected Dictionary<string, List<CustomerAction<Object>>> loadedCallback = new Dictionary<string, List<CustomerAction<Object>>>();

    protected ResourceLoadTask currentDownloadTask;

    #region public Methord

    /// <summary>
    /// Adds the task.
    /// only Download.
    /// </summary>
    /// <param name="url">URL.</param>
    /// <param name="version">Version.</param>
    public void AddTask(string url, int version)
    {
        if(HasTask(url))
            return;
        tasks.Add(url,ResourceLoadTask.Get(new DownLoaderTask(url,version), true));
    }

    public void AddTask(DownLoaderTask task, string name)
    {
        if(HasTask(task.URL))
            return;
        tasks.Add(task.URL,ResourceLoadTask.Get(task,name));
    }

    public virtual void LoadAsyn(string name, CustomerAction<Object> callback)
    {
        if(!CheckBuffer(name,callback))
            AddTask(new DownLoaderTask(AssetbundleBaseURL+name + ".assetbundle",1),name);
        return;
    }
    public void RemoveTask(string url)
    {
    }

    public void NotifyWhenLoaded(ResourceLoadTask task)
    {
        buffers.Add(task.Name, task);
        if(loadedCallback.ContainsKey(task.Name))
        {
            List<CustomerAction<Object>> temp = loadedCallback[task.Name];
            for(int i = 0, max = temp.Count; i < max; i++)
                temp[i](task.Asset);
        }
    }


    private int indexFor;

    private int maxFor;

    private AssetRequestBase tempAssetRequestTask;

    /// <summary>
    /// Update this instance.
    /// 
    /// </summary>
    public virtual void Update()
    {
        //create new www.
        if(currentDownloadTask == null)
        {
            if(tasks.Count > 0)
            {
                CreateWWW();
            }
        }
        else
        {
            //chech if done or not.
            if(currentDownloadTask.WWWTask.IsDone)
            {
                currentDownloadTask.WWWing = false;
                if(currentDownloadTask.WWWTask.ErrorMsg != null)
                {
                    GlobalLog.LogWarning(currentDownloadTask.Name +" load failed, " + currentDownloadTask.WWWTask.ErrorMsg);
                }
                else
                {
                    if(!currentDownloadTask.OnlyDownload)
                        AdddLoadingBuffer(currentDownloadTask);
                }
                currentDownloadTask = null;
            }
        }

        //check loaded or not.
        for(indexFor = 0, maxFor = assetBundleLoadingBuffer.Count; indexFor < maxFor; indexFor++)
        {
            if(assetBundleLoadingBuffer[indexFor] == null)
                continue;
            if(assetBundleLoadingBuffer[indexFor].RequestTask.Request.isDone)
            {
                NotifyWhenLoaded(assetBundleLoadingBuffer[indexFor]);
                assetBundleLoadingBuffer[indexFor] = null;
            }
        }
    }
    #endregion

    protected bool CheckBuffer(string name, CustomerAction<Object> callback)
    {
        if(string.IsNullOrEmpty(name))
            return true;
        //if(callback != null)
        {
            if(buffers.ContainsKey(name))
            {
                if(callback != null)
                    callback(buffers[name].Asset);
                return true;
            }
            else
            {
                if(callback != null)
                {
                    if(!loadedCallback.ContainsKey(name))
                        loadedCallback.Add(name, new List<CustomerAction<Object>>());
                    loadedCallback[name].Add(callback);
                }
                return false;
            }
        }
    }

    protected virtual AssetRequestBase LoadAsset(ResourceLoadTask task)
    {
        if(task.IsLocal)
            return new AssetLocalRequestTask(Resources.LoadAsync(task.Name));
        else
            return new AssetWWWRequestTask(task.WWWTask.Asset.LoadAsync(task.Name,task.WWWTask.AssetType));
    }

    private void CreateWWW()
    {
        Dictionary<string,ResourceLoadTask>.Enumerator enumertor = tasks.GetEnumerator();
        if(enumertor.MoveNext())
        {
            currentDownloadTask = enumertor.Current.Value;
            if(!currentDownloadTask.IsLocal)
            {
                currentDownloadTask.WWWing = currentDownloadTask.WWWTask.StartDownload();
            }
            else
            {
                //add load buff.TODO.
            }
            if(!currentDownloadTask.WWWing)
            {
                currentDownloadTask = null;
            }
            tasks.Remove(enumertor.Current.Key);
        }
    }

    private void AdddLoadingBuffer(ResourceLoadTask val)
    {
        tempAssetRequestTask = LoadAsset(val);
        if(tempAssetRequestTask != null)
        {
            val.CreateRequest(tempAssetRequestTask);
            for(indexFor = 0, maxFor = assetBundleLoadingBuffer.Count; indexFor < maxFor; indexFor++)
            {
                if(assetBundleLoadingBuffer[indexFor] == null)
                {
                    assetBundleLoadingBuffer[indexFor] = val;
                    break;
                }
            }
            if(indexFor == maxFor)
                assetBundleLoadingBuffer.Add(val);
        }
    }
    
    private bool HasTask(string url)
    {
        if(string.IsNullOrEmpty(url) || tasks.ContainsKey(url))
            return true;
        if(currentDownloadTask != null && !currentDownloadTask.IsLocal && currentDownloadTask.WWWTask.URL == url)
            return true;

        for(indexFor = 0, maxFor = assetBundleLoadingBuffer.Count; indexFor < maxFor; indexFor++)
        {
            if(assetBundleLoadingBuffer[indexFor] == null)
                continue;
            if(assetBundleLoadingBuffer[indexFor].Url == url)
            {
                return true;
            }
        }
        return false;
    }
}

