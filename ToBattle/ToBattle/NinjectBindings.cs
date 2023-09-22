using Ninject.Modules;
using NLog;
using ToBattle.Services;

namespace ToBattle
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().ToMethod(m => LogManager.GetLogger(typeof(Game).GetType().FullName));

            Bind<SettingsService>().To<SettingsService>();
            Bind<RandomService>().To<RandomService>().InSingletonScope();
            Bind<HeroService>().To<HeroService>();

            Bind<Game>().To<Game>();
        }
    }
}
