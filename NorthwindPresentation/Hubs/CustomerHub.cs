using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NorthwindApplication.Customer.Actions;
using System.Reactive.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using NorthwindApplication.Customer;

namespace NorthwindPresentation.Hubs
{
    [Authorize]
    public class CustomerHub : Hub
    {
        private readonly IHubContext<CustomerHub> _hubContext;
        private readonly ILogger<CustomerHub> _logger;

        /// <summary>
        /// keep track of subscriptions so we can unsubscribe
        /// </summary>
        private readonly IDictionary<string, IDisposable> _subscriptions = new Dictionary<string, IDisposable>();

        public CustomerHub(IHubContext<CustomerHub> hubContext, ILogger<CustomerHub> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
            
            _logger.LogInformation("New hub instance");
        }


        public async Task OpenCustomer(string customerId)
        {
            _logger.LogInformation("hub: OpenCustomer");
            // add current connection to a group by customer id
            await Groups.AddAsync(Context.ConnectionId, customerId);
            
            // subscribe to store
            if (!_subscriptions.Keys.Contains(customerId))
            {
                _subscriptions[customerId] = StoreContainer.CustomerStore
                    .Select(state => state.OpenCustomers.FirstOrDefault(c => c.Customer.CustomerId == customerId))
                    .DistinctUntilChanged()
                    .Subscribe(async oc => {
                        if (oc == null) return;
                        _logger.LogInformation("hub: customer {customerId} changed. calling push");
                        await _hubContext.Clients.Group(oc.Customer.CustomerId).SendAsync("PushCustomer", oc);
                    });
            }         
            // dispatch OpenCustomer action
            StoreContainer.CustomerStore.Dispatch(new OpenCustomer(customerId, Context.User.Identity.Name));       
        }

        public void UpdateCustomer(Customer customer)
        {
            StoreContainer.CustomerStore.Dispatch(new UpdateCustomer(customer));
        }

        public void SaveCustomer(Customer customer)
        {
            StoreContainer.CustomerStore.Dispatch(new SaveCustomer(customer));
        }


        public async Task CustomerListSubscribe()
        {
            await Groups.AddAsync(Context.ConnectionId, "CustomerList");
            
            // subscribe to store
            StoreContainer.CustomerStore
                    .Select(state => state.CustomerList)
                    .DistinctUntilChanged()
                    .Subscribe(async customerList =>
                    {
                        await _hubContext.Clients.Group("CustomerList").SendAsync("PushCustomerList", customerList);
                    });
        }
        
        
        
        
        
    }
}