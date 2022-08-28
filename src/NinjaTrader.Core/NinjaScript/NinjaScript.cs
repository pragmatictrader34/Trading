using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Serialization;

using NinjaTrader.Code;


// ReSharper disable CheckNamespace

namespace NinjaTrader.NinjaScript
{
  public abstract class NinjaScript : ICloneable
  {
    private static bool isStopWatchEnabled;
    private NinjaTrader.NinjaScript.NinjaScript.StopwatchTime stopwatchTime;
    private Dictionary<Thread, NinjaTrader.NinjaScript.NinjaScript.ThreadStopwatch> threadStopwatches;
    private string description;

    /// <summary>Clears all data from the NinjaTrader output window.</summary>
    public void ClearOutputWindow() => Output.Reset(this.PrintTo);

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string Decrypt(string text) => (string) null;

    [XmlIgnore]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public string EditGuid { get; set; }

    public static bool IsStopWatchEnabled
    {
      get => NinjaTrader.NinjaScript.NinjaScript.isStopWatchEnabled;
      [MethodImpl(MethodImplOptions.NoInlining)] set
      {
      }
    }

    /// <summary>
    /// Determines if the current NinjaScript object should be visible on the chart.  When an object's IsVisible property is set to false, the object will NOT be displayed on the chart and will not be calculated to save resources.
    /// </summary>
    public bool IsVisible { get; set; }

    /// <summary>
    /// Call this method on the new generation NinjaScript object to make sure correct generation of clone is created. Calling it on the old instance would defeat the purpose.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [Obsolete]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static object MemberwiseClone(object obj) => (object) null;

    /// <summary>Plays a .wav file while running on real-time data.</summary>
    /// <param name="file">The absolute file path of the .wav file to play</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void PlaySound(string file)
    {
    }

    /// <summary>
    /// Converts object data to a string format and appends the specified value as text to the NinjaScript Output window.
    /// </summary>
    /// <param name="value">The object to print to the output window</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Print(object value)
    {
    }

    /// <summary>
    /// Determines either tab of the NinjaScript Output window the Print() and ClearOutputWindow() method targets.
    /// </summary>
    [XmlIgnore]
    [Browsable(false)]
    public PrintTo PrintTo { get; set; }

    /// <summary>
    /// Sends an email message through the default email sharing service
    /// </summary>
    /// <param name="to">The email recipient</param>
    /// <param name="subject">Subject line of email</param>
    /// <param name="text">Message body of email</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void SendMail(string to, string subject, string text)
    {
    }

    /// <summary>
    /// Sends a message or screen shot to a social network or Share Service.
    /// </summary>
    /// <param name="serviceName">A string value which represents the share service to be used</param>
    /// <param name="message">A string value which represents the text body sent to the social network or other Share</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Share(string serviceName, string message)
    {
    }

    /// <summary>
    /// Sends a message or screen shot to a social network or Share Service
    /// </summary>
    /// <param name="serviceName">A string value which represents the share service to be used</param>
    /// <param name="message">A string value which represents the text body sent to the social network or other Share providers</param>
    /// <param name="args">A generic object[]  array used to designate additional information sent to the share service</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Share(string serviceName, string message, object[] args)
    {
    }

    /// <summary>
    /// Sends a message or screen shot to a social network or Share Service.
    /// </summary>
    /// <param name="serviceName">A string value which represents the share service to be used</param>
    /// <param name="message">A string value which represents the text body sent to the social network or other Share providers</param>
    /// <param name="screenshotPath">Optional string path to screenshot or other images sent to the social network or other Share providers</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Share(string serviceName, string message, string screenshotPath)
    {
    }

    /// <summary>
    /// Sends a message or screen shot to a social network or Share Service.
    /// </summary>
    /// <param name="serviceName">A string value which represents the share service to be used</param>
    /// <param name="message">A string value which represents the text body sent to the social network or other Share providers</param>
    /// <param name="screenshotPath">Optional string path to screenshot or other images sent to the social network or other Share providers</param>
    /// <param name="args">A generic object[]  array used to designate additional information sent to the share service</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Share(string serviceName, string message, string screenshotPath, object[] args)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void StartStopWatch()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void StopStopWatch()
    {
    }

    public static Dictionary<string, NinjaTrader.NinjaScript.NinjaScript.StopwatchTime> StopwatchTimes { get; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected internal void VendorLicense(
      string vendorName,
      string product,
      string vendorUrl,
      string vendorMail,
      Func<bool> doVerify = null)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public bool VerifyVendorLicense() => false;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public virtual object Clone() => (object) null;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public virtual void CopyTo(NinjaTrader.NinjaScript.NinjaScript ninjaScript)
    {
    }

    /// <summary>
    /// Text which is used on the UI's information box to be displayed to a user when configuration a NinjaScript object.
    /// </summary>
    [Browsable(false)]
    [XmlIgnore]
    public string Description
    {
      get => this.description;
      [MethodImpl(MethodImplOptions.NoInlining)] protected set
      {
      }
    }

    /// <summary>
    /// Determines the text display on the chart panel.  This is also listed in the UI as the "Label" which can be manually changed.  The default behavior of this property will including the indicator Name along with its input and data series parameters.  However this behavior can be overridden if desired.
    /// </summary>
    [Browsable(false)]
    public virtual string DisplayName => this.Name;

    /// <summary>
    /// Generates a NinjaScript category log event record and associated time stamp which is output to the Log tab of the NinjaTrader Control Center / Account Data windows
    /// </summary>
    /// <param name="message">A string value which represents the message to be logged</param>
    /// <param name="logLevel">Sets the message level for the log event</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Log(string message, NinjaTrader.Cbi.LogLevel logLevel)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void LogAndPrint(Type resourceType, string name, object[] args, NinjaTrader.Cbi.LogLevel logLevel)
    {
    }

    [Browsable(false)]
    public abstract string LogTypeName { [MethodImpl(MethodImplOptions.NoInlining)] get; }

    /// <summary>Determines the listed name of the NinjaScript object.</summary>
    [XmlIgnore]
    public string Name { get; set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected NinjaScript()
    {
    }

    /// <summary>
    /// This method is used for changing the State of any running NinjaScript object.
    /// </summary>
    /// <param name="state">The State to be set</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public abstract void SetState(State state);

    /// <summary>
    /// Represents the current progression of the object as it advances from setup, processing data, to termination.  These states can be used for setting up or declaring various resources and properties.
    /// </summary>
    [Browsable(false)]
    [XmlIgnore]
    public State State { get; set; }

    static NinjaScript()
    {
      NinjaTrader.NinjaScript.NinjaScript.StopwatchTimes = new Dictionary<string, NinjaTrader.NinjaScript.NinjaScript.StopwatchTime>();
    }

    public class StopwatchTime
    {
      public string LogTypeName { get; set; }

      public long TotalTicks { get; set; }

      [MethodImpl(MethodImplOptions.NoInlining)]
      static StopwatchTime()
      {
      }
    }

    private class ThreadStopwatch
    {
      public Stopwatch Stopwatch { get; set; }

      public int StopwatchLevels { get; set; }

      [MethodImpl(MethodImplOptions.NoInlining)]
      public ThreadStopwatch()
      {
      }

      [MethodImpl(MethodImplOptions.NoInlining)]
      static ThreadStopwatch()
      {
      }
    }
  }
}
