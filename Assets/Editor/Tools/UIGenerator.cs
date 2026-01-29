// Editor/UIGenerator.cs

using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace PrismaFramework.Editor.Tools
{
    public class UIGenerator
    {
        [MenuItem("Assets/Prisma/Generate UI Code")]
        public static void Generate()
        {
            GameObject prefab = Selection.activeGameObject;
            if (!prefab) return;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using UnityEngine.UI;");
            sb.AppendLine("using TMPro;");
            sb.AppendLine("using PrismaFramework.Client.UI.Components;"); // 咱们的 LocalizedText
            sb.AppendLine();
            sb.AppendLine("namespace PrismaFramework.Client.UI.Windows");
            sb.AppendLine("{");
            // 使用 partial class，这样生成的代码和手写的逻辑代码分离
            sb.AppendLine($"    public partial class {prefab.name} : UIWindow");
            sb.AppendLine("    {");

            // 递归查找所有以 "_" 开头的子节点
            var allNodes = prefab.GetComponentsInChildren<Transform>(true);
            foreach (var node in allNodes)
            {
                if (node.name.StartsWith("_"))
                {
                    string typeName = GuessType(node);
                    string varName = node.name; // 比如 _btn_close

                    sb.AppendLine($"        [SerializeField] private {typeName} {varName};");
                }
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            // 写入文件: Assets/Scripts/UI/Windows/Bind/{PrefabName}.Bind.cs
            string path = Application.dataPath + $"/Scripts/Client/UI/Windows/Bind/{prefab.name}.Bind.cs";
            File.WriteAllText(path, sb.ToString());
            AssetDatabase.Refresh();

            Debug.Log($"UI Code Generated: {path}");
        }

        private static string GuessType(Transform node)
        {
            // 简单的类型推断逻辑
            if (node.name.Contains("btn")) return "Button";
            if (node.name.Contains("txt")) return "TMP_Text"; // 或者 LocalizedText
            if (node.name.Contains("img")) return "Image";
            return "Transform";
        }
    }
}