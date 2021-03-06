﻿using System;
using System.Threading.Tasks;
using GovenorReports.Data;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovenorReports.TagHelpers
{
    public class PassFailTagHelper : TagHelper
    {
        public AuditReport Model { get; set; }
        public string Key { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if(Model == null)
            {
                throw new ArgumentNullException($"{nameof(Model)} must be set for {nameof(PassFailTagHelper)}");
            }
            if (Key == null)
            {
                throw new ArgumentNullException($"{nameof(Key)} must be set for {nameof(PassFailTagHelper)}");
            }

            if (Model.Properties.ContainsKey(Key))
            {
                var result = (bool)Model.Properties[Key];
                var divClass = !result ? "bg-success" : "bg-danger";
                var iClass = !result ? "glyphicon glyphicon-ok" : "glyphicon glyphicon-remove";
                var childContent = await output.GetChildContentAsync();

                output.TagName = "div";
                output.Attributes.Add("class", divClass);
                output.Content.SetHtmlContent($"<i class='{iClass}'></i>{childContent.GetContent()}");
            }
            else
            {   
                var childContent = await output.GetChildContentAsync();

                output.TagName = "div";
                output.Attributes.Add("class", "bg-warning");
                output.Content.SetHtmlContent($"<i class='glyphicon glyphicon-question-sign'></i>{childContent.GetContent()}");
            }
        }
    }
}
