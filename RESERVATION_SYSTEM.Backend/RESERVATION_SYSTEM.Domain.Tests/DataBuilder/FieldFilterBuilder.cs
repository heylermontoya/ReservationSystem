using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Domain.Tests.DataBuilder
{
    public class FieldFilterBuilder
    {
        private string _field;
        private string _value;
        private TypeDateTime? _typeDateTime;
        private DateTime? _endDate;
        private TypeOrderBy? _typeOrderBy;

        public FieldFilterBuilder()
        {
            _field = "Default Field";
            _value = "Default Value";
            _typeDateTime = null;
            _endDate = null;
            _typeOrderBy = null;
        }

        public FieldFilter Build()
        {
            return new FieldFilter
            {
                Field = _field,
                Value = _value,
                TypeDateTime = _typeDateTime,
                EndDate = _endDate,
                TypeOrderBy = _typeOrderBy
            };
        }

        public FieldFilterBuilder WithField(string field)
        {
            _field = field;
            return this;
        }

        public FieldFilterBuilder WithValue(string value)
        {
            _value = value;
            return this;
        }

        public FieldFilterBuilder WithTypeDateTime(TypeDateTime? typeDateTime)
        {
            _typeDateTime = typeDateTime;
            return this;
        }

        public FieldFilterBuilder WithEndDate(DateTime? endDate)
        {
            _endDate = endDate;
            return this;
        }

        public FieldFilterBuilder WithTypeOrderBy(TypeOrderBy? typeOrderBy)
        {
            _typeOrderBy = typeOrderBy;
            return this;
        }
    }
}
