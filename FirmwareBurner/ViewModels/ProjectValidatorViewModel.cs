using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Validation;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels
{
    public class ProjectValidatorViewModel : ViewModelBase
    {
        private readonly ProjectViewModel _project;
        private readonly IProjectValidator _validator;

        private Lazy<IList<String>> _validationErrors;

        public ProjectValidatorViewModel(ProjectViewModel Project, IProjectValidator Validator)
        {
            _project = Project;
            _validator = Validator;
            ProjectViewModel project = Project;
            _validationErrors = new Lazy<IList<string>>(ErrorsLoader);
            project.ProjectChanged += OnProjectChanged;
        }

        public bool IsValid
        {
            get { return !_validationErrors.Value.Any(); }
        }

        public IList<String> ValidationErrors
        {
            get { return _validationErrors.Value; }
        }

        private void OnProjectChanged(object Sender, EventArgs Args)
        {
            _validationErrors = new Lazy<IList<string>>(ErrorsLoader);
            RaisePropertyChanged(string.Empty);
        }

        private IList<string> ErrorsLoader() { return _validator.GetValidationErrors(_project).ToList(); }
    }

    public interface IProjectValidatorViewModelProvider
    {
        ProjectValidatorViewModel GetViewModel(ProjectViewModel Project);
    }

    internal class ProjectValidatorViewModelProvider : IProjectValidatorViewModelProvider
    {
        private readonly IProjectValidator _validator;

        public ProjectValidatorViewModelProvider(IProjectValidator Validator) { _validator = Validator; }

        public ProjectValidatorViewModel GetViewModel(ProjectViewModel Project)
        {
            return new ProjectValidatorViewModel(Project, _validator);
        }
    }
}
