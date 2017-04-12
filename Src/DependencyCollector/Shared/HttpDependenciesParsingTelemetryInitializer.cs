﻿namespace Microsoft.ApplicationInsights.DependencyCollector
{
    using System;
    using Channel;
    using DataContracts;
    using Extensibility;
    using Implementation;
    using Implementation.HttpParsers;

    /// <summary>
    /// Telemetry Initializer that parses http dependencies into well-known types like Azure Storage.
    /// </summary>
    public class HttpDependenciesParsingTelemetryInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// If telemetry item is http dependency - converts it to the well-known type of the dependency.
        /// </summary>
        /// <param name="telemetry">Telemetry item to convert.</param>
        public void Initialize(ITelemetry telemetry)
        {
            var httpDependency = telemetry as DependencyTelemetry;

            if (httpDependency != null && httpDependency.Type != null && httpDependency.Type.Equals(RemoteDependencyConstants.HTTP, StringComparison.OrdinalIgnoreCase))
            {
                bool parsed =
                    AzureBlobHttpParser.TryParse(ref httpDependency)
                    || AzureTableHttpParser.TryParse(ref httpDependency)
                    || AzureQueueHttpParser.TryParse(ref httpDependency)
                    || DocumentDbHttpParser.TryParse(ref httpDependency)
                    || AzureServiceBusHttpParser.TryParse(ref httpDependency)
                    || GenericServiceHttpParser.TryParse(ref httpDependency)
                    || AzureIotHubHttpParser.TryParse(ref httpDependency);
            }
        }
    }
}
