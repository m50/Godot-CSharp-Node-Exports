#if TOOLS
using Godot;
using System.Reflection;
using System.Linq;
using ClassName.Attributes;
using System.Collections.Generic;

namespace ClassName
{
    [Tool]
    public class Plugin : EditorPlugin
    {
        private List<string> _customTypes;

        public override void _EnterTree()
        {
            _customTypes = new List<string>();
            _BuildTypes();
            Connect("resource_saved", this, "OnResourceSaved");
            AddToolMenuItem("Reload C# Resources", this, nameof(_BuildTypes));
        }

        public override void _ExitTree()
        {
            foreach (var t in _customTypes)
            {
                RemoveCustomType(t);
            }
            RemoveToolMenuItem("Reload C# Resources");
        }

        public void OnResourceSaved(Resource resource)
        {
            _BuildTypes();
        }

        private void _BuildTypes()
        {
            _ExitTree();
            _customTypes = new List<string>();
            var assembly = Assembly.GetExecutingAssembly();
            var typeList = assembly.GetTypes().Where(
                t => t.GetCustomAttributes(typeof(ClassPathAttribute), true).Length > 0
            ).ToList();
            foreach (var t in typeList)
            {
                ClassPathAttribute typeAttr = t.GetCustomAttribute<ClassPathAttribute>();
                IconAttribute icon = t.GetCustomAttribute<IconAttribute>();

                Script script = GD.Load<Script>(typeAttr.scriptPath);
                Texture texture = null;
                if (icon != null) texture = ResourceLoader.Load<Texture>(icon.imagePath);
                AddCustomType(t.Name, t.BaseType.Name, script, texture);
                _customTypes.Add(t.Name);
            }
        }
    }
}
#endif
