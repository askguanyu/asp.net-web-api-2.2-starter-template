using System;
using System.Diagnostics;
using System.Web.Http.ExceptionHandling;
using Serilog.Context;

namespace LegacyStandalone.Web.MyConfigurations.Exceptions
{
    public class MyExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
#if DEBUG
            Trace.TraceError(context.ExceptionContext.Exception.ToString());
#endif
            using (LogContext.PushProperty("Class",
                context.ExceptionContext.ControllerContext.ControllerDescriptor.ControllerType))
            using (LogContext.PushProperty("User",
                context.RequestContext.Principal.Identity.Name))
            {
                LogException(context.ExceptionContext.Exception);
            }
        }

        private void LogException(Exception ex)
        {
            if (ex != null)
            {
                LogException(ex.InnerException);
                Serilog.Log.Logger.Error(ex.ToString());
            }
        }
    }
}