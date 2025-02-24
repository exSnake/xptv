using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xptv.Presentation.Common.Components.Loading.ViewModels;

public interface ILoadingOverlayViewModel
{
    bool IsLoading { get; set; }
    string Message { get; set; }
}