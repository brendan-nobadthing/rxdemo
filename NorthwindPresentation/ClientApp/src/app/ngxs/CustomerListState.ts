import { State, Action, StateContext } from '@ngxs/store';
import { Customer } from './Customer';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

export class LoadCustomerListAction {
  static readonly type = '[CustomerList] LoadCustomerListAction';
}

export class CustomerListStateModel {
  customers: Customer[];
}

@State<CustomerListStateModel>({
  name: 'customerList',
})
export class CustomerListState {

  constructor(private httpClient: HttpClient) {}

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




