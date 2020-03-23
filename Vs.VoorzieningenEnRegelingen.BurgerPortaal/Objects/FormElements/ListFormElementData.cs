﻿using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class ListFormElementData : OptionsFormElementData, IListFormElementData
    {
        public override string Value
        {
            get
            {
                if (_value == string.Empty)
                {
                    return Options.ToList().FirstOrDefault().Key;
                }
                return _value;
            }
            set { _value = value; }
        }

        public override void FillFromExecutionResult(IExecutionResult result)
        {
            base.FillFromExecutionResult(result);

            Size = FormElementSize.Large;
        }

        public override void DefineOptions(IExecutionResult result)
        {
            (result.Questions.Parameters.GetAll().First().Value as List<object>)?.ForEach(v => Options.Add(v.ToString(), v.ToString()));
        }
    }
}
