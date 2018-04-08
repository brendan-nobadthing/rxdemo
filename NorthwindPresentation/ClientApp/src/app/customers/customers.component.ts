import { Component, OnInit } from '@angular/core';
import { Store, Select } from '@ngxs/store';
import { LoadCustomerListAction } from '../ngxs/CustomerListState';
import { Observable } from 'rxjs/Observable';
import { Customer } from '../ngxs/Customer';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {

  @Select(s => s.customerList.customers) customers$: Observable<Customer[]>;

  constructor(private store: Store) {}

  ngOnInit() {

    // this.customers$.subscribe(c => console.log('via component subscruption', c));

    this.store.dispatch(new LoadCustomerListAction());
  }

}
