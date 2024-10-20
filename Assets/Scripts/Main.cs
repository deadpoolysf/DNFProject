using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZMGC.Battle;
using ZMGC.Hall;
using ZM.AssetFrameWork;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //初始化资源管理框架
        ZMAssetsFrame.Instance.InitFrameWork();
        //初始化UI框架
        UIModule.Instance.Initialize();
        //构建大厅世界
        WorldManager.CreateWorld<HallWorld>();

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 资源解压完成后调用
    /// </summary>
    public void StartGame()
    {

    }


    // Update is called once per frame
    void Update()
    {
        WorldManager.OnUpdate();
    }
}
