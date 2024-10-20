using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZM.AssetFrameWork;

namespace ZMGC.Battle
{
    public class BattleWorld : World
    {
        /// <summary>
        /// �߼�֡�ۼ�����ʱ��
        /// </summary>
        private float mAccLogicRuntime;
        /// <summary>
        /// ��һ���߼�֡��ʼ��ʱ��
        /// </summary>
        private float mNextLogicFrameTime;
        /// <summary>
        /// �߼�֡����ʱ��
        /// </summary>
        private float logicDeltaTime;
        /// <summary>
        /// Ӣ�ۿ�����
        /// </summary>
        public HeroLogicCtrl HeroLogicCtrl { get; private set; }
        /// <summary>
        /// ���������
        /// </summary>
        public MonsterLogicCtrl MonsterLogicCtrl { get; private set; }
        public override void OnCretae()
        {
            base.OnCretae();
            HeroLogicCtrl = BattleWorld.GetExitsLogicCtrl<HeroLogicCtrl>();
            MonsterLogicCtrl = BattleWorld.GetExitsLogicCtrl<MonsterLogicCtrl>();
            //��ʼ��Ӣ�ۺ͹���
            HeroLogicCtrl.InitHero();
            MonsterLogicCtrl.InitMonster();

            UIModule.PopUpWindow<BattleWindow>();
            Debug.Log("BattleWorld OnCretae");
        }

        /// <summary>
        /// Unity��Ⱦ֡���£�ģ���߼�֡����
        /// </summary>
        public override void OnUpdate()
        {
            base.OnUpdate();

            //�߼�֡����ʱ���ۼ�
            mAccLogicRuntime += Time.deltaTime;

            //�ۼ�ʱ�������һ���߼�֡ʱ�䣬��Ҫ�����߼�֡
            //׷֡����
            while(mAccLogicRuntime > mNextLogicFrameTime)
            {
                //�߼�֡����
                OnLogicFrameUpdate();
                mNextLogicFrameTime += LogicFrameConfig.LogicFrameInterval;
                //�߼�֡id����
                LogicFrameConfig.LogicFrameId++;
            }
            //��0-1��һ��ֵ����ʾ��һ����Ⱦ֡���߼�֡�Ľ���
            logicDeltaTime = (mAccLogicRuntime + LogicFrameConfig.LogicFrameInterval - mNextLogicFrameTime) / LogicFrameConfig.LogicFrameInterval;
        }

        /// <summary>
        /// �������߼�֡���£�����ͨ������˵��ã�
        /// </summary>
        public void OnLogicFrameUpdate()
        {
            HeroLogicCtrl.OnLogicFrameUpdate();
            MonsterLogicCtrl.OnLogicFrameUpdate();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        /// <summary>
        /// ������ȫ���ٺ���
        /// </summary>
        /// <param name="args"></param>
        public override void OnDestroyPostProcess(object args)
        {
            base.OnDestroyPostProcess(args);
        }
    }
}