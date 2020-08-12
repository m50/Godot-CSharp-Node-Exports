#if TOOLS
using Godot;
using System.Reflection;
using System.Linq;
using CSharpExports.Attributes;
using System.Collections.Generic;

namespace CSharpExports
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
        }

        public override void _ExitTree()
        {
            foreach (var t in _customTypes)
            {
                RemoveCustomType(t);
            }
        }

        public void OnResourceSaved(Resource resource)
        {
            _ExitTree();
            _customTypes = new List<string>();
            _BuildTypes();
        }

        private void _BuildTypes()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var typeList = assembly.GetTypes().Where(
                t => t.GetCustomAttributes(typeof(TypeExportAttribute), true).Length > 0
            ).ToList();
            foreach (var t in typeList)
            {
                TypeExportAttribute typeAttr = t.GetCustomAttribute<TypeExportAttribute>();
                IconAttribute icon = t.GetCustomAttribute<IconAttribute>();

                Script script = GD.Load<Script>(typeAttr.scriptPath);
                Texture texture = null;
                if (icon != null) texture = GD.Load<Texture>(icon.imagePath);
                AddCustomType(t.Name, t.BaseType.Name, script, texture);
                _customTypes.Add(t.Name);
            }
        }
    }
}
#endif
