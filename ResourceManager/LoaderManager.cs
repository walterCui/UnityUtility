using UnityEngine;
using System.Collections;

/// <summary>
/// Loader manager.
/// </summary>
[AddComponentMenu("X/Loader/LoaderManager")]
public class LoaderManager : MonoBehaviour
{
    public static LoaderManager Instance
    {
        get;
        private set;
    }

    public CharacterBoneLoader BoneLoader
    {
        get;
        private set;
    }

    public CharacterElementLoader CharElementLoader
    {
        get;
        private set;
    }

    private ArrayList loaders;

    private int indexFor;

    private int maxFor;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    { 
        
        
        BoneLoader = new CharacterBoneLoader();
        CharElementLoader = new CharacterElementLoader();
        loaders = new ArrayList(){BoneLoader,
            CharElementLoader,
        };
    }
	
    // Update is called once per frame
    void Update()
    {
        for(indexFor = 0, maxFor = loaders.Count; indexFor < maxFor; indexFor++)
            (loaders[indexFor] as DownLoaderBase).Update();
    }
}

