import { Customer } from './Customer';

export class LoadCustomerListAction {
  static readonly type = '[CustomerList] LoadCustomerListAction';
}

export class SubscribeCustomerListAction {
  static readonly type = '[CustomerList] SubscribeCustomerListAction';
}

export class UnSubscribeCustomerListAction {
  static readonly type = '[CustomerList] UnSubscribeCustomerListAction';
}

export class CustomerListPushedAction {
  static readonly type = '[CustomerList] CustomerListPushedAction';
  constructor(public customerList: Customer[]) {}
}
