using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Paladin.Infrastructure
{
    public static class RazorWorkflowExtensions
    {
        public static MvcHtmlString WorkflowActionLink(this HtmlHelper html, string action, string controller,
           string displayText, int linkTargetStage, int currentStage, int minRequiredStage, int highestCompletedStage)
        {
            if (highestCompletedStage >= minRequiredStage)
            {
                //Generate Url
                var targetUrl = UrlHelper.GenerateUrl("Default", action, controller,
                    null, RouteTable.Routes, html.ViewContext.RequestContext, false);

                //Create <a> tag
                var anchorBuilder = new TagBuilder("a");
                anchorBuilder.MergeAttribute("href", targetUrl);

                //Assign CSS classes
                string classes = "btn btn-progress";
                if (linkTargetStage == currentStage)
                {
                    classes += " btn-progress-active";
                }
                anchorBuilder.MergeAttribute("class", classes);

                //Return as MVC string
                anchorBuilder.InnerHtml = displayText;
                return new MvcHtmlString(anchorBuilder.ToString(TagRenderMode.Normal));
            }
            else
            {
                var spanBuilder = new TagBuilder("span");
                spanBuilder.MergeAttribute("class", "btn btn-progress");
                spanBuilder.InnerHtml = displayText;
                return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
            }
        }
    }
}