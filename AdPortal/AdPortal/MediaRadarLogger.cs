using System;

namespace AdPortal
{
    // I've included a logging interface as one of my cross cutting concerns under the assumption that media radar
    // has a standard method of logging all of their application.  I have not built an implementation for the same reason.
    // The interface below is similar to the one used in the .Net log4net project.

    /// <summary>
    /// An empty mockup of MediaRadar's standard logging interface.
    /// </summary>
    public class MediaRadarLoggerMock : ILogger
    {
        void ILogger.Debug(object message)
        {
        }

        void ILogger.Info(object message)
        {
        }

        void ILogger.Warn(object message)
        {
        }

        void ILogger.Error(object message)
        {
        }

        void ILogger.Fatal(object message)
        {
        }

        void ILogger.Debug(object message, Exception t)
        {
        }

        void ILogger.Info(object message, Exception t)
        {
        }

        void ILogger.Warn(object message, Exception t)
        {
        }

        void ILogger.Error(object message, Exception t)
        {
        }

        void ILogger.Fatal(object message, Exception t)
        {
        }

        void ILogger.DebugFormat(string format, params object[] args)
        {
        }

        void ILogger.InfoFormat(string format, params object[] args)
        {
        }

        void ILogger.WarnFormat(string format, params object[] args)
        {
        }

        void ILogger.ErrorFormat(string format, params object[] args)
        {
        }

        void ILogger.FatalFormat(string format, params object[] args)
        {
        }

        void ILogger.DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
        }

        void ILogger.InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
        }

        void ILogger.WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
        }

        void ILogger.ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
        }

        void ILogger.FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
        }
    }
}
