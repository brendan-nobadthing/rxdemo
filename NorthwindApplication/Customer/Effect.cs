using System;
using Redux;

namespace NorthwindApplication.Customer
{
    
    public abstract class ActionEffect<TS>
    {
        public abstract IAction Effect(TS prevState, IAction action);
    }

    public abstract class ActionEffect<TA, TS> : ActionEffect<TS>
        where TA: IAction
    {
        public override IAction Effect(TS prevState, IAction action)
        {
            if (!(action is TA)) {
                throw new NotImplementedException();
            }
            return Effect(prevState, (TA)action);
        }
        
        public abstract IAction Effect(TS prevState, TA action);
    }
    
    
   
}