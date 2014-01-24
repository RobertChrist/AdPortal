namespace AdPortal
{
    using System;

    // I've included a logging interface as one of my cross cutting concerns under the assumption that media radar
    // has a standard method of logging all of their application.  I have not built a usable implementation for the same reason.
    // The interface below is similar to the one used in the .Net log4net project.
    
    /// <summary>
    /// A mockup of MediaRadar's standard logging interface.  In this example, we model the log4net logging interface.  
    /// </summary>
    public interface ILogger
    {
        void Debug(object message);
        void Info(object message);
        void Warn(object message);
        void Error(object message);
        void Fatal(object message);
        
        void Debug(object message, Exception t);
        void Info(object message, Exception t);
        void Warn(object message, Exception t);
        void Error(object message, Exception t);
        void Fatal(object message, Exception t);
        
        void DebugFormat(string format, params object[] args);
        void InfoFormat(string format, params object[] args);
        void WarnFormat(string format, params object[] args);
        void ErrorFormat(string format, params object[] args);
        void FatalFormat(string format, params object[] args);
        
        void DebugFormat(IFormatProvider provider, string format, params object[] args);
        void InfoFormat(IFormatProvider provider, string format, params object[] args);
        void WarnFormat(IFormatProvider provider, string format, params object[] args);
        void ErrorFormat(IFormatProvider provider, string format, params object[] args);
        void FatalFormat(IFormatProvider provider, string format, params object[] args);
    }
}
