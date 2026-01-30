using System.IO;
using Cysharp.Text;
using UnityEditor;
using UnityEngine;

namespace PrismaFramework.Editor.Tools;

public class BuildTools
{
    public static string GetBuildPath()
    {
        var sb = ZString.CreateStringBuilder();
        //获取构建目标
        var target = EditorUserBuildSettings.activeBuildTarget;
        //获取构建模式
        var mode = EditorUserBuildSettings.development ? "Development" : "Release";
        var buildPath =
            $"{Application.dataPath}/../Build/{target}/{mode}/{GetBuildFileName()}/{GetBuildFileName()}.{GetBuildFileExt()}";
        sb.AppendFormat("正在构建：{0}/{1}，路径为：{2}", target, mode, buildPath);
        Debug.Log(sb.ToString());
        return buildPath;
    }

    private static string GetBuildFileName()
    {
        return $"{Application.productName}_{Application.version}";
    }

    private static string GetBuildFileExt()
    {
        return "exe";
    }

    [MenuItem("Build/Quick Build")]
    public static void Build()
    {
        var buildPath = GetBuildPath();
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, buildPath, EditorUserBuildSettings.activeBuildTarget,
            BuildOptions.None);
    }
}