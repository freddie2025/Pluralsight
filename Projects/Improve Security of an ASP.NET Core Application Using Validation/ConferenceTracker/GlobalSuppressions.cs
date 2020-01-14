// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Prevent warning to learners that in the case of Configure as it isn't static by default and some projects will require Configure to not be static", Scope = "member", Target = "~M:ConferenceTracker.Startup.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder,Microsoft.AspNetCore.Hosting.IWebHostEnvironment)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Prevent warning to learners that in the case of ConfigureServices as it isn't static by default and some projects will require ConfigureServices to not be static", Scope = "member", Target = "~M:ConferenceTracker.Startup.ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "Prevent warning to learners that in the case of Program as it isn't static by default and some projects will require Program to not be static", Scope = "type", Target = "~T:ConferenceTracker.Program")]
