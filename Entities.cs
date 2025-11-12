
using XrmTools.Meta.Attributes;

[assembly: CodeGenReplacePrefixes("abc_")]
[assembly: CodeGenGlobalOptionSet(GlobalOptionSetGenerationMode.GlobalOptionSetFile)]
[assembly: Entity("account", AttributeNames = "name, industrycode")]