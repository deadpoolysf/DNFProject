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
        //�첽��������
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        //Ĭ�ϲ�����������
        ao.allowSceneActivation = false;

        float curProgress = 0;
        float maxProgress = 100;

        //unity���ؽ���ֻ���0-90��ʣ��10��Ҫ�ô�����й���
        while (curProgress < 90)
        {
            curProgress = ao.progress * 100.0f;
            //ͨ��һ���¼��ѽ����׳�
            UIEventControl.DispensEvent(UIEventEnum.SceneProgressUpdate, curProgress);
            //��һ����֡����ui��һ����Ⱦ�Ĺ���
            yield return null;
        }

        while (curProgress < maxProgress)
        {
            curProgress++;
            //ͨ��һ���¼��ѽ����׳�
            UIEventControl.DispensEvent(UIEventEnum.SceneProgressUpdate, curProgress);
            //��һ����֡����ui��һ����Ⱦ�Ĺ���
            yield return null;
        }

        //�����Ѽ�����ɵĳ���
        ao.allowSceneActivation = true;
        yield return null;
        LoadSceneFinish?.Invoke();
    }
}
