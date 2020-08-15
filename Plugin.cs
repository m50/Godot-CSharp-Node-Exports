#if TOOLS
using Godot;
using System.Reflection;
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
            BuildTypes();
            Connect("resource_saved", this, "OnResourceSaved");
            AddToolMenuItem("Reload C# Resources", this, nameof(BuildTypes));
        }

        public override void _ExitTree()
        {
            _RemoveTypes();
            RemoveToolMenuItem("Reload C# Resources");
        }

        public void OnResourceSaved(Resource resource)
        {
            BuildTypes();
        }

        private void _RemoveTypes()
        {
            foreach (var t in _customTypes)
                RemoveCustomType(t);
        }

        public void BuildTypes(object ud) => BuildTypes();
        public void BuildTypes()
        {
            _RemoveTypes(); // Prevent duplicates of the types.
            _customTypes = new List<string>();
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var t in assembly.GetTypes())
            {
                ClassNameAttribute typeAttr = t.GetCustomAttribute<ClassNameAttribute>();
                if (typeAttr == null) continue;
                if (!t.IsSubclassOf(typeof(Godot.Resource)) && !t.IsSubclassOf(typeof(Godot.Node)))
                {
                    GD.PrintErr($"[{t}]: ClassNameAttribute only works with Resources or Nodes.");
                    continue;
                }

                IconAttribute icon = t.GetCustomAttribute<IconAttribute>();

                Script script = ResourceLoader.Load<Script>(typeAttr.ScriptPath);
                Texture texture = null;
                if (icon != null) texture = ResourceLoader.Load<Texture>(icon.ImagePath);

                AddCustomType(t.Name, t.BaseType.Name, script, texture);
                _customTypes.Add(t.Name);
            }
        }
    }
}
#endif
