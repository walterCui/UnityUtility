using UnityEngine;
using System.Collections;

public abstract class AssetRequestBase
{
    public abstract AsyncOperation Request
    {
        get;
    }

    public float Progress
    {
        get
        {
            if(Request != null)
                return Request.progress;
            return 0;
        }
    }

    public abstract Object Asset
    {
        get;
    }
}

