import { State, Action, StateContext, Store } from '@ngxs/store';
import { Customer } from './Customer';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { CustomersHubService } from '../customers/customers-hub.service';
import {
  SubscribeCustomerListAction,
  CustomerListPushedAction,
  LoadCustomerListAction,
  UnSubscribeCustomerListAction
  } from './CustomerListActions';


export class CustomerListStateModel {
  customers: Customer[];
}

@State<CustomerListStateModel>({
  name: 'customerList',
})
export class CustomerListState {

  constructor(
    private httpClient: HttpClient,
    private customersHub: CustomersHubService
  ) {}

  @Action(SubscribeCustomerListAction)
  SubscribeCustomerList(ctx: StateContext<CustomerListStateModel>, action: SubscribeCustomerListAction) {
    this.customersHub.subscribeCustomerList();
  }

  @Action(UnSubscribeCustomerListAction)
  UbSubscribeCustomerList(ctx: StateContext<CustomerListStateModel>, action: UnSubscribeCustomerListAction) {
    this.customersHub.unSubscribeCustomerList();
  }

  @Action(CustomerListPushedAction)
  CustomerListPushed(ctx: StateContext<CustomerListStateModel>, action: CustomerListPushedAction) {
    const state = ctx.getState();
    ctx.setState({
      ...state,
      customers: action.customerList
    });
  }

  @Action(LoadCustomerListAction)
  loadCustomerList({ setState }: StateContext<CustomerListStateModel>) {
    console.log('LOAD CUSTOMERS');
    return this.httpClient.get<Customer[]>('https://localhost:5001/api/customer/list')
      .pipe(tap(result => {
        console.log('GOT CUSTOMERS', result);
        setState({
          customers: result
        });
      }));
  }

}




