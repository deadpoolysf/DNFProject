using FixIntPhysics;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HideMonoScript]
[System.Serializable]
public class SkillDamageConfig
{
    [LabelText("����֡")]
    public int triggerFrame;//����֡

    [LabelText("����֡")]
    public int endFrame;//����֡

    [LabelText("������������� value=0 Ĭ��һ�Σ�>0��Ϊ�����")]
    public int triggerIntervalMs;//������������� value=0 Ĭ��һ�Σ�>0��Ϊ�����

    [LabelText("�Ƿ������Ч�ƶ�")]
    public bool isFollowEffect;//��ײ���Ƿ������Ч�ƶ�

    [LabelText("�˺�����")]
    public DamageType damageType;//�˺�����

    [LabelText("�˺�����")]
    public int damageRate;//�˺�����

    [LabelText("�˺���ⷽʽ"), OnValueChanged("OnDectectionValueChange")]
    public DamageDetectionMode detectionMode;//�˺���ⷽʽ

    [LabelText("Box��ײ����"), ShowIf("mShowBox3D"), OnValueChanged("OnBoxValueChange")]
    public Vector3 boxSize = new Vector3(1, 1, 1);//Box��ײ�Ĵ�С

    [LabelText("Box��ײ��ƫ��"), ShowIf("mShowBox3D"), OnValueChanged("OnColliderOffsetChange")]
    public Vector3 boxOffset = new Vector3(0, 0, 0);//Box��ײ��ƫ��ֵ

    [LabelText("Բ����ײ��ƫ��ֵ"), ShowIf("mShowShpere3D"), OnValueChanged("OnColliderOffsetChange")]
    public Vector3 sphereOffset = new Vector3(0, 0.9f, 0);//Բ����ײ��ƫ��ֵ

    [LabelText("Բ���˺����뾶"), ShowIf("mShowShpere3D"), OnValueChanged("OnRaduisValueChange")]
    public float raduis = 1;//Բ���˺����뾶

    [LabelText("Բ����뾶�߶�"), ShowIf("mShowShpere3D")]
    public float raduisHeight = 0;//Բ����뾶�߶�

    [LabelText("��ײ��λ������")]
    public ColliderPosType colliderPosType = ColliderPosType.FollowDir;//��ײ��λ������

    [LabelText("�˺�����Ŀ��")]
    public TargetType targetType;//�˺�����Ŀ��

    [TitleGroup("����Buff", "�˺���Ч��һ˲�䣬����ָ���Ķ��buff")]
    public int[] addBuffs;//���ܸ���BUff

    [TitleGroup("������������", "����˺����Ҽ����ͷ���Ϻ󴥷��ļ���")]
    public int triggerSkillid;//��������id

#if UNITY_EDITOR
    private bool mShowBox3D;    //�Ƿ���ʾBox3D��ײ��
    private bool mShowShpere3D; //�Ƿ���ʾShpere3D��ײ��
    private FixIntBoxCollider boxCollider;    //Box3D��ײ��
    private FixIntSphereCollider sphereCollider;    //Box3D��ײ��
    private int mCurLogicFrame = 0;//��ǰִ�е����߼�֡

    /// <summary>
    /// ��ײ������ͷ����仯
    /// </summary>
    /// <param name="detectionMode"></param>
    public void OnDectectionValueChange(DamageDetectionMode detectionMode)
    {
        mShowBox3D = detectionMode == DamageDetectionMode.BOX3D;
        mShowShpere3D = detectionMode == DamageDetectionMode.Sphere3D;
        CreateCollider();
    }

    /// <summary>
    /// ��ײ��ƫ�Ʊ仯
    /// </summary>
    /// <param name="offset"></param>
    public void OnColliderOffsetChange(Vector3 offset)
    {
        if (detectionMode == DamageDetectionMode.BOX3D && boxCollider != null)
        {
            boxCollider.SetBoxData(GetColliderOffsetPos(), boxSize, colliderPosType == ColliderPosType.FollowPos);
        }
        else if (detectionMode == DamageDetectionMode.Sphere3D && sphereCollider != null)
        {
            sphereCollider.SetBoxData(raduis, GetColliderOffsetPos(), colliderPosType == ColliderPosType.FollowPos);
        }
    }

    /// <summary>
    /// ��ײ���߷����仯
    /// </summary>
    public void OnBoxValueChange(Vector3 size)
    {
        if(boxCollider != null)
        {
            boxCollider.SetBoxData(GetColliderOffsetPos(), size, colliderPosType == ColliderPosType.FollowPos);
        }
        else
        {
            Debug.LogError("boxCollider is null");
        }    
    }

    /// <summary>
    /// ��ײ��뾶�����仯
    /// </summary>
    public void OnRaduisValueChange(float raduis)
    {
        if (sphereCollider != null)
        {
            sphereCollider.SetBoxData(raduis,GetColliderOffsetPos(), colliderPosType == ColliderPosType.FollowPos);
        }
        else
        {
            Debug.LogError("sphereCollider is null");
        }
    }

    /// <summary>
    /// ��ȡƫ��λ�ã���ɫλ��+ƫ�ƣ�
    /// </summary>
    /// <returns></returns>
    public Vector3 GetColliderOffsetPos()
    {
        Vector3 characterPos = SkillCompilerWindow.GetCharacterPos();
        if(detectionMode==DamageDetectionMode.BOX3D)
        {
            return characterPos + boxOffset;
        }
        else if (detectionMode == DamageDetectionMode.Sphere3D)
        {
            return characterPos + sphereOffset;
        }
        return Vector3.zero;
    }

    /// <summary>
    /// ������ײ��
    /// </summary>
    public void CreateCollider()
    {
        DestroyCollider();
        if (detectionMode==DamageDetectionMode.BOX3D)
        {
            boxCollider = new FixIntBoxCollider(boxSize, GetColliderOffsetPos());
            boxCollider.SetBoxData(GetColliderOffsetPos(), boxSize, colliderPosType == ColliderPosType.FollowPos);
        }
        else if(detectionMode == DamageDetectionMode.Sphere3D)
        {
            sphereCollider = new FixIntSphereCollider(raduis, GetColliderOffsetPos());
            sphereCollider.SetBoxData(raduis, GetColliderOffsetPos(), colliderPosType == ColliderPosType.FollowPos);
        }
    }

    /// <summary>
    /// ������ײ��
    /// </summary>
    public void DestroyCollider()
    {
        if(boxCollider!=null)
        {
            boxCollider.OnRelease();
        }
        if(sphereCollider!=null)
        {
            sphereCollider.OnRelease();
        }
    }

    /// <summary>
    /// ��ǰ���ڳ�ʼ��
    /// </summary>
    public void OnInit()
    {
        CreateCollider();
    }

    /// <summary>
    /// ��ǰ���ڹر�
    /// </summary>
    public void OnRelease()
    {
        DestroyCollider();
    }

    public void PlaySkillStart()
    {
        mCurLogicFrame = 0;
        DestroyCollider();
    }

    public void PlaySkillEnd()
    {
        DestroyCollider();
    }

    public void OnLogicFrameUpdate()
    {
        //�Ƿ񵽴ﴥ��֡
        if(mCurLogicFrame == triggerFrame)
        {
            CreateCollider();
        }
        else if(mCurLogicFrame == endFrame)
        {
            DestroyCollider();
        }
        mCurLogicFrame++;
    }
#endif
}

public enum TargetType
{
    [LabelText("δ����")] None,//δ����
    [LabelText("����")] Teammate,//����
    [LabelText("����")] Enemy,//����
    [LabelText("����")] Self,//����
    [LabelText("���ж���")] AllObject,//���ж���
}

public enum ColliderPosType
{
    [LabelText("�����ɫ����")] FollowDir,//�����ɫ����
    [LabelText("�����ɫλ��")] FollowPos,//�����ɫλ��
    [LabelText("��������")] ConterPos,//��������
    [LabelText("Ŀ��λ��")] TargetPos,//Ŀ��λ��
}

public enum DamageType
{
    [LabelText("���˺�")] None,//���˺�
    [LabelText("�����˺�")] ADDamage,//�����˺�
    [LabelText("ħ���˺�")] APDamage,//ħ���˺�
}

public enum DamageDetectionMode
{
    [LabelText("������")] None,//������
    [LabelText("3DBox��ײ���")] BOX3D,//3DBox��ײ���
    [LabelText("3DԲ����ײ���")] Sphere3D,//3DԲ����ײ���
    [LabelText("3DԲ������ײ���")] Cylinder3D,//3DԲ������ײ���
    [LabelText("�뾶�ľ���")] RadiusDistance,//�뾶�ľ��� ������������
    [LabelText("����Ŀ��")] AllTarget,//ͨ����������������Ŀ��
}
