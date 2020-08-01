namespace CSharpExports.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    sealed class TypeExportAttribute : System.Attribute
    {
        public readonly string scriptPath;

        public TypeExportAttribute(string scriptPath)
        {
            this.scriptPath = scriptPath;
        }
    }
}
