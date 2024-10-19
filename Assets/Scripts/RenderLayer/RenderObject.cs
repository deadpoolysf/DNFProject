using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderObject : MonoBehaviour
{
    /// <summary>
    /// �߼�����
    /// </summary>
    public LogicObject LogicObject;

    /// <summary>
    /// λ�ò�ֵ�ٶ�
    /// </summary>
    protected float mSmoothPosSpeed;

    /// <summary>
    /// �����߼�����
    /// </summary>
    /// <param name="logicObj"></param>
    public void SetLogicObject(LogicObject logicObj)
    {
        LogicObject = logicObj;
        //��ʼ��λ��
        transform.position = logicObj.LogicPos.ToVector3();
    }

    /// <summary>
    /// ��Ⱦ��ű�����
    /// </summary>
    public virtual void OnCreate()
    {

    }

    /// <summary>
    /// ��Ⱦ��ű��ͷ�
    /// </summary>
    public virtual void OnRelease()
    {

    }

    /// <summary>
    /// Unity��Ⱦ֡��30��60��120֡�����ã�
    /// </summary>
    void Update()
    {
        UpdatePosition();
        UpdateDir();
    }

    /// <summary>
    /// ͨ��λ�ø����߼�
    /// </summary>
    public void UpdatePosition()
    {
        //���߼�λ������ֵ������������Ⱦ�����ƶ�
        transform.position = Vector3.Lerp(transform.position, LogicObject.LogicPos.ToVector3(), Time.deltaTime * mSmoothPosSpeed);
    }

    /// <summary>
    /// ͨ�÷�������߼�
    /// </summary>
    public void UpdateDir()
    {
        //�����߼�����
        transform.rotation = Quaternion.Euler(LogicObject.LogicDir.ToVector3());
    }
}
