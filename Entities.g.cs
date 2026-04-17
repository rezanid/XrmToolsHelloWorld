using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace XrmToolsHelloWorld
{
	/// <summary>
	/// Display Name: Account
	/// </summary>
	[GeneratedCode("TemplatedCodeGenerator", "1.6.0.0")]
	[EntityLogicalName("account")]
	[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public partial class Account : Microsoft.Xrm.Sdk.Entity
	{
		public partial class Meta 
		{
			public const string EntityLogicalName = "account";
			public const string EntityLogicalCollectionName = "accounts";
			public const string EntitySetName = "accounts";
			public const string PrimaryNameAttribute = "name";
			public const string PrimaryIdAttribute = "accountid";

			public partial class Fields
			{
				public const string EMailAddress1 = "emailaddress1";
				public const string IndustryCode = "industrycode";
				public const string Name = "name";
				public const string Revenue = "revenue";
				public const string Telephone1 = "telephone1";

				public static bool TryGet(string memberName, out string logicalName)
				{
					switch (memberName)
					{
						case nameof(EMailAddress1): logicalName = EMailAddress1; return true;
						case nameof(IndustryCode): logicalName = IndustryCode; return true;
						case nameof(Name): logicalName = Name; return true;
						case nameof(Revenue): logicalName = Revenue; return true;
						case nameof(Telephone1): logicalName = Telephone1; return true;
						default: logicalName = null; return false;
					}
				}
				public static IEnumerable<string> GetAll() => 
					new string[] {EMailAddress1,IndustryCode,Name,Revenue,Telephone1,};

				public static string Get(string memberName)
					=> TryGet(memberName, out var logicalName)
						? logicalName
						: throw new ArgumentException("Invalid attribute logical name.", nameof(memberName));
			}

			public partial class OptionSets
			{
				/// <summary>
				/// Type of industry with which the account is associated.
				/// </summary>
				[DataContract]
				public enum Industry
				{
					[EnumMember] Accounting = 1,
					[EnumMember] AgricultureAndNonPetrolNaturalResourceExtraction = 2,
					[EnumMember] BroadcastingPrintingAndPublishing = 3,
					[EnumMember] Brokers = 4,
					[EnumMember] BuildingSupplyRetail = 5,
					[EnumMember] BusinessServices = 6,
					[EnumMember] Consulting = 7,
					[EnumMember] ConsumerServices = 8,
					[EnumMember] DesignDirectionAndCreativeManagement = 9,
					[EnumMember] DistributorsDispatchersAndProcessors = 10,
					[EnumMember] DoctorSOfficesAndClinics = 11,
					[EnumMember] DurableManufacturing = 12,
					[EnumMember] EatingAndDrinkingPlaces = 13,
					[EnumMember] EntertainmentRetail = 14,
					[EnumMember] EquipmentRentalAndLeasing = 15,
					[EnumMember] Financial = 16,
					[EnumMember] FoodAndTobaccoProcessing = 17,
					[EnumMember] InboundCapitalIntensiveProcessing = 18,
					[EnumMember] InboundRepairAndServices = 19,
					[EnumMember] Insurance = 20,
					[EnumMember] LegalServices = 21,
					[EnumMember] NonDurableMerchandiseRetail = 22,
					[EnumMember] OutboundConsumerService = 23,
					[EnumMember] PetrochemicalExtractionAndDistribution = 24,
					[EnumMember] ServiceRetail = 25,
					[EnumMember] SigAffiliations = 26,
					[EnumMember] SocialServices = 27,
					[EnumMember] SpecialOutboundTradeContractors = 28,
					[EnumMember] SpecialtyRealty = 29,
					[EnumMember] Transportation = 30,
					[EnumMember] UtilityCreationAndDistribution = 31,
					[EnumMember] VehicleRetail = 32,
					[EnumMember] Wholesale = 33,
				}
			}
		}

		/// <summary>
		/// Max Length: 100<br/>
		/// Required Level: None<br/>
		/// Valid for: Create Update Read<br/>
		/// </summary>
		[AttributeLogicalName("emailaddress1")]
		public string EMailAddress1
		{
			get => TryGetAttributeValue("emailaddress1", out string value) ? value : null;
			set => this["emailaddress1"] = value;
		}
		/// <summary>
		/// Required Level: None<br/>
		/// Valid for: Create Update Read<br/>
		/// </summary>
		[AttributeLogicalName("industrycode")]
		public Account.Meta.OptionSets.Industry? IndustryCode
		{
			get => TryGetAttributeValue("industrycode", out OptionSetValue opt) && opt != null ? (Account.Meta.OptionSets.Industry?)opt.Value : null;
			set => this["industrycode"] = value == null ? null : new OptionSetValue((int)value);
		}
		/// <summary>
		/// Max Length: 160<br/>
		/// Required Level: ApplicationRequired<br/>
		/// Valid for: Create Update Read<br/>
		/// </summary>
		[AttributeLogicalName("name")]
		public string Name
		{
			get => TryGetAttributeValue("name", out string value) ? value : null;
			set => this["name"] = value;
		}
		/// <summary>
		/// Required Level: None<br/>
		/// Valid for: Create Update Read<br/>
		/// </summary>
		[AttributeLogicalName("revenue")]
		public decimal? Revenue
		{
			get => TryGetAttributeValue("revenue", out Money money) ? (decimal?)money.Value : null;
			set => this["revenue"] = value.HasValue ? new Money(value.Value) : null;
		}
		/// <summary>
		/// Max Length: 50<br/>
		/// Required Level: None<br/>
		/// Valid for: Create Update Read<br/>
		/// </summary>
		[AttributeLogicalName("telephone1")]
		public string Telephone1
		{
			get => TryGetAttributeValue("telephone1", out string value) ? value : null;
			set => this["telephone1"] = value;
		}
		public Account() : base(Meta.EntityLogicalName) { }
		public Account(Guid id) : base(Meta.EntityLogicalName, id) { }
		public Account(string keyName, object keyValue) : base(Meta.EntityLogicalName, keyName, keyValue) { }
		public Account(KeyAttributeCollection keyAttributes) : base(Meta.EntityLogicalName, keyAttributes) { }
	}
	/// <summary>
	/// Display Name: Contact
	/// </summary>
	[GeneratedCode("TemplatedCodeGenerator", "1.6.0.0")]
	[EntityLogicalName("contact")]
	[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public partial class Contact : Microsoft.Xrm.Sdk.Entity
	{
		public partial class Meta 
		{
			public const string EntityLogicalName = "contact";
			public const string EntityLogicalCollectionName = "contacts";
			public const string EntitySetName = "contacts";
			public const string PrimaryNameAttribute = "fullname";
			public const string PrimaryIdAttribute = "contactid";

			public partial class Fields
			{
				public const string EMailAddress1 = "emailaddress1";
				public const string FirstName = "firstname";
				public const string FullName = "fullname";
				public const string LastName = "lastname";
				public const string Telephone1 = "telephone1";

				public static bool TryGet(string memberName, out string logicalName)
				{
					switch (memberName)
					{
						case nameof(EMailAddress1): logicalName = EMailAddress1; return true;
						case nameof(FirstName): logicalName = FirstName; return true;
						case nameof(FullName): logicalName = FullName; return true;
						case nameof(LastName): logicalName = LastName; return true;
						case nameof(Telephone1): logicalName = Telephone1; return true;
						default: logicalName = null; return false;
					}
				}
				public static IEnumerable<string> GetAll() => 
					new string[] {EMailAddress1,FirstName,FullName,LastName,Telephone1,};

				public static string Get(string memberName)
					=> TryGet(memberName, out var logicalName)
						? logicalName
						: throw new ArgumentException("Invalid attribute logical name.", nameof(memberName));
			}
		}

		/// <summary>
		/// Max Length: 100<br/>
		/// Required Level: None<br/>
		/// Valid for: Create Update Read<br/>
		/// </summary>
		[AttributeLogicalName("emailaddress1")]
		public string EMailAddress1
		{
			get => TryGetAttributeValue("emailaddress1", out string value) ? value : null;
			set => this["emailaddress1"] = value;
		}
		/// <summary>
		/// Max Length: 50<br/>
		/// Required Level: Recommended<br/>
		/// Valid for: Create Update Read<br/>
		/// </summary>
		[AttributeLogicalName("firstname")]
		public string FirstName
		{
			get => TryGetAttributeValue("firstname", out string value) ? value : null;
			set => this["firstname"] = value;
		}
		/// <summary>
		/// Max Length: 160<br/>
		/// Required Level: None<br/>
		/// Valid for: Read<br/>
		/// </summary>
		[AttributeLogicalName("fullname")]
		public string FullName
		{
			get => TryGetAttributeValue("fullname", out string value) ? value : null;
		}
		/// <summary>
		/// Max Length: 50<br/>
		/// Required Level: ApplicationRequired<br/>
		/// Valid for: Create Update Read<br/>
		/// </summary>
		[AttributeLogicalName("lastname")]
		public string LastName
		{
			get => TryGetAttributeValue("lastname", out string value) ? value : null;
			set => this["lastname"] = value;
		}
		/// <summary>
		/// Max Length: 50<br/>
		/// Required Level: None<br/>
		/// Valid for: Create Update Read<br/>
		/// </summary>
		[AttributeLogicalName("telephone1")]
		public string Telephone1
		{
			get => TryGetAttributeValue("telephone1", out string value) ? value : null;
			set => this["telephone1"] = value;
		}
		public Contact() : base(Meta.EntityLogicalName) { }
		public Contact(Guid id) : base(Meta.EntityLogicalName, id) { }
		public Contact(string keyName, object keyValue) : base(Meta.EntityLogicalName, keyName, keyValue) { }
		public Contact(KeyAttributeCollection keyAttributes) : base(Meta.EntityLogicalName, keyAttributes) { }
	}
}