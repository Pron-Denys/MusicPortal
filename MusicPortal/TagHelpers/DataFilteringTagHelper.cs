using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MusicPortal.Models;

namespace MusicPortal.TagHelpers
{
    public class DataFilteringTagHelper(IUrlHelperFactory urlHelperFactory) : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext viewContext { get; set; } = null!;

        public string? Action {  get; set; }
        public DataFilteringViewModel? Filter {  get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(viewContext);
            output.TagName = "div";
            output.Attributes.Add("class", "card-music");
            output.Attributes.Add("style", "justify-content: start;");
            TagBuilder form = new TagBuilder("form");
            form.Attributes.Add("method", "get");
            form.Attributes.Add("action", urlHelper.Action(Action));
            TagBuilder label_1 = new TagBuilder("label");
            label_1.InnerHtml.Append(@Resources.Resource.Name);
            TagBuilder label_2 = new TagBuilder("label");
            label_2.AddCssClass("label-genre");
            label_2.InnerHtml.Append(@Resources.Resource.Genre);
            TagBuilder br = new TagBuilder("br");
            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("type", "text");
            input.Attributes.Add("name", "nameMusic");
            input.Attributes.Add("value", Filter!.Name);
            input.Attributes.Add("placeholder", $"{Resources.Resource.EnterTheName}...");
            TagBuilder select = new TagBuilder("select");
            select.AddCssClass("list-genre");
            select.Attributes.Add("size", "1");
            select.Attributes.Add("name", "idGenre");
            TagBuilder option = new TagBuilder("option");
            option.Attributes.Add("selected", "selected");
            option.Attributes.Add("value", $"{Filter.SelectedGenre.Id}");
            option.InnerHtml.Append(Filter.SelectedGenre.Name!);
            select.InnerHtml.AppendHtml(option);
            foreach (Genre genre in Filter.Genres)
            {
                if (genre.Name == Filter.SelectedGenre.Name) continue;
                option = new TagBuilder("option");
                option.Attributes.Add("value", $"{genre.Id}");
                option.InnerHtml.Append(genre.Name!);
                select.InnerHtml.AppendHtml(option);
            }
            TagBuilder submit = new TagBuilder("input");
            submit.AddCssClass("btn-filter");
            submit.Attributes.Add("type", "submit");
            submit.Attributes.Add("value", Resources.Resource.Filter);
            form.InnerHtml.AppendHtml(label_1);
            form.InnerHtml.AppendHtml(label_2);
            form.InnerHtml.AppendHtml(br);
            form.InnerHtml.AppendHtml(input);
            form.InnerHtml.AppendHtml(select);
            form.InnerHtml.AppendHtml(submit);
            output.Content.AppendHtml(form);
        }
    }
}
