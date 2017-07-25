using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.Common.Cloneable;

namespace Tools.Store
{
    public abstract class StoreBase<TKey, TEntity>
        where TEntity : IDeepCloneable<TEntity>
    {
        protected Dictionary<TKey, TEntity> Cache { get; private set; }
        protected bool IsComplete { get; private set; }

        protected StoreBase()
        {          
            Cache = new Dictionary<TKey, TEntity>();
            IsComplete = false;
        }

        protected void AddItemsToCacheWithCheck(IEnumerable<TEntity> items)
        {
            TKey key;

            foreach (var item in items)
            {
                key = GetKey(item);

                if (!Cache.ContainsKey(key))
                    Cache.Add(key, item.DeepClone());
            }
        }

        protected void AddItemsToCacheWithoutCheck(IEnumerable<TEntity> items)
        {
            TKey key;

            foreach (var item in items)
            {
                key = GetKey(item);

                Cache.Add(key, item.DeepClone());
            }
        }

        protected void UpdateCacheItemsWithoutCheck(IEnumerable<TEntity> items)
        {
            TKey key;

            foreach (var item in items)
            {
                key = GetKey(item);

                Cache[key] = item.DeepClone();
            }
        }

        protected void DeleteCacheItemsWithoutCheck(IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
                Cache.Remove(key);
        }

        protected List<TEntity> GetAllItemsFromCache()
        {
            var clonedItems = new List<TEntity>(Cache.Values.Count);

            foreach (var item in Cache.Values)
                clonedItems.Add(item.DeepClone());

            return clonedItems;
        }

        protected List<TEntity> Load(Func<List<TEntity>> loadServiceSideAction)
        {
            if (!IsComplete)
            {
                var items = loadServiceSideAction();

                AddItemsToCacheWithCheck(items);

                IsComplete = true;
            }

            return GetAllItemsFromCache();
        }

        protected async Task<List<TEntity>> LoadAsync(Func<Task<List<TEntity>>> loadServiceSideAction)
        {
            if (!IsComplete)
            {
                var items = await loadServiceSideAction();

                AddItemsToCacheWithCheck(items);

                IsComplete = true;
            }

            return GetAllItemsFromCache();
        }

        protected void Add(List<TEntity> items, Action<List<TEntity>> addServiceSideAction)
        {
            addServiceSideAction(items);

            AddItemsToCacheWithoutCheck(items);
        }

        protected async Task AddAsync(List<TEntity> items, Func<List<TEntity>, Task> addServiceSideAction)
        {
            await addServiceSideAction(items);

            AddItemsToCacheWithoutCheck(items);
        }

        protected void Update(List<TEntity> items, Action<List<TEntity>> updateServiceSideAction)
        {
            updateServiceSideAction(items);

            UpdateCacheItemsWithoutCheck(items);
        }

        protected async Task UpdateAsync(List<TEntity> items, Func<List<TEntity>, Task> updateServiceSideAction)
        {
            await updateServiceSideAction(items);

            UpdateCacheItemsWithoutCheck(items);
        }

        protected void Delete(List<TKey> keys, Action<List<TKey>> deleteServiceSideAction)
        {
            deleteServiceSideAction(keys);

            DeleteCacheItemsWithoutCheck(keys);
        }

        protected async Task DeleteAsync(List<TKey> keys, Func<List<TKey>, Task> deleteServiceSideAction)
        {
            await deleteServiceSideAction(keys);

            DeleteCacheItemsWithoutCheck(keys);
        }

        protected abstract TKey GetKey(TEntity item);
    }
}
