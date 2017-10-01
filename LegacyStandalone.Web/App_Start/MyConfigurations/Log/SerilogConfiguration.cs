using System.Collections.ObjectModel;
using System.Data;
using LegacyApplication.Shared.Configurations;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace LegacyStandalone.Web.MyConfigurations.Log
{
    public class SerilogConfiguration
    {
        public static void CreateLogger()
        {
            const string connectionString = AppSettings.DefaultConnection;
            const string tableName = "Logs";
            var columnOptions = new ColumnOptions
            {
                AdditionalDataColumns = new Collection<DataColumn>
                {
                    new DataColumn {DataType = typeof (string), ColumnName = "User"},
                    new DataColumn {DataType = typeof (string), ColumnName = "Class"}
                }
            };
            columnOptions.Store.Add(StandardColumn.LogEvent);
            const string outputTemplate = "[{Timestamp:HH:mm:ss.FFF} {Level}] {Message} ({SourceContext:l}){NewLine}{Exception}";
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("SourceContext", null)
                .Enrich.FromLogContext()
                .WriteTo.Debug(
                    outputTemplate: outputTemplate)
                .WriteTo.RollingFile("logs\\{Date}.log", shared: true, restrictedToMinimumLevel: LogEventLevel.Debug,
                    outputTemplate: outputTemplate)
                .WriteTo.MSSqlServer(connectionString, tableName, columnOptions: columnOptions, autoCreateSqlTable: true, restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();
        }
    }
}