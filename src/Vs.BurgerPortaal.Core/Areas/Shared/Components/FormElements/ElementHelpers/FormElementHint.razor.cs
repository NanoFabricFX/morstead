﻿using Microsoft.AspNetCore.Components;

namespace Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements.ElementHelpers
{
    public partial class FormElementHint
    {
        [Parameter]
        public string Text { get; set; }

        protected bool Show => !string.IsNullOrWhiteSpace(Text);
    }
}
