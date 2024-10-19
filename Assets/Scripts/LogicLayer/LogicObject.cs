using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixMath;
using FixIntPhysics;
//LogicObject ͬʱ���� �����Ӣ��ͬʱ���еĻ�������
/// <summary>
/// ֻ��������������Ժͽӿڵ��ṩ����������巽����ʵ��
/// </summary>
public abstract class LogicObject
{
    private FixIntVector3 logicPos;//�߼������߼�λ��
    private FixIntVector3 logicDir;//�߼�������
    private FixIntVector3 logicAngle;//�߼�������ת�Ƕ�
    private FixInt logicMoveSpeed = 3;//�߼������ƶ��ٶ�
    private FixInt logicXAxis = 1;//�߼�����
    private FixIntVector3 isActive;//��ǰ�߼������Ƿ񼤻�
    //private bool isForceAllowMove = false;//�Ƿ�ǿ�������ƶ�
    //private bool isForceNotAlllowModifyDir = false;//�Ƿ������޸ĳ���

    //��������
    public FixIntVector3 LogicPos { get { return logicPos; } set { logicPos = value; } }//�߼������߼�λ��
    public FixIntVector3 LogicDir { get { return logicDir; } set { logicDir = value; } }//�߼�������
    public FixIntVector3 LogicAngle { get { return logicAngle; } set { logicAngle = value; } }//�߼�������ת�Ƕ�
    public FixInt LogicMoveSpeed { get { return logicMoveSpeed; } set { logicMoveSpeed = value; } }//�߼������ƶ��ٶ�
    public FixInt LogicXAxis { get { return logicXAxis; } set { logicXAxis = value; } }//�߼�����
    public FixIntVector3 IsActive { get { return isActive; } set { isActive = value; } }//��ǰ�߼������Ƿ񼤻�
    //public bool IsForceAllowMove { get { return isForceAllowMove; } set { Debug.Log("isForceAllowMove:" + isForceAllowMove); isForceAllowMove = value; } }//�Ƿ�ǿ�������ƶ�
    //public bool IsForceNotAlllowModifyDir { get { return isForceNotAlllowModifyDir; } set { Debug.Log("isForceAlllowModifyDir:" + isForceAllowMove); isForceNotAlllowModifyDir = value; } }//�Ƿ������޸ĳ���

    /// <summary>
    /// ��Ⱦ����
    /// </summary>
    public RenderObject RenderObj { get; protected set; }
    /// <summary>
    /// ��������ײ��
    /// </summary>
    public FixIntBoxCollider Collider { get; protected set; }
    /// <summary>
    /// �߼�����״̬
    /// </summary>
    public LogicObjectState ObjectState { get; set; }
    /// <summary>
    /// �߼���������
    /// </summary>
    public LogicObjectType ObjectType { get; set; }
    /// <summary>
    /// �߼������ж�״̬
    /// </summary>
    public LogicObjectActionState ActionSate { get; set; }

    /// <summary>
    /// ��ʼ���ӿ�
    /// </summary>
    public virtual void OnCreate()
    {

    }
    /// <summary>
    /// �߼�֡���½ӿ�
    /// </summary>
    public virtual void OnLogicFrameUpdate()
    {

    }
    /// <summary>
    /// �߼������ͷŽӿ�
    /// </summary>
    public virtual void OnDestroy()
    {

    }
}
public enum LogicObjectActionState
{
    Idle,//����
    Move,//�ƶ���
    ReleasingSkill,//�ͷż�����
    Floating,//������
    Hiting,//�ܻ���
    StockPileing,//������
}

public enum LogicObjectType
{
    Hero,
    Monster,
    Effect,
}

public enum LogicObjectState
{
    Survival,//�����
    Death,//����
}