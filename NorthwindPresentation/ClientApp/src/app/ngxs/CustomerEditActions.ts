import { CustomerEditStateModel } from './CustomerEditState';
import { Customer } from './Customer';


export class CustomerPushedAction {
  static readonly type = '[CustomerEdit] CustomerPushedAction';
  constructor(public payload: CustomerEditStateModel) {}
}


export class CustomerChangedAction {
  static readonly type = '[CustomerEdit] CustomerChangedAction';
  constructor(public payload: Customer) {}
}

export class SaveAction {
  static readonly type = '[CustomerEdit] SaveAction';
  constructor(public payload: Customer) {}
}


export class TestAction {
  static readonly type = '[CustomerEdit] TestAction';
  constructor() {}
}
