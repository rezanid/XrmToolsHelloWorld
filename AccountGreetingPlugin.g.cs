using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using XrmTools;

namespace XrmToolsHelloWorld
{
    [GeneratedCode("TemplatedCodeGenerator", "1.5.0.2")]
    public partial class AccountGreetingPlugin
    {
        /// <summary>
        /// This method should be called before accessing any target, image or any of your dependencies.
        /// </summary>
        protected IDisposable CreateScope(IServiceProvider serviceProvider)
        {
            var scope = new DependencyScope<AccountGreetingPlugin>();
            scope.Set<IServiceProvider>(serviceProvider);
        
            var iTracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
        
            scope.Set<IPluginExecutionContext>((IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext)));
            scope.Set<IOrganizationServiceFactory>((IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory)));
            scope.Set<ITracingService>((ITracingService)serviceProvider.GetService(typeof(ITracingService)));
            scope.Set<XrmToolsHelloWorld.IGreetingService>(scope.Set(new XrmToolsHelloWorld.GreetingService((ITracingService)serviceProvider.GetService(typeof(ITracingService)), scope.Set(new XrmToolsHelloWorld.OrdinalFormatterService()), scope.Set(new XrmToolsHelloWorld.AccountStatisticsService(this.Config, this.SecureConfig, this.OrganizationService)), scope.Set(new XrmToolsHelloWorld.AccountQueryService(this.OrganizationService)))));
            return scope;
        }
	    [EntityLogicalName("account")]
	    public class TargetAccount : Entity
	    {
	    	public static class Meta
	    	{
	    		public const string EntityLogicalName = "account";
	    		public const string EntityLogicalCollectionName = "accounts";
	    		public const string EntitySetName = "accounts";
	    		public const string PrimaryNameAttribute = "";
	    		public const string PrimaryIdAttribute = "accountid";
	    
	    		public partial class Fields
	    		{
	    			public const string Description = "description";
	    			public const string IndustryCode = "industrycode";
	    			public const string Name = "name";
	    		
	    			public static bool TryGet(string logicalName, out string attribute)
	    			{
	    				switch (logicalName)
	    				{
	    					case nameof(Description): attribute = Description; return true;
	    					case nameof(IndustryCode): attribute = IndustryCode; return true;
	    					case nameof(Name): attribute = Name; return true;
	    					default: attribute = null; return false;
	    				}
	    			}
	    
	    			public string this[string logicalName]
	    			{
	    				get => TryGet(logicalName, out var value)
	    					? value
	    					: throw new ArgumentException("Invalid attribute logical name.", nameof(logicalName));
	    			}
	    		}
	    
	    		public partial class OptionSets
	    		{
	    			/// <summary>
	    			/// Type of industry with which the account is associated.
	    			/// </summary>
	    			[DataContract]
	    			public enum Industry
	    			{
	    				[EnumMember]
	    				Accounting = 1,
	    				[EnumMember]
	    				AgricultureAndNonPetrolNaturalResourceExtraction = 2,
	    				[EnumMember]
	    				BroadcastingPrintingAndPublishing = 3,
	    				[EnumMember]
	    				Brokers = 4,
	    				[EnumMember]
	    				BuildingSupplyRetail = 5,
	    				[EnumMember]
	    				BusinessServices = 6,
	    				[EnumMember]
	    				Consulting = 7,
	    				[EnumMember]
	    				ConsumerServices = 8,
	    				[EnumMember]
	    				DesignDirectionAndCreativeManagement = 9,
	    				[EnumMember]
	    				DistributorsDispatchersAndProcessors = 10,
	    				[EnumMember]
	    				DoctorSOfficesAndClinics = 11,
	    				[EnumMember]
	    				DurableManufacturing = 12,
	    				[EnumMember]
	    				EatingAndDrinkingPlaces = 13,
	    				[EnumMember]
	    				EntertainmentRetail = 14,
	    				[EnumMember]
	    				EquipmentRentalAndLeasing = 15,
	    				[EnumMember]
	    				Financial = 16,
	    				[EnumMember]
	    				FoodAndTobaccoProcessing = 17,
	    				[EnumMember]
	    				InboundCapitalIntensiveProcessing = 18,
	    				[EnumMember]
	    				InboundRepairAndServices = 19,
	    				[EnumMember]
	    				Insurance = 20,
	    				[EnumMember]
	    				LegalServices = 21,
	    				[EnumMember]
	    				NonDurableMerchandiseRetail = 22,
	    				[EnumMember]
	    				OutboundConsumerService = 23,
	    				[EnumMember]
	    				PetrochemicalExtractionAndDistribution = 24,
	    				[EnumMember]
	    				ServiceRetail = 25,
	    				[EnumMember]
	    				SigAffiliations = 26,
	    				[EnumMember]
	    				SocialServices = 27,
	    				[EnumMember]
	    				SpecialOutboundTradeContractors = 28,
	    				[EnumMember]
	    				SpecialtyRealty = 29,
	    				[EnumMember]
	    				Transportation = 30,
	    				[EnumMember]
	    				UtilityCreationAndDistribution = 31,
	    				[EnumMember]
	    				VehicleRetail = 32,
	    				[EnumMember]
	    				Wholesale = 33,
	    			}
	    		}
	    	}
	    
	    	/// <summary>
	    	/// Max Length: 2000</br>
	    	/// Required Level: None</br>
	    	/// Valid for: Create Update Read</br>
	    	/// </summary>
	    	[AttributeLogicalName("description")]
	    	public string Description
	    	{
	    		get => TryGetAttributeValue("description", out string value) ? value : null;
	    		set => this["description"] = value;
	    	}
	    	/// <summary>
	    	/// Required Level: None</br>
	    	/// Valid for: Create Update Read</br>
	    	/// </summary>
	    	[AttributeLogicalName("industrycode")]
	    	public TargetAccount.Meta.OptionSets.Industry? IndustryCode
	    	{
	    		get => TryGetAttributeValue("industrycode", out OptionSetValue opt) && opt != null ? (TargetAccount.Meta.OptionSets.Industry?)opt.Value : null;
	    		set => this["industrycode"] = value == null ? null : new OptionSetValue((int)value);
	    	}
	    	/// <summary>
	    	/// Max Length: 160</br>
	    	/// Required Level: ApplicationRequired</br>
	    	/// Valid for: Create Update Read</br>
	    	/// </summary>
	    	[AttributeLogicalName("name")]
	    	public string Name
	    	{
	    		get => TryGetAttributeValue("name", out string value) ? value : null;
	    		set => this["name"] = value;
	    	}
	    }
	    
	    public TargetAccount Target
        {
            get => EntityOrDefault<TargetAccount>(DependencyScope<AccountGreetingPlugin>.Current.Require<IPluginExecutionContext>().InputParameters, "Target");
        }


	    private static T EntityOrDefault<T>(DataCollection<string, object> keyValues, string key) where T : Entity
        {
            if (keyValues is null) return default;
            return keyValues.TryGetValue(key, out var obj) ? obj is Entity entity ? entity.ToEntity<T>() : default : default;
        }

        private static T EntityOrDefault<T>(DataCollection<string, Entity> keyValues, string key) where T : Entity
        {
            if (keyValues is null) return default;
            return keyValues.TryGetValue(key, out var entity) ? entity?.ToEntity<T>() : default;
        }

        private static T Require<T>() => DependencyScope<AccountGreetingPlugin>.Current.Require<T>();
        private static T Require<T>(string name) => DependencyScope<AccountGreetingPlugin>.Current.Require<T>(name);

        private static bool TryGet<T>(out T instance) => DependencyScope<AccountGreetingPlugin>.Current.TryGet(out instance);
        private static bool TryGet<T>(string name, out T instance) => DependencyScope<AccountGreetingPlugin>.Current.TryGet(name, out instance);

        private static T Set<T>(T instance) => DependencyScope<AccountGreetingPlugin>.Current.Set(instance);
        private static T Set<T>(string name, T instance) => DependencyScope<AccountGreetingPlugin>.Current.Set(name, instance);
        private static T SetAndTrack<T>(T instance) where T : IDisposable => DependencyScope<AccountGreetingPlugin>.Current.SetAndTrack(instance);
        private static T SetAndTrack<T>(string name, T instance) where T : IDisposable => DependencyScope<AccountGreetingPlugin>.Current.SetAndTrack(name, instance);
    }
}
