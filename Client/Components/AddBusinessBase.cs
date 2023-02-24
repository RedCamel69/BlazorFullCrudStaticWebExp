using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Components;



namespace BlazorEcommerceStaticWebApp.Client.Components
{
    public class AddBusinessBase : ComponentBase
    {
        protected Business? business { get; set; }
       
        protected bool ShowAddBusinessBase { get; set; }

        [Parameter]
        public string AddBusinessBaseTitle { get; set; } = "Add Business";

        [Parameter]
        public string ConfirmationMessage { get; set; } = "Are you sure you want to add business?";

        public void Show()
        {
            business = new Business();
            ShowAddBusinessBase = true;
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<bool> AddBusinessChanged { get; set; }

       // [Parameter]
       // public Business NewBusiness { get; set; }

        protected virtual async Task OnConfirmationChange(bool value, Business business = null)
        {
                    
            ShowAddBusinessBase = false;
            //NewBusiness = business;
            await AddBusinessChanged.InvokeAsync(true);
        }
    }
}
