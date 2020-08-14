namespace CSharpExports.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    sealed class ClassPathAttribute : System.Attribute
    {
        public readonly string scriptPath;

        public ClassPathAttribute(string scriptPath)
        {
            this.scriptPath = scriptPath;
        }
    }
}
