using UnityEngine;
using System.Collections;
using XUtility;

/// <summary>
/// WWW task.
/// </summary>
public class DownLoaderTask
{
    public string URL
    {
        get;
        private set;
    }

    public int Version
    {
        get;
        private set;
    }

    public float Progress
    {
        get
        {
            if(www != null)
                return www.progress;
            else
                return 0;
        }
    }

    public bool IsDone
    {
        get{
            if(www != null)
                return www.isDone;
            return false;
        }
    }

    /// <summary>
    /// Gets the asset.
    /// </summary>
    /// <value>The asset.</value>
    public AssetBundle Asset
    {
        get
        {
            if(www != null && www.isDone)
                return www.assetBundle;
            return null;
        }
    }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    /// <value>The error message.</value>
    public string ErrorMsg
    {
        get
        {
            if(www != null)
                return www.error;
            return null;
        }
    }

    public System.Type AssetType = typeof(GameObject);

    public bool UseCache = false;

    private WWW www;

    private LogWraper log = LogWraper.GetLog<DownLoaderTask>();

    /// <summary>
    /// Starts the load.
    /// </summary>
    public bool StartDownload()
    {
        if(string.IsNullOrEmpty(URL))
            return false;
        if(www != null)
        {
            log.LogError(" www is not null, so can't start new load!The name is " + URL);
            return false;
        }

        if(UseCache)
            www = WWW.LoadFromCacheOrDownload(URL,Version);
        else
            www = new WWW(URL);

        return true;
    }

    /// <summary>
    /// Stops the load.
    /// </summary>
    public void StopDownload()
    {
    }

    /// <summary>
    /// Release this instance.
    /// </summary>
    public void Release(bool unloadAllLoadedObjects)
    {
        if(www == null)
            return;
        if(www.assetBundle != null)
            www.assetBundle.Unload(unloadAllLoadedObjects);
        www = null;
    }

    public DownLoaderTask(string url, int version)
    {
        URL = url;
        Version = version;
    }
}

