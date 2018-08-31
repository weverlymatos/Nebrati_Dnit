using Dnit.Core.Behaviors;

namespace Dnit.UnitTests.Mocks
{
    public class MockEventToCommandBehavior : EventToCommandBehavior
    {
        public void RaiseEvent(params object[] args)
        {
            _handler.DynamicInvoke(args);
        }
    }
}
