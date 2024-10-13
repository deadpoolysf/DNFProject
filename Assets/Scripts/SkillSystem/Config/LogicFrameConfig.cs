using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicFrameConfig
{
    /// <summary>
    /// 逻辑帧id 自增
    /// </summary>
    public static long LogicFrameId;
    /// <summary>
    /// 实际逻辑帧间隔
    /// </summary>
    public static float LogicFrameInterval = 0.066f; //1秒15帧
    /// <summary>
    /// 毫秒级逻辑帧间隔，用来计算当前逻辑帧累加时间
    /// </summary>
    public static float LogicFrameIntervalms = 66f; //1秒15帧
}
