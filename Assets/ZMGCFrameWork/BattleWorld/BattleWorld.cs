using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZMGC.Battle
{
    public class BattleWorld : World
    {
        public override void OnCretae()
        {
            base.OnCretae();
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