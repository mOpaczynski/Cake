#tool "nuget:?package=NUnit.ConsoleRunner"
#tool "nuget:?package=OpenCover"
#tool "nuget:?package=ReportGenerator"

var target = Argument("target", "Default");

Task("Restore-Nuget-Packages")
    .Does(() => {
        var solutionFilePath = "./CakeDemo.sln";

        NuGetRestore(solutionFilePath);
    });

Task("Build-Net-Projects")
    .IsDependentOn("Restore-Nuget-Packages")
    .Does(() => {
        var solutionFilePath = GetFiles("./**/Sticker.sln").First();

        MSBuild(solutionFilePath, new MSBuildSettings { ArgumentCustomization = args => args.Append("/consoleloggerparameters:ForceConsoleColor") }
            .SetConfiguration(projectConfiguration)
            .WithTarget("Rebuild"));
    });

Task("Unit-Tests")
    .Does(() => {
        var testAssemblies = GetFiles($"./*UnitTests/bin/{projectConfiguration}/*UnitTests.dll");
        var settings = new NUnit3Settings { Results = new[] { new NUnit3Result { FileName = "UnitTestsResult.xml" } }, Labels = NUnit3Labels.Off };

        OpenCover(tool => 
            { tool.NUnit3(testAssemblies, settings); },
            new FilePath("./OpenCoverResult.xml"),
            new OpenCoverSettings { ReturnTargetCodeOffset = 0 });

        ReportGenerator(new FilePath("./OpenCoverResult.xml"), new DirectoryPath("./coverage/"));
    });


Task("Test-Migrations-And-Seed")
    .Does(() => {

    });

Task("Default")
    .Does(() => {
        Information("Cake was build and installed successfully.");
    });

RunTarget(target);