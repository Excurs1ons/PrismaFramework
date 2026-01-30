using System;
using System.IO;
using Cysharp.Text;
using PrismaFramework.GameLauncher;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
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

    [MenuItem("Build/Full Build",priority = 0)]
    public static void BuildWithHotfixDll()
    {
        //预构建
        HybridCLR.Editor.Commands.PrebuildCommand.GenerateAll();
        BuildHotfixDll();
        BuildAssets();
        Build();
    }

    [MenuItem("Build/Build Player",priority = 1)]
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

    [MenuItem("Build/Build Assets", priority = 2)]
    public static void BuildAssets()
    {
        Debug.Log("Start Build Addressables Content...");
        AddressableAssetSettings.BuildPlayerContent();
        Debug.Log("Build Addressables Content Finish.");
    }

    [MenuItem("Build/Build Hotfix Dll",priority = 3)]
    public static void BuildHotfixDll()
    {
        var target = EditorUserBuildSettings.activeBuildTarget;
        string hotfixDllSrcDir = HybridCLR.Editor.SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
        string hotfixDllDstDir = Application.streamingAssetsPath;
        
        if (!Directory.Exists(hotfixDllDstDir))
        {
            Directory.CreateDirectory(hotfixDllDstDir);
        }
        
        
        string srcPath = Path.Combine(hotfixDllSrcDir, GlobalDefinitions.MAIN_DLL_NAME);
        string dstPath = Path.Combine(hotfixDllDstDir, GlobalDefinitions.MAIN_DLL_NAME + ".bytes");
        
        if (File.Exists(srcPath))
        {
            File.Copy(srcPath, dstPath, true);
            Debug.Log($"[BuildHotfixDll] Copied {srcPath} to {dstPath}");
        }
        else
        {
            Debug.LogError($"[BuildHotfixDll] Source DLL not found: {srcPath}");
        }
    }
}