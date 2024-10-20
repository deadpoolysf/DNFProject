using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZM.AssetFrameWork;

namespace ZMGC.Battle
{
    public class BattleWorld : World
    {
        /// <summary>
        /// 逻辑帧累计运行时间
        /// </summary>
        private float mAccLogicRuntime;
        /// <summary>
        /// 下一个逻辑帧开始的时间
        /// </summary>
        private float mNextLogicFrameTime;
        /// <summary>
        /// 逻辑帧增量时间
        /// </summary>
        private float logicDeltaTime;
        /// <summary>
        /// 英雄控制器
        /// </summary>
        public HeroLogicCtrl HeroLogicCtrl { get; private set; }
        /// <summary>
        /// 怪物控制器
        /// </summary>
        public MonsterLogicCtrl MonsterLogicCtrl { get; private set; }
        public override void OnCretae()
        {
            base.OnCretae();
            HeroLogicCtrl = BattleWorld.GetExitsLogicCtrl<HeroLogicCtrl>();
            MonsterLogicCtrl = BattleWorld.GetExitsLogicCtrl<MonsterLogicCtrl>();
            //初始化英雄和怪物
            HeroLogicCtrl.InitHero();
            MonsterLogicCtrl.InitMonster();

            UIModule.PopUpWindow<BattleWindow>();
            Debug.Log("BattleWorld OnCretae");
        }

        /// <summary>
        /// Unity渲染帧更新，模拟逻辑帧更新
        /// </summary>
        public override void OnUpdate()
        {
            base.OnUpdate();

            //逻辑帧运行时间累加
            mAccLogicRuntime += Time.deltaTime;

            //累计时间大于下一个逻辑帧时间，需要更新逻辑帧
            //追帧操作
            while(mAccLogicRuntime > mNextLogicFrameTime)
            {
                //逻辑帧更新
                OnLogicFrameUpdate();
                mNextLogicFrameTime += LogicFrameConfig.LogicFrameInterval;
                //逻辑帧id自增
                LogicFrameConfig.LogicFrameId++;
            }
            //从0-1的一个值，表示跑一个渲染帧后，逻辑帧的进度
            logicDeltaTime = (mAccLogicRuntime + LogicFrameConfig.LogicFrameInterval - mNextLogicFrameTime) / LogicFrameConfig.LogicFrameInterval;
        }

        /// <summary>
        /// 真正的逻辑帧更新（后期通过服务端调用）
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
        /// 世界完全销毁后处理
        /// </summary>
        /// <param name="args"></param>
        public override void OnDestroyPostProcess(object args)
        {
            base.OnDestroyPostProcess(args);
        }
    }
}