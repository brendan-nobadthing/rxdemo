import { State, Action, StateContext } from '@ngxs/store';
import { Customer } from './Customer';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

export class OpenCustomerAction {
  static readonly type = '[CustomerEdit] OpenCustomerAction';
}

export class CustomerEditStateModel {
  customer: Customer;
  editors: string[];
}

export class CustomerPushedAction {
  static readonly type = '[CustomerEdit] OpenCustomerAction';
  constructor(public payload: CustomerEditStateModel) {}
}



@State<CustomerEditStateModel>({
  name: 'customerEdit',
})
export class CustomerEditState {

  constructor() {}

  @Action(CustomerPushedAction)
  customerPushed(ctx: StateContext<CustomerEditStateModel>, action: CustomerPushedAction) {
    const state = ctx.getState();
    ctx.setState(action.payload);
  }

}
