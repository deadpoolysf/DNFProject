/*--------------------------------------------------------------------------------------
* Title: 数据脚本自动生成工具
* Author: 铸梦xy
* Date:2024/10/9 0:53:53
* Description:数据层,主要负责游戏数据的存储、更新和获取
* Modify:
* 注意:以下文件为自动生成，强制再次生成将会覆盖
----------------------------------------------------------------------------------------*/
using UnityEngine;

namespace ZMGC.Hall
{
    public class UserDataMgr : IDataBehaviour
    {
        public string userName;

        public void OnCreate()
        {
            Debug.Log("UserDataMgr OnCreate");
        }

        public void OnDestroy()
        {

        }

    }
}
