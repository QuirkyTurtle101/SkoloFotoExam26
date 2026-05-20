using SkoloFotoExam26.Interfaces;

namespace SkoloFotoExam26.Helpers
{
    public class FilterFunction : IFilterFunction
    {
        public List<T> FilterFunc<T>(List<T> objects, List<Predicate<T>> predicates)
        {
            List<T> filterList = new List<T>();
            foreach (T obj in objects)
            {
                
                foreach (Predicate<T> predicate in predicates)
                {
                    bool matchesAnyPreds = true;
                    if (!predicate(obj))
                    {
                        matchesAnyPreds = false;
                    }
                    else if (matchesAnyPreds && (!filterList.Contains(obj)))
                    {
                        filterList.Add(obj);
                    }

                }

            }
            return filterList;
        }
    }
}
