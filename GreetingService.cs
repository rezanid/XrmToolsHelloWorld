using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using XrmTools.Meta.Attributes;

namespace XrmToolsHelloWorld
{
    public interface IGreetingService
    {
        string GetGreeting(string name, int? industryCode = null);
    }

    public class GreetingService : IGreetingService
    {
        private readonly IOrdinalFormatterService _formatter;
        private readonly IAccountStatisticsService _statisticsService;
        private readonly ITracingService _tracing;
        private readonly IAccountQueryService _accountQueryService;

        public GreetingService(
            ITracingService tracingService,
            IOrdinalFormatterService formatter,
            IAccountStatisticsService statisticsService,
            IAccountQueryService accountQueryService)
        {
            _tracing = tracingService ?? throw new ArgumentNullException(nameof(tracingService));
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            _statisticsService = statisticsService ?? throw new ArgumentNullException(nameof(statisticsService));
            _accountQueryService = accountQueryService ?? throw new ArgumentNullException(nameof(accountQueryService));
        }

        public string GetGreeting(string name, int? industryCode = null)
        {
            // Overall account ordinal (new record will be +1).
            _tracing.Trace("Calculating overall account ordinal.");
            var overallOrdinal = _formatter.FormatOrdinal(_statisticsService.GetAccountCount() + 1);

            // If no industry info, fall back to original logic.
            if (!industryCode.HasValue)
                return $"Hello, {name}! You are our {overallOrdinal} account.";

            // Retrieve existing accounts in the same industry.
            _tracing.Trace($"Retrieving accounts with industry code {industryCode}.");
            var sameIndustryAccounts = _accountQueryService.GetAccountsByIndustryCode(industryCode.Value);
            var existingIndustryCount = sameIndustryAccounts == null ? 0 : sameIndustryAccounts.Count;

            // Ordinal within this industry (current one will be +1).
            var industryOrdinal = _formatter.FormatOrdinal(existingIndustryCount + 1);

            // None existing in this industry yet.
            if (existingIndustryCount == 0)
                return $"Hello, {name}! You are our first account in this industry and our {overallOrdinal} overall.";

            // Threshold for listing peers.
            const int listThreshold = 5;

            if (existingIndustryCount < listThreshold)
            {
                // Collect distinct non-empty names.
                var peerNames = sameIndustryAccounts
                    //.Select(a => a.GetAttributeValue<string>("name"))
                    .Select(a => a.Name)
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .Distinct()
                    .ToList();

                var peersSegment = peerNames.Count > 0
                    ? $" alongside {string.Join(", ", peerNames)}"
                    : string.Empty;

                return $"Hello, {name}! You are our {industryOrdinal} account in this industry{peersSegment}. Overall you are our {overallOrdinal} account.";
            }

            // Too many to list; just summarize.
            return $"Hello, {name}! You are our {industryOrdinal} account in this industry (currently {existingIndustryCount} existing). Overall you are our {overallOrdinal} account.";
        }
    }
}
