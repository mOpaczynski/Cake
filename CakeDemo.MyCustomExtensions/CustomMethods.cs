using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using CakeDemo.MyCustomExtensions.Helpers;
using static System.FormattableString;

namespace CakeDemo.MyCustomExtensions
{
    public static class CustomMethods
    {
        [CakeMethodAlias]
        public static int MultiplyByFour(this ICakeContext context, int x)
        {
            return x * 4;
        }

        [CakeMethodAlias]
        public static void MigrateDatabases(this ICakeContext context)
        {
            var solutionRootDirectory = GetRootPath();
            var migrateExecFile = Directory.GetFiles(Invariant($"{solutionRootDirectory}"), "migrate.exe", SearchOption.AllDirectories).First();
            var dataAccessProjects = Directory.GetDirectories(solutionRootDirectory, "*.DataAccess", SearchOption.AllDirectories);

            foreach (var dataAccessProject in dataAccessProjects)
            {
                var configFilePath = Directory.GetFiles(dataAccessProject, "App.config", SearchOption.TopDirectoryOnly).First();
                var assembyFilePath = Directory.GetFiles(dataAccessProject, Invariant($"{GetLastSegment(dataAccessProject)}.dll"), SearchOption.AllDirectories).First(x => x.Contains("bin") && x.Contains("Release"));
                var migrateExecFileCopyPath = Invariant($"{Path.GetDirectoryName(assembyFilePath)}/migrate.exe");
                var migrateExecArguments = Invariant($"{GetLastSegment(dataAccessProject)}.dll Configuration /startupConfigurationFile:\"{configFilePath}\" /verbose");

                if (!File.Exists(migrateExecFileCopyPath))
                {
                    File.Copy(migrateExecFile, migrateExecFileCopyPath);
                }

                var runMigrator = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = migrateExecFileCopyPath,
                        Arguments = migrateExecArguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                };

                runMigrator.OutputDataReceived += (s, e) => context.Log.Information(e.Data);
                runMigrator.ErrorDataReceived += (s, e) => context.Log.Information(e.Data);

                runMigrator.Start();
                runMigrator.BeginOutputReadLine();
                runMigrator.BeginErrorReadLine();
                runMigrator.WaitForExit();

                if (runMigrator.ExitCode != 0)
                {
                    throw new Exception(Invariant($"Migrate.exe: Process returned an error (exit code {runMigrator.ExitCode})."));
                }
            }
        }

        [CakeMethodAlias]
        public static List<PackageSettings> GetProjectsToPack(this ICakeContext context)
        {
            var solutionRootDirectory = GetRootPath();
            var projectsToPack = Directory.GetDirectories(solutionRootDirectory, "*CakeDemo.*", SearchOption.TopDirectoryOnly);
            var outPutFolder = new DirectoryInfo(Invariant($"{solutionRootDirectory}/packed-for-deploy")).FullName;
            List<PackageSettings> packageSettings = new List<PackageSettings>();

            Directory.CreateDirectory(outPutFolder);

            foreach (var project in projectsToPack)
            {
                if (project.Contains("MyCustomExtensions") || project.Contains("Tests"))
                {
                    continue;
                }

                var projectSettings = new PackageSettings
                {
                    Id = GetLastSegment(project),
                    Version = "1.0.0.1",
                    Description = Invariant($"The {GetLastSegment(project)} deployment package, built on {DateTime.Now}."),
                    Author = "Strange Issues Department",
                    FilesSource = new DirectoryInfo(Invariant($"{project}/bin")).FullName,
                    OutputDirectory = outPutFolder,
                    Overwrite = true
                };

                packageSettings.Add(projectSettings);
            }

            return packageSettings;
        }

        private static string GetRootPath()
        {
            return new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.Parent?.FullName;
        }

        private static string GetLastSegment(string path)
        {
            return new DirectoryInfo(path).Name;
        }
    }
}
