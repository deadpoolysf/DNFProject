using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        //��ʼ��UI���
        UIModule.Instance.Initialize();

        UIModule.Instance.PopUpWindow<CreateRoleWindow>();

        DontDestroyOnLoad(gameObject);
    }

    public void AsyncLoadScene()
    {
        UIModule.Instance.PopUpWindow<LoadingWindow>();
        StartCoroutine(IAsyncLoadScene());
    }

    IEnumerator IAsyncLoadScene()
    {
        //�첽��������
        AsyncOperation ao = SceneManager.LoadSceneAsync("Battle");
        //Ĭ�ϲ�����������
        ao.allowSceneActivation = false;

        float curProgress = 0;
        float maxProgress = 100;

        //unity���ؽ���ֻ���0-90��ʣ��10��Ҫ�ô�����й���
        while(curProgress < 90)
        {
            curProgress = ao.progress * 100.0f;
            //ͨ��һ���¼��ѽ����׳�
            UIEventControl.DispensEvent(UIEventEnum.SceneProgressUpdate,curProgress);
            //��һ����֡����ui��һ����Ⱦ�Ĺ���
            yield return null;
        }

        while(curProgress < maxProgress)
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
        //����Ӣ��
        
        //�������д���
        UIModule.Instance.DestroyAllWindow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
