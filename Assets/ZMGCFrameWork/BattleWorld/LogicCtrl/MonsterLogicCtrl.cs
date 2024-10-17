/*--------------------------------------------------------------------------------------
* Title: 业务逻辑脚本自动生成工具
* Author: 铸梦xy
* Date:2024/10/17 20:41:12
* Description:业务逻辑层,主要负责游戏的业务逻辑处理
* Modify:
* 注意:以下文件为自动生成，强制再次生成将会覆盖
----------------------------------------------------------------------------------------*/
using FixIntPhysics;
using System.Collections.Generic;
using UnityEngine;
using ZM.AssetFrameWork;

namespace ZMGC.Battle
{
    public class MonsterLogicCtrl : ILogicBehaviour
    {
        //怪物生成位置列表
        public List<Vector3> monsterPosList = new List<Vector3> { new Vector3(0, 0, 0) };

        //怪物生成id数组
        public int[] monsterIdArr = new int[] { 20001, };
        public void OnCreate()
        {

        }
        /// <summary>
        /// 初始化场景中怪物
        /// </summary>
        public void InitMonster()
        {
            int index = 0;
            foreach (var item in monsterIdArr)
            {
                GameObject monsterObj = ZMAssetsFrame.Instantiate(AssetPathConfig.GAME_PREFABS_MONSTER + "20001", null);
                monsterObj.transform.position = monsterPosList[index];

                //处理怪物碰撞数据
                BoxColliderGizmo boxInfo = monsterObj.GetComponent<BoxColliderGizmo>();
                boxInfo.enabled = false;
                //创建定点数碰撞体
                FixIntBoxCollider monsterBox = new FixIntBoxCollider(boxInfo.mSize, boxInfo.mConter);
                monsterBox.SetBoxData(boxInfo.mConter, boxInfo.mSize);
                monsterBox.UpdateColliderInfo(monsterObj.transform.position, boxInfo.mSize);

                index++;
            }
        }

        public void OnDestroy()
        {

        }

    }
}
