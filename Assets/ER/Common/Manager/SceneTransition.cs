using System;
using UnityEngine;

namespace ER
{
    /// <summary>
    /// 场景过渡基类
    /// </summary>
    public abstract class SceneTransition:MonoBehaviour
    {
        protected Action callback;
        /// <summary>
        /// 进度
        /// </summary>
        private float progress;
        /// <summary>
        /// 场景加载进度
        /// </summary>
        public virtual float Progress { get => progress; set => progress =value; }
        /// <summary>
        /// 加载场景(开始过渡)
        /// </summary>
        public abstract void EnterTransition();
        /// <summary>
        /// 离开场景(结束过渡)
        /// </summary>
        public abstract void ExitTransition();
        /// <summary>
        /// 加载场景(开始过渡): 调用此方法则不直接加载场景,而是将加载场景的委托传入, 等待过渡动画准备后有本对象执行回调委托 来加载场景
        /// </summary>
        /// <param name="_callback"></param>
        public void EnterTransition(Action _callback)
        {
            callback = _callback;
            EnterTransition();
        }

    }
}