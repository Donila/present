namespace Present.WebMvc.Configuration
{
    public static class Resolver
    {
        private static IClassesContainer _container;

        public static IClassesContainer Container 
        { 
            get { return _container ?? (_container = new ClassesContainer()); }
        }
    }
}