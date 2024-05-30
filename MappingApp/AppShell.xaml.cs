namespace MappingApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreateRoutePage), typeof(CreateRoutePage));
        }
    }
}
