using ConferenceTracker.Controllers;
using ConferenceTracker.Entities;
using ConferenceTracker.Repositories;
using ConferenceTrackerTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace ConferenceTrackerTests
{
    public class SpeakerTests
    {
        Type speakerClass = ReflectionHelpers.GetUserType("ConferenceTracker.Entities.Speaker");
        Type speakerControllerClass = ReflectionHelpers.GetUserType("ConferenceTracker.Controllers.SpeakersController");
        ISpeakerRepository testRepository = new TestSpeakerRepository();

        [Fact(DisplayName = "Add Required Attributes to Speaker @add-required-attributes-to-speaker")]
        public void AddRequiredAttributesToSpeaker()
        {
            Assert.True(speakerClass != null, "`ConferenceTracker.Entities.Speaker` was not found. Did you accidentally move, delete, or rename it?");
            var idProperty = speakerClass.GetProperty("Id");
            Assert.True(idProperty != null, @"`Entities\Speaker` doesn't contain a `Id` property. Did you accidentally move, delete, or rename it?");
            Assert.True(idProperty.GetCustomAttributes(typeof(RequiredAttribute), false)?.FirstOrDefault() != null, @"`Entities\Speaker`'s `Id` property does not have the `Required` attribute.");
            var firstNameProperty = speakerClass.GetProperty("FirstName");
            Assert.True(firstNameProperty != null, @"`Entities\Speaker` doesn't contain a `FirstName` property. Did you accidentally move, delete, or rename it?");
            Assert.True(firstNameProperty.GetCustomAttributes(typeof(RequiredAttribute), false)?.FirstOrDefault() != null, @"`Entities\Speaker`'s `FirstName` property does not have the `Required` attribute.");
            var lastNameProperty = speakerClass.GetProperty("LastName");
            Assert.True(lastNameProperty != null, @"`Entities\Speaker` doesn't contain a `LastName` property. Did you accidentally move, delete, or rename it?");
            Assert.True(lastNameProperty.GetCustomAttributes(typeof(RequiredAttribute), false)?.FirstOrDefault() != null, @"`Entities\Speaker`'s `LastName` property does not have the `Required` attribute.");
            var descriptionProperty = speakerClass.GetProperty("Description");
            Assert.True(descriptionProperty != null, @"`Entities\Speaker` doesn't contain a `Description` property. Did you accidentally move, delete, or rename it?");
            Assert.True(descriptionProperty.GetCustomAttributes(typeof(RequiredAttribute), false)?.FirstOrDefault() != null, @"`Entities\Speaker`'s `Description` property does not have the `Required` attribute.");
        }

        [Fact(DisplayName = "Add DataType Attributes to Speaker @add-datatype-attributes-to-speaker")]
        public void AddDataTypeAttributesToSpeaker()
        {
            var firstNameProperty = speakerClass.GetProperty("FirstName");
            Assert.True(firstNameProperty != null, @"`Entities\Speaker` doesn't contain a `FirstName` property. Did you accidentally move, delete, or rename it?");
            var firstNamePropertyDataTypeAttribute = (DataTypeAttribute)firstNameProperty.GetCustomAttributes(typeof(DataTypeAttribute), false)?.FirstOrDefault();
            Assert.True(firstNamePropertyDataTypeAttribute != null && firstNamePropertyDataTypeAttribute.DataType == DataType.Text, @"`Entities\Speaker`'s `FirstName` property does not have the `DateType` attribute with an argument of `DataType.Text`.");

            var lastNameProperty = speakerClass.GetProperty("LastName");
            Assert.True(lastNameProperty != null, @"`Entities\Speaker` doesn't contain a `LastName` property. Did you accidentally move, delete, or rename it?");
            var lastNamePropertyDataTypeAttribute = (DataTypeAttribute)lastNameProperty.GetCustomAttributes(typeof(DataTypeAttribute), false)?.FirstOrDefault();
            Assert.True(lastNamePropertyDataTypeAttribute != null && lastNamePropertyDataTypeAttribute.DataType == DataType.Text, @"`Entities\Speaker`'s `LastName` property does not have the `DataType` attribute with an argument of `DataType.Text`.");

            var descriptionProperty = speakerClass.GetProperty("Description");
            Assert.True(descriptionProperty != null, @"`Entities\Speaker` doesn't contain a `Description` property. Did you accidentally move, delete, or rename it?");
            var descriptionPropertyDataTypeAttribute = (DataTypeAttribute)descriptionProperty.GetCustomAttributes(typeof(DataTypeAttribute), false)?.FirstOrDefault();
            Assert.True(descriptionPropertyDataTypeAttribute != null && descriptionPropertyDataTypeAttribute.DataType == DataType.MultilineText, @"`Entities\Speaker`'s `Description` property does not have the `DataType` attribute with an argument of `DataType.MultilineText`.");

            var emailAddressProperty = speakerClass.GetProperty("EmailAddress");
            Assert.True(emailAddressProperty != null, @"`Entities\Speaker` doesn't contain a `EmailAddress` property. Did you accidentally move, delete, or rename it?");
            var emailAddressPropertyDataTypeAttribute = (DataTypeAttribute)emailAddressProperty.GetCustomAttributes(typeof(DataTypeAttribute), false)?.FirstOrDefault();
            Assert.True(emailAddressPropertyDataTypeAttribute != null && emailAddressPropertyDataTypeAttribute.DataType == DataType.EmailAddress, @"`Entities\Speaker`'s `EmailAddress` property does not have the `DataType` attribute with an argument of `DataType.EmailAddress`.");

            var phoneNumberProperty = speakerClass.GetProperty("PhoneNumber");
            Assert.True(phoneNumberProperty != null, @"`Entities\Speaker` doesn't contain a `PhoneNumber` property. Did you accidentally move, delete, or rename it?");
            var phoneNumberPropertyDataTypeAttribute = (DataTypeAttribute)phoneNumberProperty.GetCustomAttributes(typeof(DataTypeAttribute), false)?.FirstOrDefault();
            Assert.True(phoneNumberPropertyDataTypeAttribute != null && phoneNumberPropertyDataTypeAttribute.DataType == DataType.PhoneNumber, @"`Entities\Speaker`'s `PhoneNumber` property does not have the `DataType` attribute with an argument of `DataType.PhoneNumber`.");
        }

        [Fact(DisplayName = "Add StringLength Attributes to Speaker @add-stringlength-attributes-to-speaker")]
        public void AddStringLengthAttributesToSpeaker()
        {
            var firstNameProperty = speakerClass.GetProperty("FirstName");
            Assert.True(firstNameProperty != null, @"`Entities\Speaker` doesn't contain a `FirstName` property. Did you accidentally move, delete, or rename it?");
            var firstNamePropertyStringLengthAttribute = (StringLengthAttribute)firstNameProperty.GetCustomAttributes(typeof(StringLengthAttribute), false)?.FirstOrDefault();
            Assert.True(firstNamePropertyStringLengthAttribute != null && firstNamePropertyStringLengthAttribute.MinimumLength == 2 && firstNamePropertyStringLengthAttribute.MaximumLength == 100, @"`Entities\Speaker`'s `FirstName` property does not have the `StringLength` attribute with a `MaximumLength` of `100` and a `MinimumLength` of `2`."); 
            
            var lastNameProperty = speakerClass.GetProperty("LastName");
            Assert.True(lastNameProperty != null, @"`Entities\Speaker` doesn't contain a `LastName` property. Did you accidentally move, delete, or rename it?");
            var lastNamePropertyStringLengthAttribute = (StringLengthAttribute)lastNameProperty.GetCustomAttributes(typeof(StringLengthAttribute), false)?.FirstOrDefault();
            Assert.True(lastNamePropertyStringLengthAttribute != null && lastNamePropertyStringLengthAttribute.MinimumLength == 2 && lastNamePropertyStringLengthAttribute.MaximumLength == 100, @"`Entities\Speaker`'s `LastName` property does not have the `StringLength` attribute with a `MaximumLength` of `100` and a `MinimumLength` of `2`.");

            var descriptionProperty = speakerClass.GetProperty("Description");
            Assert.True(descriptionProperty != null, @"`Entities\Speaker` doesn't contain a `Description` property. Did you accidentally move, delete, or rename it?");
            var descriptionPropertyStringLengthAttribute = (StringLengthAttribute)descriptionProperty.GetCustomAttributes(typeof(StringLengthAttribute), false)?.FirstOrDefault();
            Assert.True(descriptionPropertyStringLengthAttribute != null && descriptionPropertyStringLengthAttribute.MinimumLength == 10 && descriptionPropertyStringLengthAttribute.MaximumLength == 500, @"`Entities\Speaker`'s `Description` property does not have the `StringLength` attribute with a `MaximumLength` of `500` and a `MinimumLength` of `10`.");
        }

        [Fact(DisplayName = "Add IValidatableObject to Speaker @add-ivalidatableobject-to-speaker")]
        public void AddIValidatableObjectToSpeaker()
        {
            Assert.True(speakerClass.GetInterface("IValidatableObject") != null, "`Speaker` does not inherrit the `IValidatableObject` interface.");
            var validateMethod = speakerClass.GetMethod("Validate");
            Assert.True(validateMethod != null, "`Speaker` does not contain a `public` `Validate` method with a return type of `IEnumerable<ValidationResult>`.");
            Assert.True(validateMethod.ReturnType == typeof(IEnumerable<ValidationResult>), "`Speaker` does not contain a `public` `Validate` method with a return type of `IEnumerable<ValidationResult>`.");
            Assert.True(validateMethod.IsPublic, "`Speaker` does not contain a `public` `Validate` method with a return type of `IEnumerable<ValidationResult>`.");
            Assert.True(validateMethod.GetParameters().FirstOrDefault().ParameterType == typeof(ValidationContext), "`Speaker`'s `Validate` method doesn't have a parameter of type `ValidationContext` named `validationContext`.");

            // Actually test the method results
            var speaker = new Speaker();
            speaker.Id = 1;
            speaker.FirstName = "Johnny";
            speaker.LastName = "Tests";
            speaker.Description = "This is a test user, if you find it in the database someone made a huge mistake.";
            speaker.EmailAddress = "Invalid@TechnologyLiveConference.com";
            speaker.PhoneNumber = "555-555-5555";

            var validationContext = new ValidationContext(speaker);
            var results = (IEnumerable<ValidationResult>)validateMethod.Invoke(speaker, new object[] { validationContext });
            Assert.True(results.Any(r => r.ErrorMessage.Contains("staff")), @"`Speaker`'s `Validate` method did not add a new `ValidationResult` with the message ""Technology Live Conference staff should not use their conference email addresses."" when the `EmailAddress` property contained `""TechnologyLiveConference.com""` (this shouldn't be case sensative)");

            speaker.EmailAddress = "Valid@Okay.com";
            validationContext = new ValidationContext(speaker);
            results = (IEnumerable<ValidationResult>)validateMethod.Invoke(speaker, new object[] { validationContext });
            Assert.True(!results.Any(r => r.ErrorMessage.Contains("staff")), @"`Speaker`'s `Validate` added a new `ValidationResult` even when the `EmailAddress` property did not contain `""TechnologyLiveConference.com""`");
        }

        [Fact(DisplayName = "Add Validation to Create View @add-validation-to-create-view")]
        public void AddValidationToCreateView()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "ConferenceTracker" + Path.DirectorySeparatorChar + "Views" + Path.DirectorySeparatorChar + "Speakers" + Path.DirectorySeparatorChar + "Create.cshtml";
            Assert.True(File.Exists(filePath), @"`ConferenceTracker\Views\Speakers\Create.cshtml` file does not exist. Did you move, delete, or rename it.");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }
            var pattern = @"@section\s*?Scripts\s*?{\s*?@\s*?{\s*?await\s*?Html.RenderPartialAsync\s*?[(]\s*?""_ValidationScriptsPartial""\s*?[)]\s*?;\s*?}\s*?}";
            var rgx = new Regex(pattern,RegexOptions.IgnoreCase);
            Assert.True(rgx.IsMatch(file), @"`ConferenceTracker\Views\Speakers\Create.cshtml` view doesn't contain a `Scripts` section containing `@{ await Html.RenderPartialAsync(""_ValidationScriptsPartial""); }`");
        }

        [Fact(DisplayName = "Add Validation Summary to Create View @add-validation-summary-to-create-view")]
        public void AddValidationSummaryToCreateView()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "ConferenceTracker" + Path.DirectorySeparatorChar + "Views" + Path.DirectorySeparatorChar + "Speakers" + Path.DirectorySeparatorChar + "Create.cshtml";
            Assert.True(File.Exists(filePath), @"`ConferenceTracker\Views\Speakers\Create.cshtml` file does not exist. Did you move, delete, or rename it.");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }
            var pattern = @"<\s*?div\s*?(asp-validation-summary\s*?=\s*?""ModelOnly""\s*?class\s*?=\s*?""text-danger""|class\s*?=\s*?""text-danger""\s*?asp-validation-summary\s*?=\s*?""ModelOnly"")\s*?(>\s*?</\s*?div\s*?>|\>)";
            var rgx = new Regex(pattern,RegexOptions.IgnoreCase);
            Assert.True(rgx.IsMatch(file), @"`ConferenceTracker\Views\Speakers\Create.cshtml` view doesn't contain a `div` with the tag `asp-validation-summary` with a value of `""ModelOnly""` in the `Create` form.");
            
            //FirstName
            pattern = @"<\s*?span\s*?(asp-validation-for\s*?=\s*?""FirstName""\s*?class\s*?=\s*?""text-danger""|class\s*?=\s*?""text-danger""\s*?asp-validation-for\s*?=\s*?""FirstName"")\s*?(>\s*?</\s*?span\s*?>|/>)";
            rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            Assert.True(rgx.IsMatch(file), @"`ConferenceTracker\Views\Speakers\Create.cshtml` view's `FirstName` input isn't followed by a `span` tag with attributes of `asp-validation-for` with a value of `""FirstName"" and a `class` attribute with a value of `""FirstName""`");
            
            //LastName
            pattern = @"<\s*?span\s*?(asp-validation-for\s*?=\s*?""LastName""\s*?class\s*?=\s*?""text-danger""|class\s*?=\s*?""text-danger""\s*?asp-validation-for\s*?=\s*?""LastName"")\s*?(>\s*?</\s*?span\s*?>|/>)";
            rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            Assert.True(rgx.IsMatch(file), @"`ConferenceTracker\Views\Speakers\Create.cshtml` view's `LastName` input isn't followed by a `span` tag with attributes of `asp-validation-for` with a value of `""LastName"" and a `class` attribute with a value of `""LastName""`");

            //Description
            pattern = @"<\s*?span\s*?(asp-validation-for\s*?=\s*?""Description""\s*?class\s*?=\s*?""text-danger""|class\s*?=\s*?""text-danger""\s*?asp-validation-for\s*?=\s*?""Description"")\s*?(>\s*?</\s*?span\s*?>|/>)";
            rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            Assert.True(rgx.IsMatch(file), @"`ConferenceTracker\Views\Speakers\Create.cshtml` view's `Description` input isn't followed by a `span` tag with attributes of `asp-validation-for` with a value of `""Description"" and a `class` attribute with a value of `""Description""`");

            //EmailAddress
            pattern = @"<\s*?span\s*?(asp-validation-for\s*?=\s*?""EmailAddress""\s*?class\s*?=\s*?""text-danger""|class\s*?=\s*?""text-danger""\s*?asp-validation-for\s*?=\s*?""EmailAddress"")\s*?(>\s*?</\s*?span\s*?>|/>)";
            rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            Assert.True(rgx.IsMatch(file), @"`ConferenceTracker\Views\Speakers\Create.cshtml` view's `EmailAddress` input isn't followed by a `span` tag with attributes of `asp-validation-for` with a value of `""EmailAddress"" and a `class` attribute with a value of `""EmailAddress""`");

            //PhoneNumber
            pattern = @"<\s*?span\s*?(asp-validation-for\s*?=\s*?""PhoneNumber""\s*?class\s*?=\s*?""text-danger""|class\s*?=\s*?""text-danger""\s*?asp-validation-for\s*?=\s*?""PhoneNumber"")\s*?(>\s*?</\s*?span\s*?>|/>)";
            rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            Assert.True(rgx.IsMatch(file), @"`ConferenceTracker\Views\Speakers\Create.cshtml` view's `PhoneNumber` input isn't followed by a `span` tag with attributes of `asp-validation-for` with a value of `""PhoneNumber"" and a `class` attribute with a value of `""PhoneNumber""`");
        }

        [Fact(DisplayName = "Add ModelState Check To Create Action @add-modelstate-check-to-create-action")]
        public void AddModelStateCheckToCreateAction()
        {
            var controller = new SpeakersController(testRepository);
            var speaker = new Speaker();
            var results = controller.Create(speaker);
            Assert.True(results.GetType() == typeof(RedirectToActionResult), "`SpeakersController`'s `HttpPost` `Create` action didn't return a `RedirectToAction` when the `ModelState` was valid.");
            controller.ModelState.AddModelError("FirstName", "The FirstName field is required.");
            results = controller.Create(speaker);
            Assert.True(results.GetType() == typeof(ViewResult), "`SpeakersController`'s `HttpPost` `Create` action didn't return a `View` when the `ModelState` was not valid.");
        }

        [Fact(DisplayName = "Add Antiforgery and Binding To Create Action @add-antiforgery-and-binding-to-create-action")]
        public void AddAntiforgeryAndBindingToCreateAction()
        {
            var createMethod = speakerControllerClass.GetMethod("Create", new Type[] { typeof(Speaker) });
            Assert.True(createMethod != null, @"`SpeakersController` does not appear to have a `Create` action an `HttpPost` attribute and accepts a parameter of `Speaker`. Did you accidentally move, delete, or rename it?");

            Assert.True(createMethod.GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), false)?.FirstOrDefault() != null, "`SpeakersController`'s `HttpPost` `Create` action doesn't have the `ValidateAntiForgeryToken` attribute.");
            var parameters = createMethod.GetParameters()?.FirstOrDefault();
            var bindAttribute = parameters.GetCustomAttributes(typeof(BindAttribute), false)?.FirstOrDefault() as BindAttribute;
            Assert.True(bindAttribute != null, "`SpeakersController`'s `HttpPost` `Create` action's `Speaker` parameter does not have the `Bind` attribute making it so only the `Id`,`FirstName`,`LastName`,`Description`,`EmailAddress`, and `PhoneNumber` bind.");
            Assert.True(!bindAttribute.Include.Any(i => i.Contains("IsStaff")), "`SpeakersController`'s `HttpPost` `Create` action's `Speaker` parameter's `Bind` attribute should NOT include `IsStaff`.");
            Assert.True(bindAttribute.Include.Count() == 6, "`SpeakersController`'s `HttpPost` `Create` action's `Speaker` parameter's `Bind` attribute making it so only the `Id`,`FirstName`,`LastName`,`Description`,`EmailAddress`, and `PhoneNumber` bind.");
        }
    }
}
