public static class TinyLogger
{
    public enum LogLevel
    {
        TRACE,
        DEBUG,
        INFO,
        WARN,
        WARNING = WARN,
        ERROR,
        FATAL,
        OFF
    }

    private static string _dateFormat;
    private static LogLevel _logLevel;
    private static TextWriter _writer;


    static TinyLogger()
    {
        _dateFormat = "yyyy-MM-dd HH:mm:ss.fff";
        _logLevel = LogLevel.OFF;
        _writer = Console.Out;
    }

    public static void StartFile(LogLevel level, string? logFile = null, bool append = true)
    {
        if (String.IsNullOrEmpty(logFile))
            logFile = Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]) + ".log";

        string state = "started";
        if (File.Exists(logFile))
        {
            if (append)
                state = "resumed";
            else
                Warn($"Previous log {logFile} will be overwritten!");
        }

        Info($"Logging {state} in {logFile}");
        Start(level, new StreamWriter(logFile, append));
    }

    public static void Start(LogLevel level, TextWriter? writer = null)
    {
        // Technically you do not need to call this method to start logging, you can just set a Level and go.
        if (writer != null)
            Writer = writer;
        Level = level;
        Info($"Logging started.");
    }

    public static void Stop()
    {
        // If you do not call this method before exiting Main, some data may remain unflushed.
        Level = LogLevel.OFF;
        Writer.Flush();
    }

    public static void Trace(object? msg) => Log(LogLevel.TRACE, msg);
    public static void Info(object? msg) => Log(LogLevel.INFO, msg);
    public static void Debug(object? msg) => Log(LogLevel.DEBUG, msg);
    public static void Warn(object? msg) => Log(LogLevel.WARN, msg);
    public static void Warning(object? msg) => Log(LogLevel.WARN, msg);
    public static void Error(object? msg) => Log(LogLevel.ERROR, msg);
    public static void Fatal(object? msg) => Log(LogLevel.FATAL, msg);

    public static void Log(LogLevel lvl, object? msg)
    {
        if (lvl >= Level)
        {
            if (msg is null)
                msg = "<null>";

            lock (Writer)
            {
                try
                {
                    Writer.WriteLine($"{DateTime.Now.ToString(Format)} [{lvl}] \t{msg}");
                    //Writer.Flush();
                }
                catch (IOException ex)
                {
                    if (Writer != Console.Out)
                    {
                        Console.Error.WriteLine($"Error writing to log file {Writer}:\n{ex.Message}");
                        Console.Error.WriteLine($"Logging switched to Console.Out");
                        _writer = Console.Out;
                        Writer.WriteLine($"{DateTime.Now.ToString(Format)} [{lvl}] \t{msg}");
                    }
                    else throw ex;
                }
            }
        }
    }

    public static string Format
    {
        get => _dateFormat;
        set
        {
            _dateFormat = value;
            Info($"DateTime format set to '{value}'");
        }
    }

    public static LogLevel Level
    {
        get => _logLevel;
        set
        {
            if (value > _logLevel)
            {
                if (value == LogLevel.OFF)
                    Info("Logging stopped.");
                else
                    Info($"Logging level set to {value}.");
                _logLevel = value;
            }
            else if (value < _logLevel)
            {
                _logLevel = value;
                Info($"Logging level set to {_logLevel}.");
            }
        }
    }

    public static TextWriter Writer
    {
        get => _writer;
        set
        {
            Info($"Logging switched to {value}");
            _writer.Flush();
            _writer = value;
        }
    }
}
