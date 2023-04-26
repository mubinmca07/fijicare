using System;
namespace FijiCareUtility
{
    public class DelegateCommand : AbstractCommand
    {
        public DelegateCommand(Action executeCallback)
            : base(o => executeCallback())
        {

        }

        public DelegateCommand(Action executeCallback, Func<bool> canExecuteCallback)
            : base(o => executeCallback(), o => canExecuteCallback())
        {

        }
    }

    public class DelegateCommand<T> : AbstractCommand
    {
        // private Action autoCompleteSeachFuncation;

        public DelegateCommand(Action<T> executeCallback)
            : base(o => executeCallback((T)o))
        {

        }


        public DelegateCommand(Action<T> executeCallback, Func<T, bool> canExecuteCallback)
            : base(o => executeCallback((T)o), o => canExecuteCallback((T)o))
        {

        }
    }
}
    

