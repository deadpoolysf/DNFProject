using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZMGC.Battle;
using ZMGC.Hall;

public class Main : MonoBehaviour
{
    public static Main Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        //初始化UI框架
        UIModule.Instance.Initialize();
        //构建大厅世界
        WorldManager.CreateWorld<HallWorld>();

        DontDestroyOnLoad(gameObject);
    }

    public void AsyncLoadScene()
    {
        UIModule.Instance.PopUpWindow<LoadingWindow>();
        StartCoroutine(IAsyncLoadScene());
    }

    IEnumerator IAsyncLoadScene()
    {
        //异步场景加载
        AsyncOperation ao = SceneManager.LoadSceneAsync("Battle");
        //默认不允许场景激活
        ao.allowSceneActivation = false;

        float curProgress = 0;
        float maxProgress = 100;

        //unity加载进度只会从0-90，剩余10需要用代码进行过渡
        while(curProgress < 90)
        {
            curProgress = ao.progress * 100.0f;
            //通过一个事件把进度抛出
            UIEventControl.DispensEvent(UIEventEnum.SceneProgressUpdate,curProgress);
            //等一个空帧，让ui有一个渲染的过程
            yield return null;
        }

        while(curProgress < maxProgress)
        {
            curProgress++;
            //通过一个事件把进度抛出
            UIEventControl.DispensEvent(UIEventEnum.SceneProgressUpdate, curProgress);
            //等一个空帧，让ui有一个渲染的过程
            yield return null;
        }

        //激活已加载完成的场景
        ao.allowSceneActivation = true;
        yield return null;
        //创建英雄
        
        //销毁所有窗口
        UIModule.Instance.DestroyAllWindow();
        //构建战斗世界
        WorldManager.CreateWorld<BattleWorld>();
        Debug.Log("UserName:"+HallWorld.GetExitsDataMgr<UserDataMgr>().userName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
