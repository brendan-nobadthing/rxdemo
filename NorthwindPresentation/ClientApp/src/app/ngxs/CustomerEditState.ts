import { State, Action, StateContext, Store } from '@ngxs/store';
import { Customer } from './Customer';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { CustomersHubService } from '../customers/customers-hub.service';
import { CustomerPushedAction, CustomerChangedAction, TestAction, SaveAction } from './CustomerEditActions';

export class OpenCustomerAction {
  static readonly type = '[CustomerEdit] OpenCustomerAction';
}

export class CustomerEditStateModel {
  customer: Customer;
  users: string[];
  isChanged: boolean;
}



@State<CustomerEditStateModel>({
  name: 'customerEdit',
  defaults: { customer: null, users: [], isChanged: false }
})
export class CustomerEditState {

  constructor(
    private customersHub: CustomersHubService
  ) {}

  @Action(CustomerPushedAction)
  CustomerPushed(ctx: StateContext<CustomerEditStateModel>, action: CustomerPushedAction) {
    console.log('CustomerPushedAction handler ', action);
    const state = ctx.getState();
    ctx.setState(action.payload);
  }

  @Action(CustomerChangedAction)
  CustomerChanged(ctx: StateContext<CustomerEditStateModel>, action: CustomerChangedAction) {
    this.customersHub.customerChanged(action.payload);
  }

  @Action(SaveAction)
  SaveAction(ctx: StateContext<CustomerEditStateModel>, action: SaveAction) {
    console.log('SaveAction Handler');
    this.customersHub.saveCustomer(action.payload);
  }

  @Action(TestAction)
  TestAction(ctx: StateContext<CustomerEditStateModel>, action: TestAction) {
    console.log('TestActionHandler');
    const state = ctx.getState();
    ctx.setState({
      ...state
    });
  }

}
