namespace ClassName.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    sealed class IconAttribute : System.Attribute
    {
        public readonly string imagePath;

        public IconAttribute(string imagePath)
        {
            this.imagePath = imagePath;
        }
    }
}
