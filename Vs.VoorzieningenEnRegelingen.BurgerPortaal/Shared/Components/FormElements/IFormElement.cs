﻿using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public interface IFormElement
    {
        string ButtonIcon { get; set; }
        string ButtonText { get; set; }
        string ErrorText { get; set; }
        string HintText { get; set; }
        IEnumerable<string> HintTextList { get; set; }
        bool IsDisabled { get; set; }
        bool IsRequired { get; set; }
        bool IsValid { get; set; }
        string Label { get; set; }
        IEnumerable<FormElementLabel> Labels { get; set; }
        string Name { get; set; }
        Dictionary<string, string> Options { get; set; }
        FormElementSize Size { get; set; }
        string TagText { get; set; }
        string Value { get; set; }
        IEnumerable<string> Values { get; set; }

        TypeInference.InferenceResult.TypeEnum InferedType { get; set; }

        bool Validate(bool unobtrusive);
    }
}