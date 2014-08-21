using UnityEngine;
using System.Collections;

public class ResourceLoadTask
{
    public DownLoaderTask WWWTask
    {
        get;
        private set;
    }

    public AssetRequestBase RequestTask
    {
        get;
        private set;
    }

    public bool IsLocal
    {
        get;
        private set;
    }

    public Object Asset
    {
        get
        {
            if(RequestTask != null)
                return RequestTask.Asset;
            return null;
        }
    }

    public string Name
    {
        get;
        private set;
    }

    public string Url
    {
        get
        {
            if(WWWTask != null)
                return WWWTask.URL;
            return Name;
        }
    }

    public bool WWWing
    {
        get;
        set;
    }

    public bool OnlyDownload
    {
        get;
        private set;
    }

    public static ResourceLoadTask Get(DownLoaderTask www, bool onlyDownload)
    {
        return new ResourceLoadTask(""){WWWTask = www, IsLocal = false,OnlyDownload = onlyDownload};
    }

    public static ResourceLoadTask Get(DownLoaderTask www, string name)
    {
        return new ResourceLoadTask(name){WWWTask = www, IsLocal = false,OnlyDownload = false};
    }

    public static ResourceLoadTask Get(string name)
    {
        return new ResourceLoadTask(name){ IsLocal = true};
    }


    public void CreateRequest(AssetRequestBase request)
    {
        RequestTask = request;
    }
//    public ResourceLoadTask()
//    {
//        WWWing = false;
//    }

    public ResourceLoadTask(string name)
    {
        WWWing = false;
        Name = name;
    }
}

