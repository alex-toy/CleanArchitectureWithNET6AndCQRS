using Xunit;
using NetArchTest.Rules;
using System.Reflection;

namespace ArchitectureTests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "Domain";
        private const string ApplicationNamespace = "Application";
        private const string InfrastructureNamespace = "Infrastructure";
        private const string PresentationNamespace = "Presentation";
        private const string WebNamespace = "Web";

        [Fact]
        public void Domain_should_not_have_dependencies_on_other_projects()
        {
            Assembly domainAssembly = typeof(Domain.AssemblyReference).Assembly;

            string[] otherProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace,
            };

            TestResult result = Types.InAssembly(domainAssembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

            Assert.True(result.IsSuccessful);
            //result.IsSuccessful
        }

        [Fact]
        public void Application_should_not_have_dependencies_on_other_projects()
        {
            Assembly assembly = typeof(Application.AssemblyReference).Assembly;

            string[] otherProjects = new[]
            {
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace,
            };

            TestResult result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Infrastructure_should_not_have_dependencies_on_other_projects()
        {
            Assembly assembly = typeof(Infrastructure.AssemblyReference).Assembly;

            string[] otherProjects = new[]
            {
                PresentationNamespace,
                WebNamespace,
            };

            TestResult result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Presentation_should_not_have_dependencies_on_other_projects()
        {
            Assembly assembly = typeof(Presentation.AssemblyReference).Assembly;

            string[] otherProjects = new[]
            {
                InfrastructureNamespace,
                WebNamespace,
            };

            TestResult result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Handlers_should_have_dependency_on_Domain()
        {
            Assembly assembly = typeof(Application.AssemblyReference).Assembly;

            PredicateList handlers = Types.InAssembly(assembly).That().HaveNameEndingWith("Handler");
            TestResult result = handlers.Should().HaveDependencyOnAll(DomainNamespace).GetResult();

            Assert.True(result.IsSuccessful);
        }
    }
}