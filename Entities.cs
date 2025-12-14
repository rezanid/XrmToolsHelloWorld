using XrmTools.Meta.Attributes;

[assembly: CodeGenReplacePrefixes("rn_")]
[assembly: CodeGenGlobalOptionSet(GlobalOptionSetGenerationMode.GlobalOptionSetFile)]
[assembly: Entity("account", AttributeNames = "name, industrycode, revenue, emailaddress1, telephone1")]
[assembly: Entity("contact", AttributeNames = "fullname, lastname, firstname, emailaddress1, telephone1")]