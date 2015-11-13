using System;
using System.Collections.Generic;
using System.Linq;
using FirmwarePacker.Enpacking;
using FirmwarePacker.Exceptions;
using FirmwarePacker.LaunchParameters;
using FirmwarePacker.Project;
using FirmwarePacker.Project.Serializers;

namespace FirmwarePacker
{
    public class SilentPacker
    {
        private readonly ILaunchParameters _launchParameters;
        private readonly IProjectSerializer _projectSerializer;
        private readonly IPackageSavingTool _savingTool;
        private readonly IVariablesProcessor _variablesProcessor;

        public SilentPacker(ILaunchParameters LaunchParameters, IPackageSavingTool SavingTool, IProjectSerializer ProjectSerializer,
                            IVariablesProcessor VariablesProcessor)
        {
            _launchParameters = LaunchParameters;
            _savingTool = SavingTool;
            _projectSerializer = ProjectSerializer;
            _variablesProcessor = VariablesProcessor;
        }

        public static IEnumerable<String> EnumerateMissingParemeters(ILaunchParameters Parameters)
        {
            if (!Parameters.VersionMajor.HasValue)
                yield return "Версия";
            if (!Parameters.VersionMinor.HasValue)
                yield return "Подверсия";
            if (!Parameters.ReleaseDate.HasValue)
                yield return "Дата релиза";
            if (Parameters.ProjectFileName == null)
                yield return "Путь к файлу проекта";
            if (Parameters.OutputFileName == null)
                yield return "Путь к выходному файлу";
        }

        private void VerifyParameters()
        {
            List<string> missingParameters = EnumerateMissingParemeters(_launchParameters).ToList();
            if (missingParameters.Any())
                throw new MisingRequiredParameters(missingParameters);
        }

        public void Run()
        {
            VerifyParameters();
            // ReSharper disable PossibleInvalidOperationException
            var version = new PackageVersion(_launchParameters.VersionMajor.Value,
                                             _launchParameters.VersionMinor.Value,
                                             _launchParameters.VersionLabel,
                                             _launchParameters.ReleaseDate.Value);
            // ReSharper restore PossibleInvalidOperationException
            PackageProject project = _projectSerializer.Load(_launchParameters.ProjectFileName);
            string fileName = _variablesProcessor.ReplaceVariables(_launchParameters.OutputFileName, project, version);
            _savingTool.SavePackage(project, version, fileName, _launchParameters.RootDirectory);
        }
    }
}
