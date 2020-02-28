//// AspNetCore.Views_Home_Contact
//using Microsoft.AspNetCore.Html;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Razor;
//using Microsoft.AspNetCore.Mvc.Razor.Internal;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Mvc.TagHelpers;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Microsoft.AspNetCore.Razor.Hosting;
//using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
//using Microsoft.AspNetCore.Razor.TagHelpers;
//using MvcSandbox.Controllers;
//using System;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//[RazorSourceChecksum("SHA1", "8b52731432ae584f39cc2bf8b222ce419b3ef971", "/Views/Home/Contact.cshtml")]
//[RazorSourceChecksum("SHA1", "6f18e0e71b35c40a96ecb76a2d3e4dade6fb93ec", "/Views/_ViewImports.cshtml")]
//public class Views_Home_Contact : RazorPage<Contact>
//{
//    #region
//    private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("type", "text", HtmlAttributeValueStyle.DoubleQuotes);

//    private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("form-control"), HtmlAttributeValueStyle.DoubleQuotes);

//    private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("asp-route", "contact-us", HtmlAttributeValueStyle.DoubleQuotes);

//    private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

//    private TagHelperExecutionContext __tagHelperExecutionContext;

//    private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

//    private string __tagHelperStringValueBuffer;

//    private TagHelperScopeManager __backed__tagHelperScopeManager = null;

//    private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

//    private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

//    private LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;

//    private InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;

//    private TagHelperScopeManager __tagHelperScopeManager
//    {
//        get
//        {
//            if (__backed__tagHelperScopeManager == null)
//            {
//                __backed__tagHelperScopeManager = new TagHelperScopeManager(base.StartTagHelperWritingScope, base.EndTagHelperWritingScope);
//            }
//            return __backed__tagHelperScopeManager;
//        }
//    }

//    [RazorInject]
//    public IModelExpressionProvider ModelExpressionProvider
//    {
//        get;
//        private set;
//    }

//    [RazorInject]
//    public IUrlHelper Url
//    {
//        get;
//        private set;
//    }

//    [RazorInject]
//    public IViewComponentHelper Component
//    {
//        get;
//        private set;
//    }

//    [RazorInject]
//    public IJsonHelper Json
//    {
//        get;
//        private set;
//    }

//    [RazorInject]
//    public IHtmlHelper<Contact> Html
//    {
//        get;
//        private set;
//    }

//    #endregion

//    public override async Task ExecuteAsync()
//    {
//        // Raw HTML
//        WriteLiteral("\r\n<h1 class=\"display-4\">Contact Us</h1>\r\n<p>Let us know your thoughts.</p>\r\n<div class=\"container\">\r\n    <div class=\"row\">\r\n        <div class=\"col-md-9\">\r\n            ");
        
//        // Form Tag helper start
//        __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "8b52731432ae584f39cc2bf8b222ce419b3ef9714762", (Func<Task>)async delegate
//        {
//            WriteLiteral("\r\n                <div class=\"form-group\">\r\n                    ");
           
//            // Label Tag helper
//            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8b52731432ae584f39cc2bf8b222ce419b3ef9715086", (Func<Task>)async delegate
//            {
//            });
//            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
//            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
//            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(base.ViewData, (Expression<Func<Contact, string>>)((Contact __model) => __model.Name));
//            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
//            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
//            if (!__tagHelperExecutionContext.Output.IsContentModified)
//            {
//                await __tagHelperExecutionContext.SetOutputContentAsync();
//            }
//            Write(__tagHelperExecutionContext.Output);
//            __tagHelperExecutionContext = __tagHelperScopeManager.End();

//            // Input Tag Helper
//            WriteLiteral("\r\n                    ");
//            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "8b52731432ae584f39cc2bf8b222ce419b3ef9716603", (Func<Task>)async delegate
//            {
//            });
//            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
//            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
//            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
//            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
//            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(base.ViewData, (Expression<Func<Contact, string>>)((Contact __model) => __model.Name));
//            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
//            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
//            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
//            if (!__tagHelperExecutionContext.Output.IsContentModified)
//            {
//                await __tagHelperExecutionContext.SetOutputContentAsync();
//            }
//            Write(__tagHelperExecutionContext.Output);
//            __tagHelperExecutionContext = __tagHelperScopeManager.End();

//            // Label Tag Helper
//            WriteLiteral("\r\n                </div>\r\n                <div class=\"form-group\">\r\n                    ");
//            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8b52731432ae584f39cc2bf8b222ce419b3ef9718489", (Func<Task>)async delegate
//            {
//            });
//            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
//            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
//            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(base.ViewData, (Expression<Func<Contact, string>>)((Contact __model) => __model.Message));
//            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
//            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
//            if (!__tagHelperExecutionContext.Output.IsContentModified)
//            {
//                await __tagHelperExecutionContext.SetOutputContentAsync();
//            }
//            Write(__tagHelperExecutionContext.Output);
//            __tagHelperExecutionContext = __tagHelperScopeManager.End();

//            // Input Tag Helper
//            WriteLiteral("\r\n                    ");
//            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "8b52731432ae584f39cc2bf8b222ce419b3ef97110009", (Func<Task>)async delegate
//            {
//            });
//            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
//            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
//            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
//            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
//            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(base.ViewData, (Expression<Func<Contact, string>>)((Contact __model) => __model.Message));
//            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
//            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
//            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
//            if (!__tagHelperExecutionContext.Output.IsContentModified)
//            {
//                await __tagHelperExecutionContext.SetOutputContentAsync();
//            }
//            Write(__tagHelperExecutionContext.Output);
//            __tagHelperExecutionContext = __tagHelperScopeManager.End();
//            WriteLiteral("\r\n                </div>\r\n                <button type=\"submit\" class=\"btn btn-primary\">Submit</button>\r\n            ");
//        });

//        // Form Tag Helper Close
//        __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
//        __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
//        __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
//        __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
//        __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Route = (string)__tagHelperAttribute_2.Value;
//        __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
//        __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
//        __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
//        await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
//        if (!__tagHelperExecutionContext.Output.IsContentModified)
//        {
//            await __tagHelperExecutionContext.SetOutputContentAsync();
//        }
//        Write(__tagHelperExecutionContext.Output);
//        __tagHelperExecutionContext = __tagHelperScopeManager.End();

//        // Razor HTML Helper
//        WriteLiteral("\r\n        </div>\r\n        <p>");
//        Write(Html.ActionLink("Home", "Index", "Home"));
//        WriteLiteral("</p>\r\n    </div>\r\n</div>");
//    }
//}
