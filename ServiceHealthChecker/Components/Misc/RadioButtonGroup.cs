using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceHealthChecker.Components.Misc
{
    public partial class RadioButtonGroup<T>
    {
        [Parameter]
        public HttpMethods SelectedItem { get; set; }

        [Parameter]
        public IEnumerable<T> Items { get; set; }

        [Parameter]
        public EventCallback<T> SelectItem { get; set; }
    }
}
