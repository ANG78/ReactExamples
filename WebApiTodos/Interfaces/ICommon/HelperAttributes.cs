using System;
using System.Reflection;

namespace Interfaces.ICommon
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class DescriptiveEnumEnforcement : Attribute
    {
        /// <summary>Defines the different types of enforcement for DescriptiveEnums.</summary>
        public enum EnforcementTypeEnum
        {
            /// <summary>Indicates that the enum must have a NameAttribute and a DescriptionAttribute.</summary>
            ThrowException,

            /// <summary>Indicates that the enum does not have a NameAttribute and a DescriptionAttribute, the value will be used instead.</summary>
            DefaultToValue
        }

        /// <summary>The enforcement type for this DescriptiveEnumEnforcementAttribute.</summary>
        public EnforcementTypeEnum EnforcementType { get; set; }

        /// <summary>Constructs a new DescriptiveEnumEnforcementAttribute.</summary>
        public DescriptiveEnumEnforcement()
        {
            this.EnforcementType = EnforcementTypeEnum.DefaultToValue;
        }

        /// <summary>Constructs a new DescriptiveEnumEnforcementAttribute.</summary>
        /// <param name="enforcementType">The initial value of the EnforcementType property.</param>
        public DescriptiveEnumEnforcement(EnforcementTypeEnum enforcementType)
        {
            this.EnforcementType = enforcementType;
        }
    }


    /// <summary>Indicates that an enum value has a description.</summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public DescriptionAttribute() { }

        public DescriptionAttribute(string description)
        {
            this.Description = description;
        }

        public override string ToString()
        {
            return this.Description;
        }
    }


    /// <summary>Provides functionality to enhance enumerations.</summary>
    public static partial class EnumAttributes
    {
        /// <summary>Returns the description of the specified enum.</summary>
        /// <param name="value">The value of the enum for which to return the description.</param>
        /// <returns>A description of the enum, or the enum name if no description exists.</returns>
        public static string GetDescription(this Enum value)
        {
            return GetEnumDescription(value);
        }

        /// <summary>Returns the description of the specified enum.</summary>
        /// <param name="value">The value of the enum for which to return the description.</param>
        /// <returns>A description of the enum, or the enum name if no description exists.</returns>
        public static string GetDescription<T>(object value)
        {
            return GetEnumDescription(value);
        }

        /// <summary>Returns the description of the specified enum.</summary>
        /// <param name="value">The value of the enum for which to return the description.</param>
        /// <returns>A description of the enum, or the enum name if no description exists.</returns>
        public static string GetEnumDescription(object value)
        {
            if (value == null)
                return null;

            Type type = value.GetType();

            //Make sure the object is an enum.
            if (!type.IsEnum)
                throw new ApplicationException("Value parameter must be an enum.");

            FieldInfo fieldInfo = type.GetField(value.ToString());
            object[] descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            //If no DescriptionAttribute exists for this enum value, check the DescriptiveEnumEnforcementAttribute and decide how to proceed.
            if (descriptionAttributes == null || descriptionAttributes.Length == 0)
            {
                object[] enforcementAttributes = fieldInfo.GetCustomAttributes(typeof(DescriptiveEnumEnforcement), false);

                //If a DescriptiveEnumEnforcementAttribute exists, either throw an exception or return the name of the enum instead.
                if (enforcementAttributes != null && enforcementAttributes.Length == 1)
                {
                    DescriptiveEnumEnforcement enforcementAttribute = (DescriptiveEnumEnforcement)enforcementAttributes[0];

                    if (enforcementAttribute.EnforcementType == DescriptiveEnumEnforcement.EnforcementTypeEnum.ThrowException)
                        throw new ApplicationException("No Description attributes exist in enforced enum of type '" + type.Name + "', value '" + value.ToString() + "'.");

                    return type.GetEnumName(value);
                }
                else //Just return the name of the enum.
                    return type.GetEnumName(value);
            }
            else if (descriptionAttributes.Length > 1)
                throw new ApplicationException("Too many Description attributes exist in enum of type '" + type.Name + "', value '" + value.ToString() + "'.");

            //Return the value of the DescriptionAttribute.
            return descriptionAttributes[0].ToString();
        }
    }


    /// <summary>Indicates that an enum value is not found.</summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class IsNotFoundAttribute : Attribute
    {
        public bool IsNotFound { get; set; }

        public IsNotFoundAttribute() { }

        public IsNotFoundAttribute(bool isNotfound = true)
        {
            this.IsNotFound = isNotfound;
        }

        public override string ToString()
        {
            return this.IsNotFound.ToString();
        }
    }


    public static partial class EnumAttributes
    {
        /// <summary>Returns the bool IsNotFound of the specified enum.</summary>
        /// <param name="value">The value of the enum for which to return the NotFound bool.</param>
        /// <returns>The not found value.</returns>
        public static bool NotFoundAttribute(this Enum value)
        {
            return GetEnumIsNotFound(value);
        }

        /// <summary>Returns the bool IsNotFound of the specified enum.</summary>
        /// <param name="value">The value of the enum for which to return the NotFound bool.</param>
        /// <returns>The not found value.</returns>
        public static bool NotFoundAttribute<T>(object value)
        {
            return GetEnumIsNotFound(value);
        }

        /// <summary>Returns the bool IsNotFound of the specified enum.</summary>
        /// <param name="value">The value of the enum for which to return the NotFound bool.</param>
        /// <returns>The not found value.</returns>
        public static bool GetEnumIsNotFound(object value)
        {
            if (value == null)
                return false;

            Type type = value.GetType();

            //Make sure the object is an enum.
            if (!type.IsEnum)
                throw new ApplicationException("Value parameter must be an enum.");

            FieldInfo fieldInfo = type.GetField(value.ToString());
            object[] descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(IsNotFoundAttribute), false);

            //If no DescriptionAttribute exists for this enum value, check the DescriptiveEnumEnforcementAttribute and decide how to proceed.
            if (descriptionAttributes == null || descriptionAttributes.Length == 0)
            {
                object[] enforcementAttributes = fieldInfo.GetCustomAttributes(typeof(DescriptiveEnumEnforcement), false);

                //If a DescriptiveEnumEnforcementAttribute exists, either throw an exception or return the name of the enum instead.
                if (enforcementAttributes != null && enforcementAttributes.Length == 1)
                {
                    DescriptiveEnumEnforcement enforcementAttribute = (DescriptiveEnumEnforcement)enforcementAttributes[0];

                    if (enforcementAttribute.EnforcementType == DescriptiveEnumEnforcement.EnforcementTypeEnum.ThrowException)
                        throw new ApplicationException("No Description attributes exist in enforced enum of type '" + type.Name + "', value '" + value.ToString() + "'.");

                    return false;
                }
                else //Just return the name of the enum.
                    return false;
            }
            else if (descriptionAttributes.Length > 1)
                throw new ApplicationException("Too many Description attributes exist in enum of type '" + type.Name + "', value '" + value.ToString() + "'.");

            //Return the value of the IsNotFoundAttribute.
            return true;

        }
    }


    /// <summary>
    /// Alternative Message for enums, -> getMsg()
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class AltMessageAttribute : Attribute
    {
        public String AltMsg { get; set; }

        public AltMessageAttribute() { }

        public AltMessageAttribute(String message)
        {
            this.AltMsg = message;
        }

        public override String ToString()
        {
            return this.AltMsg;
        }
    }


    public static partial class EnumAttributes
    {
        /// <summary>Returns the Alternative Message of the specified enum.</summary>
        /// <param name="value">The value of the enum for which to return the NotFound bool.</param>
        /// <returns>The not found value.</returns>
        public static String GetAltMessage(this Enum value)
        {
            return GetEnumAltMessage(value);
        }

        /// <summary>Returns the Alternative Message of the specified enum.</summary>
        /// <param name="value">The value of the enum for which to return the NotFound bool.</param>
        /// <returns>The not found value.</returns>
        public static String GetAltMessage<T>(object value)
        {
            return GetEnumAltMessage(value);
        }

        /// <summary>Returns the Alternative Message of the specified enum.</summary>
        /// <param name="value">The value of the enum for which to return the NotFound bool.</param>
        /// <returns>The not found value.</returns>
        public static String GetEnumAltMessage(object value)
        {
            if (value == null)
                return String.Empty;

            Type type = value.GetType();

            //Make sure the object is an enum.
            if (!type.IsEnum)
                throw new ApplicationException("Value parameter must be an enum.");

            FieldInfo fieldInfo = type.GetField(value.ToString());
            object[] descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(AltMessageAttribute), false);

            //If no MessageAttribute exists for this enum value, check the DescriptiveEnumEnforcementAttribute and decide how to proceed.
            if (descriptionAttributes == null || descriptionAttributes.Length == 0)
            {
                object[] enforcementAttributes = fieldInfo.GetCustomAttributes(typeof(DescriptiveEnumEnforcement), false);

                //If a DescriptiveEnumEnforcementAttribute exists, either throw an exception or return the name of the enum instead.
                if (enforcementAttributes != null && enforcementAttributes.Length == 1)
                {
                    DescriptiveEnumEnforcement enforcementAttribute = (DescriptiveEnumEnforcement)enforcementAttributes[0];

                    if (enforcementAttribute.EnforcementType == DescriptiveEnumEnforcement.EnforcementTypeEnum.ThrowException)
                        throw new ApplicationException("No Description attributes exist in enforced enum of type '" + type.Name + "', value '" + value.ToString() + "'.");

                    return type.GetEnumName(value);
                }
                else //Just return the name of the enum.
                    return type.GetEnumName(value);
            }
            else if (descriptionAttributes.Length > 1)
                throw new ApplicationException("Too many Description attributes exist in enum of type '" + type.Name + "', value '" + value.ToString() + "'.");

            //Return the value of the IsNotFoundAttribute.
            return descriptionAttributes[0].ToString();

        }
    }



}
