using System.Text.Json;

namespace ProductionSystem
{
    internal static class JsonRulesReader
    {
        public static Rule[] GetRules(string path)
        {
            string text = File.ReadAllText(path);

            JsonDocument jsonDocument;

            try
            {
                jsonDocument = JsonDocument.Parse(text);
            }
            catch
            {
                throw new WrongFormatException("File content isn't valid JSON value.");
            }

            JsonElement jsonArray = jsonDocument.RootElement;

            if (jsonArray.ValueKind != JsonValueKind.Array)
                throw new WrongFormatException("Root element expected to be an array, got " + jsonArray.ValueKind);

            int length = jsonArray.GetArrayLength();

            Rule[] rules = new Rule[length];

            for (int i = 0; i < length; i++)
                rules[i] = jsonArray[i].GetRule();

            return rules;
        }

        private static Rule GetRule(this JsonElement jsonRule)
        {
            if (jsonRule.ValueKind != JsonValueKind.Object)
                throw new WrongFormatException("Rule element expected to be an object, got " + jsonRule.ValueKind);

            string name = jsonRule.GetString("n");

            string directValue = jsonRule.GetString("d");

            JsonElement jsonReverseValue;

            JsonElement jsonRulePart = jsonRule.GetElement("c");

            IRulePart rootRulePart = jsonRulePart.GetRulePart();

            int importancy = jsonRule.GetInt("i");

            try
            {
                jsonReverseValue = jsonRule.GetElement("r");
            }
            catch
            {
                return new Rule(rootRulePart, name, directValue, importancy);
            }

            string reverseValue = jsonReverseValue.ExtractString("r");

            return new TwoHeadedRule(rootRulePart, name, directValue, reverseValue, importancy);
        }

        private static JsonElement GetElement(this JsonElement jsonElement, string propertyName)
        {
            try
            {
                return jsonElement.GetProperty(propertyName);
            }
            catch
            {
                throw new WrongFormatException("\"" + propertyName + "\" property expected, but not found.");
            }
        }

        private static string GetString(this JsonElement jsonElement, string propertyName)
        {
            JsonElement childElement = GetElement(jsonElement, propertyName);

            if (childElement.ValueKind != JsonValueKind.String)
                throw new WrongFormatException("\"" + propertyName + "\" expected to be string, got " + childElement.ValueKind);

            return childElement.ExtractString(propertyName);
        }

        private static int GetInt(this JsonElement jsonElement, string propertyName)
        {
            JsonElement childElement = GetElement(jsonElement, propertyName);

            if (childElement.ValueKind != JsonValueKind.Number)
                throw new WrongFormatException("\"" + propertyName + "\" expected to be a number, got " + childElement.ValueKind);

            return childElement.ExtractInt(propertyName);
        }

        private static int ExtractInt(this JsonElement jsonInt, string propertyName)
        {
            try
            {
                return jsonInt.GetInt32();
            }
            catch
            {
                throw new WrongFormatException("Failed to represent \"" + propertyName + "\" as an integer.");
            }
        }

        private static string ExtractString(this JsonElement jsonString, string propertyName)
        {
            string? result = jsonString.GetString();

            if (result == null)
                throw new WrongFormatException("\"" + propertyName + "\" expected to be not null, got null.");

            return result;
        }

        private static IRulePart GetRulePart(this JsonElement jsonRulePart)
        {
            if (jsonRulePart.ValueKind != JsonValueKind.Object)
                throw new WrongFormatException("Rule part expected to be string, got " + jsonRulePart.ValueKind);

            string partType = jsonRulePart.GetString("t");

            switch (partType)
            {
                case "f":
                    string name = jsonRulePart.GetString("n");
                    string value = jsonRulePart.GetString("v");

                    return new FactRulePart(name, value);
                case "o":
                    IRulePart[] orParts = jsonRulePart.GetObjects("c").GetRuleParts();

                    return new OrRulePart(orParts);
                case "a":
                    IRulePart[] andParts = jsonRulePart.GetObjects("c").GetRuleParts();

                    return new AndRulePart(andParts);
                case "n":
                    JsonElement condition = jsonRulePart.GetElement("c");
                    IRulePart notPart = condition.GetRulePart();

                    return new NotRulePart(notPart);
                default:
                    throw new WrongFormatException("Got unexpected rule type: " + partType);
            }
        }

        private static JsonElement[] GetObjects(this JsonElement jsonElement, string propertyName)
        {
            JsonElement childElement = jsonElement.GetElement(propertyName);

            switch (childElement.ValueKind)
            {
                case JsonValueKind.Object:
                    return new JsonElement[] { childElement };
                case JsonValueKind.Array:
                    int length = childElement.GetArrayLength();

                    JsonElement[] array = new JsonElement[length];

                    for (int i = 0; i < length; i++)
                        array[i] = childElement[i];

                    return array;
                default:
                    throw new WrongFormatException("Rule part's condition expected to be an object or an array, got " + childElement.ValueKind);
            }
        }

        private static IRulePart[] GetRuleParts(this JsonElement[] jsonElements)
        {
            int length = jsonElements.Length;

            IRulePart[] parts = new IRulePart[length];

            for (int i = 0; i < length; i++)
                parts[i] = jsonElements[i].GetRulePart();

            return parts;
        }
    }
}
