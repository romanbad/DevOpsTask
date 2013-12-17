using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Web.BootstrapSupport
{
	public class ControlGroup : IDisposable
	{
		private readonly HtmlHelper _html;

		public ControlGroup(HtmlHelper html)
		{
			_html = html;
		}

		#region IDisposable Members

		public void Dispose()
		{
			_html.ViewContext.Writer.Write(_html.EndControlGroup());
		}

		#endregion
	}

	public static class ControlGroupExtensions
	{
		public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html, Expression<Func<T, object>> modelProperty)
		{
			return BeginControlGroupFor(html, modelProperty, null);
		}

		public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html, Expression<Func<T, object>> modelProperty,
		                                                  object htmlAttributes)
		{
			return BeginControlGroupFor(html, modelProperty,
			                            HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html, Expression<Func<T, object>> modelProperty,
		                                                  IDictionary<string, object> htmlAttributes)
		{
			var propertyName = ExpressionHelper.GetExpressionText(modelProperty);
			return BeginControlGroupFor(html, propertyName, null);
		}

		public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html, string propertyName)
		{
			return BeginControlGroupFor(html, propertyName, null);
		}

		public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html, string propertyName, object htmlAttributes)
		{
			return BeginControlGroupFor(html, propertyName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html, string propertyName,
		                                                  IDictionary<string, object> htmlAttributes)
		{
			var controlGroupWrapper = new TagBuilder("div");
			controlGroupWrapper.MergeAttributes(htmlAttributes);
			controlGroupWrapper.AddCssClass("control-group");
			var partialFieldName = propertyName;
			var fullHtmlFieldName =
				html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(partialFieldName);
			if (!html.ViewData.ModelState.IsValidField(fullHtmlFieldName))
			{
				controlGroupWrapper.AddCssClass("error");
			}
			var openingTag = controlGroupWrapper.ToString(TagRenderMode.StartTag);
			return MvcHtmlString.Create(openingTag);
		}

		public static IHtmlString EndControlGroup(this HtmlHelper html)
		{
			return MvcHtmlString.Create("</div>");
		}

		public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, Expression<Func<T, object>> modelProperty)
		{
			return ControlGroupFor(html, modelProperty, null);
		}

		public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, Expression<Func<T, object>> modelProperty,
		                                              object htmlAttributes)
		{
			var propertyName = ExpressionHelper.GetExpressionText(modelProperty);
			return ControlGroupFor(html, propertyName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, string propertyName)
		{
			return ControlGroupFor(html, propertyName, null);
		}

		public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, string propertyName, object htmlAttributes)
		{
			return ControlGroupFor(html, propertyName, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, string propertyName,
		                                              IDictionary<string, object> htmlAttributes)
		{
			html.ViewContext.Writer.Write(BeginControlGroupFor(html, propertyName, htmlAttributes));
			return new ControlGroup(html);
		}
	}

	public static class Alerts
	{
		public const string SUCCESS = "success";
		public const string ATTENTION = "attention";
		public const string ERROR = "error";
		public const string INFORMATION = "info";

		public static string[] ALL
		{
			get { return new[] {SUCCESS, ATTENTION, INFORMATION, ERROR}; }
		}
	}
}