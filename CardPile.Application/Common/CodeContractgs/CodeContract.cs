using CardPile.Application.Common.Extensions;
using System.Linq.Expressions;

namespace CardPile.Application.Common.CodeContractgs
{

    public abstract class CodeContract
    {

        #region - - - - - - Enumerations - - - - - -

        public enum ExceptionReasonEnum
        {
            Null,
            Empty,
            WhiteSpace
        }

        public enum ValueSourceEnum
        {
            Argument,
            ArgumentProperty,
            Variable
        }

        #endregion

        #region - - - - - - Actions - - - - - -

        protected static class Actions
        {
            public static Action<string, string> StringIsEmpty { get; } = (value, itemName) => { if (value == string.Empty) throw new StringIsEmptyException(itemName); };
            public static Action<(Type, Type), string> TypeImplementsInterface { get; } = (value, itemName) => { if (!value.Item1.IsInterfaceImplemented(value.Item2)) throw new ArgumentException($"{value.Item1.Name} does not implement {value.Item2.Name}."); };
            public static Action<Type, string> TypeIsNotInterface { get; } = (value, itemName) => { if (!value.IsInterface) throw new ArgumentException($"{value.Name} is not an interface."); };
            public static Action<Type, string> TypeIsNotInstantiable { get; } = (value, itemName) => { if (!value.IsInstantiable()) throw new ArgumentException($"{value.Name} cannot be instantiated."); };
        }

        protected static class Actions<T>
        {
            public static Action<T, string> ItemIsNull { get; } = (item, itemName) => { if (item == null) throw ArgumentNullException(itemName); };
        }

        #endregion

        #region - - - - - - Contracts - - - - - -

        //--------------------------------------------------------------------------------

        public static void ImplementsInterface(Expression<Func<Type>> itemExpression, Expression<Func<Type>> interfaceTypeExpression)
        {
            TestPredicates(interfaceTypeExpression, Actions<Type>.ItemIsNull, Actions.TypeIsNotInterface);

            var _Types = (itemExpression.Compile().Invoke(), interfaceTypeExpression.Compile().Invoke());

            Expression<Func<(Type, Type)>> expression = () => _Types;

            TestPredicates(expression, Actions.TypeImplementsInterface);
        }

        public static void IsInstantiable(Expression<Func<Type>> itemExpression)
            => TestPredicates(itemExpression, Actions<Type>.ItemIsNull, Actions.TypeIsNotInstantiable);

        public static void IsInterface(Expression<Func<Type>> itemExpression)
            => TestPredicates(itemExpression, Actions<Type>.ItemIsNull, Actions.TypeIsNotInterface);

        public static void IsNotNull<T>(Expression<Func<T>> itemExpression)
            => TestPredicates(itemExpression, Actions<T>.ItemIsNull);

        public static void IsNotNullOrEmpty(Expression<Func<string>> itemExpression)
            => TestPredicates(itemExpression, Actions<string>.ItemIsNull, Actions.StringIsEmpty);

        public static void IsNotNullOrWhiteSpace(string value, string path, ValueSourceEnum? valueSource = null)
        {
            if (value == null) throw new Exception(ExceptionReasonEnum.Null, path, valueSource);
            if (string.IsNullOrEmpty(value)) throw new Exception(ExceptionReasonEnum.Empty, path, valueSource);
            if (string.IsNullOrWhiteSpace(value)) throw new Exception(ExceptionReasonEnum.WhiteSpace, path, valueSource);
        }


        //--------------------------------------------------------------------------------

        protected static void TestPredicates<T>(Expression<Func<T>> itemExpression, params Action<T, string>[] predicates)
        {
            if (itemExpression == null) throw new ItemExpressionIsNullException();

            if (!(itemExpression.Body is MemberExpression _BodyExpression)) throw new ArgumentException(nameof(itemExpression), $"{nameof(itemExpression)} must be [() => parameterName].");
            if (!(_BodyExpression.Expression is ConstantExpression _ConstantExpression)) throw new ArgumentException(nameof(itemExpression), $"{nameof(itemExpression)} must be [() => parameterName].");

            //Due to how the framework implements wrapping a variable, it's not easy to get the constant expression's value by probing the _ConstantExpression variable.
            var _Item = itemExpression.Compile().Invoke();
            var _ItemName = _BodyExpression.Member.Name;

            foreach (var _Predicate in predicates) _Predicate(_Item, _ItemName);
        }

        #endregion //Contracts

        #region " - - - - - - Exceptions - - - - - - "

        public static ArgumentNullException ArgumentNullException(string argumentName) => new ArgumentNullException(argumentName ?? string.Empty, $"{argumentName ?? string.Empty} cannot be null.");

        public class Exception : System.Exception
        {

            #region - - - - - - Constructors - - - - - -

            public Exception(ExceptionReasonEnum exceptionReason, string valuePath, ValueSourceEnum? valueSource)
                : base($"{valueSource?.ToString() ?? string.Empty} '{valuePath}' is {exceptionReason.ToString()}".Trim())
            {
                ExceptionReason = exceptionReason;
                ValuePath = valuePath;
                ValueSource = valueSource;
            }

            #endregion

            #region - - - - - - Properties - - - - - -

            public ExceptionReasonEnum ExceptionReason { get; }

            public string ValuePath { get; }

            public ValueSourceEnum? ValueSource { get; }

            #endregion

        }

        #endregion

    }

}
