using ConferenceTracker.Entities;
using ConferenceTrackerTests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace ConferenceTrackerTests
{
    public class PresentationControllerTests
    {
        static string userCodeFile = "Controllers" + Path.DirectorySeparatorChar + "PresentationsController.cs";
        static SyntaxNode ast = TestHelpers.GetUserAst(userCodeFile);

        [Fact(DisplayName = "Add ILogger Field To PresentationsController @add-ilogger-field-to-presentationscontroller")]
        public void AddILoggerFieldToPresentationController()
        {

            IEnumerable<FieldDeclarationSyntax> fields = TestHelpers.GetFields(ast);
            var allowedOriginsField = fields?.FirstOrDefault(v => v.ToString().Contains("_logger"));
            Assert.True(allowedOriginsField != null, @"`PresentationsController` does not appear to contain a `private` `readonly` `ILogger` field named `_logger`");
            Assert.True(allowedOriginsField.Modifiers.Any(SyntaxKind.PrivateKeyword), "`PresentationsController`'s `_logger` field should have an access modifier of `private`.");
            Assert.True(allowedOriginsField.Modifiers.Any(SyntaxKind.ReadOnlyKeyword), "`PresentationsController`'s `_logger` field should have use the `readonly` keyword.");
            Assert.True(allowedOriginsField.ToString().Contains("ILogger"), "`PresentationsController`'s `_logger` field should have a type of `ILogger`.");
        }

        [Fact(DisplayName = "Update PresentationController Constructor @update-presenationscontroller-constructor")]
        public void UpdatePresentationControllerConstructor()
        {
            ConstructorDeclarationSyntax constructor = TestHelpers.GetConstructors(ast).FirstOrDefault();
            // if we false negative on whitespace it'll be here, but I think that'll be a rare enough issue to not hurt performance running multiple passes or regex
            var methodArguments = constructor.ParameterList.Parameters.FirstOrDefault(p => p.Type.ToString().Contains("ILogger<PresentationsController>"));
            Assert.True(methodArguments != null, "`PresentationsController`'s constructor should accept a third parameter of type `ILogger<PresentationsController>` with the name `logger`.");
            var setLogger = constructor.DescendantNodes().OfType<AssignmentExpressionSyntax>().FirstOrDefault(a => a.ToString().Contains("_logger"));
            Assert.True(setLogger != null, "`PresentationsController`'s constructor should set `_logger` to the provided `logger` parameter.");
        }

        [Fact(DisplayName = "Add Logging To Edit Action @add-logging-to-edit-action")]
        public void AddLoggingToEditAction()
        {

            IEnumerable<MethodDeclarationSyntax> methods = TestHelpers.GetMethods(ast);
            var method = methods.FirstOrDefault(m => m.ToString().Contains("Edit") && !m.ParameterList.Parameters.Any(p => p.ToString().Contains("presentation")));
            var logChecking = method.DescendantNodes().OfType<InvocationExpressionSyntax>().FirstOrDefault(m => m.ToString().Contains("LogInformation") && m.ToString().Contains("Getting"));
            Assert.True(logChecking != null, @"`PresentationsController.Edit` does not appear to call `LogInformation` near the before checking if `presentation.id` is `null` with a message of `""Getting presentation id: "" + id + "" for edit.""`") ;
            var conditionals = TestHelpers.GetIfStatements(method);
            var isIdNullConditional = conditionals.FirstOrDefault(c => c.ToString().Contains("id"));
            var idNull = isIdNullConditional?.DescendantNodes().OfType<InvocationExpressionSyntax>()?.FirstOrDefault(m => m.ToString().Contains("LogError"));
            Assert.True(idNull != null, @"`PresentationsController.Edit` does not appear to call `LogError` with a message of `""Presentation id was null.""` when `id` is `null`");
            var idNullArguments = idNull.ArgumentList.Arguments.Any(a => a.ToString().Contains("was null"));
            Assert.True(idNull != null, @"`PresentationsController.Edit` does not appear to call `LogError` with a message of `""Presentation id was null.""` when `id` is `null`");
            var presentationNullConditional = conditionals.FirstOrDefault(c => c.ToString().Contains("presentation"));
            var presentationNull = presentationNullConditional.DescendantNodes().OfType<InvocationExpressionSyntax>()?.FirstOrDefault(m => m.ToString().Contains("LogWarning"));
            Assert.True(presentationNull != null, @"`PresentationsController.Edit` does not appear to call `LogWarning` with a message of `""Presentation id, "" + id + "", was not found.""`");
            var presentationNullArguments = presentationNullConditional.DescendantNodes().FirstOrDefault(a => a.ToString().Contains("not found"));
            Assert.True(presentationNullArguments != null, @"`PresentationsController.Edit` does not appear to call `LogWarning` with a message of `""Presentation id, "" + id + "", was not found.""`");
            var logSuccess = method.DescendantNodes().OfType<InvocationExpressionSyntax>().FirstOrDefault(m => m.ToString().Contains("LogInformation") && m.ToString().Contains("was found"));
            Assert.True(logSuccess != null, @"`PresentationsController.Edit` does not appear to call `LogInformation` with a message of `""Presentation id, "" + id + "", was found.Returning 'Edit view'""");
        }
    }
}
