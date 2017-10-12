using System;
using System.Data;

namespace BugsTrackers.BusinessLogicLayer {



    public class QueryClause {


        private string _booleanOperator;
        private string _fieldName;
        private string _comparisonOperator;
        private string _fieldValue;
        private SqlDbType _dataType;


        public string BooleanOperator {
            get { return _booleanOperator; }
        }

        public string FieldName {
            get { return _fieldName; }
        }


        public string ComparisonOperator {
            get { return _comparisonOperator; }
        }


        public string FieldValue {
            get { return _fieldValue; }
        }

        public SqlDbType DataType {
            get { return _dataType; }
        }

        public QueryClause(string booleanOperator, string fieldName, string comparisonOperator, string fieldValue, SqlDbType dataType) {
            _booleanOperator = booleanOperator;
            _fieldName = fieldName;
            _comparisonOperator = comparisonOperator;
            _fieldValue = fieldValue;
            _dataType = dataType;
        }
    }
}
