using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.Runtime;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.AwsCloudWatch;
using System;

namespace N5.Challenge.Core.Application.Service.Logger
{
    public class SerilogCloudWatcher
    {
        private readonly string logGroup;
        private readonly string logEventLevel;
        private readonly string accessKey;
        private readonly string secretKey;
        private readonly string region;
        public SerilogCloudWatcher(string logGroup, string logEventLevel, string accessKey, string secretKey, string region)
        {
            this.logGroup = logGroup;
            this.logEventLevel = logEventLevel;
            this.accessKey = accessKey;
            this.secretKey = secretKey;
            this.region = region;
        }
        public void ConfigSeriLogAWS()
        {
            try
            {
                var formatter = new Serilog.Formatting.Json.JsonFormatter();
                var logOptions = new CloudWatchSinkOptions
                {
                    LogGroupName = logGroup,
                    TextFormatter = formatter,
                    MinimumLogEventLevel = GetLogEventLevel(logEventLevel),
                    BatchSizeLimit = 100,
                    QueueSizeLimit = 1000,
                    Period = TimeSpan.FromSeconds(1),
                    CreateLogGroup = true,
                    LogStreamNameProvider = new DefaultLogStreamProvider(),
                    RetryAttempts = 2
                };

                AWSCredentials credentials = null;
                if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey))
                    credentials = FallbackCredentialsFactory.GetCredentials();
                else
                    credentials = new BasicAWSCredentials(accessKey, secretKey);

                var client = new AmazonCloudWatchLogsClient(credentials,
                    RegionEndpoint.GetBySystemName(region));

                Log.Logger = new LoggerConfiguration()
                    .WriteTo.AmazonCloudWatch(logOptions, client)
                    .CreateLogger();
                Serilog.Debugging.SelfLog.Enable(Console.Error);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enable Log [{ex.Message}]");
            }
        }

        private static LogEventLevel GetLogEventLevel(string strLogEventLevel)
        {
            LogEventLevel logEventLevel = LogEventLevel.Error;

            switch (strLogEventLevel.ToUpper())
            {
                case
                    "VERBOSE":
                    logEventLevel = LogEventLevel.Verbose;
                    break;
                case
                    "DEBUG":
                    logEventLevel = LogEventLevel.Debug;
                    break;
                case
                    "INFORMATION":
                    logEventLevel = LogEventLevel.Information;
                    break;
                case
                    "WARNING":
                    logEventLevel = LogEventLevel.Warning;
                    break;
                case
                    "ERROR":
                    logEventLevel = LogEventLevel.Error;
                    break;
                case
                    "FATAL":
                    logEventLevel = LogEventLevel.Fatal;
                    break;
            }

            return logEventLevel;
        }
    }
}
