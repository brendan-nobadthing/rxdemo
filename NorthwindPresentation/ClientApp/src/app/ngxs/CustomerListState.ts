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
    console.log('CustomerListPushedAction handler');
    const state = ctx.getState();
    ctx.setState({
      ...state,
      customers: action.customerList
    });
  }

}




