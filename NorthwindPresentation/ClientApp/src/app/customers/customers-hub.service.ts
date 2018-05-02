import { Injectable } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import { Store } from '@ngxs/store';
import { CustomerListPushedAction } from '../ngxs/CustomerListActions';

@Injectable()
export class CustomersHubService {

  public hubConnection: HubConnection;

  constructor(private store: Store) {
    this.hubConnection = new HubConnection('hubs/customer');

    this.hubConnection.on('PushCustomer', (msg) => {
      console.log('PushCustomer', msg);
      // this.customer = msg.customer;
      // this.customerForm.patchValue(msg.customer, { emitEvent: false });
    });

    this.hubConnection.on('PushCustomerList', (msg) => {
      console.log('PushCustomerList', msg);
        this.store.dispatch(new CustomerListPushedAction(msg));
    });

    this.hubConnection.start();
   }


   subscribeCustomerList() {
     this.hubConnection.invoke('CustomerListSubscribe');
   }

   unSubscribeCustomerList() {
    this.hubConnection.invoke('CustomerListUnSubscribe');
  }

}
