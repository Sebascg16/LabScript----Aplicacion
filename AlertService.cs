
using Microsoft.JSInterop;

namespace LabScript
{
    public class AlertService : IAsyncDisposable
    {
        readonly Lazy<Task<IJSObjectReference>> ijsObjectReference;
        public AlertService(IJSRuntime ijsRuntime)
        {
            this.ijsObjectReference = new Lazy<Task<IJSObjectReference>>(() =>
            ijsRuntime.InvokeAsync<IJSObjectReference>("import", 
            "./content/LabScript/Pages/Home.razor.js").AsTask());
        }

        public async ValueTask DisposeAsync()
        {
           if (ijsObjectReference.IsValueCreated)
            {
                IJSObjectReference moduleJs = await ijsObjectReference.Value;
                await moduleJs.DisposeAsync();
            }
        }

        public async Task CallJsAlertFunction()
        {
           var jsModule = await ijsObjectReference.Value;

            await jsModule.InvokeVoidAsync("jsFuncion");
        }
    }
}
