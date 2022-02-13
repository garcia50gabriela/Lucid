using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class InMemoryVariableStorage : Yarn.Unity.VariableStorageBehaviour
{
        /// <summary>
        /// Where we're actually keeping our variables
        /// </summary>
        private Dictionary<string, object> variables = GameData.variables;
        private Dictionary<string, System.Type> variableTypes = new Dictionary<string, System.Type>(); // needed for serialization

        [Header("Optional debugging tools")]
        [HideInInspector] public bool showDebug;

        /// <summary>
        /// A <see cref="UnityEngine.UI.Text"/> that can show the current list
        /// of all variables in-game. Optional.
        /// </summary>
        [SerializeField, Tooltip("(optional) output list of variables and values to Text UI in-game")]
        internal UnityEngine.UI.Text debugTextView = null;
        void SetVariable(string name, Yarn.IType type, string value)
        {
            if (type == Yarn.BuiltinTypes.Boolean)
            {
                bool newBool;
                if (bool.TryParse(value, out newBool))
                {
                    SetValue(name, newBool);
                }
                else
                {
                    throw new System.InvalidCastException($"Couldn't initialize default variable {name} with value {value} as Bool");
                }
            }
            else if (type == Yarn.BuiltinTypes.Number)
            {
                float newNumber;
                if (float.TryParse(value, out newNumber))
                { // TODO: this won't work for different cultures (e.g. French write "0.53" as "0,53")
                    SetValue(name, newNumber);
                }
                else
                {
                    throw new System.InvalidCastException($"Couldn't initialize default variable {name} with value {value} as Number (Float)");
                }
            }
            else if (type == Yarn.BuiltinTypes.String)
            {
                SetValue(name, value); // no special type conversion required
            }
            else
            {
                throw new System.ArgumentOutOfRangeException($"Unsupported type {type.Name}");
            }
        }

        private void ValidateVariableName(string variableName)
        {
            if (variableName.StartsWith("$") == false)
            {
                throw new System.ArgumentException($"{variableName} is not a valid variable name: Variable names must start with a '$'. (Did you mean to use '${variableName}'?)");
            }
        }

        public override void SetValue(string variableName, string stringValue)
        {
            ValidateVariableName(variableName);

            variables[variableName] = stringValue;
            variableTypes[variableName] = typeof(string);
        }

        public override void SetValue(string variableName, float floatValue)
        {
            ValidateVariableName(variableName);

            variables[variableName] = floatValue;
            variableTypes[variableName] = typeof(float);
        }

        public override void SetValue(string variableName, bool boolValue)
        {
            ValidateVariableName(variableName);

            variables[variableName] = boolValue;
            variableTypes[variableName] = typeof(bool);
        }

        public override bool TryGetValue<T>(string variableName, out T result)
        {
            ValidateVariableName(variableName);

            // If we don't have a variable with this name, return the null
            // value
            if (variables.ContainsKey(variableName) == false)
            {
                result = default;
                return false;
            }

            var resultObject = variables[variableName];

            if (typeof(T).IsAssignableFrom(resultObject.GetType()))
            {
                result = (T)resultObject;
                return true;
            }
            else
            {
                throw new System.InvalidCastException($"Variable {variableName} exists, but is the wrong type (expected {typeof(T)}, got {resultObject.GetType()}");
            }


        }

        /// <summary>
        /// Removes all variables from storage.
        /// </summary>
        public override void Clear()
        {
            variables.Clear();
            variableTypes.Clear();
        }

        /// <summary>
        /// returns a boolean value representing if the particular variable is inside the variable storage
        /// </summary>
        public override bool Contains(string variableName)
        {
            return variables.ContainsKey(variableName);
        }

 }
