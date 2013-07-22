using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Examples.ViewModels;
using Caliburn.Micro;

namespace Examples.Infrastructure
{
    public class IntelliboxExamplesBootstrapper : Bootstrapper<MainWindowVM>
    {
        protected override void Configure() {
            base.Configure();

            ViewLocator.NameTransformer.AddRule(@"(?<namespace>(.*\.)*)ViewModels\.(?<basename>[A-Za-z_]\w*)VM$", @"${namespace}${basename}");
        }
    }
}
