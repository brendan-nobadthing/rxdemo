import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, Select } from '@ngxs/store';
import { LoadCustomerListAction, SubscribeCustomerListAction, UnSubscribeCustomerListAction } from '../ngxs/CustomerListActions';
import { Observable } from 'rxjs/Observable';
import { Customer } from '../ngxs/Customer';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit, OnDestroy {

  @Select(s => s.customerList.customers) customers$: Observable<Customer[]>;

  constructor(private store: Store) {}

  ngOnInit() {
    this.store.dispatch(new SubscribeCustomerListAction());
  }

  ngOnDestroy() {
    this.store.dispatch(new UnSubscribeCustomerListAction());
  }

}
