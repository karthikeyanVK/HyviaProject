namespace Hyvia.Data.Model
{
    public interface IMapper<TInput, TOutput>

    {

        TOutput MapFrom(TInput input);

    }
}
