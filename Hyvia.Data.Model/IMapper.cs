using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyvia.Data.Model
{
    public interface IMapper<TInput, TOutput>

    {

        TOutput MapFrom(TInput input);

    }
}
