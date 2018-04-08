import { Component, OnInit, OnDestroy } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import { Customer } from '../../ngxs/Customer';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-customer',
  templateUrl: './edit-customer.component.html',
  styleUrls: ['./edit-customer.component.css']
})
export class EditCustomerComponent implements OnInit, OnDestroy {


  public hubConnection: HubConnection;
  public customer: Customer;
  public customerId: string;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {

    this.customerId = this.activatedRoute.snapshot.params['id'];
    this.hubConnection = new HubConnection('hubs/customer');

    this.hubConnection.on('PushCustomer', (msg) => {
      console.log('got oc', msg);
      this.customer = msg.customer;
    });

    this.hubConnection.start()
      .then(() => {
        console.log('Connection started');
        this.hubConnection.invoke('OpenCustomer', this.customerId);
      })
      .catch(err => { console.error(err); });
  }

}
