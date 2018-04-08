using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NorthwindApplication.Customer.Actions;
using System.Reactive.Linq;
using NorthwindApplication.Customer;

namespace NorthwindPresentation.Hubs
{
    public class CustomerHub : Hub
    {
        private readonly IHubContext<CustomerHub> _hubContext;

        /// <summary>
        /// keep track of subscriptions so we can unsubscribe
        /// </summary>
        private readonly IDictionary<string, IDisposable> _subscriptions = new Dictionary<string, IDisposable>();

        public CustomerHub(IHubContext<CustomerHub> hubContext)
        {
            _hubContext = hubContext;
        }


        public async Task OpenCustomer(string customerId)
        {
            // add current connection to a group by customer id
            await Groups.AddAsync(Context.ConnectionId, customerId);
            
            // subscribe to store
            if (!_subscriptions.Keys.Contains(customerId))
            {
                _subscriptions[customerId] = StoreContainer.CustomerStore
                    .Select(state => state.OpenCustomers.FirstOrDefault(c => c.Customer.CustomerId == customerId))
                    .DistinctUntilChanged()
                    .Subscribe(async oc => {
                        if (oc != null)
                        {
                            // need to use hubcontext here as this.Clients will be disposed
                            await _hubContext.Clients.Group(oc.Customer.CustomerId).SendAsync("PushCustomer", oc);
                        }
                    });
            }
            
            // dispatch OpenCustomer action
            StoreContainer.CustomerStore.Dispatch(new OpenCustomer(customerId, Context.User.Identity.Name));       
        }

//        public async Task ChangeCustomer(Customer customer)
//        {
//            
//        }
//
//        public async Task SaveCustomer(Customer customer)
//        {
//            
//        }
//
//        public async Task CloseCustomer(Customer customer)
//        {
//            
//        }
        
        
        
        
        
        
    }
}