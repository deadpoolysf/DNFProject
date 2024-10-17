/*--------------------------------------------------------------------------------------
* Title: 业务逻辑脚本自动生成工具
* Author: 铸梦xy
* Date:2024/10/17 20:40:08
* Description:业务逻辑层,主要负责游戏的业务逻辑处理
* Modify:
* 注意:以下文件为自动生成，强制再次生成将会覆盖
----------------------------------------------------------------------------------------*/
using ZM.AssetFrameWork;

namespace ZMGC.Battle
{
    public class HeroLogicCtrl : ILogicBehaviour
    {

        public void OnCreate()
        {

        }

        /// <summary>
        /// 初始化场景英雄
        /// </summary>
        public void InitHero()
        {
            //创建英雄
            ZMAssetsFrame.Instantiate(AssetPathConfig.GAME_PREFABS_HERO + "1000", null);
        }

        public void OnDestroy()
        {

        }

    }
}
