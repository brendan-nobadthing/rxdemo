import { Injectable } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import { Store } from '@ngxs/store';
import { CustomerListPushedAction } from '../ngxs/CustomerListActions';
import { Customer } from '../ngxs/Customer';
import { CustomerPushedAction } from '../ngxs/CustomerEditActions';


@Injectable()
export class CustomersHubService {
  public hubConnection: HubConnection;

  constructor(private store: Store) {
    this.hubConnection = new HubConnection('hubs/customer');

    this.hubConnection.on('PushCustomer', msg => {
      console.log('PushCustomer', msg);
      this.store.dispatch(new CustomerPushedAction(msg));
    });

    this.hubConnection.on('PushCustomerList', msg => {
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

  openCustomer(customerId: string) {
    console.log('hub invoking OpenCustomer ', customerId);
    this.hubConnection.invoke('OpenCustomer', customerId);
  }

  customerChanged(customer: Customer) {
    this.hubConnection.invoke('UpdateCustomer', customer);
  }

  saveCustomer(customer: Customer) {
    console.log('hub invoking SaveCustomer');
    this.hubConnection.invoke('SaveCustomer', customer);
  }

  closeCustomer() {
    this.hubConnection.invoke('CloseCustomer');
  }
}
