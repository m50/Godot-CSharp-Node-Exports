namespace ClassName.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    sealed class IconAttribute : System.Attribute
    {
        public readonly string ImagePath;

        public IconAttribute(string imagePath)
        {
            ImagePath = imagePath;
        }
    }
}
