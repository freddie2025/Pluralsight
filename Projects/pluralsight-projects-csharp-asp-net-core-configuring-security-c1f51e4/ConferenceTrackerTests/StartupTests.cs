using ConferenceTrackerTests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConferenceTrackerTests
{
    /* DISCLAIMER : Feel free to explore how these tests work, however; DO NOT take these tests as an example of how to write tests, as we're testing
    user submitted code. We're effectively testing code that may or may not exist, this requires us to use reflection, regex, etc. These are NOT normal 
    or recommended practices (They're just what we've had to do to make this work for the abnormal use case that is education) */

    public class StartupTests
    {
        static string userCodeFile = "Startup.cs";
        static SyntaxNode ast = TestHelpers.GetUserAst(userCodeFile);
        IEnumerable<MethodDeclarationSyntax> methods = TestHelpers.GetMethods(ast);
        IEnumerable<FieldDeclarationSyntax> fields = TestHelpers.GetFields(ast);


        [Fact(DisplayName = "Add UseHttpsRedirection To Startup @add-usehttpsredirection-to-startup")]
        public void AddUseHttpsRedirectionToStartup()
        {
            var method = methods.Where(m => m.Identifier.Value.Equals("Configure")).FirstOrDefault();
            var methodCalls = method.Body.DescendantNodes().OfType<InvocationExpressionSyntax>();
            var useHttpsRedirectionCall = methodCalls.FirstOrDefault(m => m.ToString().Contains("app") && m.ToString().Contains("UseHttpsRedirection"));
            Assert.True(useHttpsRedirectionCall != null, "`Startup.Configure` doesn't contain a call to `UseHttpsRedirection` off of `app`.");
            var useStaticFilesCall = methodCalls.FirstOrDefault(m => m.ToString().Contains("app") && m.ToString().Contains("UseStaticFiles"));
            Assert.True(useStaticFilesCall != null, "`Startup.Configure` doesn't appear to contain a call to `UseStaticFiles`, did you accidentally delete it?");
            Assert.True(useHttpsRedirectionCall.FullSpan.End < useStaticFilesCall.FullSpan.Start, "`Startup.Configure`'s `UseHttpsRedirection` needs to be called before `UseStaticFiles`.");
        }

        [Fact(DisplayName = "Add UseHsts To Startup @add-usehsts-to-startup")]
        public void AddUseHstsToStartup()
        {
            var method = methods.Where(m => m.Identifier.Value.Equals("Configure")).FirstOrDefault();
            var conditionals = TestHelpers.GetIfStatements(method);
            var isDevConditional = conditionals.FirstOrDefault(c => c.ToString().Contains("IsDevelopment"));
            Assert.True(isDevConditional != null, "`Startup.Configure` does not appear to contain a condition that checks if `env.IsDevolpment()` is `true` or `false`.");
            var isDevIfChildNodes = isDevConditional.Condition.ChildNodes();

            // if true, check for else
            // if(env.IsDevelopment())
            if ((isDevConditional.Condition.Kind() == SyntaxKind.InvocationExpression)
                // if(env.IsDevelopment() == true)
                || (isDevConditional.Condition.Kind() == SyntaxKind.EqualsExpression && isDevIfChildNodes.Any(cn => cn.ToString() == "true"))
                // if(env.IsDevelopment() != false)
                || (isDevConditional.Condition.Kind() == SyntaxKind.NotEqualsExpression && isDevIfChildNodes.Any(cn => cn.ToString() == "false")))
            {
                var methodCalls = isDevConditional.Else?.DescendantNodes().OfType<InvocationExpressionSyntax>();
                var useHstsCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("app") && m.ToString().Contains("UseHsts"));
                Assert.True(useHstsCall != null, "`Startup.Configure` does not appear to call `UseHsts` when `env.IsDevelopment()` is `false`.");
                var useStaticFilesCall = method?.Body.DescendantNodes().OfType<InvocationExpressionSyntax>().FirstOrDefault(m => m.ToString().Contains("app") && m.ToString().Contains("UseStaticFiles"));
                Assert.True(useStaticFilesCall != null, "`Startup.Configure` doesn't appear to contain a call to `UseStaticFiles`, did you accidentally delete it?");
                Assert.True(useHstsCall.Span.End < useStaticFilesCall.Span.Start, "`Startup.Configure`'s `UseHsts` call should be called near the beginning of the `Configure` method.");
            }

            // if false proceed
            // if(!env.IsDevelopment())
            else if ((isDevConditional.Condition.Kind() == SyntaxKind.LogicalNotExpression)
                // if(env.IsDevelopment() == false)
                || (isDevConditional.Condition.Kind() == SyntaxKind.EqualsExpression && isDevIfChildNodes.Any(cn => cn.ToString() == "false")))
            {
                var methodCalls = isDevConditional?.DescendantNodes().OfType<InvocationExpressionSyntax>();
                var useHstsCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("app") && m.ToString().Contains("UseHsts"));
                Assert.True(useHstsCall != null, "`Startup.Configure` does not appear to call `UseHsts` when `env.IsDevelopment()` is `false`.");
                var useStaticFilesCall = method?.Body.DescendantNodes().OfType<InvocationExpressionSyntax>().FirstOrDefault(m => m.ToString().Contains("app") && m.ToString().Contains("UseStaticFiles"));
                Assert.True(useStaticFilesCall != null, "`Startup.Configure` doesn't appear to contain a call to `UseStaticFiles`, did you accidentally delete it?");
                Assert.True(useHstsCall.Span.End < useStaticFilesCall.Span.Start, "`Startup.Configure`'s `UseHsts` call should be called near the beginning of the `Configure` method.");
            }

            // if neither, fail test
            else
                Assert.True(1 == 2, "`Startup.Configure` does not appear to contain a condition to check if `env.IsDevelopment()` is `true` or `false`. (it's also possible you do have an conditional that checks if `env.IsDevelopment()` is `true` but is in an unexpected format, please use `env.IsDevelopment()` directly in your condition and not a variable.)");
        }

        [Fact(DisplayName = "Add Exception Handling To Startup @add-exception-handling-to-startup")]
        public void AddExceptionHandlingToStartup()
        {
            var method = methods.Where(m => m.Identifier.Value.Equals("Configure")).FirstOrDefault();
            var conditionals = TestHelpers.GetIfStatements(method);
            var isDevConditional = conditionals.FirstOrDefault(c => c.ToString().Contains("IsDevelopment"));
            Assert.True(isDevConditional != null, "`Startup.Configure` does not appear to contain a condition that checks if `env.IsDevolpment()` is `true` or `false`.");
            var isDevIfChildNodes = isDevConditional.Condition.ChildNodes();

            // if true, check for else
            // if(env.IsDevelopment())
            if ((isDevConditional.Condition.Kind() == SyntaxKind.InvocationExpression)
                // if(env.IsDevelopment() == true)
                || (isDevConditional.Condition.Kind() == SyntaxKind.EqualsExpression && isDevIfChildNodes.Any(cn => cn.ToString() == "true"))
                // if(env.IsDevelopment() != false)
                || (isDevConditional.Condition.Kind() == SyntaxKind.NotEqualsExpression && isDevIfChildNodes.Any(cn => cn.ToString() == "false")))
            {
                var methodCalls = isDevConditional.Else?.DescendantNodes().OfType<InvocationExpressionSyntax>();
                var useExceptionHandlerCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseExceptionHandler"));
                Assert.True(useExceptionHandlerCall != null, "`Startup.Configure` doesn't appear to contain a call to `UseExceptionHandler` when `env.IsDevelopment()` is `false`.");
                var useExceptionHandlerArgument = useExceptionHandlerCall?.ArgumentList.Arguments.FirstOrDefault(a => a.ToString().Contains("/Home/Error"));
                Assert.True(useExceptionHandlerArgument != null, @"`Startup.Configure`'s `UseExceptionHandler` call doesn't appear to have an argument of `""/Home/Error""`");
                var useHstsCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("app") && m.ToString().Contains("UseHsts"));
                Assert.True(useExceptionHandlerCall.Span.End < useHstsCall.Span.Start, "`Startup.Configure`'s call to `UseExceptionHandler` should be before the call to `UseHsts`");
                var useDeveloperExceptionPageCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseDeveloperExceptionPage"));
                Assert.True(useDeveloperExceptionPageCall == null, "`Startup.Configure` should only make a call to `UseDeveloperExceptionPage` when `env.IsDevelopment()` is `true` not when it's `false`.");
                var useDatabaseErrorPageCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseDatabaseErrorPage"));
                Assert.True(useDatabaseErrorPageCall == null, "`Startup.Configure` should only make a call to `UseDatabaseErrorPage` when `env.IsDevelopment()` is `true` not when it's `false`.");

                methodCalls = isDevConditional.DescendantNodes().OfType<InvocationExpressionSyntax>();
                useDeveloperExceptionPageCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseDeveloperExceptionPage"));
                Assert.True(useDeveloperExceptionPageCall != null, "`Startup.Configure` does not appear to contain a call to `UseDeveloperExceptionPage` when `env.IsDevelopment()` is `true`.");
                useDatabaseErrorPageCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseDatabaseErrorPage"));
                Assert.True(useDatabaseErrorPageCall != null, "`Startup.Configure` does not appear to contain a call to `UseDatabaseErrorPage` when `env.IsDevelopment()` is `true`.");
            }

            // if false proceed
            // if(!env.IsDevelopment())
            else if ((isDevConditional.Condition.Kind() == SyntaxKind.LogicalNotExpression)
                // if(env.IsDevelopment() == false)
                || (isDevConditional.Condition.Kind() == SyntaxKind.EqualsExpression && isDevIfChildNodes.Any(cn => cn.ToString() == "false")))
            {
                var methodCalls = isDevConditional.Else?.DescendantNodes().OfType<InvocationExpressionSyntax>();
                var useExceptionHandlerCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseExceptionHandler"));
                Assert.True(useExceptionHandlerCall == null, "`Startup.Configure` should only call `UseExceptionHandler` when `env.IsDevelopment()` is `false`.");
                var useDeveloperExceptionPageCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseDeveloperExceptionPage"));
                Assert.True(useDeveloperExceptionPageCall != null, "`Startup.Configure` should call `UseDeveloperExceptionPage` when `env.IsDevelopment()` is `true`.");
                var useDatabaseErrorPageCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseDatabaseErrorPage"));
                Assert.True(useDatabaseErrorPageCall != null, "`Startup.Configure` should call `UseDatabaseErrorPage` when `env.IsDevelopment()` is `true`.");

                methodCalls = isDevConditional.DescendantNodes().OfType<InvocationExpressionSyntax>();
                useExceptionHandlerCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseExceptionHandler"));
                Assert.True(useExceptionHandlerCall != null, "`Startup.Configure` should only call to `UseExceptionHandler` when `env.IsDevelopment()` is `false`.");
                var useExceptionHandlerArgument = useExceptionHandlerCall?.ArgumentList.Arguments.FirstOrDefault(a => a.ToString().Contains("/Home/Error"));
                Assert.True(useExceptionHandlerArgument != null, @"`Startup.Configure`'s `UseExceptionHandler` call doesn't appear to have an argument of `""/Home/Error""`");
                var useHstsCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("app") && m.ToString().Contains("UseHsts"));
                Assert.True(useExceptionHandlerCall.Span.End < useHstsCall.Span.Start, "`Startup.Configure`'s call to `UseExceptionHandler` should be before the call to `UseHsts`");
            }

            // if neither, fail test
            else
                Assert.True(1 == 2, "`Startup.Configure` does not appear to contain a condition to check if `env.IsDevelopment()` is `true` or `false`. (it's also possible you do have an conditional that checks if `env.IsDevelopment()` is `true` but is in an unexpected format, please use `env.IsDevelopment()` directly in your condition and not a variable.)");
        }

        [Fact(DisplayName = "Add Private Readonly Field _allowedOrigins To Startup @add-private-readonly-field-_allowedOrigins-to-startup")]
        public void AddPrivateReadonlyFieldAllowedOriginsToStartup()
        {
            var allowedOriginsField = fields?.FirstOrDefault(v => v.ToString().Contains("_allowedOrigins"));
            Assert.True(allowedOriginsField != null, @"`Startup` does not appear to contain a `private` `readonly` `string` field named `_allowedOrigins` with a value of `""_allowedOrigins""`");
            Assert.True(allowedOriginsField.Modifiers.Any(SyntaxKind.PrivateKeyword), "`Startup`'s `_allowedOrigins` field should have an access modifier of `private`.");
            Assert.True(allowedOriginsField.Modifiers.Any(SyntaxKind.ReadOnlyKeyword), "`Startup`'s `_allowedOrigins` field should have use the `readonly` keyword.");
            Assert.True(allowedOriginsField.ToString().Contains("string"),"`Startup`'s `_allowedOrigins` field should have a type of `string`.");
            Assert.True(allowedOriginsField.ToString().Contains(@"""_allowedOrigins"""), @"`Startup`'s `_allowedOrigins` field should have a default value of `""_allowedOrigins""`");
        }

        [Fact(DisplayName = "Add Cors Support To ConfigureServices @add-cors-support-to-configureservices")]
        public void AddCorsSupportToConfigureServices()
        {
            var method = methods.Where(m => m.Identifier.Value.Equals("ConfigureServices")).FirstOrDefault();
            var methodCalls = method.Body.DescendantNodes().OfType<InvocationExpressionSyntax>();
            var addCorsCall = methodCalls.FirstOrDefault(m => m.ToString().Contains("services") && m.ToString().Contains("AddCors"));
            Assert.True(addCorsCall != null, "`Startup.ConfigureServices` does not appear to contain a call to `AddCors` off of `services`.");
            // I've chosen to not verify the specific argument was provided beyond making sure the actually included "WithOrigins".
            // (If it compiles and they have that it's close enough, trying to disect the argument bit by bit will likely result in tons of false failures.)
            var addCorsArguments = addCorsCall.ArgumentList.Arguments.FirstOrDefault(a => a.ToString().Contains("WithOrigins"));
            Assert.True(addCorsArguments != null, @"`Startup.ConfigureServices` includes a call to `AddCors` but didn't include an argument of `options => { options.AddPolicy(_allowedOrigins, builder => { builder.WithOrigins(""http://pluralsight.com""); }); }`");
        }

        [Fact(DisplayName = "Add Cors Support to Configure Method @add-cors-support-to-configure-method")]
        public void AddCorsSupportToConfigureMethod()
        {
            var method = methods.Where(m => m.Identifier.Value.Equals("Configure")).FirstOrDefault();
            var methodCalls = method.Body.DescendantNodes().OfType<InvocationExpressionSyntax>();
            var useCorsCall = methodCalls.FirstOrDefault(m => m.ToString().Contains("app") && m.ToString().Contains("UseCors"));
            Assert.True(useCorsCall != null, "`Startup.Configure` does not appear to contain a call to `UseCors` off of `app`.");
            var useCorsArguments = useCorsCall.ArgumentList.Arguments.FirstOrDefault(a => a.ToString().Contains("_allowedOrigins"));
            Assert.True(useCorsArguments != null, @"`Startup.Configure` includes a call to `UseCors` but didn't include an argument of `_allowedOrigins`");
            var useHttpsRedirectionCall = methodCalls.FirstOrDefault(m => m.ToString().Contains("app") && m.ToString().Contains("UseHttpsRedirection"));
            Assert.True(useHttpsRedirectionCall != null, "`Startup.Configure` doesn't contain a call to `UseHttpsRedirection` off of `app`.");
            Assert.True(useCorsArguments.Span.End < useHttpsRedirectionCall.Span.Start, "`Startup.Configure`'s call to `UseCors` should be before the call to `UseHttpsRedirection`.");
        }

        [Fact(DisplayName = "Add Logging to Startup @add-logging-to-startup")]
        public void AddLoggingToStartup()
        {
            var method = methods.Where(m => m.Identifier.Value.Equals("Configure")).FirstOrDefault();
            // if we false negative on whitespace it'll be here, but I think that'll be a rare enough issue to not hurt performance running multiple passes or regex
            var methodArguments = method?.ParameterList.Parameters.FirstOrDefault(p => p.Type.ToString().Contains("ILogger<Startup>"));
            Assert.True(methodArguments != null, "`Startup.Configure` should accept a third parameter of type `ILogger<Startup>` with the name `logger`.");

            var conditionals = TestHelpers.GetIfStatements(method);
            var isDevConditional = conditionals.FirstOrDefault(c => c.ToString().Contains("IsDevelopment"));
            Assert.True(isDevConditional != null, "`Startup.Configure` does not appear to contain a condition that checks if `env.IsDevolpment()` is `true` or `false`.");
            var isDevIfChildNodes = isDevConditional.Condition.ChildNodes();

            // if true, check for else
            // if(env.IsDevelopment())
            if ((isDevConditional.Condition.Kind() == SyntaxKind.InvocationExpression)
                // if(env.IsDevelopment() == true)
                || (isDevConditional.Condition.Kind() == SyntaxKind.EqualsExpression && isDevIfChildNodes.Any(cn => cn.ToString() == "true"))
                // if(env.IsDevelopment() != false)
                || (isDevConditional.Condition.Kind() == SyntaxKind.NotEqualsExpression && isDevIfChildNodes.Any(cn => cn.ToString() == "false")))
            {
                var methodCalls = isDevConditional.DescendantNodes().OfType<InvocationExpressionSyntax>();
                var logInformationCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("LogInformation"));
                Assert.True(logInformationCall != null, "`Startup.Configure` does not appear to contain a call to `LogInformation` when `env.IsDevelopment()` is `true`.");
                var logInformationArguements = logInformationCall.ArgumentList.Arguments.FirstOrDefault(a => a.ToString().Contains("Environment is in development"));
                Assert.True(logInformationArguements != null, @"`Startup.Configure` contains a call to `LogInformation` but it doesn't have an argument of `""Environment is in development""`");
                var useExceptionHandlerCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseExceptionHandler"));
                Assert.True(useExceptionHandlerCall != null, "`Startup.Configure` doesn't appear to contain a call to `UseExceptionHandler` when `env.IsDevelopment()` is `false`.");
                Assert.True(logInformationCall.Span.End < useExceptionHandlerCall.Span.Start, "`Startup.Configure`'s call to `LogInformation` should be made before the call to `UserExceptionHandler`.");
            }

            // if false proceed
            // if(!env.IsDevelopment())
            else if ((isDevConditional.Condition.Kind() == SyntaxKind.LogicalNotExpression)
                // if(env.IsDevelopment() == false)
                || (isDevConditional.Condition.Kind() == SyntaxKind.EqualsExpression && isDevIfChildNodes.Any(cn => cn.ToString() == "false")))
            {
                var methodCalls = isDevConditional.Else?.DescendantNodes().OfType<InvocationExpressionSyntax>();
                var logInformationCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("LogInformation"));
                Assert.True(logInformationCall != null, "`Startup.Configure` does not appear to contain a call to `LogInformation` when `env.IsDevelopment()` is `true`.");
                var logInformationArguements = logInformationCall.ArgumentList.Arguments.FirstOrDefault(a => a.ToString().Contains("Environment is in development"));
                Assert.True(logInformationArguements != null, @"`Startup.Configure` contains a call to `LogInformation` but it doesn't have an argument of `""Environment is in development""`");
                var useExceptionHandlerCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseExceptionHandler"));
                Assert.True(useExceptionHandlerCall != null, "`Startup.Configure` doesn't appear to contain a call to `UseExceptionHandler` when `env.IsDevelopment()` is `false`.");
                Assert.True(logInformationCall.Span.End < useExceptionHandlerCall.Span.Start, "`Startup.Configure`'s call to `LogInformation` should be made before the call to `UserExceptionHandler`.");
            }

            // if neither, fail test
            else
                Assert.True(1 == 2, "`Startup.Configure` does not appear to contain a condition to check if `env.IsDevelopment()` is `true` or `false`. (it's also possible you do have an conditional that checks if `env.IsDevelopment()` is `true` but is in an unexpected format, please use `env.IsDevelopment()` directly in your condition and not a variable.)");
        }


        [Fact(DisplayName = "Set SecretMessage Field @set-secretmessage-field")]
        public void SetSecretMessageField()
        {
            var method = methods.Where(m => m.Identifier.Value.Equals("ConfigureServices")).FirstOrDefault();
            var setConfiguration = method?.DescendantNodes().OfType<AssignmentExpressionSyntax>()?.FirstOrDefault(m => m.ToString().Contains("Configuration"));
            Assert.True((setConfiguration?.ToString().Contains("SecretMessage") != null && setConfiguration?.ToString().Contains(@"""SecretMessage""") != null), @"`Startup.ConfigureServices` does not appear to set the `SecretMessage` property to `Configuration[""SecretMessage""]`");
        }

        [Fact(DisplayName = "Create Secret @create-secret")]
        public void CreateSecret()
        {
            // We can't actually test this since the secret lives on the learner's computer so this is here just to mark the task as done (even though we can't tell, I'm okay if the learner simply skips this task)
        }

        [Fact(DisplayName = "Configure CookiePolicyOptions in ConfigureServices @configure-cookiepolicyoptions-in-configureservices")]
        public void ConfigureCookiePolicyOptions()
        {
            var method = methods.Where(m => m.Identifier.Value.Equals("ConfigureServices")).FirstOrDefault();
            var methodCalls = method?.DescendantNodes().OfType<InvocationExpressionSyntax>();
            var cookiePolicyOptionsCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("CookiePolicyOptions"));
            Assert.True(cookiePolicyOptionsCall != null, "`Startup.ConfigureServices` does not appear to contain a call to `Configure<CookiePolicyOptions>`");
            // The task specifically says we aren't worried about what they set the cookie policy to, just that they set something with an example provided, so we're not testing anything past they set something
            var cookiePolicyOptionsArguement = cookiePolicyOptionsCall.ArgumentList?.Arguments.FirstOrDefault(a => a.ToString().Contains("options"));
            Assert.True(cookiePolicyOptionsArguement != null, "`Startup.ConfigureServices`'s call to `Configure<CookiePolicyOptions>` does not appear to contain arguements for `options` (for example `options => { options.CheckConsentNeeded = context => true; options.MinimumSameSitePolicy = SameSiteMode.None; }`");
            var addControllersWithViewsCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("AddControllersWithViews"));
            Assert.True(cookiePolicyOptionsCall.Span.End < addControllersWithViewsCall?.Span.Start, "`Startup.ConfigureServices`'s call to `Configure<CookiePolicyOptions>` should happen before our call to `AddControllersWithViews`");
        }

        [Fact(DisplayName = "Call UseCookiePolicy in Configure @call-usecookiepolicy-configure")]
        public void CallUseCookiePolicyInConfigure()
        {
            var method = methods.Where(m => m.Identifier.Value.Equals("Configure")).FirstOrDefault();
            var methodCalls = method?.DescendantNodes().OfType<InvocationExpressionSyntax>();
            var useCookiePolicyCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseCookiePolicy"));
            Assert.True(useCookiePolicyCall != null, "`Startup.Configure` does not appear to contain a call to `UseCookiePolicy`");
            var useRoutingCall = methodCalls?.FirstOrDefault(m => m.ToString().Contains("UseRouting"));
            Assert.True(useCookiePolicyCall.Span.End < useRoutingCall.Span.Start, "`Startup.Configure`'s `UseCookiePolicy` call should happen before the `UseRouting` call.");
        }
    }
}
