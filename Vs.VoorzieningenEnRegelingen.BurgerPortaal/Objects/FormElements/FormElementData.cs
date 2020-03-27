﻿using System;
using System.Collections.Generic;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Enums;
using Vs.Core.Extensions;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class FormElementData : IFormElementData
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public FormElementSize Size { get; set; }
        public string TagText { get; set; }
        public string HintText { get; set; }
        public IEnumerable<string> HintTextList { get; set; }
        public string ErrorText { get => !IsValid ? GetErrorText() : string.Empty; }
        public bool IsDisabled { get; set; } = false;
        public bool IsRequired { get; set; } = false;
        public bool IsValid { get; set; } = true;
        public TypeInference.InferenceResult.TypeEnum InferedType { get; set; }
        public virtual string Value { get; set; }

        public string ElementSize => Size.GetDescription();

        public IList<string> ErrorTexts = new List<string>();

        public virtual void Validate(bool unobtrusive = false)
        {
            //reset values
            IsValid = true;
            ErrorTexts = new List<string>();

            var valid = ValidateValueIsSet();

            if (!unobtrusive)
            {
                IsValid = valid;
            }
        }

        private bool ValidateValueIsSet()
        {
            var valid = !string.IsNullOrWhiteSpace(Value);
            if (!valid)
            {
                ErrorTexts.Add("Vul een waarde in.");
            }

            return valid;
        }

        private string GetErrorText()
        {
            return string.Join(Environment.NewLine, ErrorTexts);
        }

        public virtual void FillFromExecutionResult(IExecutionResult result, IContentController contentController)
        {
            //todo MPS write test for this
            InferedType = result.InferedType;
            Name = result.QuestionFirstParameter.Name;
            Label = contentController.GetText(result.SemanticKey, FormElementContentType.Label);
            TagText = contentController.GetText(result.SemanticKey, FormElementContentType.Tag);
            HintText = contentController.GetText(result.SemanticKey, FormElementContentType.Hint);
        }
    }
}
