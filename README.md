# C# Exports for Godot

In gdscript in Godot, you can use `class_name Name, "res://path/to/icon.png"` and
the class will export into the editor. However, in C# this isn't really possible.

As such, this can be used to work around that issue.

Example:

```csharp
using CSharpExports.Attributes;
using Godot;

namespace GameObjects.Plants
{
    [TypeExport("res://Src/GameObjects/Plants/Plant.cs")]
    [Icon("res://Assets/GameObjects/Plants/Pumpkin/PumpkinIcon.png")]
    public class Plant : Sprite
    {
        //...
    }
}
```

![Screenshot of editor](images/image.png)

As you can see here, we have exported the Plant type and added a Pumpkin icon to it.

Reloads happen whenever a resource is saved, or can be done manually in `Project \> Tools \> Reload C# Resources`.

Since this plugin interacts with the rest of our code, it is required to be referenced in your csproj file.

```xml
<Compile Include="addons/**/*.cs" Condition=" '$(Configuration)' == 'Debug' " />
```

## Known Issues

The biggest limitation is that you have to provide the path to the actual script.
While annoying, this is required. In theory, this could be determined by the namespace,
but that would require some additional structuring and/or configuration of namespaces.
