#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
// Define directories.
var buildDir = Directory("./src/Example/bin") + Directory(configuration);


#load "utilities.cake"

Task("Clean")
    .Does(() =>
{
    //CleanDirectory(buildDir);
    Information("Default clean");
});


Task("Default")
    .IsDependentOn("Clean")
    .Does(() =>
{
});


//SkipTask("Clean");
OverrideTask("Clean", ()=>{
    Information("New clean");
});

var newTask = GetTask("Default");


RunTarget(target);
