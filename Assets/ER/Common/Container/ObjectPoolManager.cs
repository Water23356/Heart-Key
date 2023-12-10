// Ignore Spelling: obj Unregister

using System.Collections.Generic;
using UnityEngine;

namespace ER
{
    /// <summary>
    /// 对象池管理类（非组件单例模式）
    /// </summary>
    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        #region 字段

        /// <summary>
        /// 池字典
        /// </summary>
        private Dictionary<string, ObjectPool> poolDictionary = new Dictionary<string, ObjectPool>();

        #endregion 字段

        #region 功能函数

        /// <summary>
        /// 从对应对象池中取对象
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        /// <returns>获取到的对象</returns>
        public Water GetObject(string poolName)
        {
            if (poolDictionary.TryGetValue(poolName, out ObjectPool pool))
            {
                return pool.GetObject();
            }
            else
            {
                Debug.LogWarning("对象池不存在：" + poolName);
                return null;
            }
        }

        /// <summary>
        /// 从对应对象池中取对象
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        /// <returns>获取到的对象</returns>
        public Water this[string poolName]
        {
            get => GetObject(poolName);
        }

        /// <summary>
        /// 将对象还回对象池
        /// </summary>
        /// <param name="obj">游戏物体对象</param>
        /// <param name="poolName">对象池名称</param>
        public void ReturnObject(Water obj)
        {
            if (obj == null || obj.Pool == null) return;
            string poolName = obj.Pool.PoolName;
            if (poolDictionary.TryGetValue(poolName, out ObjectPool pool))
            {
                pool.ReturnObject(obj);
            }
            else
            {
                Debug.LogWarning("对象池不存在：" + poolName);
            }
        }

        /// <summary>
        /// 注册对象池
        /// </summary>
        /// <param name="pool">对象池实例</param>
        public void RegisterPool(ObjectPool pool)
        {
            if (!poolDictionary.ContainsKey(pool.PoolName))
            {
                poolDictionary.Add(pool.PoolName, pool);
            }
            else
            {
                Debug.LogWarning("对象池已存在：" + pool.PoolName);
            }
        }

        /// <summary>
        /// 注销对象池
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        public void UnregisterPool(string poolName)
        {
            if (poolDictionary.ContainsKey(poolName))
            {
                poolDictionary.Remove(poolName);
            }
            else
            {
                Debug.LogWarning("对象池不存在：" + poolName);
            }
        }
        /// <summary>
        /// 检查指定对象池是否已经注册
        /// </summary>
        /// <param name="poolName"></param>
        /// <returns></returns>
        public bool CheckPool(string poolName)
        {
            return poolDictionary.ContainsKey(poolName);
        }
        /// <summary>
        /// 创建一个新的对象池,并注册管理
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        /// <param name="obj">使用的预制体</param>
        /// <param name="count">池默认大小</param>
        /// <param name="parent">对象池父物体</param>
        public void CreatePool(string poolName,Water obj,int count = 20,Transform parent = null)
        {
            GameObject gobj = new GameObject(poolName);
            gobj.transform.SetParent(parent);
            ObjectPool pool = gobj.AddComponent<ObjectPool>();
            pool.Prefab = obj.gameObject;
            pool.SetSize(count);
        }
        /// <summary>
        /// 创建一个新的对象池,并注册管理
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        /// <param name="obj">使用的预制体(注意,必须挂载Water脚本)</param>
        /// <param name="count">池默认大小</param>
        /// <param name="parent">对象池父物体</param>
        public void CreatePool(string poolName, GameObject obj, int count = 20, Transform parent = null)
        {
            GameObject gobj = new GameObject(poolName);
            gobj.transform.SetParent(parent);
            ObjectPool pool = gobj.AddComponent<ObjectPool>();
            pool.Init(poolName, obj,count);
            pool.SetSize(count);
        }

        #endregion 功能函数
    }
}