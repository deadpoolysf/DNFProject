using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZM.AssetFrameWork;

namespace ZMGC.Battle
{
    public class BattleWorld : World
    {
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

            Debug.Log("BattleWorld OnCretae");
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