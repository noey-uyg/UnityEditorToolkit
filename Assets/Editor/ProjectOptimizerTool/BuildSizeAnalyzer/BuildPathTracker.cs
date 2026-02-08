using noeyToolkit;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class BuildPathTracker : IPostprocessBuildWithReport
{
    public int callbackOrder => throw new System.NotImplementedException();

    public void OnPostprocessBuild(BuildReport report)
    {
        string buildPath = report.summary.outputPath;
        BuildSizeAnalyzerLogic.StoreBuildPath(buildPath);

        UnityEngine.Debug.Log($"Build Path Trakced : {buildPath}");
    }
}
