

namespace Citizens.Core
{
    using Newtonsoft.Json;
    public abstract class LogicalExpression<T> : ILogicalExpression
    {
        public LogicalExpression(CompareGates compare, string fieldName, params T[] values)
        {
            this.Compare = compare;
            this.FieldName = fieldName;
            this.Values = values;
        }
        public LogicalExpression() { }

        [JsonProperty("compare")]
        public CompareGates Compare { get; set; }

        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [JsonProperty("values")]
        public abstract T[] Values { get; set; }

        public abstract string GenerateExpression();
    }
}
