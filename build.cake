#tool "nuget:?package=NUnit.ConsoleRunner"
#tool "nuget:?package=OpenCover"
#tool "nuget:?package=ReportGenerator"
#tool "nuget:?package=OctopusTools"

#r "tools/MyCustomExtensions/CakeDemo.MyCustomExtensions.dll"

var target = Argument("target", "Default");

Task("Restore")
    .Does(() => {
        var solutionFilePath = "./CakeDemo.sln";

        NuGetRestore(solutionFilePath);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
        var solutionFilePath = "./CakeDemo.sln";

        MSBuild(solutionFilePath, new MSBuildSettings { ArgumentCustomization = args => args.Append("/consoleloggerparameters:ForceConsoleColor") }
            .SetConfiguration("Release")
            .WithTarget("Rebuild"));
    });

Task("Unit-Tests")
    .Does(() => {
        var testAssemblies = GetFiles($"./*UnitTests/bin/Release/*UnitTests.dll");
        var settings = new NUnit3Settings { Results = new[] { new NUnit3Result { FileName = "UnitTestsResult.xml" } }, Labels = NUnit3Labels.Off };

        NUnit3(testAssemblies, settings);
    });


Task("Migrate-Database")
    .Does(() => {
        MigrateDatabases();
    });

Task("Octopus-Package")
    .Does(() => {
        var projects = GetProjectsToPack();

        foreach (var project in projects) {
            var packageSettings = new OctopusPackSettings {
                Version = project.Version,
                Description = project.Description,
                BasePath = project.FilesSource,
                Author = project.Author,
                OutFolder = project.OutputDirectory,
                Overwrite = project.Overwrite
            };

            OctoPack(project.Id, packageSettings);
        }
    });

Task("Deploy")
    .Does(() => {
        Information("Deploying first application...");
        Information("Deploying second application...");
        Information("Deploying third application...");
        Information("Deploying fourth application...");
        Information("Deploying fifth application...");
        Information("...");
        Information("Success!!!");
    });

Task("Demo-Pipeline")
    .IsDependentOn("Build")
    .IsDependentOn("Unit-Tests")
    .IsDependentOn("Octopus-Package")
    .IsDependentOn("Migrate-Database")
    .IsDependentOn("Deploy")
    .Does(() => {
        Information("Success! All tasks were executed without any problems.");
    });

Task("Custom-Methods")
    .Does(() => {
        var x = 4;

        Information("Input number is: " + x);
        Information("Using MultipluByFour method gives: " + MultiplyByFour(x));
    });

Task("Custom-Properties")
    .Does(() => {
        Information("Fourty six property: " + FourtySix);
        Information("Version property: " + CurrentVersion);
    });

Task("Custom-Extensions-Demo")
    .IsDependentOn("Custom-Methods")
    .IsDependentOn("Custom-Properties")
    .Does(() => {
        Information("Custom extensions were executed.");
    });

Task("Default")
    .Does(() => {
        Information("This task will run if target is not set.");
    });

RunTarget(target);