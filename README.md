# ProvisionData.ResourceHelpr
Read Strings and Streams from local Assemblies

[![Build status](https://ci.appveyor.com/api/projects/status/dkxir0tkpf3tq31w/branch/master?svg=true)](https://ci.appveyor.com/project/dougkwilson/pdsi-resource-helpr/branch/master)

# Usage

Add an Embedded Resource to your project:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <EmbeddedResource Include="TextFile.txt" />
  </ItemGroup>
</Project>
```

Retrieve it at runtime:

```csharp
[Fact]
public void Must_return_a_String_this_assembly()
{
    RH.GS<ATypeInYourAssembly>("TextFile.txt").Should().Be("This is a text file.");
}
```
