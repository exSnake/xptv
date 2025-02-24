namespace xptv
{
    public partial class App : Application
    {
        private readonly IServiceProvider _services;

        public App(IServiceProvider services)
        {
            InitializeComponent();
            _services = services;
            MainPage = new AppShell(); // Torniamo alla versione semplice
        }
    }
}
