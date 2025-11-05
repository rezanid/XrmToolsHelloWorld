namespace XrmToolsHelloWorld
{
    using XrmTools.Meta.Attributes;

    public interface IGreetingService
    {
        string GetGreeting(string name);
    }

    public class GreetingService : IGreetingService
    {
        private readonly IOrdinalFormatterService _formatter;
        private readonly IAccountStatisticsService _statisticsService;

        public GreetingService(
            IOrdinalFormatterService formatter,
            IAccountStatisticsService statisticsService)
        {
            _formatter = formatter ?? throw new System.ArgumentNullException(nameof(formatter));
            _statisticsService = statisticsService ?? throw new System.ArgumentNullException(nameof(statisticsService));
        }

        public string GetGreeting(string name)
        {
            var ordinal = _formatter.FormatOrdinal(_statisticsService.GetAccountCount() + 1);
            return $"Hello, {name}! You are our {ordinal} account.";
        }
    }
}
