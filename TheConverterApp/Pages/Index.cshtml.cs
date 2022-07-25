using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConverterApplication;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheConverterApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ConvertDTO ConvertModel { get; set; }

        [BindProperty]
        public double Result { get; set; }

        public SelectList ExchangeList = new SelectList(new List<SelectListItem>
        {
            new SelectListItem{Selected=true,Text = "USD", Value="USD"},
            new SelectListItem{Selected=false,Text = "CAD", Value="CAD"},
            new SelectListItem{Selected=false,Text = "GBP", Value="GBP"},
            new SelectListItem{Selected=false,Text = "EUR", Value="EUR"},
        }, "Value", "Text", 0);

        private readonly ICurrencyConverter _currencyConverter;

        public IndexModel(ICurrencyConverter currencyConverter)
        {

            _currencyConverter = currencyConverter;
        }

        public void OnGet()
        {
            ConvertModel = new ConvertDTO();
        }

        public void OnPost(ConvertDTO ConvertModel)
        {
            Result = _currencyConverter.Convert(ConvertModel.From, ConvertModel.To, ConvertModel.Amount);
        }

        public void OnGetClear()
        {
            ConvertModel = new ConvertDTO();
            Result = 0;
        }
    }
}
