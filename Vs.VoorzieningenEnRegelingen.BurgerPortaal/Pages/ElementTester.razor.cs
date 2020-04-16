﻿using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages
{
    public partial class ElementTester
    {
        readonly IFormElementData FieldText = new TextFormElementData
        {
            Label = "TextLabel",
            Name = "TextName",
            Value = "TextValue",
            HintText = "TextHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.String
        };

        private void ValidateText()
        {
            FieldText.CustomValidate();
        }

        readonly IFormElementData FieldTextArea = new TextFormElementData
        {
            Label = "TextAreaLabel",
            Name = "TextAreaName",
            Value = "TextAreaValue",
            HintText = "TextAreaHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.String
        };

        private void ValidateTextArea()
        {
            FieldTextArea.CustomValidate();
        }

        readonly IFormElementData FieldNumber = new NumericFormElementData
        {
            Label = "NumberLabel",
            Name = "NumberName",
            Value = "NumberValue",
            HintText = "NumberHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.Double
        };

        private void ValidateNumber()
        {
            FieldNumber.CustomValidate();
        }

        readonly IFormElementData FieldSelect = new ListFormElementData
        {
            Label = "SelectLabel",
            Name = "SelectName",
            Value = "SelectValue",
            HintText = "SelectHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.List,
            Options = new Dictionary<string, string>
            {
                { "", "MoetLeegZijn" },
                { "Optie1", "Optie1" },
                { "Optie2", "Optie2" },
                { "SelectValue", "Optie3" },
                { "Optie4", "Optie4" }
            }
        };

        private void ValidateSelect()
        {
            FieldSelect.CustomValidate();
        }

        readonly IFormElementData FieldRadio = new BooleanFormElementData
        {
            Label = "RadioLabel",
            Name = "RadioName",
            Value = "",
            HintText = "RadioHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.Boolean,
            Options = new Dictionary<string, string>
            {
                { "Optie1", "Optie1" },
                { "Optie2", "Optie2" },
                { "SelectValue", "Optie3" },
                { "Optie4", "Optie4" }
            }
        };

        private void ValidateRadio()
        {
            FieldRadio.CustomValidate();
        }

        readonly IFormElementData FieldRadioHorizontal = new BooleanFormElementData
        {
            Label = "RadioHorizontalLabel",
            Name = "RadioHorizontalName",
            Value = "",
            HintText = "RadioHorizontalHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.Boolean,
            Options = new Dictionary<string, string>
            {
                { "Ja", "Ja" },
                { "Nee", "Nee" }
            }
        };

        private void ValidateRadioHorizontal()
        {
            FieldRadioHorizontal.CustomValidate();
        }

        readonly IFormElementData FieldDate = new DateFormElementData
        {
            Label = "DateLabel",
            Name = "DatelName",
            Value = "",
            HintText = "DateHint",
            InferedType = Core.TypeInference.InferenceResult.TypeEnum.DateTime
        };

        private void ValidateDate()
        {
            FieldDate.CustomValidate();
        }
    }
}
