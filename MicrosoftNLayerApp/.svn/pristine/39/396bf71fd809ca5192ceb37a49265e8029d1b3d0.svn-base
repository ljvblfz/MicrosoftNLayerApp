using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels
{
    public class MenuStateViewModel
    {
        private readonly Tuple<string,string> _controllerActionPair;

        private readonly IDictionary<Tuple<string, string>, string> _mapControllerActionMenuOption = new Dictionary<Tuple<string, string>, string>() 
        {
            { Tuple.Create("Customer".ToUpperInvariant(),"Index".ToUpperInvariant()), "CustomerList".ToUpperInvariant() },
            { Tuple.Create("Customer".ToUpperInvariant(),"Create".ToUpperInvariant()),"NewCustomer".ToUpperInvariant()},
            { Tuple.Create("BankAccount".ToUpperInvariant(),"Index".ToUpperInvariant()),"TransferList".ToUpperInvariant()},
            { Tuple.Create("BankAccount".ToUpperInvariant(),"TransferMoney".ToUpperInvariant()),"PerformTransfer".ToUpperInvariant()},
            { Tuple.Create("Order".ToUpperInvariant(),"Index".ToUpperInvariant()), "OrderList".ToUpperInvariant()},
            { Tuple.Create("Order".ToUpperInvariant(),"PerformOrder".ToUpperInvariant()),"PerformOrder".ToUpperInvariant()}
        };

        public MenuStateViewModel(string controller, string action)
        {
            _controllerActionPair = Tuple.Create(controller.ToUpperInvariant(), action.ToUpperInvariant());
        }


        
        
        public bool IsCurrentAction(string menuOption)
        {
            string upperOption = menuOption.ToUpperInvariant();

            if (_mapControllerActionMenuOption.ContainsKey(_controllerActionPair))
            {
                return upperOption.Equals(_mapControllerActionMenuOption[_controllerActionPair]);
            }
            else
            {
                return false;
            }
        }

    }
}