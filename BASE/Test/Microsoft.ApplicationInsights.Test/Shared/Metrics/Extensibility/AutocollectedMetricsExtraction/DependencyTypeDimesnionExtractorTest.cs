﻿namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Metrics
{
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DependencyTypeDimesnionExtractorTest
    {
        [TestMethod]
        public void NullTarget()
        {
            var item = new DependencyTelemetry();
            var extractor = new TypeDimensionExtractor();
            var extractedDimension = extractor.ExtractDimension(item);
            Assert.AreEqual(string.Empty,extractedDimension);
        }

        [TestMethod]
        public void EmptyTarget()
        {
            var item = new DependencyTelemetry();
            item.Type = string.Empty;
            var extractor = new TypeDimensionExtractor();
            var extractedDimension = extractor.ExtractDimension(item);
            Assert.AreEqual(string.Empty, extractedDimension);
        }

        [TestMethod]
        public void ActualTarget()
        {
            var item = new DependencyTelemetry();
            item.Type = "ExpectedType";
            var extractor = new TypeDimensionExtractor();
            var extractedDimension = extractor.ExtractDimension(item);
            Assert.AreEqual("ExpectedType", extractedDimension);
        }
    }
}
