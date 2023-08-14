namespace BlazorEcommerceStaticWebApp.Client.Services.Helper
{
    public class StateContainer
    {
        private bool useAzureSQL = true;
        private bool useSQLite = false;

        public bool UseAzureSQL
        {
            get => useAzureSQL;
            set
            {
                useAzureSQL = value;
                useSQLite = !value;
                NotifyStateChanged();
            }
        }

        public bool UseSQLite
        {
            get => useSQLite;
            set
            {
                useSQLite = value;
                useAzureSQL = !value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
