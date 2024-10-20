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
        //��ʼ����Դ������
        ZMAssetsFrame.Instance.InitFrameWork();
        //��ʼ��UI���
        UIModule.Instance.Initialize();
        //������������
        WorldManager.CreateWorld<HallWorld>();

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// ��Դ��ѹ��ɺ����
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
