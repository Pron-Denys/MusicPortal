namespace MusicPortal.TagHelpers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using MusicPortal.Models;
    public class SortHeaderTagHelper(IUrlHelperFactory urlHelperFactory) : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? viewContext { get; set; }
        public string? Action {  get; set; }
        public SortState SortOrder { get; set; }
        public string? NameMusic { get; set; }
        public int IdGenre { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(viewContext!);
            output.TagName = "a";
            output.Attributes.Add("class", "a-btn");
            output.Attributes.Add("style", "margin-left: 80px;");
            output.Attributes.Add("href", urlHelper.Action(Action, new { sortorder = SortOrder, nameMusic = NameMusic, idGenre = IdGenre }));
        }
    }
}
