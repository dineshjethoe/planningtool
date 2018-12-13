using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Castle.MicroKernel.Registration;
using Entities;
using Repositories.Interfaces;
using Repositories.Services;

namespace WinApp
{
    public class Installers : IWindsorInstaller
    {

        public void Install(Castle.Windsor.IWindsorContainer container,
            Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IValidateAssignment>().ImplementedBy<ValidateAssignment>());
            container.Register(Component.For<ISystemInformationService>().ImplementedBy<SystemInformationService>());
            container.Register(Component.For<IService<Task>>().ImplementedBy<TaskService>());
            container.Register(Component.For<IService<Employee>>().ImplementedBy<EmployeeService>());
            container.Register(Component.For<IService<AssignedTask>>().ImplementedBy<AssignedTaskService>());

            container.Register(Component.For<MainForm>().LifestyleTransient());
        }

    }
}
