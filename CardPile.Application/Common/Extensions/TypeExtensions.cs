using CardPile.Application.Common.CodeContractgs;

namespace CardPile.Application.Common.Extensions
{

    public static class TypeExtensions
    {

        /// <summary>
        /// Gets a collection of types representing each implemented interface, filtered by the type of interface.
        /// </summary>
        /// <param name="thisType">The type to check.</param>
        /// <param name="interfaceType">The type of interface which may be implemented multiple times.</param>
        /// <returns>IEnumerable(Type)</returns>
        public static IEnumerable<Type> GetImplementedInterfacesByInterfaceType(this Type thisType, Type interfaceType)
        {
            CodeContract.IsNotNull(() => thisType);
            CodeContract.IsInterface(() => interfaceType);

            return thisType.GetInterfaces().Where(i =>
            {

                //If the InterfaceType is a generic type definition i.e. typeof(InterfaceType<>), then the interface being tested must be generic.
                //  - Calling typeof(INonGeneric).GetGenericTypeDefinition() will throw an exception.
                if (interfaceType.IsGenericTypeDefinition) return i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType;

                return i == interfaceType;
            });

        }

        /// <summary>
        /// Gets a value indicating if this type can be instantiated.
        /// </summary>
        /// <param name="thisType">The type to check.</param>
        /// <returns>bool</returns>
        public static bool IsInstantiable(this Type thisType)
        {
            CodeContract.IsNotNull(() => thisType);

            return !thisType.IsInterface && !thisType.IsAbstract;
        }

        /// <summary>
        /// Gets a value indicating if this type implements the specified interface.
        /// </summary>
        /// <param name="thisType">The type to check.</param>
        /// <param name="interfaceType">The type of interface which may be implemented.</param>
        /// <returns>bool</returns>
        public static bool IsInterfaceImplemented(this Type thisType, Type interfaceType)
        {
            CodeContract.IsNotNull(() => thisType);
            CodeContract.IsInterface(() => interfaceType);

            return thisType.GetImplementedInterfacesByInterfaceType(interfaceType).Any();
        }

    }

}
