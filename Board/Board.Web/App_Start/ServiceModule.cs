using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Board.Model;

namespace Board.Web
{
    public class ServiceModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(Assembly.Load("Board.Repository"))
                      .Where(t => t.Name.EndsWith("Repository"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();

            builder.RegisterType(typeof(BoardContext)).As(typeof(IContext)).InstancePerLifetimeScope();       
        }
    }
}