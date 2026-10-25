using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MusicPortal.TagHelpers
{
    using Microsoft.AspNetCore.Mvc;
    using MusicPortal.Models;
    public class PaginationTagHelper(IUrlHelperFactory urlHelperFactory) : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext viewContext { get; set; } = null!;
        public string? Action {  get; set; }
        public PaginationViewModel? Pagination {  get; set; }
        [HtmlAttributeName(DictionaryAttributePrefix = "property-url-")]
        public Dictionary<string, object> Attributes { get; set; } = [];
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Pagination.TotalPages <= 1)
                return;
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(viewContext);
            output.TagName = "div";
            output.Attributes.Add("class", "pagination");
            TagBuilder button_1 = new TagBuilder("button");
            button_1.AddCssClass("element-pagination");
            TagBuilder link_1 = new TagBuilder("a");
            link_1.InnerHtml.Append(Resources.Resource.Back);
            link_1.AddCssClass("a-btn");
            if (Pagination!.HasPreviosPage)
            {
                link_1.Attributes.Add("href", urlHelper.Action(Action, new { sortorder = Attributes["sort"], nameMusic = Attributes["music"], idGenre = Attributes["genre"], page = (int)Attributes["page"] - 1 }));
            }
            button_1.InnerHtml.AppendHtml(link_1);
            TagBuilder span = new TagBuilder("span");
            span.AddCssClass("element-pagination");
            span.InnerHtml.Append($"{Pagination.Page} - {Pagination.TotalPages}");
            TagBuilder button_2 = new TagBuilder("button");
            button_2.AddCssClass("element-pagination");
            TagBuilder link_2 = new TagBuilder("a");
            link_2.InnerHtml.Append(Resources.Resource.Forward);
            link_2.AddCssClass("a-btn");
            if (Pagination!.HasNextPage)
            {
                link_2.Attributes.Add("href", urlHelper.Action(Action, new { sortorder = Attributes["sort"], nameMusic = Attributes["music"], idGenre = Attributes["genre"], page = (int)Attributes["page"] + 1 }));
            }
            button_2.InnerHtml.AppendHtml(link_2);
            output.Content.AppendHtml(button_1);
            output.Content.AppendHtml(span);
            output.Content.AppendHtml(button_2);
        }
    }
}
