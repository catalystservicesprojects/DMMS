using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace ModelFlattener
{
    class Program
    {
        // 🟢 CONFIGURE HERE
        static string targetNamespace = "DMMS.Models"; // Namespace to scan
        static string sourceFolder = @"D:\NinadGawankar\Projects\DMMS\Core.Models"; // Path to your Models folder
        static string outputFolder = @"D:\NinadGawankar\Projects\DMMS\Core.WebMVC.App.Generated\Models"; // Where to save flattened models

        static void Main(string[] args)
        {
            Console.WriteLine("=== Model Flattener Started ===");

            var dllPath = Path.Combine(AppContext.BaseDirectory, "DMMS.Models.dll");
            var assembly = Assembly.LoadFrom(dllPath);

            // Load current assembly
            //var assembly = Assembly.Load("DMMS.ModelMaker"); // Change to your assembly name

            // Get all classes in the namespace
            var modelTypes = assembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace != null && t.Namespace.StartsWith(targetNamespace))
                .ToList();

            Console.WriteLine($"Found {modelTypes.Count} model(s).");

            foreach (var modelType in modelTypes)
            {
                Console.WriteLine($"Processing {modelType.FullName}...");
                ProcessModel(modelType);
            }

            Console.WriteLine("=== Done! Flattened models written successfully. ===");
        }

        static void ProcessModel(Type modelType)
        {
            var allProps = GetAllProperties(modelType);

            // Prepare directory for output
            string relativePath = modelType.Namespace!.Replace(targetNamespace, "")
                .Replace('.', Path.DirectorySeparatorChar);
            string outputDir = Path.Combine(outputFolder, relativePath);
            Directory.CreateDirectory(outputDir);

            //string fileName = $"{modelType.Name}_Flattened.cs";
            string fileName = $"{modelType.Name}.cs";
            string filePath = Path.Combine(outputDir, fileName);

            string classCode = GenerateFlattenedModelCode(modelType, allProps);
            File.WriteAllText(filePath, classCode);
        }

        static List<PropertyInfo> GetAllProperties(Type type)
        {
            var props = new List<PropertyInfo>();
            while (type != null && type != typeof(object))
            {
                props.AddRange(type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
                type = type.BaseType!;
            }

            // Remove duplicates by name
            return props
                .GroupBy(p => p.Name)
                .Select(g => g.First())
                .ToList();
        }

        static string GenerateFlattenedModelCode(Type originalType, List<PropertyInfo> allProps)
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine();
            //sb.AppendLine($"namespace {originalType.Namespace}.Flattened");
            sb.AppendLine($"namespace {originalType.Namespace}");
            sb.AppendLine("{");
            sb.AppendLine($"    // Auto-generated flattened model for {originalType.FullName}");
            //sb.AppendLine($"    public class {originalType.Name}_Flattened");
            sb.AppendLine($"    public class {originalType.Name}");
            sb.AppendLine("    {");

            foreach (var prop in allProps)
            {
                string typeName = GetFriendlyTypeName(prop.PropertyType);
                sb.AppendLine($"        public {typeName} {prop.Name} {{ get; set; }}");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        static string GetFriendlyTypeName(Type t)
        {
            if (t.IsGenericType)
            {
                var genericTypeName = t.GetGenericTypeDefinition().Name;
                genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
                var genericArgs = string.Join(", ", t.GetGenericArguments().Select(GetFriendlyTypeName));
                return $"{genericTypeName}<{genericArgs}>";
            }

            return t.Name switch
            {
                "Int32" => "int",
                "String" => "string",
                "Boolean" => "bool",
                "Decimal" => "decimal",
                "Double" => "double",
                "Single" => "float",
                _ => t.Name
            };
        }
    }
}