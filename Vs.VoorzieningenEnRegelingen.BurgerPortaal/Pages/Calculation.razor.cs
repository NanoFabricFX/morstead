﻿using Microsoft.AspNetCore.Components;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers;
using Vs.VoorzieningenEnRegelingen.Core;
using System.Linq;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages
{
    public partial class Calculation
    {
        //the formElement we are showing
        private IFormElement _formElement;

        private int _displayQuestionNumber => _hasRights ?
            FormTitleHelper.GetQuestionNumber(_sequenceController.Sequence, _sequenceController.LastExecutionResult) :
            -1;
        private string _displayQuestion => _hasRights ?
            FormTitleHelper.GetQuestion(_sequenceController.LastExecutionResult) :
            "Geen recht";
        private string _displayQuestionTitle => _hasRights ?
            FormTitleHelper.GetQuestionTitle(_sequenceController.LastExecutionResult) :
            "U heeft geen recht op zorgtoeslag.";
        private string _displayQuestionDescription => _hasRights ?
            FormTitleHelper.GetQuestionDescription(_sequenceController.LastExecutionResult) :
            "Met de door u ingevulde gegevens heeft u geen recht op zorgtoeslag. " +
            "Voor meer informatie over zorgtoeslag in uw situatie, neem contact op met de Belastingdienst.";

        private string _result =>
            _sequenceController.LastExecutionResult.Parameters.Any(p => p.Name == "zorgtoeslag") ?
                " <strong>Uw zorgtoeslag is €" +
                ((double)
                    _sequenceController.LastExecutionResult.Parameters.
                        FirstOrDefault(p => p.Name == "zorgtoeslag").Value)
                        .ToString("#.00").Replace('.', ',') + " per maand.</strong>" :
                null;

        private bool _hasRights = true;

        private bool _showPreviousButton => _sequenceController.CurrentStep <= 1;
        private bool _showNextButton => !_hasRights || _result != null;

        [Inject]
        private ISequenceController _sequenceController { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _sequenceController.Sequence.Yaml = _testYaml;
            //get the first step
            GetNextStep();
        }

        private void GetNextStep()
        {
            if (FormIsValid())
            {
                //increase the request step
                _sequenceController.IncreaseStep();
                _sequenceController.ExecuteStep(GetCurrentParameters());
                Display();
            }
            StateHasChanged();
        }

        private void GetPreviousStep()
        {
            //reset the rights
            _hasRights = true;
            //decrease the request step, can never be lower than 1
            _sequenceController.DecreaseStep();
            _sequenceController.ExecuteStep(GetCurrentParameters());
            Display();
            StateHasChanged();
        }

        private void Display()
        {
            if (RechtHelper.HasRecht(_sequenceController.LastExecutionResult))
            {
                _formElement = FormElementHelper.ParseExecutionResult(_sequenceController.LastExecutionResult);
                if (_formElement.ShowElement)
                {
                    _formElement.Value = FormElementHelper.GetValue(_sequenceController.Sequence, _sequenceController.LastExecutionResult) ?? string.Empty;
                    ValidateForm(true); //set the IsValid and ErrorText Property
                }
            }
            else
            {
                _formElement = new FormElement();
                _hasRights = false;
            }
        }

        private bool FormIsValid()
        {
            //always return true if the sequence is before the first step
            if (_sequenceController.CurrentStep == 0)
            {
                return true;
            }
            ValidateForm();
            return _formElement.IsValid;
        }

        /// <summary>
        /// Will validate the form
        /// </summary>
        /// <param name="unobtrusive">Will not set a visible error message of IsValid tag to false if true (to use when first displaying generating with an empty value)</param>
        /// <returns>Whether or not the form is valid.</returns>
        private bool ValidateForm(bool unobtrusive = false)
        {
            return _formElement?.Validate(unobtrusive) ?? true;
        }

        /// <summary>
        /// Only return the current value if it is a valid value
        /// </summary>
        /// <returns></returns>
        private ParametersCollection GetCurrentParameters()
        {
            ValidateForm();
            if (_formElement?.IsValid ?? false)
            {
                if (_formElement.InferedType == TypeInference.InferenceResult.TypeEnum.Boolean)
                {
                    return GetCurrentBooleanParameter();
                }
                if (_formElement.InferedType == TypeInference.InferenceResult.TypeEnum.Double)
                {
                    return GetCurrentNumberParameter();
                }
                return new ParametersCollection {
                    new ClientParameter(_formElement.Name, _formElement.Value, _formElement.InferedType)
                };
                //Key = 0
            }
            return null;
        }

        private ParametersCollection GetCurrentBooleanParameter()
        {
            var result = new ParametersCollection();
            //get all parameter options
            foreach (var key in _formElement.Options.Keys)
            {
                result.Add(new ClientParameter(key, key == _formElement.Value ? "ja" : "nee", _formElement.InferedType));
            }

            return result;
        }

        private ParametersCollection GetCurrentNumberParameter()
        {
            return new ParametersCollection
            {
                new ClientParameter(_formElement.Name, _formElement.Value.Replace(',', '.'), _formElement.InferedType)
            };
        }

        private string _testYaml = @"# Zorgtoeslag for burger site demo
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 1.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
berekening:
 - stap: woonland
   formule: woonlandfactor
   recht: woonlandfactor > 0
 - stap: woonsituatie
   keuze:
   - situatie: alleenstaande
   - situatie: aanvrager_met_toeslagpartner
 - stap: vermogensdrempel
   keuze:
   - situatie: hoger_dan_vermogensdrempel
   - situatie: lager_dan_vermogensdrempel
   recht: lager_dan_vermogensdrempel
 - stap: inkomensdrempel
   keuze:
   - situatie: hoger_dan_inkomensdrempel
   - situatie: lager_dan_inkomensdrempel
   recht: lager_dan_inkomensdrempel
 - stap: toetsingsinkomen
   formule: toetsingsinkomen
   recht: toetsingsinkomen < toetsingsinkomensdrempel
 - stap: zorgtoeslag
   formule: zorgtoeslag
formules:
 - woonlandfactor:
     formule: lookup('woonlandfactoren',woonland,'woonland','factor', 0)
 - standaardpremie:
   - situatie: alleenstaande
     formule: 1609
   - situatie: aanvrager_met_toeslagpartner
     formule: 3218
 - toetsingsinkomen:
     formule: toetsingsinkomenbedrag
 - toetsingsinkomensdrempel:
   - situatie: alleenstaande
     formule: 29562
   - situatie: aanvrager_met_toeslagpartner
     formule: 37885
 - drempelinkomen:
     formule: 20941
 - normpremie:
   - situatie: alleenstaande     
     formule: min(percentage(2.005) * drempelinkomen + max(percentage(13.520) * (toetsingsinkomen - drempelinkomen),0), 1189)
   - situatie: aanvrager_met_toeslagpartner
     formule: min(percentage(4.315) * drempelinkomen + max(percentage(13.520) * (toetsingsinkomen - drempelinkomen),0), 2314)
 - zorgtoeslag:
     formule: round((standaardpremie - normpremie) * woonlandfactor / 12,2)
tabellen:
  - naam: woonlandfactoren
    woonland, factor:
      - [ Nederland,           1.0    ]
      - [ België,              0.7392 ]
      - [ Bosnië-Herzegovina,  0.0672 ]
      - [ Bulgarije,           0.0735 ]
      - [ Cyprus,              0.1363 ]
      - [ Denemarken,          0.9951 ]
      - [ Duitsland,           0.8701 ]
      - [ Estland,             0.2262 ]
      - [ Finland,             0.7161 ]
      - [ Frankrijk,           0.8316 ]
      - [ Griekenland,         0.2490 ]
      - [ Hongarije,           0.1381 ]
      - [ Ierland,             0.8667 ]
      - [ IJsland,             0.9802 ]
      - [ Italië,              0.5470 ]
      - [ Kaapverdië,          0.0177 ]
      - [ Kroatië,             0.1674 ]
      - [ Letland,             0.0672 ]
      - [ Liechtenstein,       0.9720 ]
      - [ Litouwen,            0.2399 ]
      - [ Luxemburg,           0.7358 ]
      - [ Macedonië,           0.0565 ]
      - [ Malta,               0.3574 ]
      - [ Marokko,             0.0193 ]
      - [ Montenegro,          0.0821 ]
      - [ Noorwegen,           1.3729 ]
      - [ Oostenrijk,          0.6632 ]
      - [ Polen,               0.1691 ]
      - [ Portugal,            0.2616 ]
      - [ Roemenië,            0.0814 ]
      - [ Servië,              0.0714 ]
      - [ Slovenië,            0.3377 ]
      - [ Slowakije,           0.2405 ]
      - [ Spanje,              0.4001 ]
      - [ Tsjechië,            0.2412 ]
      - [ Tunesië,             0.0292 ]
      - [ Turkije,             0.0874 ]
      - [ Verenigd Koninkrijk, 0.7741 ]
      - [ Zweden,              0.8213 ]
      - [ Zwitserland,         0.8000 ]
      - [ Anders,              0      ]";
    }
}