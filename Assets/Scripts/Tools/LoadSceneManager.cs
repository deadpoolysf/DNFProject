using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZM.AssetFrameWork;

public class LoadSceneManager : MonoSingleton<LoadSceneManager>
{
    public void AsyncLoadScene(string sceneName,Action LoadSceneFinish)
    {
        UIModule.Instance.PopUpWindow<LoadingWindow>();
        StartCoroutine(IAsyncLoadScene(sceneName, LoadSceneFinish));
    }

    IEnumerator IAsyncLoadScene(string sceneName, Action LoadSceneFinish)
    {
        //异步场景加载
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        //默认不允许场景激活
        ao.allowSceneActivation = false;

        float curProgress = 0;
        float maxProgress = 100;

        //unity加载进度只会从0-90，剩余10需要用代码进行过渡
        while (curProgress < 90)
        {
            curProgress = ao.progress * 100.0f;
            //通过一个事件把进度抛出
            UIEventControl.DispensEvent(UIEventEnum.SceneProgressUpdate, curProgress);
            //等一个空帧，让ui有一个渲染的过程
            yield return null;
        }

        while (curProgress < maxProgress)
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
        LoadSceneFinish?.Invoke();
    }
}
