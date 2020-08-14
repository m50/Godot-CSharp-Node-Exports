using System.Runtime.CompilerServices;

namespace ClassName.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    sealed class ClassNameAttribute : System.Attribute
    {
        public readonly string ScriptPath;

        public ClassNameAttribute([CallerFilePath] string scriptPath = "")
        {
            ScriptPath = scriptPath;
        }
    }
}
