using System.Reflection;

namespace Container
{
    public interface IAppLogger
    {
        void MethodEntry(object logEventInfo, MethodBase methodBase);
        void MethodExit(object logEventInfo, MethodBase methodBase, double methodExecutionDuration, int messageCode);
        void Trace(string message, MethodBase methodBase);
        void Exception(Exception exception, MethodBase methodBase, string extraMessage = "");
    }
}
