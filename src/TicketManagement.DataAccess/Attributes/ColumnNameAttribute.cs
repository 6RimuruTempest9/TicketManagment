using System;

namespace TicketManagement.DataAccess.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    internal sealed class ColumnNameAttribute : Attribute
    {
        private readonly string _columnName;

        public ColumnNameAttribute(string columnName)
            : base()
        {
            if (columnName == null)
            {
                throw new ArgumentNullException("columnName");
            }

            _columnName = columnName;
        }

        public string ColumnName => _columnName;

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj == this)
            {
                return true;
            }

            var columnNameAttribute = obj as ColumnNameAttribute;

            if (columnNameAttribute == null)
            {
                return false;
            }

            if (!columnNameAttribute._columnName.Equals(_columnName))
            {
                return false;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 5125;

                hash = (hash * 3295) + (_columnName?.GetHashCode() ?? 0);

                return hash;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
