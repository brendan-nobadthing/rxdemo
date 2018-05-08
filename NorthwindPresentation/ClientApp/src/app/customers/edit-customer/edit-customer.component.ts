import { Component, OnInit, OnDestroy } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import { Customer } from '../../ngxs/Customer';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CustomersHubService } from '../customers-hub.service';
import { Store, Select } from '@ngxs/store';
import { Observable } from 'rxjs/Observable';
import { CustomerEditStateModel } from '../../ngxs/CustomerEditState';
import { filter } from 'rxjs/operators/filter';
import { TestAction, SaveAction } from '../../ngxs/CustomerEditActions';

@Component({
  selector: 'app-edit-customer',
  templateUrl: './edit-customer.component.html',
  styleUrls: ['./edit-customer.component.css']
})
export class EditCustomerComponent implements OnInit, OnDestroy {


  public customerId: string;
  customerForm: FormGroup;
  customerEdit$: Observable<CustomerEditStateModel>;

  @Select(s => s.customerEdit.customer) customer$: Observable<Customer>;



  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private customersHub: CustomersHubService,
    private store: Store
  ) { }



  ngOnInit() {

    this.store.dispatch(new TestAction());

    this.buildForm();

    this.customerId = this.activatedRoute.snapshot.params['id'];

    this.subscribeToForm();
    this.subscribeToStore();

    this.customersHub.openCustomer(this.customerId);
  }

  subscribeToStore() {

    this.customer$.subscribe(c => console.log('got customer ', c));

    this.customerEdit$ = this.store.select(s => s.customerEdit)
      .pipe(
        filter(ce => typeof(ce) !== 'undefined'),
        filter(ce => ce.customer !== null)
      );

    this.customerEdit$
      .subscribe(ce => {
        console.log('patching customer from ', ce);
        this.customerForm.patchValue(ce.customer, { emitEvent: false });
      });
  }


  subscribeToForm() {
    this.customerForm.valueChanges.subscribe(val => {
      val.customerId = this.customerId;
      this.customersHub.customerChanged(val);
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

  saveChanges() {
    console.log('component save changes');
    const customer = this.customerForm.value;
    console.log(customer);
    customer.customerId = this.customerId;
    this.store.dispatch(new SaveAction(customer));
  }


  ngOnDestroy() {
    this.customersHub.closeCustomer();
  }

}
