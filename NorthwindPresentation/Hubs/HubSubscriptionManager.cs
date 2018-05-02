using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Design;

namespace NorthwindPresentation.Hubs
{
    
    
    /// <summary>
    /// manage store subscriptions created by the hub
    /// </summary>
    public class HubSubscriptionManager
    {

        private readonly NullPropagatingDictionary<string, NullPropagatingDictionary<string, IDisposable>> _store;

        public HubSubscriptionManager()
        {
            _store = new NullPropagatingDictionary<string, NullPropagatingDictionary<string, IDisposable>>();
        }

        /// <summary>
        /// Register a subscription 
        /// </summary>
        /// <param name="connectionId">Hub Connection Id</param>
        /// <param name="key">key to identify the subscrition</param>
        /// <param name="subscription">IDisposable subscription instance</param>
        public void Add(string connectionId, string key, IDisposable subscription)
        {
            if (_store[connectionId]?.ContainsKey(key) ?? false) DisposeAndRemove(connectionId, key);
            if (!_store.ContainsKey(connectionId)) _store[connectionId] = new NullPropagatingDictionary<string, IDisposable>();
            _store[connectionId][key] = subscription;
        }
    

        /// <summary>
        /// dispose, then remove a subscription by key
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="key"></param>
        public void DisposeAndRemove(string connectionId, string key)
        {
            _store[connectionId]?[key]?.Dispose();
            _store[connectionId]?.Remove(key);
        }
    
        /// <summary>
        /// dispose and remove all subscriptions for a client connection
        /// </summary>
        /// <param name="connectionId"></param>
        public void DisposeAndRemoveAll(string connectionId)
        {
            _store[connectionId]?.Values?.ToList()
                .ForEach(d => d.Dispose());
            _store.Remove(connectionId);
        }
        
        
    }

    /// <summary>
    /// override Dictionary to make [] friendly to the ?. operator
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public class NullPropagatingDictionary<TKey, TVal> : Dictionary<TKey, TVal>
    {
        public new TVal this[TKey key]
        {
            get
            {
                TryGetValue(key, out var val);
                return val;
            }
            set => Add(key, value);
        }
    }
    
}