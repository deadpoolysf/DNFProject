using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderObject : MonoBehaviour
{
    /// <summary>
    /// 逻辑对象
    /// </summary>
    public LogicObject LogicObject;

    /// <summary>
    /// 位置插值速度
    /// </summary>
    protected float mSmoothPosSpeed;

    /// <summary>
    /// 设置逻辑对象
    /// </summary>
    /// <param name="logicObj"></param>
    public void SetLogicObject(LogicObject logicObj)
    {
        LogicObject = logicObj;
        //初始化位置
        transform.position = logicObj.LogicPos.ToVector3();
    }

    /// <summary>
    /// 渲染层脚本创建
    /// </summary>
    public virtual void OnCreate()
    {

    }

    /// <summary>
    /// 渲染层脚本释放
    /// </summary>
    public virtual void OnRelease()
    {

    }

    /// <summary>
    /// Unity渲染帧（30、60、120帧可设置）
    /// </summary>
    void Update()
    {
        UpdatePosition();
        UpdateDir();
    }

    /// <summary>
    /// 通用位置更新逻辑
    /// </summary>
    public void UpdatePosition()
    {
        //对逻辑位置做插值动画，流畅渲染对象移动
        transform.position = Vector3.Lerp(transform.position, LogicObject.LogicPos.ToVector3(), Time.deltaTime * mSmoothPosSpeed);
    }

    /// <summary>
    /// 通用方向更新逻辑
    /// </summary>
    public void UpdateDir()
    {
        //更新逻辑朝向
        transform.rotation = Quaternion.Euler(LogicObject.LogicDir.ToVector3());
    }
}
