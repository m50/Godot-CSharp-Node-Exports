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

## Known Issues

The biggest limitation is that you have to provide the path to the actual script.
While annoying, this is required. In theory, this could be determined by the namespace,
but that would require some additional structuring and/or configuration of namespaces.

Since Godot doesn't have a signal for build complete, this reloads the objects
everytime it enters the tree, and so adding a new one requires unloading and re-loading
the plugin. To try and minimize this, it also reloads if a resource has been saved.
