using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dnit.Core.Services.OpenUrl
{
    public class OpenUrlService : IOpenUrlService
    {
        public void OpenUrl(string url)
        {
            Device.OpenUri(new Uri(url));
        }
    }
}
