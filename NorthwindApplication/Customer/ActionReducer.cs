using System;
using Redux;

namespace NorthwindApplication.Customer
{

    public abstract class ActionReducer<TS>
    {
        public abstract TS Reducer(TS prevState, IAction action);
    }

    public abstract class ActionReducer<TA, TS> : ActionReducer<TS>
        where TA: IAction
    {
        public override TS Reducer(TS prevState, IAction action)
        {
            if (!(action is TA)) {
                throw new NotImplementedException();
            }
            return Reducer(prevState, (TA)action);
        }
        
        public abstract TS Reducer(TS prevState, TA action);
    }
    
}