namespace CorsIssue
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class GraphQuery
    {
        public IEnumerable<int> GetTestData()
            => Enumerable.Range(1,199);
    }
}
