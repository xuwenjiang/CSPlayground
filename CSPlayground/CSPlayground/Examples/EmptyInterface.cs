using System;
namespace CSPlayground.Examples
{
    public abstract class AbstractExample
    {
        public void RunExample()
        {
            DisplayExampleListAndRunBasedOnSelection();
        }

        protected abstract void DisplayExampleListAndRunBasedOnSelection();
    }
}
