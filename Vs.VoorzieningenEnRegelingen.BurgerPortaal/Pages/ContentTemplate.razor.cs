﻿using Microsoft.AspNetCore.Components;
using Vs.Cms.Core.Helper;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages
{
    public partial class ContentTemplate
    {
        [Inject]
        private IYamlScriptController YamlScriptController { get; set; }

        private const string None = "none";
        private const string Block = "block";

        readonly ITextFormElementData YamlLogic = new TextFormElementData
        {
            Size = FormElementSize.ExtraLarge,
            Label = "Regels Yaml Url",
            Name = "Rule",
            TagText = "Optioneel",
            Value = "https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/Vs.VoorzieningenEnRegelingen.Core.TestData/YamlScripts/YamlZorgtoeslag5.yaml"
        };

        private string _urlDisplay = None;

        private string UrlYamlContent => "<code><pre>" + _urlYamlContentNonFormatted+ "</pre></code>";

        private string _urlYamlContentNonFormatted = string.Empty;

        private void SubmitUrl()
        {
            var yaml = YamlContentParser.ParseHelper(YamlLogic.Value);
            _urlYamlContentNonFormatted = GetYamlContentTemplate(yaml);
            _urlDisplay = Block;
        }

        private void HideUrlContentResult()
        {
            _urlDisplay = None;
        }

        readonly ITextFormElementData YamlLogicText = new TextFormElementData
        {
            Size = FormElementSize.ExtraLarge,
            Label = "Regels Yaml Text",
            Name = "Rule Als Text",
            TagText = "Optioneel",
            Value = "Vul hier de Yaml"
        };

        private string _textDisplay = None;

        private string UrlTextContent => "<code><pre>" + _textYamlContentNonFormatted + "</pre></code>";

        private string _textYamlContentNonFormatted = string.Empty;
        private void SubmitText()
        {
            _textYamlContentNonFormatted = GetYamlContentTemplate(YamlLogicText.Value);
            _textDisplay = Block;
        }

        private void HideTextContentResult()
        {
            _textDisplay = None;
        }

        private string GetYamlContentTemplate(string body)
        {
            var result = YamlScriptController.Parse(body);
            if (result.IsError)
            {
                return "Er zit een fout in de Yaml";
            }
            return YamlScriptController.CreateYamlContentTemplate();
        }
    }
}