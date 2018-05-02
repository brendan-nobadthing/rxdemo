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
        private readonly HubSubscriptionManager _hubSubscriptionManager;


        public CustomerHub(IHubContext<CustomerHub> hubContext, 
            ILogger<CustomerHub> logger, 
            HubSubscriptionManager hubSubscriptionManager)
        {
            _hubContext = hubContext;
            _logger = logger;
            _hubSubscriptionManager = hubSubscriptionManager;

            _logger.LogInformation("New hub instance");
        }


        public async Task OpenCustomer(string customerId)
        {
            var connectionId = Context.ConnectionId;
            _logger.LogInformation("hub: OpenCustomer {customerId} for {connectionId}", customerId, Context.ConnectionId);
            
            // subscribe to store
            var sub = StoreContainer.CustomerStore
                .Select(state => state.OpenCustomers.FirstOrDefault(c => c.Customer.CustomerId == customerId))
                .DistinctUntilChanged()
                .Subscribe(async oc => {
                    if (oc == null) return;
                    _logger.LogInformation("hub: customer {customerId} changed. calling push");
                    await _hubContext.Clients.Client(connectionId).SendAsync("PushCustomer", oc);
                });
            _hubSubscriptionManager.Add(connectionId, "Customer", sub);
                     
            // dispatch OpenCustomer action
            StoreContainer.CustomerStore.Dispatch(new OpenCustomer(customerId, Context.User.Identity.Name));       
        }
        

        public async Task CloseCustomer()
        {
            _hubSubscriptionManager.DisposeAndRemove(Context.ConnectionId, "Customer");
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
            var connectionId = Context.ConnectionId;
            _logger.LogInformation("CustomerListSubscribe {connectionId}", connectionId);
            
            // subscribe to store
            var sub = StoreContainer.CustomerStore
                .Select(state => state.CustomerList)
                .DistinctUntilChanged()
                .Subscribe(async customerList =>
                {
                    _logger.LogInformation("Pushing Customer List");
                    await _hubContext.Clients.Client(connectionId).SendAsync("PushCustomerList", customerList);
                });
            _hubSubscriptionManager.Add(connectionId, "CustomerList", sub);
            
            // dispatch load
            // StoreContainer.CustomerStore.Dispatch(new LoadCustomerListAction());
        }
        
        
        public async Task CustomerListUnSubscribe()
        {
            _logger.LogInformation("CustomerListUnSubscribe {connectionId}", Context.ConnectionId);
            _hubSubscriptionManager.DisposeAndRemove(Context.ConnectionId, "CustomerList");
        }
        
        
        
        
        
    }
}