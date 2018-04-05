using System;
using System.Threading.Tasks;
using Redux;

namespace NorthwindApplication.Customer
{
    public abstract class ActionEffect<TS>
    {
        public abstract Task<IAction> Effect(TS prevState, IAction action);
    }
    
    public abstract class ActionEffect<TA, TS> : ActionEffect<TS>
        where TA: IAction
    {
        public override async Task<IAction> Effect(TS prevState, IAction action)
        {
            if (!(action is TA)) {
                throw new NotImplementedException();
            }
            return await Effect(prevState, (TA)action);
        }
        
        public abstract Task<IAction> Effect(TS prevState, TA action);
    }
}