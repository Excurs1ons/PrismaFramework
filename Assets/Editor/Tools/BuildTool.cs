using System;
using System.IO;
using Cysharp.Text;
using UnityEditor;
using UnityEngine;

namespace PrismaFramework.Editor.Tools;

public static class BuildTool
{
    private static string GetBuildPath()
    {
        var sb = ZString.CreateStringBuilder();
        //获取构建目标
        var target = EditorUserBuildSettings.activeBuildTarget;
        //获取构建模式
        var mode = EditorUserBuildSettings.development ? "Development" : "Release";
        var buildPath =
            $"{Application.dataPath}/../Build/{target}/{mode}/{GetBuildDirName()}/{GetBuildFileName()}.{GetExecutableFileExt()}";
        sb.AppendFormat("正在构建：{0}/{1}，路径为：{2}", target, mode, buildPath);
        Debug.Log(sb.ToString());
        return buildPath;
    }

    private static string GetExecutableFileExt()
    {
        var target = EditorUserBuildSettings.activeBuildTarget;
        switch (target)
        {
            case BuildTarget.StandaloneWindows64:
            case BuildTarget.StandaloneWindows:
                return "exe";
            case BuildTarget.Android:
                return "apk";
            default:
                Debug.LogErrorFormat("不支持的构建目标:{0}", target);
                throw new ArgumentOutOfRangeException();
        }
    }

    private static string GetBuildDirName()
    {
        return GetBuildFileName() + "_" + Application.version;
    }

    private static string GetBuildFileName()
    {
        return $"{Application.productName}";
    }

    [MenuItem("Build/Quick Build")]
    public static void Build()
    {
        var buildPath = GetBuildPath();
        var options = BuildOptions.None;
        if (EditorUserBuildSettings.development)
        {
            options |= BuildOptions.Development;
        }

        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, buildPath, EditorUserBuildSettings.activeBuildTarget,
            options);
    }
}