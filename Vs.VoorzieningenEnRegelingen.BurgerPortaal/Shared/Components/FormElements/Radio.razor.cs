﻿using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements
{
    public partial class Radio
    {
        private IBooleanFormElementData _data => Data as IBooleanFormElementData;
        private bool _isHorizontalRadio => _data.Options.Count == 2 && _data.Options.All(o => o.Value.Length <= 10);
        protected IEnumerable<string> _keys => _data.Options.Keys;

        public override void FillDataFromResult(IExecutionResult result)
        {
            Data = new BooleanFormElementData();
            Data.FillFromExecutionResult(result);
        }
    }
}
