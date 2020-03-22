﻿using Microsoft.Extensions.DependencyInjection;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers.Interface;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.Interface;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<ISequenceController, SequenceController>();
            services.AddScoped<ISequence, Sequence>();
            Service.Initializer.Initialize(services);
        }
    }
}
