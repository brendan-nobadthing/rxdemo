import { Component, OnInit, OnDestroy } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import { Customer } from '../../ngxs/Customer';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-edit-customer',
  templateUrl: './edit-customer.component.html',
  styleUrls: ['./edit-customer.component.css']
})
export class EditCustomerComponent implements OnInit, OnDestroy {


  public hubConnection: HubConnection;
  // public customer: Customer;
  public customerId: string;
  customerForm: FormGroup;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.buildForm();

    this.customerId = this.activatedRoute.snapshot.params['id'];
    this.hubConnection = new HubConnection('hubs/customer');

    this.hubConnection.on('PushCustomer', (msg) => {
      console.log('PushCustomer', msg);
      // this.customer = msg.customer;
      this.customerForm.patchValue(msg.customer, { emitEvent: false });
    });

    this.hubConnection.start()
      .then(() => {
        console.log('Connection started');
        this.hubConnection.invoke('OpenCustomer', this.customerId);
      })
      .catch(err => { console.error(err); });

    this.customerForm.valueChanges.subscribe(val => {
      val.customerId = this.customerId;
      this.hubConnection.invoke('UpdateCustomer', val);
    });


  }

  buildForm() {
    this.customerForm = this.fb.group({
      companyName: [''],
      address: [''],
      city: [''],
      contactName: ['']
    });
  }


  ngOnDestroy() {
    this.hubConnection.stop();
  }

}
