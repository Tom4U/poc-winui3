using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ohms.Kopf.Desktop.Core.Models;

namespace Ohms.Kopf.Desktop.Features.Demo
{
    internal partial class DemoViewModel : ViewModel
    {
        private string title = "Demo Feature";

        public string Title
        { get => title; set { SetProperty(ref title, value, nameof(Title)); } }
    }
}