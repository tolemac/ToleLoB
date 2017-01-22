using System;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ToleLoB.CQRS.Queries
{
    internal class QueryResultCache
    {
        private ConcurrentDictionary<Type, ConcurrentDictionary<string, CachedQueryResult>> _cache =
            new ConcurrentDictionary<Type, ConcurrentDictionary<string, CachedQueryResult>>();
        private ConcurrentDictionary<object, ConcurrentBag<Type>> _typesByItem =
            new ConcurrentDictionary<object, ConcurrentBag<Type>>();

        private ConcurrentBag<Type> GetTypeList(object item)
        {
            return _typesByItem.GetOrAdd(item, new ConcurrentBag<Type>());
        }
        private ConcurrentDictionary<string, CachedQueryResult> GetResultList(IQuery query)
        {
            return _cache.GetOrAdd(query.GetType(), (newType) =>
            {
                foreach (var item in query.QueryedStateItems)
                {
                    var typeList = GetTypeList(item);
                    typeList.Add(newType);
                }
                return new ConcurrentDictionary<string, CachedQueryResult>();
            });
        }

        public void Add(IQuery query, object parameters, object result)
        {
            var resultList = GetResultList(query);
            var parametersHash = Common.GetStringHash(JsonConvert.SerializeObject(parameters));
            var obj = new CachedQueryResult(this, query, parametersHash, result);
            resultList.AddOrUpdate(parametersHash, obj, (key, existingValue) => obj);
        }

        public ICachedQueryResult Get(IQuery query, object parameters)
        {
            var resultList = GetResultList(query);
            var parametersHash = Common.GetStringHash(JsonConvert.SerializeObject(parameters));
            CachedQueryResult result;
            if (resultList.TryGetValue(parametersHash, out result))
            {
                return result;
            }
            return null;
        }

        public void PurgeByStateItems(object[] stateItems)
        {
            var processed = new List<Type>();
            foreach (var item in stateItems)
            {
                var typeList = GetTypeList(item);
                foreach (var type in typeList)
                {
                    if (processed.Contains(type))
                        continue;
                    _cache[type].Clear();
                    processed.Add(type);
                }
            }
        }

        public void PurgeResult(ICachedQueryResult result)
        {
            var resultList = _cache[result.QueryType];
            CachedQueryResult removed;
            resultList.TryRemove(result.ParameterHash, out removed);
        }
    }
}