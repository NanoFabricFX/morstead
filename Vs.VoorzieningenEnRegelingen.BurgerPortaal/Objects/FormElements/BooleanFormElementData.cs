﻿using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public class BooleanFormElementData : OptionsFormElementData, IBooleanFormElementData
    {
        public override void DefineOptions(IExecutionResult result)
        {
            foreach (var p in result.Questions.Parameters.GetAll())
            {
                Options.Add(p.Name, FormElementHelper.GetParameterDisplayName(p.Name, result.Parameters));
            }
        }
    }
}
