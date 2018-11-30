#tool  "nuget:?package=GitVersion.CommandLine&Version=3.6.5"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var outputDir = Directory("./output");
#load "./src/utilities.cake"
 




Task("Clean").Does(() => {
    CleanDirectories(outputDir);
});

bool isTaskOverriden = false;
Task("OldTask").Does(() => {});
OverrideTask("OldTask", ()=> { isTaskOverriden = true; });
Task("Test")
    .IsDependentOn("OldTask")
    .Does((context) =>
{
    if (!isTaskOverriden) {
        throw new Exception("Test failed: task was not overriden");
    }
});

Task("Pack")
    .Does(() =>
{
    var gitVersion = GetGitVersion();
    var files = GetFiles("*.nuspec");
    foreach(var file in files)
    {
        NuGetPack(file, new NuGetPackSettings {
            OutputDirectory = outputDir,
            Version = gitVersion.NuGetVersionV2
        });
    }
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Test")
    .IsDependentOn("Pack")
    .Does((context) =>
{

});


RunTarget(target);



Cake.Common.Tools.GitVersion.GitVersion GetGitVersion(){
        using(var process = StartAndReturnProcess("./tools/GitVersion.CommandLine.3.6.5/tools/GitVersion.exe", new ProcessSettings{ Arguments = "/output buildserver" }))
        {
            process.WaitForExit();
            Information("Exit code: {0}", process.GetExitCode());
        }

        // capture version to json
        return GitVersion(new GitVersionSettings {
            UpdateAssemblyInfo = true
        });
}