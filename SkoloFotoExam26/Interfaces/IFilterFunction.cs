namespace SkoloFotoExam26.Interfaces
{
    public interface IFilterFunction
    {
        List<T> FilterFunc<T>(List<T> objects, List<Predicate<T>> predicates);

    }
}
