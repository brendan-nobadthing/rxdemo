import { Injectable } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';

@Injectable()
export class CustomersHubService {

  public hubConnection: HubConnection;

  constructor() {
    this.hubConnection = new HubConnection('hubs/customer');

    this.hubConnection.on('PushCustomer', (msg) => {
      console.log('PushCustomer', msg);
      // this.customer = msg.customer;
      // this.customerForm.patchValue(msg.customer, { emitEvent: false });
    });

    this.hubConnection.start();
   }

}
