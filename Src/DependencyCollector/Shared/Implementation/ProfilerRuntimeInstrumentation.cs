﻿namespace Microsoft.ApplicationInsights.DependencyCollector.Implementation
{
    using Microsoft.Diagnostics.Instrumentation.Extensions.Intercept;

    internal static class ProfilerRuntimeInstrumentation
    {
        internal static void DecorateProfilerForHttp(ref ProfilerHttpProcessing httpCallbacks)
        {
            // Decorates Http GetResponse, 0 params
            Functions.Decorate(
                "System",
                "System.dll",
                "System.Net.HttpWebRequest.GetResponse",
                httpCallbacks.OnBeginForGetResponse,
                httpCallbacks.OnEndForGetResponse,
                httpCallbacks.OnExceptionForGetResponse,
                isStatic: false);

            // Decorates Http GetRequestStream, 1 param
            Functions.Decorate(
                "System",
                "System.dll",
                "System.Net.HttpWebRequest.GetRequestStream",
                httpCallbacks.OnBeginForGetRequestStream,
                null,
                httpCallbacks.OnExceptionForGetRequestStream,
                isStatic: false);

            // Decorates Http BeginGetResponse, 2 params
            Functions.Decorate(
                "System",
                "System.dll",
                "System.Net.HttpWebRequest.BeginGetResponse",
                httpCallbacks.OnBeginForBeginGetResponse,
                null,
                null,
                isStatic: false);

            // Decorates Http EndGetResponse, 1 param
            Functions.Decorate(
                "System",
                "System.dll",
                "System.Net.HttpWebRequest.EndGetResponse",
                null,
                httpCallbacks.OnEndForEndGetResponse,
                httpCallbacks.OnExceptionForEndGetResponse,
                isStatic: false);

            // Decorates Http BeginGetRequestStream
            Functions.Decorate(
                "System",
                "System.dll",
                "System.Net.HttpWebRequest.BeginGetRequestStream",
                httpCallbacks.OnBeginForBeginGetRequestStream,
                null,
                null,
                isStatic: false);

            // Decorates Http EndGetRequestStream, 2 params
            Functions.Decorate(
                "System",
                "System.dll",
                "System.Net.HttpWebRequest.EndGetRequestStream",
                null,
                null,
                httpCallbacks.OnExceptionForEndGetRequestStream,
                isStatic: false);
        }

        internal static void DecorateProfilerForSql(ref ProfilerSqlProcessing sqlCallbacks)
        {
            //// ___ ExecuteNonQuery ___ ////

            // Decorates Sql BeginExecuteNonQuery, 0 params (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.BeginExecuteNonQuery",
                sqlCallbacks.OnBeginForOneParameter,
                null,
                null,
                isStatic: false);

            // Decorates Sql BeginExecuteNonQuery(AsyncCallback, Object), 2 params (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.BeginExecuteNonQuery",
                sqlCallbacks.OnBeginForThreeParameters,
                null,
                null,
                isStatic: false);

            // Decorates Sql EndExecuteNonQuery, 1 param (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.EndExecuteNonQuery",
                null,
                sqlCallbacks.OnEndForTwoParameters,
                sqlCallbacks.OnExceptionForTwoParameters,
                isStatic: false);

            // Decorate Sql ExecuteNonQuery, 0 param (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.ExecuteNonQuery",
                sqlCallbacks.OnBeginForOneParameter,
                sqlCallbacks.OnEndForOneParameter,
                sqlCallbacks.OnExceptionForOneParameter,
                isStatic: false);

            // Decorate Sql ExecuteNonQueryAsync(CancellationToken)
            // Only latest instrumentation engine supports Tasks so we need to uncomment this code and add funcs when StatusMonitor is out
            // + Decorate in the same way ExecuteReader, ExecuteXmlReader and ExecuteScalar
            ////Functions.Decorate(
            ////    "System.Data",
            ////    "System.Data.dll",
            ////    "System.Data.SqlClient.SqlCommand.ExecuteNonQueryAsync",
            ////    sqlCallbacks.OnBeginForTwoParameter,
            ////    null,
            ////    null,
            ////    isStatic: false);
            //// Instead of Decorating public methods we have to use private one that may change signature from one framework to the other: 

            // Read comment above. Decorate BeginExecuteNonQueryAsync, 2 param (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.BeginExecuteNonQueryAsync",
                sqlCallbacks.OnBeginForTwoParameters,
                null,
                null,
                isStatic: false);

            // Read comment above. Decorate EndExecuteNonQueryAsync, 1 param (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.EndExecuteNonQueryAsync",
                null,
                sqlCallbacks.OnEndForOneParameter,
                sqlCallbacks.OnExceptionForOneParameter,
                isStatic: false);

            //// ___ ExecuteReader ___ ////

            // Decorates Sql BeginExecuteReader, 0 param (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.BeginExecuteReader",
                sqlCallbacks.OnBeginForOneParameter,
                null,
                null,
                isStatic: false);

            // Decorates Sql BeginExecuteReader(CommandBehavior), 1 param (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.BeginExecuteReader",
                sqlCallbacks.OnBeginForTwoParameters,
                null,
                null,
                isStatic: false);

            // Decorates Sql BeginExecuteReader(AsyncCallback, Object), 2 param (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.BeginExecuteReader",
                sqlCallbacks.OnBeginForThreeParameters,
                null,
                null,
                isStatic: false);

            // Decorates Sql BeginExecuteReader(AsyncCallback, Object, CommandBehavior), 3 params (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.BeginExecuteReader",
                sqlCallbacks.OnBeginForFourParameters,
                null,
                null,
                isStatic: false);

            // Decorates Sql EndExecuteReader, 1 param (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.EndExecuteReader",
                null,
                sqlCallbacks.OnEndForTwoParameters,
                sqlCallbacks.OnExceptionForTwoParameters,
                isStatic: false);

            // Decorate Sql ExecuteReader, 0 params(+this) (we instrument 2 overloads of ExecuteReader because there are cases when methods get inlined or tail call optimized)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.ExecuteReader",
                sqlCallbacks.OnBeginForOneParameter,
                sqlCallbacks.OnEndForOneParameter,
                sqlCallbacks.OnExceptionForOneParameter,
                isStatic: false);

            // Decorate Sql ExecuteReader(CommandBehavior), 2 params(+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.ExecuteReader",
                sqlCallbacks.OnBeginForThreeParameters,
                sqlCallbacks.OnEndForThreeParameters,
                sqlCallbacks.OnExceptionForThreeParameters,
                isStatic: false);

            // Should be replaced with public method when InstrumentationEngine supports Tasks. 
            // Decorate BeginExecuteReaderAsync, 3 param (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.BeginExecuteReaderAsync",
                sqlCallbacks.OnBeginForThreeParameters,
                null,
                null,
                isStatic: false);

            // Should be replaced with public method when InstrumentationEngine supports Tasks.
            // Decorate EndExecuteReaderAsync, 1 param (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.EndExecuteReaderAsync",
                null,
                sqlCallbacks.OnEndForOneParameter,
                sqlCallbacks.OnExceptionForOneParameter,
                isStatic: false);

            //// ___ ExecuteScalar ___ ////

            // Decorate Sql ExecuteScalar, 0 params(+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.ExecuteScalar",
                sqlCallbacks.OnBeginForOneParameter,
                sqlCallbacks.OnEndForOneParameter,
                sqlCallbacks.OnExceptionForOneParameter,
                isStatic: false);

            //// ___ ExecuteXmlReader ___ ////

            // Decorates Sql BeginExecuteXmlReader, 0 params(+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader",
                sqlCallbacks.OnBeginForOneParameter,
                null,
                null,
                isStatic: false);

            // Decorates Sql BeginExecuteXmlReader(AsyncCallback, Object), 2 params(+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader",
                sqlCallbacks.OnBeginForThreeParameters,
                null,
                null,
                isStatic: false);

            // Decorates Sql EndExecuteXmlReader(IAsyncResult), 1 param(+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.EndExecuteXmlReader",
                null,
                sqlCallbacks.OnEndForTwoParameters,
                sqlCallbacks.OnExceptionForTwoParameters,
                isStatic: false);

            // Decorate Sql ExecuteXmlReader, 0 params(+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.ExecuteXmlReader",
                sqlCallbacks.OnBeginForOneParameter,
                sqlCallbacks.OnEndForOneParameter,
                sqlCallbacks.OnExceptionForOneParameter,
                isStatic: false);

            // Should be replaced with public method when InstrumentationEngine supports Tasks. 
            // Decorate BeginExecuteXmlReaderAsync, 2 param (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.BeginExecuteXmlReaderAsync",
                sqlCallbacks.OnBeginForTwoParameters,
                null,
                null,
                isStatic: false);

            // Should be replaced with public method when InstrumentationEngine supports Tasks.
            // Decorate EndExecuteXmlReaderAsync, 1 param (+this)
            Functions.Decorate(
                "System.Data",
                "System.Data.dll",
                "System.Data.SqlClient.SqlCommand.EndExecuteXmlReaderAsync",
                null,
                sqlCallbacks.OnEndForOneParameter,
                sqlCallbacks.OnExceptionForOneParameter,
                isStatic: false);
        }
    }
}
