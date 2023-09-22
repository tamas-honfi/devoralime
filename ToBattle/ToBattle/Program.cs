using System.Reflection;
using Ninject;
using Ninject.Extensions.Logging.NLog4;
using ToBattle;

// Use Ninject as a DI container
var ninjectKernel = new StandardKernel(
    new NinjectSettings()
    {
        // This is needed for Ninject to properly load the NLOG module
        LoadExtensions = false
    }, 
    new NLogModule()
);

// Load bindings from NinjectBindings.cs
ninjectKernel.Load(Assembly.GetExecutingAssembly());

// Load Game through DI, so I can use DI in everything else
var game = ninjectKernel.Get<Game>();

// Run the game
game.Run();
